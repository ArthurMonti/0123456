using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class CaixaDAL
    {
        private Banco b;

        internal CaixaDAL()
        {
            b = Banco.GetInstance();
        }

        internal bool Abrir(Models.Caixa caixa, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", caixa.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@saldo", caixa.Saldo);
            b.getComandoSQL().Parameters.AddWithValue("@status", caixa.Status);
            b.getComandoSQL().Parameters.AddWithValue("@valor_abertura", caixa.SaldoAbertura);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun_abertura", caixa.Funcionario_abriu.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@data_abertura", caixa.Abertura);
            b.getComandoSQL().CommandText = @"insert into caixa (cod_caixa, saldo, status, valor_abertura, cod_fun_abertura, data_abertura)
                                                                      values (@cod_caixa,@saldo, @status, @valor_abertura, @cod_fun_abertura, @data_abertura);";

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

        internal bool Fechar(Models.Caixa caixa, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            
            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", caixa.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@saldo", caixa.Saldo);
            b.getComandoSQL().Parameters.AddWithValue("@status", caixa.Status);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun_fechamento", caixa.Funcionario_fechou.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@data_abertura", caixa.Abertura);

            b.getComandoSQL().CommandText = @"update caixa set saldo = @saldo,status = @status, cod_fun_fechamento = @cod_fun_fechamento
                                                                      where cod_caixa = @cod_caixa and data_abertura = @data_abertura; ";

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

        internal bool ReAbrir(Models.Caixa caixa, bool Transacao, bool Parada)
        {
            b.getComandoSQL().Parameters.Clear();
            //Coloca os ma
            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", caixa.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@saldo", caixa.Saldo);
            b.getComandoSQL().Parameters.AddWithValue("@status", caixa.Status);
            b.getComandoSQL().Parameters.AddWithValue("@valor_abertura", caixa.SaldoAbertura);
            b.getComandoSQL().Parameters.AddWithValue("@cod_fun_abertura", caixa.Funcionario_abriu.Cod_fun);
            b.getComandoSQL().Parameters.AddWithValue("@data_abertura", caixa.Abertura);
            b.getComandoSQL().CommandText = @"update caixa set caixa (cod_caixa, saldo, status, valor_abertura, cod_fun_abertura, data_abertura)
                                                                      values (@cod_caixa,@saldo, @status, @valor_abertura, @cod_fun_abertura, @data_abertura);";

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



        internal Models.Caixa BuscaCaixa(int cod_caixa, string abertura)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", cod_caixa);
            b.getComandoSQL().Parameters.AddWithValue("@data_abertura", abertura);


            b.getComandoSQL().CommandText = @"select *
                                                from caixa 
                                                where cod_caixa = @cod_caixa and data_abertura = @data_abertura";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Caixa caixa = null;
                int cod_fun_fechamento = 0;
                if (row["cod_fun_fechamento"].ToString().Length > 0)
                {
                    cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                }
                caixa = new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento));
                return caixa;
            }
            else
                return null;
        }

        internal Models.Caixa BuscaCaixaAberto(int cod_caixa, string abertura)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", cod_caixa);
            b.getComandoSQL().Parameters.AddWithValue("@data_abertura", abertura);


            b.getComandoSQL().CommandText = @"select *
                                                from caixa 
                                                where cod_caixa = @cod_caixa and data_abertura = @data_abertura and status = 'A'";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Caixa caixa = null;
                int cod_fun_fechamento = 0;
                if (row["cod_fun_fechamento"].ToString().Length > 0)
                {
                    cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                }
                caixa = new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento));
                return caixa;
            }
            else
                return null;
        }

        internal Models.Caixa BuscarCaixaAbertoporCodigo(int cod_caixa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@cod_caixa", cod_caixa);


            b.getComandoSQL().CommandText = @"select *
                                                from caixa 
                                                where cod_caixa = @cod_caixa and status='A'";



            DataTable dt = b.ExecutaSelect(true);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Caixa caixa = null;
                int cod_fun_fechamento = 0;
                if (row["cod_fun_fechamento"].ToString().Length > 0)
                {
                    cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                }
                caixa = new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento));
                return caixa;
            }
            else
                return null;
        }




        internal List<Models.Caixa> ObterCaixasPorCodigo()
        {

            b.getComandoSQL().Parameters.Clear();
            

            b.getComandoSQL().CommandText = @"select distinct cod_caixa, *
                                              from caixa";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Caixa> dados = new List<Models.Caixa>();
                

                foreach (DataRow row in dt.Rows)
                {
                    int cod_fun_fechamento = 0;
                    if (row["cod_fun_fechamento"].ToString().Length > 0)
                    {
                        cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                    }
                    dados.Add(new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento)));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Caixa> ObterCaixasAbertosDESC()
        {

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select *
                                              from caixa where status = 'A' order by data_abertura DESC";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Caixa> dados = new List<Models.Caixa>();


                foreach (DataRow row in dt.Rows)
                {
                    int cod_fun_fechamento = 0;
                    if(row["cod_fun_fechamento"].ToString().Length>0)
                    {
                        cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                    }
                    dados.Add(new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento)));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Caixa> ObterCaixasFechadosDESC()
        {
            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from caixa where status = 'F' order by data_abertura DESC";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Caixa> dados = new List<Models.Caixa>();


                foreach (DataRow row in dt.Rows)
                {
                    dados.Add(new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_fechamento"].ToString()))));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Caixa> ObterCaixasAbertos()
        {
            b.getComandoSQL().Parameters.Clear();


            b.getComandoSQL().CommandText = @"select *
                                              from caixa where status = 'A'";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Caixa> dados = new List<Models.Caixa>();


                foreach (DataRow row in dt.Rows)
                {
                    int cod_fun_fechamento = 0;
                    if (row["cod_fun_fechamento"].ToString().Length > 0)
                    {
                        cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                    }
                    dados.Add(new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento)));
                }
                return dados;
            }
            else
                return null;
        }

        internal List<Models.Caixa> ObterCaixasAbertosporData(string abertura)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@data_abertura",abertura);

            b.getComandoSQL().CommandText = @"select *
                                              from caixa where status = 'A' and data_abertura = @data_abertura";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Models.Caixa> dados = new List<Models.Caixa>();


                foreach (DataRow row in dt.Rows)
                {
                    int cod_fun_fechamento = 0;
                    if (row["cod_fun_fechamento"].ToString().Length > 0)
                    {
                        cod_fun_fechamento = Convert.ToInt32(row["cod_fun_fechamento"].ToString());
                    }
                    dados.Add(new Models.Caixa(Convert.ToInt32(row["cod_caixa"].ToString()), Convert.ToDecimal(row["saldo"].ToString()), Convert.ToDecimal(row["valor_abertura"].ToString()),
                                            Convert.ToChar(row["status"].ToString()), Convert.ToDateTime(row["data_abertura"].ToString()),
                                            new FuncionarioDAL().BuscarFuncionario(Convert.ToInt32(row["cod_fun_abertura"].ToString())),
                                            new FuncionarioDAL().BuscarFuncionario(cod_fun_fechamento)));
                }
                return dados;
            }
            else
                return null;
        }
    }
}
   