using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infra.Interfaces
{
    public interface IClienteRepository
    {
        SqlTransaction SqlTransaction { get; set; }
        IEnumerable<Cliente> Select();
        Cliente SelectById(Int64 id);
        bool Insert(Cliente request);
        bool Update(Cliente request);
        bool Delete(Cliente cliente);
    }
}
