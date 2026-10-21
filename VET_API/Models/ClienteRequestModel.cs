using System.ComponentModel.DataAnnotations;

namespace VET_API.Models
{
    public class ClienteRequestModel
    {
        [Required] public string Cedula { get; set; } = string.Empty;
        [Required] public string Nombre { get; set; } = string.Empty;
        [Required] public string CorreoElectronico { get; set; } = string.Empty;
    }
}
