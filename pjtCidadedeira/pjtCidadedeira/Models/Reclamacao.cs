using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    public class Reclamacao
    {
        public int ReclamacaoID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CategoriaID { get; set; }
        public DateTime Data { get; set; }
        public string LinkImagem { get; set; }
        public string Status { get; set; }
        public int UsuarioID { get; set; }
        public int EnderecoID { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
    }
}