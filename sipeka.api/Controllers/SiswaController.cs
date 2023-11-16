using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace sipeka.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiswaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SiswaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ContentResult Get()
        {
            string query = @"select id_siswa, nama_siswa, jenis_kelamin from siswa";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SipekaDB");

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            var json = JsonConvert.SerializeObject(table);
            return Content(json, "application/json");
        }
    }
}
