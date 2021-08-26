using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EdgasSystem.DAL
{
    public class Banco
    {
        private static Banco b;
        /// <summary>
        /// Campo responsável pela definição da string de conexão
        /// </summary>
        private string _strConexao;
        /// <summary>
        /// Campo responsável pelo comando de SQL a ser executado
        /// </summary>
        private SqlCommand _ComandoSQL;
        /// <summary>
        /// Propriedade que expõe o campo para definição do comando de SQL a ser executado
        /// </summary>

        public SqlCommand getComandoSQL() { return _ComandoSQL; }

        public void setComandoSQL(SqlCommand _ComandoSQL) { this._ComandoSQL = _ComandoSQL; }
        /// <summary>
        /// Campo que define o objeto de conexão
        /// </summary>
        private SqlConnection _conn;
        /// <summary>
        /// Campo que define o objeto de transação
        /// </summary>
        private SqlTransaction _transacao;


        /// <summary>
        /// Construtor que define uma string de conexão fixa e cria os objetos de conexão e 
        /// comando
        /// </summary>
        /// 

        private Banco()
        {

            _strConexao = @"Data Source=DESKTOP-H92LBCO; Initial Catalog=EdgasSystem;User Id=sa;Password=edgas; MultipleActiveResultSets=true;"; //Banco Casa
            _conn = new SqlConnection(_strConexao);
            _ComandoSQL = new SqlCommand();
            _ComandoSQL.Connection = _conn;
        }
        public static Banco GetInstance()
        {
            if (b == null)
                b = new Banco();

            return b;
        }

        /// <summary>
        /// Construtor que recebe por parametro a string de conexão a ser utilizada e cria
        /// os objetos de comando e conexão
        /// </summary>
        /// <param name="stringConexao">String de conexão a ser utilizada</param>

        /// <summary>
        /// Método para abrir a conexão com o banco de dados
        /// </summary>
        /// <param name="transacao">true -> Com transação | false -> Sem transação</param>
        /// <returns></returns>
        internal bool AbreConexao(bool transacao)
        {
            try
            {
                _conn.Open();
                if (transacao)
                {
                    _transacao = _conn.BeginTransaction();
                    _ComandoSQL.Transaction = _transacao;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Métodos para fechar a conexão com o banco de dados
        /// </summary>
        /// <returns>Retorna um booleano para indicar o resultado da operação</returns>
        internal bool FechaConexao()
        {
            try
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Finaliza uma transação
        /// </summary>
        /// <param name="commit">true -> Executa o commit | false -. Executa o rollback</param>
        internal void FinalizaTransacao(bool commit)
        {
            if (commit)
                _transacao.Commit();
            else
                _transacao.Rollback();
            FechaConexao();
        }
        /// <summary>
        /// Destrutor que fecha a conexão com o banco de dados
        /// </summary>
        ~Banco()
        {
            FechaConexao();
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Insert, Update e Delete
        /// </summary>
        /// <returns>Retorna um número inteiro que indica a quantidade de linhas afetadas</returns>
        internal int ExecutaComando(bool transacao = false)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            int retorno;
            if (_conn.State != ConnectionState.Open)
                AbreConexao(transacao);
            try
            {
                retorno = _ComandoSQL.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                retorno = -1;
                Debug.WriteLine(ex.Message.ToString());
                Debug.WriteLine("--------Numero"+ex.Number.ToString());
                GlobalExceptionHandler.SQLErrorMessage = ex.Message.ToString();
                GlobalExceptionHandler.SQLErrorNumber = ex.Number.ToString();

                //throw new Exception("Erro ao executar o comando SQL:", ex);
                return 0;
            }
            finally
            {
                if (!transacao)
                {
                    //Retorna o tipo de comando para texto, necessário quando se usa Storage Procedure
                    _ComandoSQL.CommandType = CommandType.Text;
                    FechaConexao();
                }
            }
            return retorno;
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Insert com retorno do último código cadastrado
        /// </summary>
        /// <returns>Retorna um número inteiro que indica a quantidade de linhas afetadas</returns>
        internal int ExecutaComando(bool transacao, out int ultimoCodigo)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            int retorno;
            ultimoCodigo = 0;
            AbreConexao(transacao);
            try
            {
                //Executa o comando de insert e já retorna o @@IDENTITY
                ultimoCodigo = Convert.ToInt32(_ComandoSQL.ExecuteScalar());

                retorno = 1;
            }
            catch (SqlException ex)
            {
                retorno = -1;
                GlobalExceptionHandler.SQLErrorMessage = ex.ToString();
                GlobalExceptionHandler.SQLErrorNumber = ex.Number.ToString();
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                if (!transacao)
                {
                    //Retorna o tipo de comando para texto, necessário quando se usa Storage Procedure
                    _ComandoSQL.CommandType = CommandType.Text;
                    FechaConexao();
                }

            }
            return retorno;
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Select
        /// </summary>
        /// <returns>Retorna um DataTable com o resultado da operação</returns>
        internal DataTable ExecutaSelect(bool transacao = false)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            AbreConexao(transacao);
            DataTable dt = new DataTable();
            try
            {
                dt.Load(_ComandoSQL.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
                GlobalExceptionHandler.SQLErrorMessage = ex.ToString();
                GlobalExceptionHandler.SQLErrorNumber = ex.Number.ToString();
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                if (!transacao)
                    FechaConexao();
            }

            return dt;
        }
        /// <summary>
        /// Método que executa comandos de Select para retornos escalares, ou seja,
        /// retorna a primeira linha e primeira coluna do resultado do comando de Select.
        /// Para nosso exemplo, sempre convertemos esse valor para Double
        /// </summary>
        /// <returns>Retorna a primeira linha e primeira coluna do resultado comando de Select</returns>
        internal double ExecutaScalar()
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            AbreConexao(false);
            double retorno;
            try
            {
                retorno = Convert.ToDouble(_ComandoSQL.ExecuteScalar());
            }
            catch (Exception ex)
            {
                retorno = -9999;
                GlobalExceptionHandler.SQLErrorMessage = ex.ToString();
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                FechaConexao();
            }
            return retorno;
        }
    }
}

