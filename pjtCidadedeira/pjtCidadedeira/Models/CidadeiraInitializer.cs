using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pjtCidadedeira.Models
{
    
    public class CidadeiraInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CidadeiraDBContext>
    {
        protected override void Seed(CidadeiraDBContext context)
        {
            var categorias = new List<Categoria>
                {
                    new Categoria{Titulo="Iluminação",Descricao="Problemas de iluminação em determinado perimetro"},
                    new Categoria{Titulo="Vias Publicas",Descricao="Problemas nas calçadas, sinalização inadequada"}
                };
            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();

            var reclamacoes = new List<Reclamacao>
                {
                    new Reclamacao{Titulo="Buraco na calçada",Descricao="Tem uma cratera no meio da calçada",CategoriaID = 1/*categorias.Single( g => g.Titulo == "Vias Publica").CategoriaID*/, Data=DateTime.Parse("2016-01-01"),UsuarioID=1,LinkImagem="c/img/fig1.png",Status="Aberto"},
                    new Reclamacao{Titulo="Lampada Queimada",Descricao="Falta de Iluminação, devido a lampada queimada ",CategoriaID = 1/*categorias.Single( g => g.Titulo == "Iluminação").CategoriaID*/,Data=DateTime.Parse("2016-01-01"),UsuarioID=2,LinkImagem="c/img/fig2.png",Status="Aberto"}
                };
            reclamacoes.ForEach(s => context.Reclamacoes.Add(s));
            context.SaveChanges();

            var comentarios = new List<Comentario>
                {
                    new Comentario{descricaoComentario="é vdd amigo", Data=DateTime.Now,UsuarioID=1,LinkImagem="c/img/fig1.png",ReclamacaoID=1}
                };
            comentarios.ForEach(s => context.Comentarios.Add(s));
            context.SaveChanges();

            var enderecos = new List<Endereco>
            {
                new Endereco {Bairro="floresta",Logradouro="Av. Cristovão Colombo" }
            };
            enderecos.ForEach(s => context.Enderecos.Add(s));
            context.SaveChanges();

        }
    }
}