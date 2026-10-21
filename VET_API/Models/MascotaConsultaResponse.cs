namespace VET_API.Models
{
    public class MascotaConsultaResponse
    {
        public string Cedula { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreMascota { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public decimal Peso { get; set; }
    }
}