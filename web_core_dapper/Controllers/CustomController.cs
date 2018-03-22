using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_core_dapper.DataProvider;
using web_core_dapper.Models;

namespace web_core_dapper.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomController : Controller
    {
        private readonly ICustomDataProvider ICustomProvider;

        public CustomController(ICustomDataProvider _icustomprovider)
        {
            this.ICustomProvider = _icustomprovider;
        }

        [HttpGet("{UserId}")]
        public async Task<IEnumerable<sp_CustomQuery_Result>> Exec_Sp_CustomQuery1(long UserId)
        {
            return await this.ICustomProvider.sp_CustomQuery(UserId);
        }

        [HttpGet("{UserId}")]
        public async Task<IEnumerable<sp_CustomQuery_Result>> Exec_Sp_CustomQuery2(long UserId)
        {
            return await this.ICustomProvider.sp_CustomQuery(UserId);
        }

    }
}