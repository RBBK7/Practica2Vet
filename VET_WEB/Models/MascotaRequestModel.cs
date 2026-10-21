using System.ComponentModel.DataAnnotations;

namespace VET_WEB.Models
{
    public class MascotaRequestModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especie es obligatoria")]
        public string Especie { get; set; } = string.Empty;

        [Required(ErrorMessage = "La raza es obligatoria")]
        public string Raza { get; set; } = string.Empty;

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.01, 9999, ErrorMessage = "El peso debe ser mayor a cero")]
        public decimal? Peso { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        public int? IdCliente { get; set; }
    }
}