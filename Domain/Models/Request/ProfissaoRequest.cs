using System;

namespace Domain.Models
{
    public class ProfissaoRequest
    {
        public Int32 IdProfissao { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
    }
}
