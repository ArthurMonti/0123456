using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class UsuarioDAL
    {
        private Banco b;

        internal UsuarioDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Usuario usuario, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@login", usuario.Login);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", usuario.Funcionario.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@pass", usuario.Password);
            b.getComandoSQL().Parameters.AddWithValue("@nivel", usuario.Nivel);

            b.getComandoSQL().CommandText = @"insert into usuario (login, cod_fun, pass, nivel) 
                                                            values (@login, @cod_fun, @pass, @nivel);";

            // b.getComandoSQL().Parameters.AddWithValue("@cod_cliente", usuario.Cod_cliente);
            //b.getComandoSQL().CommandText = @"UPDATE cliente set nome = @nome, cpf = @cpf, telefone = @telefone, endereco = @endereco, bairro = @bairro,
            //                                                       cidade = @cidade, cep = @cep, saldo_fiado = @saldo_fiado, limite_fiado = @limite_fiado  where cod_cliente = @cod_cliente;";
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

        internal bool Alterar(Models.Usuario usuario, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@login", usuario.Login);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", usuario.Funcionario.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@pass", usuario.Password);
            b.getComandoSQL().Parameters.AddWithValue("@nivel", usuario.Nivel);
            b.getComandoSQL().Parameters.AddWithValue("@login", usuario.Login);

            b.getComandoSQL().CommandText = @"UPDATE usuario set login = @login, cod_fun = @cod_fun, pass = @pass, nivel = @nivel
                                                                  where login = @login;";
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

        internal bool Excluir(string login, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@login", login);

            b.getComandoSQL().CommandText = @"delete from usuario 
                                              where login = @login";

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

        internal Models.Usuario BuscarLogin(string login)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@login", login);

            b.getComandoSQL().CommandText = @"select *
                                                from usuario 
                                                where login = @login";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Usuario usuario = null;

                usuario = new Models.Usuario(row["login"].ToString(), new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun"].ToString())),
                                             row["pass"].ToString(), Convert.ToInt32(row["nivel"].ToString()));


                return usuario;
            }
            else
                return null;
        }

        internal List<Models.Usuario> ObterUsuarios(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from usuario";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Usuario> dados = new List<Models.Usuario>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Usuario(row["login"].ToString(), new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun"].ToString())),
                                             row["pass"].ToString(), Convert.ToInt32(row["nivel"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }

        internal bool ValidaLogin(string login, string pass)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@login", login);
            b.getComandoSQL().Parameters.AddWithValue("@pass", pass);

            b.getComandoSQL().CommandText = @"select *
                                                from usuario 
                                                where login = @login and pass = @pass";



            DataTable dt = b.ExecutaSelect(true);

            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        internal Models.Usuario BuscaUsuarioPorFuncionario(int cod_fun)
        {

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", cod_fun);

            b.getComandoSQL().CommandText = @"select *
                                                from usuario 
                                                where cod_fun = @cod_fun";

            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Usuario usuario = null;

                usuario = new Models.Usuario(row["login"].ToString(), new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun"].ToString())),
                                             row["pass"].ToString(), Convert.ToInt32(row["nivel"].ToString()));


                return usuario;
            }
            else
                return null;
        }


    }
}
   