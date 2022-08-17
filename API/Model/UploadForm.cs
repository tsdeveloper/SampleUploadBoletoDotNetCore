using System.ComponentModel.DataAnnotations;

namespace API.Model;

public class UploadForm
{
    [Required] public IFormFile UploadFile { get; set; }
}