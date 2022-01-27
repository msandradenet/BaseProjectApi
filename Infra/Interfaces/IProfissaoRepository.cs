using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infra.Interfaces
{
    public interface IProfissaoRepository
    {
        SqlTransaction SqlTransaction { get; set; }
        IEnumerable<Profissao> Select();
        Profissao SelectById(Int32 id);
    }
}
