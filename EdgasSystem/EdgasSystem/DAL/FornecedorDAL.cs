using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class FornecedorDAL
    {
        private Banco b;

        internal FornecedorDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Gravar(Models.Fornecedor fornecedor, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
            b.getComandoSQL().Parameters.AddWithValue("@nome", fornecedor.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@endereco", fornecedor.Endereco);
            b.getComandoSQL().Parameters.AddWithValue("@cidade", fornecedor.Cidade);
            b.getComandoSQL().Parameters.AddWithValue("@cep", fornecedor.Cep);
            b.getComandoSQL().Parameters.AddWithValue("@telefone", fornecedor.Telefone);

            b.getComandoSQL().Parameters.AddWithValue("@contato", fornecedor.Contato);
            b.getComandoSQL().Parameters.AddWithValue("@telefone_contato", fornecedor.Telefone_contato);
            Debug.Write("entrei");

            if (fornecedor.Cod_fornecedor == 0)
                b.getComandoSQL().CommandText = @"insert into fornecedor (cnpj, nome, endereco, cidade, cep, telefone, contato, telefone_contato) 
                                                                      values (@cnpj, @nome, @endereco, @cidade, @cep, @telefone, @contato, @telefone_contato);";
            else
            {
                b.getComandoSQL().Parameters.AddWithValue("@cod_fornecedor", fornecedor.Cod_fornecedor);
                b.getComandoSQL().CommandText = @"UPDATE fornecedor set cnpj = @cnpj, nome = @nome, endereco = @endereco, cidade = @cidade, cep = @cep,
                                                                     telefone = @telefone, contato = @contato, telefone_contato = @telefone_contato  where cod_fornecedor = @cod_fornecedor;";
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

        internal bool Excluir(int cod_fornecedor, bool Transacao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_fornecedor", cod_fornecedor);

            b.getComandoSQL().CommandText = @"delete from fornecedor 
                                              where cod_fornecedor = @cod_fornecedor";

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

        internal Models.Fornecedor BuscarFornecedor(int cod_fornecedor)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_fornecedor", cod_fornecedor);

            b.getComandoSQL().CommandText = @"select *
                                                from fornecedor 
                                                where cod_fornecedor = @cod_fornecedor";


            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Models.Fornecedor(Convert.ToInt32(row["cod_fornecedor"].ToString()), row["cnpj"].ToString(), row["nome"].ToString(), row["endereco"].ToString(), row["cidade"].ToString(),
                                            row["cep"].ToString(), row["telefone"].ToString(), row["contato"].ToString(), row["telefone_contato"].ToString());
            }
            else
                return null;
        }

        internal List<Models.Fornecedor> ObterFornecedores(string filtro)
        {

            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from fornecedor";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Fornecedor> dados = new List<Models.Fornecedor>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Fornecedor(Convert.ToInt32(row["cod_fornecedor"].ToString()), row["cnpj"].ToString(), row["nome"].ToString(), row["endereco"].ToString(), row["cidade"].ToString(),
                                            row["cep"].ToString(), row["telefone"].ToString(), row["contato"].ToString(), row["telefone_contato"].ToString()));
                }
                return dados;
            }
            else
                return null;
        }
    }
}
