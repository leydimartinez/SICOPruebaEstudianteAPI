using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SICOPruebaEstudiante.Models.Context
{
    public class PruebaSICOContext : DbContext
    {


        public PruebaSICOContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<EstudianteCurso> EstudianteCurso { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }


        public List<Estudiante> GetAllStudentDataStoredProcedure()
        {
            List<Estudiante> listStudentData = new List<Estudiante>();
            try
            {
                string cadenaConexion = Database.GetDbConnection().ConnectionString;
                DataTable studentData = new DataTable();
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(cadenaConexion))
                {
                    sqlCon.Open();
                    SqlCommand sqlCommand = new SqlCommand("Get_AllStudentData", sqlCon);
                    sqlCommand.CommandType = CommandType.StoredProcedure; 
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(studentData);

                    studentData.AsEnumerable().ToList().ForEach(row => listStudentData.Add(new Estudiante()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        TipoIdentificacion = row["TipoIdentificacion"].ToString(),
                        Identificacion = row["Identificacion"].ToString(),
                        Nombre1 = row["Nombre1"].ToString(),
                        Nombre2 = row["Nombre2"].ToString(),
                        Apellido1 = row["Apellido1"].ToString(),
                        Apellido2 = row["Apellido2"].ToString(),
                        Email = row["Email"].ToString(),
                        Celular = row["Celular"].ToString(),
                        Direccion = row["Direccion"].ToString(),
                        Ciudad = row["Ciudad"].ToString()
                    })); 
                }
                return listStudentData;
            }
            catch (Exception ex)
            {
                return listStudentData;
            }
        }

        public Estudiante SaveStudentDataStoredProcedure(Estudiante estudiante)
        {
            try
            {
                string cadenaConexion = Database.GetDbConnection().ConnectionString;
                DataTable studentData = new DataTable();
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(cadenaConexion))
                {
                    sqlCon.Open();
                    SqlCommand sqlCommand = new SqlCommand("Save_StudentData", sqlCon);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@TipoIdentificacion", estudiante.TipoIdentificacion));
                    sqlCommand.Parameters.Add(new SqlParameter("@Identificacion", estudiante.Identificacion));
                    sqlCommand.Parameters.Add(new SqlParameter("@Nombre1", estudiante.Nombre1));
                    sqlCommand.Parameters.Add(new SqlParameter("@Nombre2", estudiante.Nombre2));
                    sqlCommand.Parameters.Add(new SqlParameter("@Apellido1", estudiante.Apellido1));
                    sqlCommand.Parameters.Add(new SqlParameter("@Apellido2", estudiante.Apellido2));
                    sqlCommand.Parameters.Add(new SqlParameter("@Email", estudiante.Email));
                    sqlCommand.Parameters.Add(new SqlParameter("@Celular", estudiante.Celular));
                    sqlCommand.Parameters.Add(new SqlParameter("@Direccion", estudiante.Direccion));
                    sqlCommand.Parameters.Add(new SqlParameter("@Ciudad", estudiante.Ciudad));
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(studentData);
                    if (studentData.Rows.Count > 0)
                    {
                        var rowData = studentData.AsEnumerable().FirstOrDefault();

                        estudiante.Id = Convert.ToInt32(rowData["Id"]);
                        return estudiante;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
