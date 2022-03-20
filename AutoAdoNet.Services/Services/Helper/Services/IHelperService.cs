using AutoAdoNet.Services.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.Helper.Services
{
    public interface IHelperService
    {
        List<T> DRMapToList<T>(IDataReader dr);
        List<T> ExecutaQueryReader<T>(string query, dynamic param = null, QueryType tipoQuery = QueryType.Select);
        int ExecutaQuery<T>(string query, dynamic param = null, QueryType tipoQuery = QueryType.Select);
        List<SqlParameter> CreateSqlParameter(dynamic values, QueryType tipoQuery = QueryType.Select);
        string GeraQuery(dynamic values, string query);
    }
}
