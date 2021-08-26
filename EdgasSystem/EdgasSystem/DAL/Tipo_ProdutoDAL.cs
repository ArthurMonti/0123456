using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class Tipo_ProdutoDAL
    {
        private Banco b;

        internal Tipo_ProdutoDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Tipo_Produto tipo, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@nome", tipo.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", tipo.Descricao);

            if (tipo.Cod_tipo_produto == 0)
                b.getComandoSQL().CommandText = @"insert into Tipo_Produto (nome, descricao) 
                                                                      values (@nome, @descricao)";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_produto", tipo.Cod_tipo_produto);
                b.getComandoSQL().CommandText = @"UPDATE Tipo_Produto 
                                                  set  nome = @nome, descricao = @descricao 
                                                  where cod_tipo_produto = @cod_tipo_produto";
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



        internal bool Excluir(int cod_tipo_produto, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_produto", cod_tipo_produto);

            b.getComandoSQL().CommandText = @"delete from Tipo_Produto 
                                              where cod_tipo_produto = @cod_tipo_produto";

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

        internal Models.Tipo_Produto BuscarTipo_Produto(int cod_tipo_produto)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_produto", cod_tipo_produto);

            b.getComandoSQL().CommandText = @"select *
                                                from Tipo_Produto 
                                                where cod_tipo_produto = @cod_tipo_produto";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Tipo_Produto tipo_des = null;

                tipo_des = new Models.Tipo_Produto(Convert.ToInt32(row["cod_tipo_produto"].ToString()), row["nome"].ToString(), row["descricao"].ToString());

                return tipo_des;
            }
            else
                return null;
        }

        internal List<Models.Tipo_Produto> ObterTipo_Produtos(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from Tipo_Produto";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Tipo_Produto> dados = new List<Models.Tipo_Produto>();

                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Tipo_Produto(Convert.ToInt32(row["cod_tipo_produto"].ToString()), row["nome"].ToString(), row["descricao"].ToString()));
                }
                return dados;
            }
            else
                return null;
        }
    }

    

}
