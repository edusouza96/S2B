using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class Comentario
    {
        public int ComentarioID { get; set; }
        public string descricaoComentario { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioID { get; set; }
        public string LinkImagem { get; set; }
        public int ReclamacaoID { get; set; }

        public virtual Reclamacao Reclamacao { get; set; }
    }
}