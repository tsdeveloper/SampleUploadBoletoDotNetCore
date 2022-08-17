using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using API.Extensions;
using API.Model;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadFilesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UploadFilesController> _logger;
    private readonly IMapper _mapper;

    public UploadFilesController(ILogger<UploadFilesController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost(Name = "process-files")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> Upload([FromForm] UploadForm uploadForm)
    {
        var result = new StringBuilder();
        var objUpload = new UploadProcess();
        using (var stream = uploadForm.UploadFile.OpenReadStream())
        {
            using (var reader = new StreamReader(stream))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadToEndAsync());
            }
        }

        result.Replace("99#RV", "").Replace("0#RV", "");

        var listUploadProcess = new List<ReturnUploadProcessDto>();
        var resultReader = new StringReader(result.ToString());
        while (resultReader.Read() > 0)
        {
            var returnReader =
                resultReader.ReadLine()?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries) ?? null;
            if (returnReader != null && returnReader.Length > 0)
            {
                var enti = returnReader[0].Split("#", StringSplitOptions.RemoveEmptyEntries);

                var uploadProcessDto = new ReturnUploadProcessDto
                {
                    DataOperacao = StringExtensions.CheckOutOfRange(0, enti)
                        ? Convert.ToDateTime(enti[0]
                            .ToDateTime())
                        : default,
                    CodigoCliente = StringExtensions.CheckOutOfRange(1, enti) ? enti[1] : string.Empty,
                    TipoOperacao = StringExtensions.CheckOutOfRange(2, enti) ? enti[2] : string.Empty,
                    IdBolsa = StringExtensions.CheckOutOfRange(3, enti) ? enti[3] : string.Empty,
                    CodigoAtivo = StringExtensions.CheckOutOfRange(4, enti) ? enti[4] : string.Empty,
                    Corretora = StringExtensions.CheckOutOfRange(5, enti) ? enti[5] : string.Empty,
                    Quantidade = StringExtensions.CheckOutOfRange(6, enti) ? ParseToInt(enti[6]) : 0,
                    PrecoUnitario = StringExtensions.CheckOutOfRange(7, enti) ? ParseToDecimal(enti[7]) : 0M
                };
                var validator = new ReturnUploadProcessDtoValidator();

                var resultValidator = validator.Validate(uploadProcessDto);

                if (!resultValidator.IsValid)
                {
                    uploadProcessDto.Mensagem.RemoveAt(0);
                    foreach (var error in resultValidator.Errors)
                    {
                        uploadProcessDto.Mensagem.Add(
                            new Mensagem { Messagem = error.ErrorMessage});
                        uploadProcessDto.StatusBoleto = EnumExtensions.GetDescription(MensagemStatus.Falha);
                    }
                }

                listUploadProcess.Add(uploadProcessDto);
            }
        }

        foreach (var objetoFinanceiro in listUploadProcess
                     .GroupBy(x => new {x.CodigoCliente}, 
                         (key, g) =>
                         g.OrderBy(i => i.CodigoCliente))
                     .Select(s => new
                     {
                         financeiro = s.Where(c => c.ValorFinanceiroOperacao > 0)
                                        .MaxBy(x => x.ValorFinanceiroOperacao),
                         
                     })
                     .ToList())
        {
            objetoFinanceiro.financeiro.ValorDescontoOperacao = new BoletoDescontoSpefication()
                .ValidarDescontoBoleto(new AplicarDescontoBoleto10PorCento(objetoFinanceiro.financeiro));
        }

        return Ok(listUploadProcess);
    }


    private decimal ParseToDecimal(string value)
    {
        try
        {
            NumberStyles style;
            CultureInfo culture;
            decimal number;
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            if (Decimal.TryParse(value, out number))
            {
                return number;
            }

            return 0M;
        }
        catch (Exception e)
        {
            return 0M;
        }
    }

    private int ParseToInt(string value)
    {
        try
        {
            NumberStyles style;
            CultureInfo culture;
            int number;

            if (Int32.TryParse(value, out number))
            {
                return number;
            }

            return 0;
        }
        catch (Exception e)
        {
            return 0;
        }
    }
}

public class AplicarDescontoBoleto10PorCento : IDescontoBoleto
{
    private readonly ReturnUploadProcessDto _uploadProcessDto;

    public AplicarDescontoBoleto10PorCento(ReturnUploadProcessDto uploadProcessDto)
    {
        _uploadProcessDto = uploadProcessDto;
    }

    public decimal aplicar()
    {
        return _uploadProcessDto.ValorFinanceiroOperacao * 0.10M;
    }
}

public static class StringExtensions
{
    public static string? CheckNullString(this string? value)
    {
        if (value == null) return null;

        return value;
    }

    public static bool CheckOutOfRange(int index, string[] array)
    {
        return (index >= 0) && (index < array.Length);
    }
}

public static class EnumExtensions
{
    public static string DescriptionAttr<T>(this T source)
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }
    
    public static string GetDescription(Enum value)  
    {  
        var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();  
        var descriptionAttribute =  
            enumMember == null  
                ? default(DescriptionAttribute)  
                : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;  
        return  
            descriptionAttribute == null  
                ? value.ToString()  
                : descriptionAttribute.Description;  
    }  
}

public class ReturnUploadProcessDto
{
    public ReturnUploadProcessDto()
    {
        Mensagem = new List<Mensagem>
        {
            new Mensagem()
        };
    }

    public DateTime DataOperacao { get; set; }
    public string CodigoCliente { get; set; }
    public string TipoOperacao { get; set; }
    public string IdBolsa { get; set; }
    public string CodigoAtivo { get; set; }
    public string Corretora { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public decimal ValorFinanceiroOperacao
    {
        get { return Quantidade * PrecoUnitario; }
    }

    public decimal ValorDescontoOperacao { get; set; }
    public string StatusBoleto { get; set; } = EnumExtensions.GetDescription(Controllers.StatusBoleto.Sucesso);
    public List<Mensagem> Mensagem { get; set; }
}

public class ReturnUploadProcessDtoValidator : AbstractValidator<ReturnUploadProcessDto>
{
    public ReturnUploadProcessDtoValidator()
    {
        RuleFor(x => x.DataOperacao)
            .GreaterThan(default(DateTime))
            .WithName("DATA DA OPERAÇÃO")
            .WithMessage(
                "O campo {PropertyName} não é uma data válida");
        RuleFor(x => x.CodigoCliente).NotEmpty().NotNull()
            .WithName("CÓDIGO DO CLIENTE/CARTEIRA")
            .WithMessage("O campo {PropertyName}  deve representar um cliente válido/existente (válidos : CARTEIRA CLIENTE A, CARTEIRA CLIENTE B, CARTEIRA CLIENTE C");
        RuleFor(x => x.TipoOperacao).NotEmpty().NotNull()
            .WithName("TIPO DA OPERAÇÃO")
            .WithMessage("O campo {PropertyName} deve representar um tipo de operação válida (válidos : Compra,Venda)");
        RuleFor(x => x.IdBolsa).NotEmpty().NotNull()
            .WithName("ID DA BOLSA")
            .WithMessage("O campo {PropertyName} deve representar um id de bolsa válido (válido : BVSP)");
        RuleFor(x => x.CodigoAtivo).NotEmpty().NotNull()
            .WithName("CÓDIGO DO ATIVO")
            .WithMessage("O campo {PropertyName} deve representar um código de ativo válido (válidos : PETR4, VALE3)");
        RuleFor(x => x.Corretora).NotEmpty().NotNull()
            .WithName("CORRETORA")
            .WithMessage("O campo {PropertyName} deve representar um código de corretora válido (válido : AGORA)");
        RuleFor(x => x.Quantidade).GreaterThan(0)
            .WithName("QUANTIDADE")
            .WithMessage("O campo {PropertyName} valor não pode ser negativo");
        RuleFor(x => x.PrecoUnitario)
            .GreaterThan(0)
            .WithName("Preço Unitário")
            .WithMessage("O campo {PropertyName} valor não pode ser negativo");
    }
}

public class Mensagem
{
    public string Messagem { get; set; } = "Importação realizada com sucesso";
    public string MensagemStatus { get; set; } = EnumExtensions.GetDescription(Controllers.MensagemStatus.Sucesso);  
}

public enum MensagemStatus
{
    [Description("Sucesso")] Sucesso,
    [Description("Error")] Falha,
}

public enum StatusBoleto
{
    [Description("OK")] Sucesso = 0,
    [Description("ERRO")] Falha = 1,
}

public interface IDescontoBoleto
{
    public decimal aplicar();
}

public class BoletoDescontoSpefication
{
    public decimal ValidarDescontoBoleto(IDescontoBoleto boleto)
    {
        return boleto.aplicar();
    }
}

