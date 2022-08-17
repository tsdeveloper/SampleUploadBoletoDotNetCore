using System.ComponentModel;

namespace Core.Enums.UploadFiles;

public enum EnumTipoOperacao
{
    [Description("Compra")] Compra = 0,
    [Description("Venda")] Venda = 1,
}