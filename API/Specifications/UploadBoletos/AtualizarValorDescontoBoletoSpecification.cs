using API.Dtos.UploadBoletos;
using AutoMapper;
using Core.Entities;
using Core.Strategies.Boletos;
using Core.Validators.Boletos;

namespace API.Specifications.UploadBoletos;

public class AtualizarValorDescontoBoletoSpecification
{
    private readonly IMapper _mapper;
    

    public AtualizarValorDescontoBoletoSpecification(IMapper mapper)
    {
        _mapper = mapper;
    }
    public void AplicarValorDescontoBoleto(ref List<UploadBoletoDto> listUploadboleto)
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