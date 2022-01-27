using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Infra.Interfaces;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace Infra.Repositories
{
    public class ClienteRepository : SqlAdapter, IClienteRepository
    {
        public ClienteRepository(SqlConnection sqlConnection) : base(sqlConnection)
        { }
              
        public IEnumerable<Cliente> Select()
        {
            string query = @"
                    SELECT * FROM CLIENTE C 
                        INNER JOIN PROFISSAO P 
                            ON C.IDPROFISSAO = P.IDPROFISSAO 
                    ORDER BY 
                        C.NOME
                ";

            var result = SqlConnection.Query<Cliente, Profissao, Cliente>(
                  query,
                  map: (cliente, profissao) =>
                  {
                      cliente.Profissao = profissao;
                      return cliente;
                  }, 
                  null,
                  SqlTransaction,
                 splitOn: "IDCLIENTE,IDPROFISSAO");

            return result;
        }

        public Cliente SelectById(Int64 id)
        {
            string query = @"
                    SELECT * FROM CLIENTE C 
                        INNER JOIN PROFISSAO P 
                            ON C.IDPROFISSAO = P.IDPROFISSAO                      
                    WHERE 
                        IDCLIENTE = @IDCLIENTE
                    ORDER BY 
                        C.NOME
                ";

            var result = SqlConnection.Query<Cliente, Profissao, Cliente>(
                  query,
                  map: (cliente, profissao) =>
                  {
                      cliente.Profissao = profissao;
                      return cliente;
                  },
                  new { IDCLIENTE = id },
                  SqlTransaction,
                 splitOn: "IDCLIENTE,IDPROFISSAO").FirstOrDefault();

            return result;
        }      

        public bool Insert(Cliente cliente)
        {
            var result = SqlConnection.Insert(cliente, SqlTransaction);

            cliente.AtualizarId((Int64)result);

            return true;
        }

        public bool Update(Cliente cliente)
        {
            bool result = SqlConnection.Update(cliente, SqlTransaction);

            return result;
        }

        public bool Delete(Cliente cliente)
        {
            bool result = SqlConnection.Delete(cliente, SqlTransaction);

            return result;
        }

    }
}
