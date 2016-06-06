using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class PorStatus
    {
        public Categoria categoria { get; set; }
        public Endereco endereco { get; set; }
        public Reclamacao reclamacao { get; set; }
        public String Status { get; set; }
        public int StatusCount { get; set; }
    }
}