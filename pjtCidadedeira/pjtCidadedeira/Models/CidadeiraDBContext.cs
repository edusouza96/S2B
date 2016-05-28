using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class CidadeiraDBContext : DbContext
    {
        public CidadeiraDBContext() : base("CidadeiraDBContext")
        {

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Reclamacao> Reclamacoes { get; set; }
        
    }
}