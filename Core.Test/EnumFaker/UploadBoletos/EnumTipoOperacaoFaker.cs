using System.ComponentModel;

namespace Core.Test.EnumFaker.UploadBoletos;

public enum EnumTipoOperacaoSucessoFaker
{
    [Description("Compra")] Compra = 0,
    [Description("Venda")] Venda = 1,
}

public enum EnumTipoOperacaoFalhaFaker
{
    [Description("Compra1")] Compra1 = 0,
    [Description("Venda1")] Venda1 = 1,
}