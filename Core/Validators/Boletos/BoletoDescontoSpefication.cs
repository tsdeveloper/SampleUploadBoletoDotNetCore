using Core.Interfaces.Boletos;

namespace Core.Validators.Boletos;

public class BoletoDescontoSpefication
{
    public decimal ValidarDescontoBoleto(IBoletoDesconto boleto)
    {
        return boleto.aplicar();
    }
}