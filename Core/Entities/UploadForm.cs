using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Entities;

public class UploadForm
{
    [Required] public IFormFile UploadFile { get; set; }
}