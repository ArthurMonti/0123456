using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EdgasSystem.Controls
{
    public class ClienteControl
    {

        public bool GravarCliente(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                dados["limite_fiado"] = dados["limite_fiado"].ToString().Replace(".", ",");
                if (dados["nome"].ToString().Length > 0)
                {
                    if(dados["cpf"].ToString().Length > 0 )
                    {
                        if (dados["fone"].ToString().Length > 0)
                        {
                            if (dados["endereco"].ToString().Length > 0 )
                            {
                                if (dados["bairro"].ToString().Length > 0)
                                {
                                    if (dados["cidade"].ToString().Length > 0)
                                    {
                                        if (dados["cep"].ToString().Length > 0)
                                        {
                                            Models.Client cliente = new Models.Client(0,
                                                      dados["nome"].ToString(),
                                                      dados["cpf"].ToString(),
                                                      dados["fone"].ToString(),
                                                      dados["endereco"].ToString(),
                                                      0, // numero
                                                      "", //Complemento
                                                      dados["bairro"].ToString(),
                                                      dados["cidade"].ToString(),
                                                      "", // estado
                                                      dados["cep"].ToString(),
                                                      Convert.ToDecimal(dados["limite_fiado"].ToString()),
                                                      Convert.ToDecimal(dados["limite_fiado"].ToString())
                                                      );
                                            if (cliente.Gravar(Transacao, true))
                                                return true;
                                            else
                                                return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
            

        }



        public bool Excluir(int cod_cliente)
        {
            bool Transacao = true;

            Models.Client cliente = new Models.Client().BuscarCliente(cod_cliente);
            try
            {
                if (cliente.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                return false;
            }
           
            
        }

        public List<Models.Client> ObterClientes(string filtro)
        {
            return new Models.Client().ObterClientes(filtro);
        }

        public Models.Client BuscarCliente(int cod_cliente)
        {
            return new Models.Client().BuscarCliente(cod_cliente);
        }

        public bool AlterarCliente(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {

                dados["limite_fiado"] = dados["limite_fiado"].ToString().Replace(".", ",");
                decimal saldo_fiado = Decimal.Parse(dados["limite_fiado"].ToString());
                if (dados["nome"].ToString().Length > 0)
                {
                    if (dados["cpf"].ToString().Length > 0)
                    {
                        if (dados["fone"].ToString().Length > 0)
                        {
                            if (dados["endereco"].ToString().Length > 0)
                            {
                                if (dados["bairro"].ToString().Length > 0)
                                {
                                    if (dados["cidade"].ToString().Length > 0)
                                    {
                                        if (dados["cep"].ToString().Length > 0)
                                        {
                                            if (dados["limite_fiado"].ToString().Length > 0)
                                            {
                                                
                                                if (dados["cod_cliente"].ToString() != "0")
                                                {
                                                    Models.Client fiado = new Models.Client().BuscarCliente(Convert.ToInt32(dados["cod_cliente"].ToString()));
                                                    saldo_fiado = (decimal)fiado.CreditBalance + (Convert.ToDecimal(dados["limite_fiado"].ToString()) - fiado.CreditLimit);
                                                    if (saldo_fiado < 0)
                                                        saldo_fiado = 0;
                                                }

                                                Models.Client cliente = new Models.Client(Convert.ToInt32(dados["cod_cliente"].ToString()),
                                                      dados["nome"].ToString(),
                                                      dados["cpf"].ToString(),
                                                      dados["fone"].ToString(),
                                                      dados["endereco"].ToString(),
                                                      0, // numero
                                                      "", //Complemento
                                                      dados["bairro"].ToString(),
                                                      dados["cidade"].ToString(),
                                                      "", // estado
                                                      dados["cep"].ToString(),
                                                      saldo_fiado,
                                                      Convert.ToDecimal(dados["limite_fiado"].ToString())
                                                      );
                                                if (cliente.Gravar(Transacao, true))
                                                    return true;
                                                else
                                                    return false;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    GlobalExceptionHandler.ControlErrorMessage = "tal tal";
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
