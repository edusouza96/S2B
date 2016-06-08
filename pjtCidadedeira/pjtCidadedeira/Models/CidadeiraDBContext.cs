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
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}