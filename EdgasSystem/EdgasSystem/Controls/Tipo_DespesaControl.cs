using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.Controls
{
    public class Tipo_DespesaControl
    {
        public bool GravarTipo_Despesa(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Tipo_Despesa tipo = new Models.Tipo_Despesa(0,
                          dados["nome"].ToString(),
                          dados["descricao"].ToString()
                          );
                if (tipo.Gravar(Transacao, true))
                    return true;
                else

                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AlterarTipo_Despesa(Dictionary<string, object> dados)
        {
            bool Transacao = true; //retorno ExecutaComando da DAL
            try
            {
                Models.Tipo_Despesa tipo = new Models.Tipo_Despesa(Convert.ToInt32(dados["cod_tipo_despesa"].ToString()),
                 dados["nome"].ToString(),
                 dados["descricao"].ToString()
                 );
                if (tipo.Gravar(Transacao, true))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirTipo_Despesa(int cod_tipo_despesa)
        {
            bool Transacao = true;

            Models.Tipo_Despesa tipo = new Models.Tipo_Despesa().BuscarTipo_Despesa(cod_tipo_despesa);
            try
            {
                if (tipo.Excluir(Transacao))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Models.Tipo_Despesa> ObterTipo_Despesa(string filtro)
        {
            return new Models.Tipo_Despesa().ObterTipo_Despesas(filtro);
        }

        public Models.Tipo_Despesa BuscarTipo_Produto(int cod_cliente)
        {
            return new Models.Tipo_Despesa().BuscarTipo_Despesa(cod_cliente);
        }

        

    }
}
