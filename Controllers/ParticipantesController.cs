using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using CompAPI.Models;
using System.Data;
using CompAPI.DAL;
using System.Text;
using Dapper;
using System.Linq;
using System;

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantesController : ControllerBase
    {
        
        private readonly IConfiguration _config;

        public ParticipantesController (IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using(IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID AS Id, ID_TIPO_PARTICIPANTE AS TipoId, TX_NOME AS Nome, ");
                sql.Append("TX_CPF AS Cpf, TX_EMAIL AS Email  ");
                sql.Append("FROM TB_PARTICIPANTE");

                List<Participante> lista = (await conexao.QueryAsync<Participante>(sql.ToString())).ToList();

                return Ok(lista);
            }

        }
    }
}