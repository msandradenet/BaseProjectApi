using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Infra.Interfaces;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Infra.Repositories
{
    public class ProfissaoRepository : SqlAdapter, IProfissaoRepository
    {
        public ProfissaoRepository(SqlConnection sqlConnection) : base(sqlConnection)
        { }
              
        public IEnumerable<Profissao> Select()
        {
            string query = "SELECT * FROM PROFISSAO WHERE STATUS = 1 ORDER BY DESCRICAO";

            var result = SqlConnection.Query<Profissao>(query, SqlTransaction);

            return result;
        }

        public Profissao SelectById(Int32 id)
        {
            string query = "SELECT * FROM PROFISSAO WHERE IDPROFISSAO = @IDPROFISSAO AND STATUS = 1";

            var result = SqlConnection.QueryFirstOrDefault<Profissao>(query, new { IDPROFISSAO = id }, SqlTransaction);

            return result;
        }   
    }
}
