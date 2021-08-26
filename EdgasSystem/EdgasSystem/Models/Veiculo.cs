using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Veiculo
    {
        private string placa;
        private double quilometragem;
        private Funcionario funcionario;
        private char status;
        private string descricao;

        public Veiculo()
        { }

        public Veiculo(string placa, double quilometragem, Funcionario funcionario, char status, string descricao)
        {
            Placa = placa;
            Quilometragem = quilometragem;
            Funcionario = funcionario;
            Status = status;
            Descricao = descricao;
        }

        public string Placa { get => placa; set => placa = value; }
        public double Quilometragem { get => quilometragem; set => quilometragem = value; }
        public Funcionario Funcionario { get => funcionario; set => funcionario = value; }
        public char Status { get => status; set => status = value; }
        public string Descricao { get => descricao; set => descricao = value; }


        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.VeiculoDAL().Gravar(this, Transacao, Parada);
        }
        public bool Alterar(bool Transacao, bool Parada)
        {
            return new DAL.VeiculoDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.VeiculoDAL().Excluir(this.Placa, Transacao);
        }

        public Models.Veiculo BuscarVeiculo(string placa)
        {
            return new DAL.VeiculoDAL().BuscarVeiculo(placa);
        }


        public List<Models.Veiculo> ObterVeiculos(string filtro)
        {
            return new DAL.VeiculoDAL().ObterVeiculos(filtro);
        }
    }
}
