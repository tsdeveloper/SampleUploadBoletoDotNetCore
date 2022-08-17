using System.ComponentModel;

namespace Core.Enums.UploadFiles;

public enum EnumStatusBoleto
{
    [Description("OK")] Sucesso = 0,
    [Description("ERRO")] Falha = 1,
}