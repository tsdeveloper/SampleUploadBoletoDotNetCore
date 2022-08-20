using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Specifications.UploadBoletos.SpecParams;

public class UploadBoletosFromTextSpecParams : BaseFormFileSpecParams
{
    [Required] public IFormFile UploadFile { get; set; }
}