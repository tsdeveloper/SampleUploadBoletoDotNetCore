using System.ComponentModel;

namespace Core.Test.EnumFaker.UploadBoletos;

public enum EnumStatusBoletoFaker
{
    [Description("OK")] Sucesso = 0,
    [Description("ERRO")] Falha = 1,
}