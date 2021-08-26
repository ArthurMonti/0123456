using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Models
{
    public class Tipo_Despesa
    {
        private int cod_tipo_despesa;
        private string nome;
        private string descricao;

        public Tipo_Despesa()
        {
        }

        public Tipo_Despesa(int cod_tipo_despesa, string nome, string descricao)
        {
            this.Cod_tipo_despesa = cod_tipo_despesa;
            this.Nome = nome;
            this.Descricao = descricao;
        }

        public int Cod_tipo_despesa { get => cod_tipo_despesa; set => cod_tipo_despesa = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }

        public bool Gravar(bool Transacao, bool Parada)
        {
            return new DAL.Tipo_DespesaDAL().Gravar(this, Transacao, Parada);
        }

        public bool Excluir(bool Transacao)
        {
            return new DAL.Tipo_DespesaDAL().Excluir(this.Cod_tipo_despesa, Transacao);
        }

        public Models.Tipo_Despesa BuscarTipo_Despesa(int cod_tipo_des)
        {
            return new DAL.Tipo_DespesaDAL().BuscarTipo_Despesa(cod_tipo_des);
        }


        public List<Models.Tipo_Despesa> ObterTipo_Despesas(string filtro)
        {
            return new DAL.Tipo_DespesaDAL().ObterTipo_Despesas(filtro);
        }


    }
}
