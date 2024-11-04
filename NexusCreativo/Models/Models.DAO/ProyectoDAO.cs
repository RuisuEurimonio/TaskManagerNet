using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace NexusCreativo.Models.Models.DAO
{
    internal class ProyectoDAO
    {


        string connectDB = "server=localhost; user=nexus; database=nexus; password=nexus; port=3307";

        public Proyecto ObtenerProyecto(int id)
        {
            Proyecto proyecto = new Proyecto();
            string query = "SELECT * FROM proyectos WHERE ID = " + id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    using (MySqlDataReader msdr = msCommand.ExecuteReader())
                    {
                        while (msdr.Read())
                        {
                            proyecto.Id = msdr.GetInt32("id");
                            proyecto.Nombre = msdr.GetString("nombre");
                            proyecto.Description = msdr.GetString("descripcion");
                        }
                    }
                    return proyecto;
                }
            }
        }

        public List<Proyecto> ObtenerProyectos()
        {
            List<Proyecto> listaDeProyectos = new List<Proyecto>();
            string query = "SELECT * FROM proyectos";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    using (MySqlDataReader msdr = msCommand.ExecuteReader())
                    {
                        while (msdr.Read())
                        {
                            Proyecto proyecto = new Proyecto();
                            proyecto.Id = msdr.GetInt32("id");
                            proyecto.Nombre = msdr.GetString("Nombre");
                            proyecto.Description = msdr.GetString("descripcion");
                            listaDeProyectos.Add(proyecto);
                        }
                    }
                    return listaDeProyectos;
                }
            }
        }

        public Proyecto CrearProyecto(Proyecto proyecto)
        {
            string query = "INSERT INTO proyectos (nombre, descripcion) VALUES (@nombre, @descripcion); SELECT LAST_INSERT_ID();";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@nombre", proyecto.Nombre);
                    msCommand.Parameters.AddWithValue("@descripcion", proyecto.Description);

                    msc.Open();
                    object result = msCommand.ExecuteScalar();
                    if (result != null)
                    {
                        return ObtenerProyecto(Convert.ToInt32(result));
                    }
                }
                return null;
            }
        }

        public Boolean EliminarProyecto(int id)
        {
            string query = "DELETE FROM proyectos WHERE id = " + id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    int result = msCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Boolean ActualizarProyecto(Proyecto proyecto)
        {
            string query = "UPDATE proyectos SET nombre = @nombre, descripcion = @descripcion WHERE id = " + proyecto.Id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@nombre", proyecto.Nombre);
                    msCommand.Parameters.AddWithValue("@descripcion", proyecto.Description);

                    msc.Open();
                    int result = msCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
