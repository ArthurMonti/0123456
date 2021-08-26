using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class FuncionarioDAL
    {
        private Banco b;

        internal FuncionarioDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Funcionario funcionario, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@nome", funcionario.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@cpf", funcionario.Cpf);
            b.getComandoSQL().Parameters.AddWithValue("@telefone", funcionario.Telefone);
            b.getComandoSQL().Parameters.AddWithValue("@salario", funcionario.Salario);
            b.getComandoSQL().Parameters.AddWithValue("@email", funcionario.Email);
            b.getComandoSQL().Parameters.AddWithValue("@data_admissao", funcionario.Data_admissao);
            b.getComandoSQL().Parameters.AddWithValue("@status", funcionario.Status);

            if (funcionario.Cod_fun == 0)
                b.getComandoSQL().CommandText = @"insert into funcionario (nome, cpf, telefone, salario, email, data_admissao, status) 
                                                                      values (@nome, @cpf, @telefone, @salario, @email, @data_admissao, @status);";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_fun", funcionario.Cod_fun);
                b.getComandoSQL().CommandText = @"UPDATE funcionario set nome = @nome, cpf = @cpf, telefone = @telefone, salario = @salario, email = @email,
                                                                     data_admissao = @data_admissao, status = @status  where cod_fun = @cod_fun;";
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

        internal bool Excluir(int cod_fun, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", cod_fun);

            b.getComandoSQL().CommandText = @"delete from funcionario 
                                              where cod_fun = @cod_fun";

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

        internal Models.Funcionario BuscarFuncionario(int cod_fun)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", cod_fun);

            b.getComandoSQL().CommandText = @"select *
                                                from funcionario 
                                                where cod_fun = @cod_fun";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Funcionario funcionario = null;

                funcionario = new Models.Funcionario(Convert.ToInt32(row["cod_fun"].ToString()), row["nome"].ToString(), row["cpf"].ToString(), row["telefone"].ToString(),
                                            Convert.ToDouble(row["salario"].ToString()), row["email"].ToString(), Convert.ToDateTime(row["data_admissao"].ToString()), Convert.ToChar(row["status"].ToString()));
                return funcionario;
            }
            else
                return null;
        }

        internal List<Models.Funcionario> ObterFuncionarios(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from funcionario";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Funcionario> dados = new List<Models.Funcionario>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Funcionario(Convert.ToInt32(row["cod_fun"].ToString()), row["nome"].ToString(), row["cpf"].ToString(), row["telefone"].ToString(),
                                            Convert.ToDouble(row["salario"].ToString()), row["email"].ToString(), Convert.ToDateTime(row["data_admissao"].ToString()), Convert.ToChar(row["status"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Funcionario> ObterFuncionariosSemUsuario()
        {
            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @" select * from funcionario where status <> 'D' and cod_fun not in (select cod_fun from usuario)";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Funcionario> dados = new List<Models.Funcionario>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Funcionario(Convert.ToInt32(row["cod_fun"].ToString()), row["nome"].ToString(), row["cpf"].ToString(), row["telefone"].ToString(),
                                            Convert.ToDouble(row["salario"].ToString()), row["email"].ToString(), Convert.ToDateTime(row["data_admissao"].ToString()), Convert.ToChar(row["status"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }
        
    }
}
