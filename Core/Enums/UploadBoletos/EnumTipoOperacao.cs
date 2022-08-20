using System.ComponentModel;

namespace Core.Enums.UploadBoletos;

public enum EnumTipoOperacao
{
    [Description("Compra")] Compra = 0,
    [Description("Venda")] Venda = 1,
}