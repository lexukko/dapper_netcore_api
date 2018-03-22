using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace dapper_console_1
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Email { get; set; }
    }

    public class sp_CustomQuery_Result
    {
        public string Usuario { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Email { get; set; }
        public string Hobbie { get; set; }
    }

    public class crud_usuarios {

        private readonly String _connStr;

        public crud_usuarios(String conStr){
            this._connStr = conStr;
        }

        public async Task<IEnumerable<Usuario>> getUsers() {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {                
                 return await db.QueryAsync<Usuario> ("Select * From usuarios;");
            }
        }

        public async Task<IEnumerable<Usuario>> Get_sp_CustomQuery(long id)
        {
            String Query = "select * from  Usuarios where id = @id;";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<Usuario>(Query, new { Id=id });
            }
        }

        public async Task<int> Insert(Usuario usuario) {

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

        public async Task<int> Update(Usuario usuario,long id)
        {
            String Query = @"Update Usuarios 
                             Set Nombre = @Nombre, Edad = @Edad, Email = @Email
                             Where id = @Id;
                            ";

            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                int res = await db.ExecuteAsync(Query, new { Id = id, Nombre = usuario.Nombre, Edad = usuario.Edad, Email = usuario.Email });
                return res;
            }
        }


        public async Task<IEnumerable<T>> getCustomQuery<T>(String customQuery)
        {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<T>(customQuery);
            }
        }

        public async Task<IEnumerable<dynamic>> getCustomQuery2(String customQuery)
        {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync(customQuery);
            }
        }

        // sp
        public async Task<IEnumerable<sp_CustomQuery_Result>> Get_sp_CustomQuery2(long id) {
            using (IDbConnection db = new SqlConnection(this._connStr))
            {
                return await db.QueryAsync<sp_CustomQuery_Result>("sp_CustomQuery", new { Id = id}, commandType: CommandType.StoredProcedure);
            }
        }
    }

    class App {

        String ConnStr = @"data source=localhost,5722;initial catalog=pruebas;persist security info=True;user id=pruebas;MultipleActiveResultSets=True;Password=pruebas";
        crud_usuarios crud_u ;

        public App() {
            this.crud_u = new crud_usuarios(this.ConnStr);
        }

        public async void Run() {

            /*
            IEnumerable<Usuario> res = await crud_u.QueryParams(1);
            foreach(Usuario usuario in res){
                Console.WriteLine(String.Format("Id: {0}",usuario.Id));
                Console.WriteLine(String.Format("Nombre: {0}", usuario.Nombre));
                Console.WriteLine(String.Format("Edad: {0}", usuario.Edad));
                Console.WriteLine(String.Format("Email: {0}", usuario.Email));
                Console.WriteLine();
            }
            */

            /*
            int res = await this.crud_u.Delete(3);
            Console.WriteLine(String.Format("Delete Affected rows {0}", res));
            */

            /*
            int res = await this.crud_u.Update(new Usuario { Nombre = "LOL", Edad = 34, Email = "nuevomail@patito.com" } , 2 );
            Console.WriteLine(String.Format("Update Affected rows {0}", res));
            */

            /*
            int res = await this.crud_u.Insert(new Usuario { Nombre = "LOL1", Edad = 34, Email = "nuevomai_1@patito.com" });
            Console.WriteLine(String.Format("Insert Affected rows {0}", res));
            int res1 = await this.crud_u.Insert(new Usuario { Nombre = "LOL2", Edad = 35, Email = "nuevomai_2@patito.com" });
            Console.WriteLine(String.Format("Insert Affected rows {0}", res1));
            int res2 = await this.crud_u.Insert(new Usuario { Nombre = "LOL3", Edad = 36, Email = "nuevomai_3@patito.com" });
            Console.WriteLine(String.Format("Insert Affected rows {0}", res2));
            */

            
            IEnumerable<sp_CustomQuery_Result> res = await crud_u.Get_sp_CustomQuery2(1);
            foreach(sp_CustomQuery_Result usuario in res){
                Console.WriteLine(String.Format("Id: {0}",usuario.Usuario));
                Console.WriteLine(String.Format("Nombre: {0}", usuario.Edad));
                Console.WriteLine(String.Format("Edad: {0}", usuario.Email));
                Console.WriteLine(String.Format("Email: {0}", usuario.Hobbie));
                Console.WriteLine();
            }
            

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            App app = new App();
            app.Run();
            Console.ReadKey();
        }
    }
}
