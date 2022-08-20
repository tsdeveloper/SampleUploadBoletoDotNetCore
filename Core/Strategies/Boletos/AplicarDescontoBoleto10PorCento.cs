using Core.Entities;
using Core.Interfaces.Boletos;

namespace Core.Strategies.Boletos;

public class AplicarDescontoBoleto10PorCento : IBoletoDesconto
{
    private readonly UploadBoleto _uploadProcessDto;

    public AplicarDescontoBoleto10PorCento(UploadBoleto uploadProcessDto)
    {
        _uploadProcessDto = uploadProcessDto;
    }

    public decimal aplicar()
    {
        return _uploadProcessDto.ValorFinanceiroOperacao * 0.10M;
    }
}