using Core.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using API.Dtos.UploadBoletos;
using AutoMapper;
using Core.Enums.UploadBoletos;
using Core.Helpers.FormFiles;
using Core.Specifications.UploadBoletos.SpecParams;
using Core.Test2.EnumFaker.UploadBoletos;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Core.Test2;

public class UploadBoletoImportacaoTest : BaseTest
{
    private  List<UploadBoletoDto> _listUploadBoleto;
    
    private Mock<IHostEnvironment> _hostMock;

    public UploadBoletoImportacaoTest()
    {
        base.Inicializador();
        
        _hostMock = new Mock<IHostEnvironment>();
        _listUploadBoleto = new List<UploadBoletoDto>
        {
            new UploadBoletoDto { CodigoCliente = "CARTEIRA CLIENTE A"},
            new UploadBoletoDto { CodigoCliente = "CARTEIRA CLIENTE B"},
            new UploadBoletoDto { CodigoCliente = "CARTEIRA CLIENTE C"},
        };
    }
    
    [Theory]
    [InlineData("CARTEIRA CLIENTE A", EnumCarteiraClienteSucessoFaker.CarteiraClienteA)]
    [InlineData("CARTEIRA CLIENTE B",EnumCarteiraClienteSucessoFaker.CarteiraClienteB)]
    [InlineData("CARTEIRA CLIENTE C",EnumCarteiraClienteSucessoFaker.CarteiraClienteC)]
    public void Deveria_Existir_UploadBoletoImportacao_Somente_Carteiras_Validas(string carteira, EnumCarteiraClienteSucessoFaker result)
    {

        carteira.ShouldNotBeNull();
        carteira.ShouldBe(result.DescriptionAttrFaker());
       
    }
    
    [Theory]
    [InlineData("CARTEIRA CLIENTE C",EnumCarteiraClienteFalhaFaker.CarteiraClienteD)]
    [InlineData("CARTEIRA CLIENTE A",EnumCarteiraClienteFalhaFaker.CarteiraClienteF)]
    public void Deveria_Retornar_Sucesso_UploadBoletoImportacao_Somente_Carteiras_Invalidas(string carteira, EnumCarteiraClienteFalhaFaker result)
    {
        carteira.ShouldNotBeNull();
        carteira.ShouldNotBe(result.DescriptionAttrFaker());
    }
    
    [Theory]
    [InlineData(EnumTipoOperacaoFalhaFaker.Compra1)]
    [InlineData(EnumTipoOperacaoFalhaFaker.Venda1)]
    public async Task Deveria_Retornar_Error_TipoOperacao_Invalido(EnumTipoOperacaoFalhaFaker expectedResult)
    {
        var fileName = @"/Files/caseUploadBoletosRV-ArquivoBoletoExemplo.txt";
       var pathApp =  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
       var pathRoot = Path.Combine(pathApp+fileName);
       using (var fileStream = new StreamReader(pathRoot))
       { 
           _listUploadBoleto = _mapper.Map<List<UploadBoletoDto>>(await FormFileHelper.ExtrairDadosBoletoToDto(fileStream.BaseStream));
       }

       ObterTipoOperacaoNaListaUploadBoletoImportacao(_listUploadBoleto, expectedResult.DescriptionAttrFaker()).ShouldBeFalse();

    }

    private bool ObterTipoOperacaoNaListaUploadBoletoImportacao(List<UploadBoletoDto> listObj, string descriptionAttr)
    {
        return listObj.Exists(x => descriptionAttr.Equals(x.TipoOperacao));
    }
}