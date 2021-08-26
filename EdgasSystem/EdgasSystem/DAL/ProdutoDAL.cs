using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class ProdutoDAL
    {

        private Banco b;

        internal ProdutoDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Produto produto, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_produto", produto.Tipo_produto.Cod_tipo_produto);
            b.getComandoSQL().Parameters.AddWithValue("@nome", produto.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", produto.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@preco", produto.Preco);
            b.getComandoSQL().Parameters.AddWithValue("@qtde_estoque", produto.Qtde_estoque);
            

            if (produto.Cod_produto == 0)
                b.getComandoSQL().CommandText = @"insert into produto (cod_tipo_produto, nome, descricao, preco, qtde_estoque) 
                                                              values (@cod_tipo_produto, @nome, @descricao, @preco, @qtde_estoque)";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_produto", produto.Cod_produto);
                b.getComandoSQL().CommandText = @"UPDATE produto set cod_tipo_produto = @cod_tipo_produto, nome = @nome, descricao = @descricao, preco = @preco, qtde_estoque = @qtde_estoque
                                                                     where cod_produto = @cod_produto";
            }


            if (b.ExecutaComando(Transacao) == 1)
            {
                if (Parada)
                    b.FinalizaTransacao(true);
                return true;
            }
            else
            {

                b.FinalizaTransacao(false);
                return false;
            }
        }

        internal bool Excluir(int cod_produto, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_produto", cod_produto);

            b.getComandoSQL().CommandText = @"delete from produto 
                                              where cod_produto = @cod_produto";

            if (b.ExecutaComando(Transacao) == 1)
            {
                b.FinalizaTransacao(true);
                return true;
            }
            else
            {
                b.FinalizaTransacao(false);
                return false;
            }
        }


        internal Models.Produto BuscarProduto(int cod_produto)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_produto", cod_produto);

            b.getComandoSQL().CommandText = @"select *
                                                from produto 
                                                where cod_produto = @cod_produto";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
               

                return new Models.Produto(Convert.ToInt32(row["cod_produto"].ToString()), new Tipo_ProdutoDAL().BuscarTipo_Produto(Convert.ToInt32(row["cod_tipo_produto"].ToString())), row["nome"].ToString(), row["descricao"].ToString(),
                                            Convert.ToDouble(row["preco"].ToString()), Convert.ToInt32(row["qtde_estoque"].ToString()));
            }
            else
                return null;
        }

        internal List<Models.Produto> ObterProdutos(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from produto";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Produto> dados = new List<Models.Produto>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Produto(Convert.ToInt32(row["cod_produto"].ToString()), new Tipo_ProdutoDAL().BuscarTipo_Produto(Convert.ToInt32(row["cod_tipo_produto"].ToString())), row["nome"].ToString(), row["descricao"].ToString(),
                                            Convert.ToDouble(row["preco"].ToString()), Convert.ToInt32(row["qtde_estoque"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Produto> ObterProdutosPorTipo(int cod_tipo)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_produto", cod_tipo);

            b.getComandoSQL().CommandText = @"select *
                                              from produto where cod_tipo_produto = @cod_tipo_produto";


            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Produto> dados = new List<Models.Produto>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Produto(Convert.ToInt32(row["cod_produto"].ToString()), new Tipo_ProdutoDAL().BuscarTipo_Produto(Convert.ToInt32(row["cod_tipo_produto"].ToString())), row["nome"].ToString(), row["descricao"].ToString(),
                                            Convert.ToDouble(row["preco"].ToString()), Convert.ToInt32(row["qtde_estoque"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }
    }
}
