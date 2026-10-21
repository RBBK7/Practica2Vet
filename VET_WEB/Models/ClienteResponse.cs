namespace VET_WEB.Models
{
    public class ClienteResponse
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}