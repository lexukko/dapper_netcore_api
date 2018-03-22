using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_core_dapper.Models;

namespace web_core_dapper.DataProvider
{
    public interface ICustomDataProvider
    {
        Task<IEnumerable<sp_CustomQuery_Result>> sp_CustomQuery(long id);
    }
}
