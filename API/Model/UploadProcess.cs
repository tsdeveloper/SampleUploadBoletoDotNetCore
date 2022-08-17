using FluentValidation;

namespace API.Model;

public class UploadProcess
{
    public DateTime DataOperacao { get; set; }
    public string CodigoCliente { get; set; }
    public string TipoOperacao { get; set; }
    public string IdBolsa { get; set; }
    public string CodigoAtivo { get; set; }
    public string Corretora { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}

public class UploadProcessValidator : AbstractValidator<UploadProcess> 
{
    public UploadProcessValidator() 
    {
        RuleFor(x => x.DataOperacao).NotNull()
            .WithName("DATA DA OPERAÇÃO")
            .WithMessage("O campo {PropertyName} deve representar um cliente válido/existente (válidos : CARTEIRA CLIENTE A, CARTEIRA CLIENTE B, CARTEIRA CLIENTE C");
        RuleFor(x => x.CodigoCliente).NotEmpty().NotNull()
            .WithName("CÓDIGO DO CLIENTE/CARTEIRA")
            .WithMessage("Property");
        RuleFor(x => x.TipoOperacao).NotEmpty().NotNull()
            .WithName("TIPO DA OPERAÇÃO")
            .WithMessage("Property");
        RuleFor(x => x.IdBolsa).NotEmpty().NotNull()
            .WithName("ID DA BOLSA")
            .WithMessage("Property");
        RuleFor(x => x.CodigoAtivo).NotEmpty().NotNull()
            .WithName("CÓDIGO DO ATIVO")
            .WithMessage("Property");
        RuleFor(x => x.Corretora).NotEmpty().NotNull()
            .WithName("CORRETORA")
            .WithMessage("Property");
        RuleFor(x => x.Quantidade).GreaterThan(0)
            .WithName("QUANTIDADE")
            .WithMessage("Property");
        RuleFor(x => x.PrecoUnitario)
            .GreaterThan(0)
            .WithName("Preço Unitário")
            .WithMessage("Property");
    }
}