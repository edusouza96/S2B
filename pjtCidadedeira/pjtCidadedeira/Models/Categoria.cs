using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
        
    }
}