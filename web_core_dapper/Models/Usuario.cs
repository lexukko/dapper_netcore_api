using System;

namespace web_core_dapper.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Email { get; set; }
    }
}
