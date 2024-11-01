using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCreativo.Models
{
    public class Usuario
    {

        public int Id { get; set; }
        public string Name { get; set;}
        public string Rol { get; set; }

        public Usuario(string Name, string Rol)
        {
            this.Name = Name;
            this.Rol = Rol;
        }

        public Usuario(int id, string Name, string Rol)
        {
            this.Id = id;
            this.Name = Name;
            this.Rol = Rol;
        }

        public Usuario()
        {
        }
    }
}
