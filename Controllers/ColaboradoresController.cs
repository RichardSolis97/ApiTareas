using API_Atareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

namespace API_Atareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ColaboradoresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpGet]
        [Route("Get")]
        public JsonResult Get()
        {
            List<Cl_Colaboradores> lista = new List<Cl_Colaboradores>();
            try
            {
                string query = @"
                SELECT ""Id"", ""colaborador""
                FROM ""Tb_Colaborador""";

                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                //NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {

                        using (NpgsqlDataReader Reader = Comando.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                lista.Add(new Cl_Colaboradores
                                {
                                    Id = Convert.ToInt32(Reader["Id"]),
                                    Nombre = Reader["Colaborador"].ToString()

                                }); ;




                            }



                        }



                    }

                    return new JsonResult(lista);

                }
            }
            catch (System.Exception e)
            {

                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = lista }));
            }

         

        }

        [HttpGet]
        [Route("Lista_ID/{Id:int}")]
        public JsonResult Lista_ID(int Id)
        {
            List<Cl_Colaboradores> lista = new List<Cl_Colaboradores>();
            try
            {
                string query = @"
               SELECT ""Id"", ""colaborador""
                    FROM ""Tb_Colaborador"" 
                    WHERE ""Id""  != @Id

                
                ";

                string sqlDataSource = _configuration.GetConnectionString("PostgresSQLConecction");
                NpgsqlDataReader Reader;

                using (NpgsqlConnection Conexion = new NpgsqlConnection(sqlDataSource))
                {
                    Conexion.Open();
                    using (NpgsqlCommand Comando = new NpgsqlCommand(query, Conexion))
                    {
                        Comando.Parameters.AddWithValue("@Id", Id);
                        Reader = Comando.ExecuteReader();
                        
                        
                            while (Reader.Read())
                            {
                                lista.Add(new Cl_Colaboradores
                                {
                                    Id = Convert.ToInt32(Reader["Id"]),
                                    Nombre = Reader["Colaborador"].ToString()

                                }); ;




                            }



                        



                    }

                    return new JsonResult(lista);

                }
            }
            catch (System.Exception e)
            {

                return new JsonResult(StatusCode(statusCode: StatusCodes.Status500InternalServerError, new { mensaje = e.Message, Response = lista }));
            }





        }






    }
}
