using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICOPruebaEstudiante.Models
{
    /*Relacion Estudiante Curso*/

    public class EstudianteCurso
    {
        public int Id { get; set; }
        [ForeignKey("Estudiante")]
        public int IdEstudiante { get; set; }
        [ForeignKey("Curso")]
        public int IdCurso { get; set; }
        public decimal NotaFinal { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Estudiante Estudiante { get; set; }
    }
}
