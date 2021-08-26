using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Caixa
    {
        
        public int Codigo { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoAbertura { get; set; }
        public char Status { get; set; }
        public DateTime Abertura { get; set; }
        public Funcionario Funcionario_abriu { get; set; }
        public Funcionario? Funcionario_fechou { get; set; }

        public Caixa()
        {

        }


        public Caixa(int codigo, decimal saldo, decimal saldoAbertura, char status, DateTime abertura, Funcionario funcionario_abriu, Funcionario funcionario_fechou)
        {
            Codigo = codigo;
            Saldo = saldo;
            SaldoAbertura = saldoAbertura;
            Status = status;
            Abertura = abertura;
            Funcionario_abriu = funcionario_abriu;
            Funcionario_fechou = funcionario_fechou;
        }


        public bool Abrir(bool Transacao, bool Parada)
        {
            return new DAL.CaixaDAL().Abrir(this, Transacao, Parada);
        }

        public bool Fechar(bool Transacao, bool Parada)
        {
            return new DAL.CaixaDAL().Fechar(this, Transacao, Parada);
        }

        public Models.Caixa BuscarCaixaAbertoporCodigo(int num_caixa)
        {
            return new DAL.CaixaDAL().BuscarCaixaAbertoporCodigo(num_caixa);
        }

        public Models.Caixa BuscarCaixa(int num_caixa, string abertura)
        {
            return new DAL.CaixaDAL().BuscaCaixa(num_caixa, abertura);
        }
        public Models.Caixa BuscarCaixaAberto(int num_caixa, string abertura)
        {
            return new DAL.CaixaDAL().BuscaCaixaAberto(num_caixa, abertura);
        }
        public List<Models.Caixa> ObterClientes()
        {
            return new DAL.CaixaDAL().ObterCaixasPorCodigo();
        }

        public List<Models.Caixa> ObterCaixasAbertosDESC()
        {
            return new DAL.CaixaDAL().ObterCaixasAbertosDESC();
        }

        public List<Models.Caixa> ObterCaixasFechadosDESC()
        {
            return new DAL.CaixaDAL().ObterCaixasFechadosDESC();
        }

        public List<Models.Caixa> ObterCaixasAbertosporData(string abertura)
        {
            return new DAL.CaixaDAL().ObterCaixasAbertosporData(abertura);
        }
        
    }
}
