using API.Controllers;
using Core.Entities;
using Core.Enums.UploadFiles;
using FluentValidation;

namespace API.Dtos.UploadBoletos;

public class UploadBoletoDto
{
    public UploadBoletoDto()
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
    public string StatusBoleto { get; set; } = EnumExtensions.GetDescription(EnumStatusBoleto.Sucesso);
    public List<Mensagem> Mensagem { get; set; }
}

public class UploadBoletosDtoValidator : AbstractValidator<UploadBoletoDto>
{
    public UploadBoletosDtoValidator()
    {
        RuleFor(x => x.DataOperacao)
            .GreaterThan(default(DateTime))
            .WithName("DATA DA OPERAÇÃO")
            .WithMessage(
                "O campo {PropertyName} não é uma data válida");
        RuleFor(x => x.CodigoCliente).NotEmpty().NotNull()
            .Must(ValidarCateiraCliente)
            .WithName("CÓDIGO DO CLIENTE/CARTEIRA")
            .WithMessage("O campo {PropertyName}  deve representar um cliente válido/existente (válidos : CARTEIRA CLIENTE A, CARTEIRA CLIENTE B, CARTEIRA CLIENTE C");
        RuleFor(x => x.TipoOperacao).NotEmpty().NotNull()
            .Must(ValidarTipoOperacao)
            .WithName("TIPO DA OPERAÇÃO")
            .WithMessage("O campo {PropertyName} deve representar um tipo de operação válida (válidos : Compra,Venda)");
        RuleFor(x => x.IdBolsa).NotEmpty().NotNull()
            .Must(ValidarIdBolsa)
            .WithName("ID DA BOLSA")
            .WithMessage("O campo {PropertyName} deve representar um id de bolsa válido (válido : BVSP)");
        RuleFor(x => x.CodigoAtivo).NotEmpty().NotNull()
            .Must(ValidarCodigoAtivo)
            .WithName("CÓDIGO DO ATIVO")
            .WithMessage("O campo {PropertyName} deve representar um código de ativo válido (válidos : PETR4, VALE3)");
        RuleFor(x => x.Corretora).NotEmpty().NotNull()
            .Must(ValidarCorretora)
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

    private bool ValidarCorretora(string enumDescription)
    {
        return enumDescription.GetEnumByDescription<EnumCorretora>();

    }
    
    private bool ValidarIdBolsa(string enumDescription)
    {
        return enumDescription.GetEnumByDescription<EnumIdBolsa>();

    }
    
    private bool ValidarCodigoAtivo(string enumDescription)
    {
        return enumDescription.GetEnumByDescription<EnumCodigoAtivo>();

    }
    
    private bool ValidarTipoOperacao(string enumDescription)
    {
        return enumDescription.GetEnumByDescription<EnumTipoOperacao>();

    }

    private bool ValidarCateiraCliente(string enumDescription)
    {
        return enumDescription.GetEnumByDescription<EnumCarteiraCliente>();
    }
}