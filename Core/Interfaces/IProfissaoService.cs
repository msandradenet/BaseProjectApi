using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IProfissaoService
    {
        public IEnumerable<Profissao> Get();
        public Profissao GetById(Int32 id);
    }
}
