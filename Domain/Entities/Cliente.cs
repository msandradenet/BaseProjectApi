using Dapper.Contrib.Extensions;
using System;

namespace Domain.Entities
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public Int64 IdCliente { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime DtNasc { get; set; }
        public bool Status { get; set; }
        public Int32? IdProfissao { get; set; }

        [Write(false)]
        public Profissao Profissao { get; set; }

        [Write(false)]
        public Int16? Idade
        {
            get
            {
                if (DateTime.Now.Month >= DtNasc.Month && DateTime.Now.Day >= DtNasc.Day)
                    return (short)(DateTime.Now.Year - DtNasc.Year);
                else
                    return (short)(DateTime.Now.Year - DtNasc.Year - 1);
            }
        }

        public void AtualizarId(Int64 id) { IdCliente = id; }
    }
}
