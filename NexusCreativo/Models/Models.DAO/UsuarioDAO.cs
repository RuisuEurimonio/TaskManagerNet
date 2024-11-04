using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCreativo.Models.Models.DAO
{
    internal class UsuarioDAO
    {
       
        string connectDB = "server=localhost; user=nexus; database=nexus; password=nexus; port=3307";

        public Usuario ObtenerUsuario(int id)
        {
            Usuario usuario = new Usuario();
            string query = "SELECT * FROM usuarios WHERE ID = " + id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using(MySqlCommand msCommand = new MySqlCommand(query, msc)) 
                {
                    msc.Open();
                    using(MySqlDataReader msdr =  msCommand.ExecuteReader())
                    {
                        while(msdr.Read())
                        {
                            usuario.Id = msdr.GetInt32("id");
                            usuario.Name = msdr.GetString("name");
                            usuario.Rol = msdr.GetString("rol");
                        }
                    }
                    return usuario;
                }
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario> ();
            string query = "SELECT * FROM usuarios";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    using (MySqlDataReader msdr = msCommand.ExecuteReader())
                    {
                        while (msdr.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.Id = msdr.GetInt32("id");
                            usuario.Name = msdr.GetString("name");
                            usuario.Rol = msdr.GetString("rol");
                            listaUsuarios.Add(usuario);
                        }
                    }
                    Console.WriteLine("Consulta realizada");
                    return listaUsuarios;
                }
            }
        }

        public Usuario CrearUsuario(Usuario usuario)
        {
            string query = "INSERT INTO usuarios (name, rol) VALUES (@name, @rol); SELECT LAST_INSERT_ID();";
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@name", usuario.Name);
                    msCommand.Parameters.AddWithValue("@rol", usuario.Rol);

                    msc.Open();
                    object result = msCommand.ExecuteScalar();
                    if(result != null)
                    {
                        return ObtenerUsuario(Convert.ToInt32(result));
                    }
                }
                return null;
            }
        }

        public Boolean EliminarUsuario(int id)
        {
            string query = "DELETE FROM usuarios WHERE id = "+ id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msc.Open();
                    int result = msCommand.ExecuteNonQuery();
                    if(result > 0 )
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Boolean ActualizarUsuario(Usuario usuario)
        {
            List<Usuario> listUsers = new List<Usuario>();
            string query = "UPDATE usuarios SET name = @name, rol = @rol WHERE id = " + usuario.Id;
            using (MySqlConnection msc = new MySqlConnection(connectDB))
            {
                using (MySqlCommand msCommand = new MySqlCommand(query, msc))
                {
                    msCommand.Parameters.AddWithValue("@name", usuario.Name);
                    msCommand.Parameters.AddWithValue("@rol", usuario.Rol);

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
