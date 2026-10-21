using Microsoft.AspNetCore.Mvc;
using VET_WEB.Models;

namespace VET_WEB.Controllers
{
    public class ClientesController(HttpClient _httpClient, IConfiguration _configuration) : Controller
    {
        [HttpGet]
        public IActionResult Registro()
        {
            return View(new ClienteRequestModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(ClienteRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Clientes/Registrar";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, model);
                var datos = await response.Content.ReadFromJsonAsync<ResultadoResponse>();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Exito"] = datos?.Mensaje;
                    return RedirectToAction("Registro");
                }

                ViewBag.Error = string.IsNullOrEmpty(datos?.Mensaje)
                    ? "No se registró la información correctamente" : datos.Mensaje;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "URL: [" + url + "] - " + ex.Message;
            }

            return View(model);
        }
    }
}