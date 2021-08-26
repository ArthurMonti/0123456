using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Tipo_Produto
    {
        private int cod_tipo_produto;
        private string nome;
        private string descricao;

        public Tipo_Produto()
        {
        }

        public Tipo_Produto(int cod_tipo_produto, string nome, string descricao)
        {
            this.Cod_tipo_produto = cod_tipo_produto;
            this.Nome = nome;
            this.Descricao = descricao;
        }

        public int Cod_tipo_produto { get => cod_tipo_produto; set => cod_tipo_produto = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }

        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.Tipo_ProdutoDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.Tipo_ProdutoDAL().Excluir(this.Cod_tipo_produto, Transacao);
        }

        public Models.Tipo_Produto BuscarTipo_Produto(int cod_tipo_produto)
        {
            return new DAL.Tipo_ProdutoDAL().BuscarTipo_Produto(cod_tipo_produto);
        }


        public List<Models.Tipo_Produto> ObterTipo_Produtos(string filtro)
        {
            return new DAL.Tipo_ProdutoDAL().ObterTipo_Produtos(filtro);
        }



    }
}
