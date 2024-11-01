using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCreativo.Models
{
    internal class Tarea
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public DateTime Period {  get; set; }
        public Usuario Usuario { get; set; }
        public Proyecto Proyecto { get; set; }

        public Tarea(int id, string name, string description, bool isCompleted, DateTime period, Usuario usuario, Proyecto proyecto)
        {
            Id = id;
            Name = name;
            Description = description;
            this.isCompleted = isCompleted;
            Period = period;
            Usuario = usuario;
            Proyecto = proyecto;
        }

        public Tarea()
        {

        }
    }
}
