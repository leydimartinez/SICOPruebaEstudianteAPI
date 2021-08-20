using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SICOPruebaEstudiante.Models;
using SICOPruebaEstudiante.Models.Context;

namespace SICOPruebaEstudiante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        PruebaSICOContext _dbContext;
        public EstudiantesController(PruebaSICOContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public ActionResult GetAllStudentData()
        {
            try
            {

                List<Estudiante> listStudentData = _dbContext.GetAllStudentDataStoredProcedure();
                return Ok(listStudentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult SaveStudentData(Estudiante studentData)
        {
            try
            {

                var studentFilter = _dbContext.Estudiante.Where(x => x.Identificacion == studentData.Identificacion).FirstOrDefault();

                if (studentFilter == null)
                {
                    Estudiante student = _dbContext.SaveStudentDataStoredProcedure(studentData);
                    if (student != null)
                    {
                        return Ok(student);
                    }
                    else
                    {
                        return BadRequest("Error al guardar los datos de los estudiantes");
                    }
                }
                else
                {
                    return BadRequest("Ya hay un estudiante registrado con esa número de identificación");
                }

               

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult EditStudentData(Estudiante studentData)
        {
            try
            {
                var estudianteAModificar = _dbContext.Estudiante.FirstOrDefault(x => x.Id == studentData.Id);
                _dbContext.Entry(estudianteAModificar).CurrentValues.SetValues(studentData);
                var modificado = _dbContext.SaveChanges();
                if (modificado > 0)
                {
                    return Ok(studentData);
                }
                else
                {
                    return BadRequest("No se realizaron cambios en los datos de los estudiantes");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*Aqui normalmente no se debe eliminar sino que se debe inactivar, es decir, 
         * cambiar el estado de activo a inactivo */
        [HttpDelete("[action]")]
        public ActionResult DeleteStudentCourse(int StudentCourseId)
        {
            try
            {
                var estudianteCursoAEliminar = _dbContext.EstudianteCurso.FirstOrDefault(x => x.Id == StudentCourseId);
                _dbContext.EstudianteCurso.Remove(estudianteCursoAEliminar);
                var modificado = _dbContext.SaveChanges();
                if (modificado > 0)
                {
                    return GetStudentAvailableCourses(estudianteCursoAEliminar.IdEstudiante);
                }
                else
                {
                    return BadRequest("No se pudo modificar los datos de los estudiantes");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult SaveStudentCourse(List<EstudianteCurso> StudeCourses)
        {
            try
            {

                _dbContext.EstudianteCurso.AddRange(StudeCourses);
                var agregados = _dbContext.SaveChanges();
                if (agregados > 0)
                {
                    return Ok(StudeCourses);
                }
                else
                {
                    return BadRequest("No se pudo guardar los datos de los estudiantes");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("[action]")]
        public ActionResult GetStudentCourses(int StudentId)
        {
            try
            {

                var cursos = _dbContext.EstudianteCurso.Include(x => x.Estudiante).Include(x => x.Curso).Where(x => x.IdEstudiante == StudentId);
                return Ok(cursos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("[action]")]
        public ActionResult GetStudentAvailableCourses(int StudentId)
        {
            try
            {

                var cursosDisponibles = (from c in _dbContext.Curso
                                         join ce in _dbContext.EstudianteCurso.Where(x => x.IdEstudiante == StudentId) on c.Id equals ce.IdCurso into ced
                                         from ac in ced.DefaultIfEmpty()
                                         where ac == null
                                         select c);


                return Ok(cursosDisponibles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}