using System.ComponentModel;

namespace Core.Enums.UploadBoletos;

public enum EnumStatusBoleto
{
    [Description("OK")] Sucesso = 0,
    [Description("ERRO")] Falha = 1,
}