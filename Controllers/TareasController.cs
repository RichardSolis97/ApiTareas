using API_Atareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_Atareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public TareasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }   



        [HttpGet]
        [Route("Get")]
        public JsonResult Lista()
        {
            List<Cl_Tareas> lista = new List<Cl_Tareas>();
            
            try
            {
                string query = @"

               SELECT ""Tb_Tareas"".""Id"", ""Descripcion"", ""Colaborador"",  ""Tb_Colaborador"".""colaborador"", ""Estado"", ""Prioridad"", ""Notas"", ""FechaInicio"", ""FechaFin""
               FROM ""Tb_Tareas""
               INNER JOIN  ""Tb_Colaborador""  
               ON ""Tb_Colaborador"".""Id"" = ""Tb_Tareas"".""Colaborador"";;
                 ";
               
                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                //NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {

                        using(NpgsqlDataReader Reader = Comando.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                lista.Add(new Cl_Tareas
                                {
                                    id = Convert.ToInt32(Reader["Id"]),
                                    Descripcion = Reader["Descripcion"].ToString(),
                                    ColaboradorId = Convert.ToInt32(Reader["Colaborador"]) ,
                                    Colaborador = Reader["colaborador"].ToString(),
                                    Estado = Reader["Estado"].ToString(),
                                    Prioridad = Reader["Prioridad"].ToString(),
                                    FechaInicio = Reader["FechaInicio"].ToString(),
                                    FechaFin = Reader["FechaFin"].ToString(),
                                    Notas = Reader["Notas"].ToString()

                                });


                                

                            }



                        }

                       
                        
                    }



                }
              

                return new JsonResult(lista);
            }
            catch (Exception e)
            {
                
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = lista }));
            }

            
                 

        }


        [HttpGet]
        [Route("Lista_ID/{Id:int}")]
        public JsonResult Lista_ID(int Id)
        {
            Cl_Tareas Datos = new Cl_Tareas();
            try
            {
                string query = @"

               SELECT ""Tb_Tareas"".""Id"", ""Descripcion"",""Colaborador"" ,  ""Tb_Colaborador"".""colaborador"", ""Estado"", ""Prioridad"", ""Notas"", ""FechaInicio"", ""FechaFin""
               FROM ""Tb_Tareas""
               INNER JOIN  ""Tb_Colaborador""  
               ON ""Tb_Colaborador"".""Id"" = ""Tb_Tareas"".""Colaborador""
               WHERE ""Tb_Tareas"".""Id""= @Id;;
                 ";

                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {

                        Comando.Parameters.AddWithValue("@ID", Id);
                        Reader = Comando.ExecuteReader();

                        while (Reader.Read())
                        {


                            Datos.id = Convert.ToInt32(Reader["Id"]);
                            Datos.Descripcion = Reader["Descripcion"].ToString();
                            Datos.ColaboradorId = Convert.ToInt32(Reader["Colaborador"]);
                            Datos.Colaborador  = Reader["colaborador"].ToString();
                            Datos.Estado = Reader["Estado"].ToString();
                            Datos.Prioridad = Reader["Prioridad"].ToString();
                            Datos.FechaInicio = Reader["FechaInicio"].ToString();
                            Datos.FechaFin = Reader["FechaFin"].ToString();
                            Datos.Notas = Reader["Notas"].ToString();






                        }


                       
                    }



                }
                return new JsonResult(Datos);
               
            }
            catch (Exception e)
            {
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = Datos }));

            }




        }




        [HttpPost]
        [Route("Post")]
        public JsonResult Post(Cl_Tareas cl_Tareas)
        {
            DataTable Datos = new DataTable();

            try
            {
                string query = @"

               INSERT INTO ""Tb_Tareas""(
	            ""Descripcion"", ""Colaborador"", ""Estado"", ""Prioridad"",  ""Notas"" ,""FechaInicio"", ""FechaFin"")
	            VALUES (@Descripcion,@Colaborador,@Estado,@Prioridad,@Notas,@FechaInicio,@FechaFin);
            ";
               
                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {
                        Comando.Parameters.AddWithValue("@Descripcion", cl_Tareas.Descripcion);
                        Comando.Parameters.AddWithValue("@Colaborador",Convert.ToInt32(cl_Tareas.Colaborador));
                        Comando.Parameters.AddWithValue("@Estado", cl_Tareas.Estado);
                        Comando.Parameters.AddWithValue("@Prioridad", cl_Tareas.Prioridad);
                        Comando.Parameters.AddWithValue("@FechaInicio", Convert.ToDateTime(cl_Tareas.FechaInicio.ToString()));
                        Comando.Parameters.AddWithValue("@FechaFin", Convert.ToDateTime(cl_Tareas.FechaFin.ToString()));
                        Comando.Parameters.AddWithValue("@Notas", cl_Tareas.Notas);
                        Reader = Comando.ExecuteReader();
                        Datos.Load(Reader);

                        Reader.Close();
                        Conexion.Close();
                    }



                }
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status200OK, new { mensaje = "ok", Response = Datos }));
            }
            catch (Exception e)
            {
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = Datos }));
            }
            

           


        }


        [HttpPut]
        [Route("Put/{Id:int}")]
        public JsonResult put(Cl_Tareas cl_Tareas , int Id)
        {
            DataTable Datos = new DataTable();
            try
            {

                string query = @"

             UPDATE ""Tb_Tareas""
	            SET  ""Descripcion""= @Descripcion,""Colaborador""= @Colaborador,""Estado""= @Estado,""Prioridad""= @Prioridad,""Notas""= @notas,""FechaInicio""= @FechaInicio,""FechaFin""= @FechaFin
                WHERE ""Tb_Tareas"".""Id"" = @Id;
            ";
               
                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {
                        Comando.Parameters.AddWithValue("@ID", Id);
                        
                        Comando.Parameters.AddWithValue("@Descripcion", cl_Tareas.Descripcion);
                        Comando.Parameters.AddWithValue("@Colaborador",Convert.ToInt32(cl_Tareas.Colaborador));
                        Comando.Parameters.AddWithValue("@Estado", cl_Tareas.Estado);
                        Comando.Parameters.AddWithValue("@Prioridad", cl_Tareas.Prioridad);
                        Comando.Parameters.AddWithValue("@FechaInicio", cl_Tareas.FechaInicio);
                        Comando.Parameters.AddWithValue("@FechaFin", cl_Tareas.FechaFin);
                        Comando.Parameters.AddWithValue("@Notas", cl_Tareas.Notas);
                        Reader = Comando.ExecuteReader();
                        Datos.Load(Reader);

                        Reader.Close();
                        Conexion.Close();
                    }



                }
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status200OK, new { mensaje = "ok", Response = Datos }));
            }
            catch (Exception e)
            {
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = Datos }));

            }



        }



        [HttpDelete]
        [Route("Delete/{id:int}")]
        public JsonResult Delete(int ID)
        {
            DataTable Datos = new DataTable();
            try
            {
                string query = @"

             DELETE FROM ""Tb_Tareas""
	         WHERE ""Id""= @Id;

            ";
               
                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {
                        Comando.Parameters.AddWithValue("@Id", ID);
                        Reader = Comando.ExecuteReader();
                        Datos.Load(Reader);

                        Reader.Close();
                        Conexion.Close();
                    }



                }
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status200OK, new { mensaje = "ok", Response = Datos }));
            }
            catch (Exception e)
            {
                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = Datos }));

            }

          
         

        }
    }

}



    