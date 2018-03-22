using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_core_dapper.DataAccess;
using web_core_dapper.Models;

namespace web_core_dapper.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsersDataProvider IUsersProvider;

        public UsuariosController(IUsersDataProvider _iusersprovider) {
            this.IUsersProvider = _iusersprovider;
        }

        // Get api/Usuarios
        // Lista Usuarios
        [HttpGet]
        public async Task<IEnumerable<Usuario>> GetUsers()
        {
            return await this.IUsersProvider.getUsers();
        }

        // Regresa 1+ usuarios
        [HttpGet("{UserId}")]
        public async Task<IEnumerable<Usuario>> GetUser(long UserId)
        {
            return await this.IUsersProvider.GetUser(UserId);
        }

        // Agrega 1 Usuario
        [HttpPost]
        public async Task Post([FromBody] Usuario user)
        {
            await this.IUsersProvider.Insert(user);
        }

        // Actualiza Usuario
        [HttpPut("{UserId}")]
        public async Task Put([FromBody] Usuario user)
        {
            await this.IUsersProvider.Update(user);
        }

        // Borra Usuario
        [HttpDelete("{UserId}")]
        public async Task Delete(long UserId)
        {
            await this.IUsersProvider.Delete(UserId);

        }

    }
}