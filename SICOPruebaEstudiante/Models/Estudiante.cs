using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICOPruebaEstudiante.Models
{
  
    public class Estudiante
    {
        public int Id { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
    }
}
