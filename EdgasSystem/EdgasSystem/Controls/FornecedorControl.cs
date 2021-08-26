using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EdgasSystem.Controls
{
    public class FornecedorControl
    {

        public bool GravarFornecedor(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                GlobalExceptionHandler.ControlErrorMessage = "Erro ao Gravar!";
                if (VerificaContato(dados["contato"].ToString(), dados["telefone_contato"].ToString()))
                {
                    Models.Fornecedor funcionario = new Models.Fornecedor(0,
                          dados["cnpj"].ToString(),
                          dados["nome"].ToString(),
                          dados["endereco"].ToString(),
                          dados["cidade"].ToString(),
                          dados["cep"].ToString(),
                          dados["telefone"].ToString(),
                          dados["contato"].ToString(),
                          dados["telefone_contato"].ToString()
                          );
                    if (funcionario.Gravar(Transacao, true))
                      return true;
                    else
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public bool Excluir(int cod_fornecedor)
        {
            bool Transacao = true;

            Models.Fornecedor fornecedor = new Models.Fornecedor().BuscarFornecedor(cod_fornecedor);
            try
            {
                if (fornecedor.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public List<Models.Fornecedor> ObterFornecedores(string filtro)
        {
            return new Models.Fornecedor().ObterFornecedores(filtro);
        }


        public Models.Fornecedor BuscarFornecedor(int cod_fornecedor)
        {
            return new Models.Fornecedor().BuscarFornecedor(cod_fornecedor);
        }

        public bool AlterarFornecedor(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                GlobalExceptionHandler.ControlErrorMessage = "Erro ao Alterar!";
                if (VerificaContato(dados["contato"].ToString(),dados["telefone_contato"].ToString()))
                {
                    Models.Fornecedor funcionario = new Models.Fornecedor(Convert.ToInt32(dados["cod_fornecedor"].ToString()),
                          dados["cnpj"].ToString(),
                          dados["nome"].ToString(),
                          dados["endereco"].ToString(),
                          dados["cidade"].ToString(),
                          dados["cep"].ToString(),
                          dados["telefone"].ToString(),
                          dados["contato"].ToString(),
                          dados["telefone_contato"].ToString()
                          );
                    if (funcionario.Gravar(Transacao, true))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private bool VerificaContato(string contato, string telefone)
        {
            
            if (contato.Length>0)
            {
                if (telefone.Length == 0 || telefone.Length == 15)
                    return true;
                else
                {
                    GlobalExceptionHandler.ControlErrorMessage = "Telefone do contato invalido!";
                    return false;
                } 
            }
            else
            {
                if(telefone.Length == 15)
                {
                    GlobalExceptionHandler.ControlErrorMessage = "Telefone do contato informado, porem não foi informado o contato!";
                    return false;
                }
                return true; 
            }
            
        }

    }
}
