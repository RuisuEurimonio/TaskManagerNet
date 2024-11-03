using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCreativo.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }

        public Proyecto(int id, string name, string description)
        {
            Id = id;
            Nombre = name;
            Description = description;
        }

        public Proyecto(string name, string description)
        {
            Nombre = name;
            Description = description;
        }

        public Proyecto()
        {

        }
    }
}
