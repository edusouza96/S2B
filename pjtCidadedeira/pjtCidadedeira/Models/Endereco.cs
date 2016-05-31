using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class Endereco
    {
        public int EnderecoID { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
    }
}