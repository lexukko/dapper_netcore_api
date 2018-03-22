using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using web_core_dapper.Models;

namespace web_core_dapper.DataProvider
{
    public class CustomDataProvider : ICustomDataProvider
    {
        private readonly String _connStr;

        public CustomDataProvider(String conStr)
        {
            this._connStr = conStr;
        }

        public async Task<IEnumerable<sp_CustomQuery_Result>> sp_CustomQuery(long id)
        {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<sp_CustomQuery_Result>("sp_CustomQuery", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
