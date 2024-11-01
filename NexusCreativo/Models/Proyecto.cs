using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCreativo.Models
{
    internal class Proyecto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Proyecto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Proyecto()
        {

        }
    }
}
