using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Funcionario
    {
        private int cod_fun;
        private string nome;
        private string cpf;
        private string telefone;
        private double salario;
        private string email;
        private DateTime data_admissao;
        private char status;

        public Funcionario()
        {
        }

        public Funcionario(int cod_fun, string nome, string cpf, string telefone, double salario, string email, DateTime data_admissao, char status)
        {
            Cod_fun = cod_fun;
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Salario = salario;
            Email = email;
            Data_admissao = data_admissao;
            Status = status;
        }

        public int Cod_fun { get => cod_fun; set => cod_fun = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public double Salario { get => salario; set => salario = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Data_admissao { get => data_admissao; set => data_admissao = value; }
        public char Status { get => status; set => status = value; }


        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.FuncionarioDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.FuncionarioDAL().Excluir(this.Cod_fun, Transacao);
        }

        public Models.Funcionario BuscarFuncionario(int cod_fun)
        {
            return new DAL.FuncionarioDAL().BuscarFuncionario(cod_fun);
        }


        public List<Models.Funcionario> ObterFuncionarios(string filtro)
        {
            return new DAL.FuncionarioDAL().ObterFuncionarios(filtro);
        }

        public List<Models.Funcionario> ObterFuncionariosSemUsuario()
        {
            return new DAL.FuncionarioDAL().ObterFuncionariosSemUsuario();
        }


    }
}
