using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class ClienteDAL
    {
        private Banco b;

        internal ClienteDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Client cliente, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@nome", cliente.Name);
            b.getComandoSQL().Parameters.AddWithValue("@cpf", cliente.Cpf);
            b.getComandoSQL().Parameters.AddWithValue("@telefone", cliente.Fone);
            b.getComandoSQL().Parameters.AddWithValue("@endereco", cliente.Address);
            b.getComandoSQL().Parameters.AddWithValue("@bairro", cliente.District);
            b.getComandoSQL().Parameters.AddWithValue("@cidade", cliente.City);
            b.getComandoSQL().Parameters.AddWithValue("@cep", cliente.ZipCode);
            b.getComandoSQL().Parameters.AddWithValue("@saldo_fiado", cliente.CreditBalance);
            b.getComandoSQL().Parameters.AddWithValue("@limite_fiado", cliente.CreditLimit);

            if (cliente.Id == 0)
                b.getComandoSQL().CommandText = @"insert into cliente (nome, cpf, telefone, endereco, bairro, cidade, cep, saldo_fiado, limite_fiado) 
                                                                      values (@nome, @cpf, @telefone, @endereco, @bairro, @cidade, @cep, @saldo_fiado, @limite_fiado);";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_cliente", cliente.Id);
                b.getComandoSQL().CommandText = @"UPDATE cliente set nome = @nome, cpf = @cpf, telefone = @telefone, endereco = @endereco, bairro = @bairro,
                                                                     cidade = @cidade, cep = @cep, saldo_fiado = @saldo_fiado, limite_fiado = @limite_fiado  where cod_cliente = @cod_cliente;";
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

        internal bool Excluir(int cod_cliente, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_cliente", cod_cliente);

            b.getComandoSQL().CommandText = @"delete from cliente 
                                              where cod_cliente = @cod_cliente";

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

        internal Models.Client BuscarCliente(int cod_cliente)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_cliente", cod_cliente);

            b.getComandoSQL().CommandText = @"select *
                                                from cliente 
                                                where cliente.cod_cliente = @cod_cliente";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Client cliente = null;
                
                cliente = new Models.Client(Convert.ToInt32(row["cod_cliente"].ToString()), row["nome"].ToString(), row["cpf"].ToString(), row["telefone"].ToString(),
                                            row["endereco"].ToString(), 0, "", row["bairro"].ToString(), row["cidade"].ToString(), "", row["cep"].ToString(),
                                            Convert.ToDecimal(row["saldo_fiado"].ToString()), Convert.ToDecimal(row["limite_fiado"].ToString()));
                
                return cliente;
            }
            else
                return null;
        }

        internal List<Models.Client> ObterClientes(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();
            

            b.getComandoSQL().CommandText = @"select *
                                              from cliente";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Client> dados = new List<Models.Client>();
                

                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Client(Convert.ToInt32(row["cod_cliente"].ToString()), row["nome"].ToString(), row["cpf"].ToString(), row["telefone"].ToString(),
                                            row["endereco"].ToString(), 0,"",row["bairro"].ToString(), row["cidade"].ToString(),"", row["cep"].ToString(),
                                            Convert.ToDecimal(row["saldo_fiado"].ToString()), Convert.ToDecimal(row["limite_fiado"].ToString())));
                }
                return dados;
            }
            else
                return null;
        }
    }
}
   