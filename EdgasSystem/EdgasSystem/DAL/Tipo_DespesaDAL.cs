using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class Tipo_DespesaDAL
    {
        private Banco b;

        internal Tipo_DespesaDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Tipo_Despesa tipo, bool Transacao, bool Parada)
        {
            int codigo;
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@nome", tipo.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", tipo.Descricao);

            if (tipo.Cod_tipo_despesa == 0)
                b.getComandoSQL().CommandText = @"insert into Tipo_Despesa (nome, descricao) 
                                                                      values (@nome, @descricao);";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_des", tipo.Cod_tipo_despesa);
                b.getComandoSQL().CommandText = @"UPDATE Tipo_Despesa set nome = @nome, descricao = @descricao where cod_tipo_des = @cod_tipo_des;";
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



        internal bool Excluir(int cod_tipo_des, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_des", cod_tipo_des);

            b.getComandoSQL().CommandText = @"delete from tipo_despesa 
                                              where cod_tipo_des = @cod_tipo_des";

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

        internal Models.Tipo_Despesa BuscarTipo_Despesa(int cod_tipo_descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_tipo_des", cod_tipo_descricao);

            b.getComandoSQL().CommandText = @"select *
                                                from Tipo_Despesa 
                                                where cod_tipo_des = @cod_tipo_des";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Tipo_Despesa tipo_des = null;

                tipo_des = new Models.Tipo_Despesa(Convert.ToInt32(row["cod_tipo_des"].ToString()), row["nome"].ToString(), row["descricao"].ToString());

                return tipo_des;
            }
            else
                return null;
        }

        internal List<Models.Tipo_Despesa> ObterTipo_Despesas(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from Tipo_Despesa";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Tipo_Despesa> dados = new List<Models.Tipo_Despesa>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Tipo_Despesa(Convert.ToInt32(row["cod_tipo_des"].ToString()), row["nome"].ToString(), row["descricao"].ToString()));
                }
                return dados;
            }
            else
                return null;
        }
    }

    

}
