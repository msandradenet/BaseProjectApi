using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infra
{
    public class SqlAdapter
    {
        public SqlConnection SqlConnection { get; set; }
        public SqlTransaction SqlTransaction { get; set; }

        public SqlAdapter(SqlConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }
    }
}
