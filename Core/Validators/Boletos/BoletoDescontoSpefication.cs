using Core.Interfaces.Boletos;

namespace Core.Validators.Boletos;

public class BoletoDescontoSpefication
{
    public decimal ValidarDescontoBoleto(IDescontoBoleto boleto)
    {
        return boleto.aplicar();
    }
}