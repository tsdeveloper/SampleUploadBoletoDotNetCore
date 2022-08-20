using System.ComponentModel;

namespace Core.Test2.EnumFaker.UploadBoletos;

public enum EnumStatusBoletoFaker
{
    [Description("OK")] Sucesso = 0,
    [Description("ERRO")] Falha = 1,
}