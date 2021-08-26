using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Produto
    {
        private int cod_produto;
        private Tipo_Produto tipo_produto;
        private string nome;
        private string descricao;
        private double preco;
        private int qtde_estoque;

        public Produto()
        {
        }
        public Produto(int cod_produto, Tipo_Produto tipo_produto, string nome, string descricao, double preco)
        {
            this.Cod_produto = cod_produto;
            this.Tipo_produto = tipo_produto;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }

        public Produto(int cod_produto, Tipo_Produto tipo_produto, string nome, string descricao, double preco, int qtde_estoque)
        {
            this.Cod_produto = cod_produto;
            this.Tipo_produto = tipo_produto;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.Qtde_estoque = qtde_estoque;
        }
        public int Cod_produto { get => cod_produto; set => cod_produto = value; }
        public Tipo_Produto Tipo_produto { get => tipo_produto; set => tipo_produto = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public double Preco { get => preco; set => preco = value; }
        public int Qtde_estoque { get => qtde_estoque; set => qtde_estoque = value; }

        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.ProdutoDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.ProdutoDAL().Excluir(this.Cod_produto, Transacao);
        }

        public Models.Produto BuscarProduto(int cod_produto)
        {
            return new DAL.ProdutoDAL().BuscarProduto(cod_produto);
        }


        public List<Models.Produto> ObterProdutos(string filtro)
        {
            return new DAL.ProdutoDAL().ObterProdutos(filtro);
        }

        public List<Models.Produto> ObterProdutosPorTipo(int cod_tipo)
        {
            return new DAL.ProdutoDAL().ObterProdutosPorTipo(cod_tipo);
        }

    }
}
