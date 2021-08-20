using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SICOPruebaEstudiante.Models.Context;

namespace SICOPruebaEstudiante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        PruebaSICOContext _dbContext;
        public CursoController(PruebaSICOContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public ActionResult GetCourses()
        {
            try
            {
 
                return Ok(_dbContext.Curso);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
