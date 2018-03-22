using System.Collections.Generic;
using System.Threading.Tasks;
using web_core_dapper.Models;

/*
 Requerido para inyeccion
     */

namespace web_core_dapper.DataAccess
{
    public interface IUsersDataProvider
    {
        Task<IEnumerable<Usuario>> getUsers();

        Task<IEnumerable<Usuario>> GetUser(long id);

        Task<int> Insert(Usuario usuario);

        Task<int> Delete(long id);

        Task<int> Update(Usuario usuario);
    }
}
