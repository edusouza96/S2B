using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nome { get; set; }
        public string User { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
    }
}