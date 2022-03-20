using AutoAdoNet.Services.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.Helper.Services
{
    public class HelperService : IHelperService
    {
        private readonly IConfiguration _config;
        public HelperService(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Trasforma DataReader em Objeto
        /// </summary>
        /// <typeparam name="T">NOme do objeto DTO</typeparam>
        /// <param name="dr">Data Reader</param>
        /// <returns>OBJETO T</returns>
        #region DRMapToList
        public List<T> DRMapToList<T>(IDataReader dr)
        {
            IDataReader valores = dr;
            var propriedade = "";
            try
            {
                List<T> list = new List<T>();
                T obj = default(T);
                while (valores.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        propriedade = prop.Name;
                        if (!object.Equals(valores[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, valores[prop.Name], null);
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message} {propriedade}");
            }
        }
        #endregion DRMapToList

        /// <summary>
        ///  Executa pesquias no banco de dados pasasndo a query e os parametros via Objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        #region ExecutaQueryReader
        public List<T> ExecutaQueryReader<T>(string query, dynamic param = null, QueryType tipoQuery = QueryType.Select)
        {
            SqlTransaction transaction = null;
            string novaQuery = "";
            try
            {
                novaQuery = GeraQuery(param, query);
                var listagem = new List<T>();
                using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DockerConnection")))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(novaQuery, connection))
                    {
                        if (tipoQuery != QueryType.Select)
                        {
                            command.Transaction = transaction;
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 7200;
                        var parametros = CreateSqlParameter(param, tipoQuery);
                        foreach (SqlParameter paran in parametros)
                        {
                            command.Parameters.AddWithValue(paran.ParameterName, paran.Value);
                        }

                        var dataReader = command.ExecuteReader();
                        listagem = DRMapToList<T>(dataReader);
                    }
                    connection.Close();
                }
                return new List<T>(listagem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion ExecutaQueryReader

        #region ExecutaQuery
        public int ExecutaQuery<T>(string query, dynamic param = null, QueryType tipoQuery = QueryType.Select)
        {
            SqlTransaction transaction = null;
            int resultado = 0;
            var sql = query;
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DockerConnection")))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction($"Transaction{Guid.NewGuid().ToString().Substring(0, 16)}");
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 7200;
                        var lista = new List<SqlParameter>();
                        foreach (SqlParameter paran in CreateSqlParameter(param, tipoQuery))
                        {
                            command.Parameters.Add(paran);
                        }

                        if(tipoQuery == QueryType.Insert)
                        {
                            resultado = Convert.ToInt32(command.ExecuteScalar());
                        } else
                        {
                            resultado = command.ExecuteNonQuery();
                        }
                 
                    }
                    transaction.Commit();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                resultado = 0;
                throw new Exception($"Erro: {ex.Message}");
            }
            return resultado;
        }
        #endregion ExecutaQuery


        /// <summary>
        ///  Cria os parametros para sql command automticamente apartir de um objeto passado
        /// </summary>
        /// <param name="values">Objeto DTO</param>
        /// <returns>Lista de parametros </returns>
        #region CreateSqlParameter
        public List<SqlParameter> CreateSqlParameter(dynamic values, QueryType tipoQuery = QueryType.Select)
        {
            var dinamicParameter = new List<SqlParameter>();
            foreach (PropertyInfo prop in values.GetType().GetProperties())
            {
                if (prop.GetValue(values, null) != null)
                {
                    if (tipoQuery == QueryType.Select)
                    {
                        if (prop.PropertyType.FullName == "System.String")
                            dinamicParameter.Add(new SqlParameter($"@{prop.Name}", SqlDbType.NVarChar, 255)
                            {
                                Value = prop.GetValue(values, null) + "%"
                            });
                        else
                            dinamicParameter.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(values, null)));
                    }
                    else
                    {
                        dinamicParameter.Add(new SqlParameter($"@{prop.Name}", prop.GetValue(values, null)));
                    }

                }
                else
                {
                    dinamicParameter.Add(new SqlParameter($"@{prop.Name}", DBNull.Value));
                }
            }
            return dinamicParameter;
        }
        #endregion CreateSqlParameter

        /// <summary>
        ///  Sistema que gera query automaticamente com os parametros passados via DTO
        /// </summary>
        /// <param name="values">Valores do objetoc que vai ser transformado em QUERY</param>
        /// <param name="query">Retorna a query pré construida com os parametros</param>
        /// <returns></returns>
        #region Geraquery
        public string GeraQuery(dynamic values, string query)
        {
            StringBuilder sqlParam = new StringBuilder();
            var resultado = "";
            foreach (PropertyInfo prop in values.GetType().GetProperties())
            {
                if (prop.GetValue(values, null) != null)
                {
                    if (prop.PropertyType.FullName == "System.String")
                        sqlParam.Append($"{prop.Name} LIKE @{prop.Name} ,");
                    else
                        sqlParam.Append($" {prop.Name} = @{prop.Name} ,");
                }
                var changeComma = sqlParam.ToString().TrimEnd(',').Replace(",", " AND ");

                if (sqlParam.Length <= 0)
                {
                    resultado = query;
                }
                else if (query.Contains("where") || query.Contains("WHERE"))
                {
                    if (query.Contains("SUBQUERY"))
                    {
                        resultado = $"{query} where {changeComma}";
                    }
                    else
                    {
                        resultado = $"{query} and {changeComma}";
                    }
                }
                else
                {
                    resultado = $"{query} where {changeComma}";
                }
            }
            resultado += $" ORDER BY Id ASC ";

            return resultado;
        }
        #endregion Geraquery
    }
}
