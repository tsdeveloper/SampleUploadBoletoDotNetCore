using System.ComponentModel;

namespace Core.Test.EnumFaker.UploadBoletos;

public enum EnumCarteiraClienteSucessoFaker
{
    [Description("CARTEIRA CLIENTE A")] CarteiraClienteA = 0,
    [Description("CARTEIRA CLIENTE B")] CarteiraClienteB = 1,
    [Description("CARTEIRA CLIENTE C")] CarteiraClienteC = 2,
    
}

public enum EnumCarteiraClienteFalhaFaker
{
    [Description("CARTEIRA CLIENTE D")] CarteiraClienteD = 0,
    [Description("CARTEIRA CLIENTE E")] CarteiraClienteE = 1,
    [Description("CARTEIRA CLIENTE F")] CarteiraClienteF = 2,
    
}