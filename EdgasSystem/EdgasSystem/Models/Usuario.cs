using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Usuario
    {
        private string login;
        private Funcionario funcionario;
        private string password;
        private int nivel;

        public Usuario()
        {

        }

        public Usuario(string login, Funcionario funcionario, string password, int nivel)
        {
            this.Login = login;
            this.Funcionario = funcionario;
            this.Password = password;
            this.Nivel = nivel;
        }

        public string Login { get => login; set => login = value; }
        public Funcionario Funcionario { get => funcionario; set => funcionario = value; }
        public string Password { get => password; set => password = value; }
        public int Nivel { get => nivel; set => nivel = value; }



        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.UsuarioDAL().Gravar(this, Transacao, Parada);
        }
        public bool Alterar(bool Transacao, bool Parada)
        {
            return new DAL.UsuarioDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.UsuarioDAL().Excluir(this.login, Transacao);
        }

        public Models.Usuario BuscarUsuario(string login)
        {
            return new DAL.UsuarioDAL().BuscarLogin(login);
        }


        public List<Models.Usuario> ObterUsuarios(string filtro)
        {
            return new DAL.UsuarioDAL().ObterUsuarios(filtro);
        }

        public bool ValidaLogin(string login,string pass)
        {
            return new DAL.UsuarioDAL().ValidaLogin(login,pass);
        }

        public Models.Usuario BuscaUsuarioPorFuncionario(int cod_fun)
        {
            return new DAL.UsuarioDAL().BuscaUsuarioPorFuncionario(cod_fun);
        }
    }


    
}
