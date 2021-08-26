using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EdgasSystem.Controls
{
    public class CaixaControl
    {

        public bool AbrirCaixa(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            GlobalExceptionHandler.ControlErrorMessage = "Erro ao gravar!";
            try
            {
                if (new Models.Caixa().BuscarCaixaAberto(Convert.ToInt32(dados["codigo"].ToString()), DateTime.Today.ToString()) == null){ 
                    if (new Models.Caixa().BuscarCaixaAbertoporCodigo(Convert.ToInt32(dados["codigo"].ToString())) == null)
                    {
                        Models.Caixa caixa = new Models.Caixa(Convert.ToInt32(dados["codigo"].ToString()),
                               Convert.ToDecimal(dados["saldo"].ToString()),
                               Convert.ToDecimal(dados["saldo"].ToString()),
                               'A',
                               DateTime.Today,
                               new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["cod_fun"].ToString())),
                               null
                               );
                        if (caixa.Abrir(Transacao, true))
                            return true;
                        else
                            return false;
                    }else
                    {
                        GlobalExceptionHandler.ControlErrorMessage = "Este caixa se encontra aberto em relação a outro dia, por favor feche-o!";
                        return false;
                    }
                }
                else
                {
                    ;
                    return false;
                }
                
           
            }
            catch(Exception e)
            {
                GlobalExceptionHandler.ControlErrorMessage = "Erro no try";
                return false;
            }
            

        }

        public bool FecharCaixa(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                    Models.Caixa caixa = new Models.Caixa(Convert.ToInt32(dados["codigo"].ToString()),
                           Convert.ToDecimal(dados["saldo"].ToString().Replace(".",",")),
                           Convert.ToDecimal(dados["saldo"].ToString().Replace(".", ",")),
                           'F',
                           Convert.ToDateTime(dados["data"].ToString()),
                           null,
                           new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["cod_fun"].ToString()))
                           );

                if (caixa.Fechar(Transacao, true))
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
