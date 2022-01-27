using Dapper.Contrib.Extensions;
using System;

namespace Domain.Entities
{
    [Table("Profissao")]
    public class Profissao
    {
        [Key]
        public Int32 IdProfissao { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }       

        public void AtualizarId(int id) { IdProfissao = id; }
    }
}
