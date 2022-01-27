using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IClienteService
    {
        public IEnumerable<Cliente> Get();
        public Cliente GetById(Int64 id);
        public bool Post(Cliente cliente);
        public bool Put(Cliente cliente);
        public bool Delete(Int64 id);
    }
}
