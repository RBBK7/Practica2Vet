using System.ComponentModel.DataAnnotations;

namespace VET_API.Models
{
    public class MascotaRequestModel
    {
        [Required] public string Nombre { get; set; } = string.Empty;
        [Required] public string Especie { get; set; } = string.Empty;
        [Required] public string Raza { get; set; } = string.Empty;
        [Required][Range(0.01, 9999)] public decimal Peso { get; set; }
        [Required][Range(1, int.MaxValue)] public int IdCliente { get; set; }
    }
}