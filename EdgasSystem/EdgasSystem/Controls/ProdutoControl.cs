using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controls
{
    public class ProdutoControl
    {
        public bool GravarProduto(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                dados["preco"] = dados["preco"].ToString().Replace(".", ",");
                Models.Produto produto = new Models.Produto(0,
                          new Models.Tipo_Produto().BuscarTipo_Produto(Convert.ToInt32(dados["cod_tipo_produto"].ToString())),
                          dados["nome"].ToString(),
                          dados["descricao"].ToString(),
                          Convert.ToDouble(dados["preco"].ToString()),
                          Convert.ToInt32(dados["qtde_estoque"].ToString())
                          );
                if (produto.Gravar(Transacao, true))
                    return true;
                else

                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AlterarProduto(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                dados["preco"] = dados["preco"].ToString().Replace(".", ",");
                Models.Produto produto = new Models.Produto(Convert.ToInt32(dados["cod_produto"].ToString()),
                          new Models.Tipo_Produto().BuscarTipo_Produto(Convert.ToInt32(dados["cod_tipo_produto"].ToString())),
                          dados["nome"].ToString(),
                          dados["descricao"].ToString(),
                          Convert.ToDouble(dados["preco"].ToString()),
                          Convert.ToInt32(dados["qtde_estoque"].ToString())
                          );
                if (produto.Gravar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirProduto(int cod_produto)
        {
            bool Transacao = true;

            Models.Produto produto = new Models.Produto().BuscarProduto(cod_produto);
            try
            {
                if (produto.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Models.Produto> ObterProdutos(string filtro)
        {
            return new Models.Produto().ObterProdutos(filtro);
        }

        public Models.Produto BuscarTProduto(int cod_produto)
        {
            return new Models.Produto().BuscarProduto(cod_produto);
        }

        

    }
}
