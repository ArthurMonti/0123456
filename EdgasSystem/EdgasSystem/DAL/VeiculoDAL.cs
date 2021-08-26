using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class VeiculoDAL
    {

        private Banco b;

        internal VeiculoDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Veiculo veiculo, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@placa", veiculo.Placa);
            b.getComandoSQL().Parameters.AddWithValue("@quilometragem", veiculo.Quilometragem);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", veiculo.Funcionario.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@status", veiculo.Status);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", veiculo.Descricao);

            b.getComandoSQL().CommandText = @"insert into veiculo (placa,quilometragem, cod_fun, status, descricao) 
                                                           values (@placa,@quilometragem, @cod_fun, @status, @descricao)";

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

        internal bool Alterar(Models.Veiculo veiculo, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@quilometragem", veiculo.Quilometragem);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun", veiculo.Funcionario.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@status", veiculo.Status);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", veiculo.Descricao);
            b.getComandoSQL().Parameters.AddWithValue("@placa", veiculo.Placa);
            
            b.getComandoSQL().CommandText = @"UPDATE veiculo set quilometragem = @quilometragem, cod_fun = @cod_fun, status = @status, descricao = @descricao
                                                                     where placa = @placa";


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

        internal bool Excluir(string placa, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@placa", placa);

            b.getComandoSQL().CommandText = @"delete from veiculo 
                                              where placa = @placa";

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


        internal Models.Veiculo BuscarVeiculo(string placa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@placa", placa);

            b.getComandoSQL().CommandText = @"select *
                                                from veiculo 
                                                where placa = @placa";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                DataRow row = dt.Rows[0];


                return new Models.Veiculo(row["placa"].ToString(), Convert.ToDouble(row["quilometragem"].ToString()), new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun"].ToString())),
                                          Convert.ToChar(row["status"].ToString()), row["descricao"].ToString()
                                            );
                
            }
            else
                return null;
        }

        internal List<Models.Veiculo> ObterVeiculos(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from veiculo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Veiculo> dados = new List<Models.Veiculo>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Veiculo(row["placa"].ToString(), Convert.ToDouble(row["quilometragem"].ToString()), new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun"].ToString())),
                                          Convert.ToChar(row["status"].ToString()), row["descricao"].ToString()
                                            ));
                }
                return dados;
            }
            else
                return null;
        }
    }
}
