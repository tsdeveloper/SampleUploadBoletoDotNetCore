using System.Globalization;
using API.Dtos.UploadBoletos;
using API.Specifications.UploadBoletos;
using AutoMapper;
using Core.Entities;
using Core.Helpers.FormFiles;
using Core.Strategies.Boletos;
using Core.Validators.Boletos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadFilesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UploadFilesController> _logger;
    private readonly IMapper _mapper;

    public UploadFilesController(ILogger<UploadFilesController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost(Name = "Boletos-files")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> Upload([FromForm] UploadForm uploadForm)
    {
        var listUploadBoletosDto = _mapper.Map<List<UploadBoletoDto>>(await FormFileHelper.ExtrairDadosBoletoToDto(uploadForm.UploadFile));

        new AtualizarValorDescontoBoletoSpecification(_mapper).AplicarValorDescontoBoleto(ref listUploadBoletosDto);

        return Ok(listUploadBoletosDto);
    }
}