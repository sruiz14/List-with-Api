using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApi.Models
{
    public class datos
    {
        public int Tabla {  get; set; }
        public string Busqueda { get; set; }
        public int IdJson { get; set; }
        public string DatJson { get; set; }
    }

    public class respuestaRequest
    {
        public string Err {  get; set; }
        public string ErrDescripcion { get; set; }
        public string json { get; set; }
    }

    public class personaDatos
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Estatus { get; set; }
    }
}