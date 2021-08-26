using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controls
{
    public class VeiculoControl
    {
        public bool GravarVeiculo(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            GlobalExceptionHandler.ControlErrorMessage = "Erro ao Gravar!";
            try
            {   Models.Veiculo temp = new Models.Veiculo().BuscarVeiculo(dados["placa"].ToString());
                Debug.Write(temp + "\n");
                if(temp==null)
                {
                    Models.Veiculo veiculo = new Models.Veiculo(dados["placa"].ToString(),
                         Convert.ToDouble(dados["quilometragem"].ToString()),
                         new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["cod_fun"].ToString())),
                         Convert.ToChar(dados["status"].ToString()),
                         dados["descricao"].ToString()
                         );

                    if (veiculo.Gravar(Transacao, true))
                        return true;
                    else

                        return false;
                }
                else
                {
                    GlobalExceptionHandler.ControlErrorMessage = "Veiculo já Cadastrado!";
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AlterarVeiculo(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Veiculo veiculo = new Models.Veiculo(dados["placa"].ToString(),
                          Convert.ToDouble(dados["quilometragem"].ToString()),
                          new Models.Funcionario().BuscarFuncionario(Convert.ToInt32(dados["cod_fun"].ToString())),
                          Convert.ToChar(dados["status"].ToString()),
                          dados["descricao"].ToString()
                          
                          );
                if (veiculo.Alterar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirVeiculo(string placa)
        {
            bool Transacao = true;

            Models.Veiculo veiculo = new Models.Veiculo().BuscarVeiculo(placa);
            try
            {
                if (veiculo.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Models.Veiculo> ObterVeiculos(string filtro)
        {
            return new Models.Veiculo().ObterVeiculos(filtro);
        }

        public Models.Veiculo BuscarVeiculo(string placa)
        {
            return new Models.Veiculo().BuscarVeiculo(placa);
        }

        

    }
}
