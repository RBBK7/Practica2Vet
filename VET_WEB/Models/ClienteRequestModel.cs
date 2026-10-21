using System.ComponentModel.DataAnnotations;

namespace VET_WEB.Models
{
    public class ClienteRequestModel
    {
        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}