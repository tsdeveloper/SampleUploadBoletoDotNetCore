using System.Globalization;
using API.Dtos.UploadBoletos;
using API.Specifications.UploadBoletos;
using AutoMapper;
using Core.Entities;
using Core.Helpers.FormFiles;
using Core.Interfaces.UploadBoletos;
using Core.Iterator;
using Core.Specifications.UploadBoletos.SpecParams;
using Core.Strategies.Boletos;
using Core.Validators.Boletos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadFilesController : ControllerBase
{
   
    private readonly ILogger<UploadFilesController> _logger;
    private readonly IMapper _mapper;
    private readonly IUploadBoletoService _uploadBoletoService;
    private readonly IBoletoSpecification _boletoSpecification;

    public UploadFilesController(ILogger<UploadFilesController> logger, IMapper mapper,
        IUploadBoletoService uploadBoletoService, IBoletoSpecification boletoSpecification)
    {
        _logger = logger;
        _mapper = mapper;
        _uploadBoletoService = uploadBoletoService;
        _boletoSpecification = boletoSpecification;
    }

    [HttpPost(Name = "Boletos-files")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> UploadBoletosFromText([FromForm] UploadBoletosFromTextSpecParams uploadForm)
    {
         
        var listUploadBoletosDto = _mapper.
                                        Map<List<UploadBoletoDto>>(await FormFileHelper
                                                        .ExtrairDadosBoletoToDto(uploadForm.UploadFile));

        _boletoSpecification.ObterBoletosGroupByCodigoCliente(ref listUploadBoletosDto);

        listUploadBoletosDto = _mapper.Map<List<UploadBoletoDto>>(await _uploadBoletoService
                        .CreateUploadBoletoListAsync(_mapper
                                .Map<List<UploadBoleto>>(listUploadBoletosDto)));
        
        return Ok(listUploadBoletosDto);
    }
}