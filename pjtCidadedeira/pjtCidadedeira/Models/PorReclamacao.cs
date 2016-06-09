using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class PorReclamacao
    {
        
        public string propriedade { get; set; }
        public DateTime data { get; set; }
        public int ReclamacaoCount { get; set; }

    }
}