using Domain.Entities;
using System;

namespace Domain.Models
{
    public class ClienteResponse
    {
        public Int64 IdCliente { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public Int16 Idade { get; set; }
        public DateTime DtNasc { get; set; }
        public bool Status { get; set; }
        public Int32? IdProfissao { get; set; }
        public Profissao Profissao { get; set; }
    }
}
