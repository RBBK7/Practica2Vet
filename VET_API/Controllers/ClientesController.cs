using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using VET_API.Models;

namespace VET_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(IConfiguration _configuration) : ControllerBase
    {
        [HttpPost("Registrar")]
        public IActionResult Registrar(ClienteRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@Cedula", model.Cedula.Trim());
            parameters.Add("@Nombre", model.Nombre.Trim());
            parameters.Add("@CorreoElectronico", model.CorreoElectronico.Trim());

            var response = context.QueryFirst<ResultadoResponse>(
                "sp_RegistrarCliente", parameters, commandType: CommandType.StoredProcedure);

            if (response.Resultado == 0)
                return Conflict(response);

            return Ok(response);
        }

        [HttpGet("Consultar")]
        public IActionResult Consultar()
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var clientes = context.Query<ClienteResponse>(
                "sp_ConsultarClientes", commandType: CommandType.StoredProcedure).ToList();

            return Ok(clientes);
        }

    }

}