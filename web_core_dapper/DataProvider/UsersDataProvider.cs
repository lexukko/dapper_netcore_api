using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using web_core_dapper.Models;

/*
 Setea Connection String en Constructor.
     */


namespace web_core_dapper.DataAccess
{
    public class UsersDataProvider : IUsersDataProvider
    {

        private readonly String _connStr;

        public UsersDataProvider(String conStr)
        {
            this._connStr = conStr;
        }

        public async Task<IEnumerable<Usuario>> getUsers()
        {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<Usuario>("Select * From usuarios;");
            }
        }

        public async Task<IEnumerable<Usuario>> GetUser(long id)
        {
            String Query = "select * from  Usuarios where id = @id;";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<Usuario>(Query, new { Id = id });
            }
        }

        public async Task<int> Insert(Usuario usuario)
        {

            String Query = @"Insert Into Usuarios (Nombre, Edad, Email)
                             Values(@Nombre, @Edad, @Email);
                            ";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                int res = await db.ExecuteAsync(Query, new { Nombre = usuario.Nombre, Edad = usuario.Edad, Email = usuario.Email });
                return res;
            }
        }

        public async Task<int> Delete(long id)
        {
            String Query = "delete from Usuarios where id = @id;";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                int res = await db.ExecuteAsync(Query, new { Id = id });
                return res;
            }
        }

        public async Task<int> Update(Usuario usuario)
        {
            String Query = @"Update Usuarios 
                             Set Nombre = @Nombre, Edad = @Edad, Email = @Email
                             Where id = @Id;
                            ";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                int res = await db.ExecuteAsync(Query, new { Id = usuario.Id, Nombre = usuario.Nombre, Edad = usuario.Edad, Email = usuario.Email });
                return res;
            }
        }

        
    }
}
