using API.Dtos.UploadBoletos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.UploadBoletos;
using Core.Strategies.Boletos;
using Core.Validators.Boletos;

namespace API.Specifications.UploadBoletos;

public class BoletoSpecification : IBoletoSpecification
{
    private readonly IMapper _mapper;
    

    public BoletoSpecification(IMapper mapper)
    {
        _mapper = mapper;
    }
    public void ObterBoletosGroupByCodigoCliente(ref List<UploadBoletoDto> listUploadboleto)
    {
        foreach (var objetoFinanceiro in listUploadboleto
                     .GroupBy(x => new {x.CodigoCliente}, 
                         (key, g) =>
                             g.OrderBy(i => i.CodigoCliente))
                     .Select(s => new
                     {
                         financeiro = s.Where(c => c.ValorFinanceiroOperacao > 0)
                             .MaxBy(x => x.ValorFinanceiroOperacao),
                         
                     })
                     .ToList())
        {
            if (objetoFinanceiro.financeiro != null)
            {
                objetoFinanceiro.financeiro.ValorDescontoOperacao = new BoletoDescontoSpefication()
                    .ValidarDescontoBoleto(new AplicarDescontoBoleto10PorCento(_mapper.Map<UploadBoleto>(objetoFinanceiro.financeiro)));
            }
        }

    }
  
    
}