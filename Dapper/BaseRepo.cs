using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DapperRepos
{
    public abstract class BaseRepo
    {

        private string ConnectionString { get; }

        protected BaseRepo(ConnectionStringProvider connectionString)
        {
            ConnectionString = connectionString.ConnectionString;
        }


        protected IEnumerable<T> Run<T>(string sql, object param = null)
        {
            return Run(i => i.Query<T>(sql, param));
        }

        protected T Run<T>(Func<IDbConnection, T> cmd)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            return cmd.Invoke(db);
        }



        protected Task<IEnumerable<T>> RunAsync<T>(string sql, object param = null)
        {
            return RunAsync(i => i.QueryAsync<T>(sql, param));
        }

        protected async  Task<IEnumerable<T>> RunAsync<T>(Func<IDbConnection, Task<IEnumerable<T>>> cmd)
        {
            await using SqlConnection con = new SqlConnection(ConnectionString);
            IEnumerable<T> result = null;
            try
            {
                await con.OpenAsync();
                result = await cmd.Invoke((IDbConnection) con);

            }finally {
                await con.CloseAsync();
            }

            return result;
        }

    }
}
