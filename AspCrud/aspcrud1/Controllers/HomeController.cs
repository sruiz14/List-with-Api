using aspcrud1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace aspcrud1.Controllers
{
    public class HomeController : Controller
    {
        string zelda = "https://localhost:44300/api/principal/";
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TablaPersonas(int Filtro)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(zelda + "table");

                var data = new
                {
                    Tabla = Filtro
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();
                
                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var persona = dsRespuesta["json"].Value<string>();
                var extractResp = JsonConvert.DeserializeObject<List<mTabla>>(persona);

                return Json(extractResp);
            }
        }

        public JsonResult TablaPersonasbusqueda(string Busqueda)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.obtenerPersonasBusqueda(Busqueda);

            return Json(x);
        }

        public JsonResult CrearClientes(mPersonas newPersona)
        {
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri(zelda + "create");

                var data = new
                {
                    Nombres = newPersona.Nombre,
                    ApellidoP = newPersona.ApellidoP,
                    ApellidoM = newPersona.ApellidoM,
                    Direccion = newPersona.Direccion,
                    Telefono = newPersona.Telefono
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Err"];

                bool extractResp = true;

                return Json(extractResp);
            }
        }

        public JsonResult VerPersona(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(zelda + "demo2");

                var data = new
                {
                    IdJson = Id,
                    DatJson = "Hola"
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var persona = dsRespuesta["json"].Value<string>();
                var extractResp = JsonConvert.DeserializeObject<List<mDatos>>(persona);

                return Json(extractResp);
            }
        }

        public JsonResult EditarPersona(mPersonas newPersona, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(zelda + "update");

                var data = new
                {
                    Id = id,
                    Nombres = newPersona.Nombre,
                    ApellidoP = newPersona.ApellidoP,
                    ApellidoM = newPersona.ApellidoM,
                    Direccion = newPersona.Direccion,
                    Telefono = newPersona.Telefono,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Mensaje"];

                var validacion = dsRespuesta["Err"].Value<string>();

                bool extractResp = true;

                if(validacion != "1")
                {
                    extractResp = false;
                }

                return Json(extractResp);
            }
        }

        public JsonResult EliminarPersona(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(zelda + "deactivate");

                var data = new
                {
                    Id = id,
                };

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = client.PostAsync(client.BaseAddress, content);
                request.Wait();

                var respuesta = request.Result.Content.ReadAsStringAsync().Result;
                var dsRespuesta = JObject.Parse(respuesta)["Err"];

                bool extractResp = true;

                return Json(extractResp);
            }
        }

        public JsonResult Guardar(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.insertPersona(newPersona);
            return Json(x);

        }

        public JsonResult DetallesPersona(int Id)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.obtenerPersonaDetalles(Id);
            return Json(x);
        }

        public JsonResult Editar(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.EditarPersona(newPersona);
            return Json(x);

        }

        public JsonResult Eliminar(int Id)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.EliminarPersona(Id);
            return Json(x);
        }

        public JsonResult Reactivar(int Id)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.ReactivarPersona(Id);
            return Json(x);
        }
    }
}