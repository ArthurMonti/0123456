using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EdgasSystem.Controls
{
    public class FuncionarioControl
    {

        public bool GravarFuncionario(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                dados["salario"] = dados["salario"].ToString().Replace(".", ",");

                Models.Funcionario funcionario = new Models.Funcionario(0,
                          dados["nome"].ToString(),
                          dados["cpf"].ToString(),
                          dados["telefone"].ToString(),
                          Convert.ToDouble(dados["salario"].ToString()),
                          dados["email"].ToString(),
                          Convert.ToDateTime(dados["data_admissao"].ToString()),
                          Convert.ToChar(dados["status"].ToString())
                          );
                
                if (funcionario.Gravar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public bool Excluir(int cod_fun)
        {
            bool Transacao = true;
            GlobalExceptionHandler.ControlErrorMessage = "Erro ao Excluir";
            Models.Funcionario cliente = new Models.Funcionario().BuscarFuncionario(cod_fun);
            try
            {
                if(cliente != null)
                {
                    if(new Models.Usuario().BuscaUsuarioPorFuncionario(cod_fun) ==null)
                    {
                        //fazer de veiculo e venda
                        if (cliente.Excluir(Transacao))
                            return true;
                        else
                            return false;
                    }else
                    {
                        GlobalExceptionHandler.ControlErrorMessage = "Este Funcionario Possui dados relacionados a ele( como algum veiculo, venda ou usuario), por isso não pode ser deletado!";
                    }
                }
                
                return false;

            }
            catch (Exception e)
            {
                return false;
            }


        }

        public List<Models.Funcionario> ObterFuncionarios(string filtro)
        {
            return new Models.Funcionario().ObterFuncionarios(filtro);
        }

        public Models.Funcionario BuscarFuncionario(int cod_fun)
        {
            return new Models.Funcionario().BuscarFuncionario(cod_fun);
        }

        public bool AlterarFuncionario(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                dados["salario"] = dados["salario"].ToString().Replace(".", ",");

                Models.Funcionario funcionario = new Models.Funcionario(Convert.ToInt32(dados["cod_fun"].ToString()),
                          dados["nome"].ToString(),
                          dados["cpf"].ToString(),
                          dados["telefone"].ToString(),
                          Convert.ToDouble(dados["salario"].ToString()),
                          dados["email"].ToString(),
                          Convert.ToDateTime(dados["data_admissao"].ToString()),
                          Convert.ToChar(dados["status"].ToString())
                          );
                if (funcionario.Gravar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
