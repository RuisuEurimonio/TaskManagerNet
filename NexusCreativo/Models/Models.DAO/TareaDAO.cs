using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NexusCreativo.Models.Models.DAO
{
    internal class TareaDAO
    {


        string connectDB = "server=localhost; user=nexus; database=nexus; password=nexus; port=3307";

        public Tarea ObtenerTarea(int id)
        {
            Tarea tarea = new Tarea();
            string query = "SELECT t.id AS tareaId, t.nombre AS tareaName, t.descripcion AS tareaDescripcion, t.isCompleted, t.period, u.id AS usuarioId, u.name AS usuarioName, p.id AS proyectoId, p.nombre AS proyectoName FROM Tareas t INNER JOIN Usuarios u ON t.usuario_id = u.id INNER JOIN Proyectos p ON t.proyecto_id = p.id WHERE t.id = "+id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    using (MySqlDataReader msdr = msCommand.ExecuteReader())
                    {
                        while (msdr.Read())
                        {
                            tarea.Id = msdr.GetInt32("tareaId");
                            tarea.Nombre = msdr.GetString("tareaName");
                            tarea.Description = msdr.GetString("tareaDescripcion");
                            tarea.isCompleted = msdr.GetBoolean("isCompleted");
                            tarea.Period = msdr.GetDateTime("period");
                            tarea.Usuario = new Usuario
                            {
                                Id = msdr.GetInt32("usuarioId"),
                                Name = msdr.GetString("usuarioName")
                            };
                            tarea.Proyecto = new Proyecto
                            {
                                Id = msdr.GetInt32("proyectoId"),
                                Nombre = msdr.GetString("proyectoName")
                            };

                        }
                    }
                    return tarea;
                }
            }
        }

        public List<Tarea> ObtenerTareas()
        {
            List<Tarea> listaTareas = new List<Tarea>();
            string query = "SELECT t.id AS tareaId, t.nombre AS tareaName, t.descripcion AS tareaDescripcion, t.isCompleted, t.period, u.id AS usuarioId, u.name AS usuarioName, p.id AS proyectoId, p.nombre AS proyectoName FROM Tareas t INNER JOIN Usuarios u ON t.usuario_id = u.id INNER JOIN Proyectos p ON t.proyecto_id = p.id";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    using (MySqlDataReader msdr = msCommand.ExecuteReader())
                    {
                        while (msdr.Read())
                        {
                            Tarea tarea = new Tarea();
                            tarea.Id = msdr.GetInt32("tareaId");
                            tarea.Nombre = msdr.GetString("tareaName");
                            tarea.Description = msdr.GetString("tareaDescripcion");
                            tarea.isCompleted = msdr.GetBoolean("isCompleted");
                            tarea.Period = msdr.GetDateTime("period");
                            tarea.Usuario = new Usuario
                            {
                                Id = msdr.GetInt32(msdr.GetOrdinal("usuarioId")),
                                Name = msdr.GetString(msdr.GetOrdinal("usuarioName"))
                            };
                            tarea.Proyecto = new Proyecto
                            {
                                Id = msdr.GetInt32(msdr.GetOrdinal("proyectoId")),
                                Nombre = msdr.GetString(msdr.GetOrdinal("proyectoName"))
                            };
                            listaTareas.Add(tarea);
                        }
                    }
                    return listaTareas;
                }
            }
        }

        public Tarea CrearTarea(Tarea tareaInputs)
        {
            Tarea tarea = new Tarea();
            string query = "INSERT INTO Tareas (nombre, descripcion, isCompleted, period, usuario_Id, proyecto_Id) VALUES (@nombre, @descripcion, @isCompleted, @period, @usuarioId, @proyectoId); SELECT LAST_INSERT_ID();";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@nombre", tareaInputs.Nombre);
                    msCommand.Parameters.AddWithValue("@descripcion", tareaInputs.Description);
                    msCommand.Parameters.AddWithValue("@isCompleted", tareaInputs.isCompleted);
                    msCommand.Parameters.AddWithValue("@period", tareaInputs.Period);
                    msCommand.Parameters.AddWithValue("@usuarioId", tareaInputs.Usuario.Id); 
                    msCommand.Parameters.AddWithValue("@proyectoId", tareaInputs.Proyecto.Id);

                    msc.Open();
                    object resultado = msCommand.ExecuteScalar();
                    if (resultado != null)
                    {
                        return ObtenerTarea(Convert.ToInt32(resultado));
                    }
                }
                return null;
            }
        }

        public Boolean EliminarTarea(int id)
        {
            string query = "DELETE FROM tareas WHERE id = " + id;
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

        public Boolean ActualizarTarea(Tarea tarea)
        {
            string query = "UPDATE Tareas SET nombre = @nombre, descripcion = @descripcion, isCompleted = @isCompleted, period = @period, usuario_Id = @usuario_Id, proyecto_Id = @proyecto_Id WHERE id = @tareaId; ";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@nombre", tarea.Nombre);
                    msCommand.Parameters.AddWithValue("@descripcion", tarea.Description);
                    msCommand.Parameters.AddWithValue("@isCompleted", tarea.isCompleted);
                    msCommand.Parameters.AddWithValue("@period", tarea.Period);
                    msCommand.Parameters.AddWithValue("@usuario_Id", tarea.Usuario.Id);
                    msCommand.Parameters.AddWithValue("@proyecto_Id", tarea.Proyecto.Id);
                    msCommand.Parameters.AddWithValue("@tareaId", tarea.Id);

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
