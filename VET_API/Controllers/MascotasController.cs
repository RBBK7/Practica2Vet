using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using VET_API.Models;

namespace VET_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController(IConfiguration _configuration) : ControllerBase
    {
        [HttpPost("Registrar")]
        public IActionResult Registrar(MascotaRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@Nombre", model.Nombre.Trim());
            parameters.Add("@Especie", model.Especie.Trim());
            parameters.Add("@Raza", model.Raza.Trim());
            parameters.Add("@Peso", model.Peso);
            parameters.Add("@IdCliente", model.IdCliente);

            var response = context.QueryFirst<ResultadoResponse>(
                "sp_RegistrarMascota", parameters, commandType: CommandType.StoredProcedure);

            if (response.Resultado == 0)
                return Conflict(response);

            return Ok(response);
        }

        [HttpGet("Consultar")]
        public IActionResult Consultar()
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var mascotas = context.Query<MascotaConsultaResponse>(
                "sp_ConsultarMascotas", commandType: CommandType.StoredProcedure).ToList();

            return Ok(mascotas);
        }
    }
}