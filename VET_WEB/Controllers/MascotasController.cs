using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VET_WEB.Models;

namespace VET_WEB.Controllers
{
    public class MascotasController(HttpClient _httpClient, IConfiguration _configuration) : Controller
    {
        private string Api => _configuration.GetValue<string>("Variables:ApiBaseUrl")!;

        private async Task CargarClientes()
        {
            var clientes = await _httpClient.GetFromJsonAsync<List<ClienteResponse>>(Api + "Clientes/Consultar")
                           ?? new List<ClienteResponse>();

            ViewBag.Clientes = clientes
                .Select(c => new SelectListItem
                {
                    Value = c.IdCliente.ToString(),
                    Text = $"{c.Cedula} - {c.Nombre}"
                }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            try { await CargarClientes(); }
            catch { ViewBag.Error = "No se pudo cargar la lista de clientes"; ViewBag.Clientes = new List<SelectListItem>(); }

            return View(new MascotaRequestModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(MascotaRequestModel model)
        {
            try
            {
                await CargarClientes();

                if (!ModelState.IsValid)
                    return View(model);

                var response = await _httpClient.PostAsJsonAsync(Api + "Mascotas/Registrar", model);
                var datos = await response.Content.ReadFromJsonAsync<ResultadoResponse>();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Exito"] = datos?.Mensaje;
                    return RedirectToAction("Registro");
                }

                ViewBag.Error = string.IsNullOrEmpty(datos?.Mensaje)
                    ? "No se registró la información correctamente" : datos.Mensaje;
            }
            catch
            {
                ViewBag.Error = "Se presentó un inconveniente técnico";
                ViewBag.Clientes ??= new List<SelectListItem>();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Consulta()
        {
            var mascotas = new List<MascotaConsultaResponse>();

            try
            {
                mascotas = await _httpClient.GetFromJsonAsync<List<MascotaConsultaResponse>>(Api + "Mascotas/Consultar")
                           ?? new List<MascotaConsultaResponse>();
            }
            catch
            {
                ViewBag.Error = "No se pudo consultar la información";
            }

            return View(mascotas);
        }
    }
}