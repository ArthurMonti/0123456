using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controls
{
    public class UsuarioControl
    {
        public bool GravarUsuario(Dictionary<string, object> dados)
        {
            GlobalExceptionHandler.ControlErrorMessage = "Erro ao Gravar!";


            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                if(new Models.Usuario().BuscarUsuario(dados["login"].ToString()) == null)
                {
                    Models.Usuario usuario = new Models.Usuario(dados["login"].ToString(),
                          new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["funcionario"].ToString())),
                          dados["senha"].ToString(),
                          Convert.ToInt32(dados["nivel"].ToString())
                          );
                    if (usuario.Gravar(Transacao, true))
                        return true;
                    else
                        return false;
                }else
                {
                    GlobalExceptionHandler.ControlErrorMessage = "Nome de Usuário já existente!";
                    return false;
                }

                
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AlterarUsuario(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Usuario usuario = new Models.Usuario(dados["login"].ToString(),
                         new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["cod_fun"].ToString())),
                         dados["pass"].ToString(),
                         Convert.ToInt32(dados["nivel"].ToString())
                         );
                if (usuario.Alterar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirUsuario(string login)
        {
            bool Transacao = true;

            Models.Usuario usuario = new Models.Usuario().BuscarUsuario(login);
            try
            {
                if (usuario.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Models.Usuario> ObterUsuario(string filtro)
        {
            return new Models.Usuario().ObterUsuarios(filtro);
        }

        public Models.Usuario BuscarUsuario(string login)
        {
            return new Models.Usuario().BuscarUsuario(login);
        }


        public bool ValidaLogin(string login,string password)
        {
            return new Models.Usuario().ValidaLogin(login, password);
        }



    }
}
