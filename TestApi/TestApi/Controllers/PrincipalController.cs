using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using TestApi.Models;
using Newtonsoft.Json;

namespace TestApi.Controllers
{
    [RoutePrefix("api/principal")]
    public class PrincipalController : ApiController
    {
        [HttpPost]
        [Route("demo")]
        public IHttpActionResult demo([FromBody] datos datos)
        {
            int IdJson = datos.IdJson.ToString() == "" ? 0 : datos.IdJson;
            string DatJson = datos.DatJson.ToString() == "" ? "0" : datos.DatJson.ToString();

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.Registrar(IdJson, DatJson);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Retorno);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("demo2")]
        public IHttpActionResult demo2([FromBody] datos datos)
        {
            int IdJson = datos.IdJson.ToString() == "" ? 0 : datos.IdJson;
            string DatJson = datos.DatJson.ToString() == "" ? "0" : datos.DatJson.ToString();

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.Registrar2(IdJson, DatJson);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult create([FromBody] personaDatos datos)
        {
            string[] DatJson = new string[4];
            string Nombres = datos.Nombres.ToString() == "" ? "0" : datos.Nombres.ToString();
            string ApellidoP = datos.ApellidoP.ToString() == "" ? "0" : datos.ApellidoP.ToString();
            string ApellidoM = datos.ApellidoM.ToString() == "" ? "0" : datos.ApellidoM.ToString();
            string Direccion = datos.Direccion.ToString() == "" ? "0" : datos.Direccion.ToString();
            int Telefono = datos.Telefono.ToString() == "" ? 0 : datos.Telefono;

            DatJson[0] = Nombres;
            DatJson[1] = ApellidoP;
            DatJson[2] = ApellidoM;
            DatJson[3] = Direccion;

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.Registrar3(Telefono, DatJson);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult update([FromBody] personaDatos datos)
        {
            string[] DatJson = new string[4];
            int[] NumJson = new int[2];
            int Id = datos.Id.ToString() == "" ? 0 : datos.Id;
            string Nombres = datos.Nombres.ToString() == "" ? "0" : datos.Nombres.ToString();
            string ApellidoP = datos.ApellidoP.ToString() == "" ? "0" : datos.ApellidoP.ToString();
            string ApellidoM = datos.ApellidoM.ToString() == "" ? "0" : datos.ApellidoM.ToString();
            string Direccion = datos.Direccion.ToString() == "" ? "0" : datos.Direccion.ToString();
            int Telefono = datos.Direccion.ToString() == "" ? 0 : datos.Telefono;

            NumJson[0] = Id;
            NumJson[1] = Telefono;

            DatJson[0] = Nombres;
            DatJson[1] = ApellidoP;
            DatJson[2] = ApellidoM;
            DatJson[3] = Direccion;

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.Registrar4(NumJson, DatJson);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("deactivate")]
        public IHttpActionResult deactivate([FromBody] personaDatos datos)
        {
            int Id = datos.Id.ToString() == "" ? 0 : datos.Id;

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.Registrar5(Id);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Retorno);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("table")]
        public IHttpActionResult table([FromBody] datos datos)
        {
            int Tabla = datos.Tabla.ToString() == "" ? 0 : datos.Tabla;

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.verTabla(Tabla);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("search")]
        public IHttpActionResult search([FromBody] datos datos)
        {
            string Busqueda = datos.Busqueda.ToString() == "" ? "0" : datos.Busqueda.ToString();

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.verResultado(Busqueda);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }

        [HttpPost]
        [Route("persona")]
        public IHttpActionResult persona([FromBody] personaDatos datos)
        {
            int Estatus = datos.Estatus.ToString() == "" ? 0 : datos.Estatus;

            dynamic Response = null;
            dynamic Retorno = null;
            try
            {
                Retorno = Principal.verPersona(Estatus);

                Response = new
                {
                    Estado = true,
                    Mensaje = Retorno,
                };

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error general");
            }

        }
    }
}