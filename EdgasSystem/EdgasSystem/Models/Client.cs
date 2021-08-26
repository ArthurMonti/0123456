using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Fone { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal? CreditBalance { get; set; }
        public decimal CreditLimit { get; set; }


        private int cod_cliente;
       
        

        public Client()
        {

        }

        public Client(int id, string name, string cpf, string fone, string address, int adressNumber, string complement,
                       string district, string city, string state, string zipCode, decimal creditLimit)
        {
            this.Id = id;
            this.Name = name;
            this.Cpf = cpf;
            this.Fone = fone;
            this.Address = address;
            this.AddressNumber = adressNumber;
            this.Complement = complement;
            this.District = district;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.CreditLimit = creditLimit;
        }


        public Client(int id, string name, string cpf, string fone, string address, int adressNumber, string complement,
                       string district, string city, string state, string zipCode, decimal? creditBalance, decimal creditLimit)
        {
            this.Id = id;
            this.Name = name;
            this.Cpf = cpf;
            this.Fone = fone;
            this.Address = address;
            this.AddressNumber = adressNumber;
            this.Complement = complement;
            this.District = district;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.CreditBalance = creditBalance;
            this.CreditLimit = creditLimit;
        }

        

        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.ClienteDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.ClienteDAL().Excluir(this.Id, Transacao);
        }

        public Models.Client BuscarCliente(int cod_cliente)
        {
            return new DAL.ClienteDAL().BuscarCliente(cod_cliente);
        }
        

        public List<Models.Client> ObterClientes(string filtro)
        {
            return new DAL.ClienteDAL().ObterClientes(filtro);
        }




    }
}
