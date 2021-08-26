using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controls
{
    public class Tipo_ProdutoControl
    {
        public bool GravarTipo_Produto(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Tipo_Produto tipo = new Models.Tipo_Produto(0,
                          dados["nome"].ToString(),
                          dados["descricao"].ToString()
                          );
                if (tipo.Gravar(Transacao, true))
                    return true;
                else

                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AlterarTipo_Produto(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Tipo_Produto tipo = new Models.Tipo_Produto(Convert.ToInt32(dados["cod_tipo_produto"].ToString()),
                 dados["nome"].ToString(),
                 dados["descricao"].ToString()
                 );
                if (tipo.Gravar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirTipo_Produto(int cod_tipo_produto)
        {
            bool Transacao = true;
            GlobalExceptionHandler.ControlErrorMessage = "Erro ao Excluir";
            Models.Tipo_Produto tipo = new Models.Tipo_Produto().BuscarTipo_Produto(cod_tipo_produto);
            try
            {
                if(tipo !=null)
                {
                    if (new Models.Produto().ObterProdutosPorTipo(cod_tipo_produto) != null )
                    {
                        if (tipo.Excluir(Transacao))
                            return true;
                        else
                            return false;
                    }
                    else
                        GlobalExceptionHandler.ControlErrorMessage = "Este Tipo de Produto está associado a algum produto, por isso não pode ser excluido no momento!";
                }
                    return false;
                
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Models.Tipo_Produto> ObterTipo_Produto(string filtro)
        {
            return new Models.Tipo_Produto().ObterTipo_Produtos(filtro);
        }

        public Models.Tipo_Produto BuscarTipo_Produto(int cod_cliente)
        {
            return new Models.Tipo_Produto().BuscarTipo_Produto(cod_cliente);
        }

        

    }
}
