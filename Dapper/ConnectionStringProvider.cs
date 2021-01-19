using System;
using System.Collections.Generic;
using System.Text;

namespace DapperRepos
{
    public class ConnectionStringProvider
    {
        public ConnectionStringProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ConnectionStringProvider()
        {
            
        }

        public string ConnectionString { get; set; }

    }
}
