using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Fornecedor
    {
        private int cod_fornecedor;
        private string cnpj;
        private string nome;
        private string endereco;
        private string cidade;
        private string cep;
        private string telefone;
        private string contato;
        private string telefone_contato;

        public Fornecedor()
        { }

        public Fornecedor(int cod_fornecedor, string cnpj, string nome, string endereco, string cidade, string cep, string telefone)
        {
            this.Cod_fornecedor = cod_fornecedor;
            this.Cnpj = cnpj;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Cidade = cidade;
            this.Cep = cep;
            this.Telefone = telefone;
        }

        public Fornecedor(int cod_fornecedor, string cnpj, string nome,  string endereco, string cidade, string cep, string telefone, string contato, string telefone_contato)
        {
            this.Cod_fornecedor = cod_fornecedor;
            this.Cnpj = cnpj;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Cidade = cidade;
            this.Cep = cep;
            this.Telefone = telefone;
            this.Contato = contato;
            this.Telefone_contato = telefone_contato;
        }

        public int Cod_fornecedor { get => cod_fornecedor; set => cod_fornecedor = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Contato { get => contato; set => contato = value; }
        public string Telefone_contato { get => telefone_contato; set => telefone_contato = value; }


        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.FornecedorDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.FornecedorDAL().Excluir(this.cod_fornecedor, Transacao);
        }

        public Models.Fornecedor BuscarFornecedor(int cod_fornecedor)
        {
            return new DAL.FornecedorDAL().BuscarFornecedor(cod_fornecedor);
        }

        public List<Models.Fornecedor> ObterFornecedores(string filtro)
        {
            return new DAL.FornecedorDAL().ObterFornecedores(filtro);
        }
    }
}
