using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class PorComentario
    {
        public string propriedade { get; set; }
        public DateTime data { get; set; }
        public int ComentarioCount { get; set; }
    }
}