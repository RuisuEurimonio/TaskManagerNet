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

        public Usuario GetUser(int id)
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

        public List<Usuario> GetUsers()
        {
            List<Usuario> listUsers = new List<Usuario> ();
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
                            listUsers.Add(usuario);
                        }
                    }
                    Console.WriteLine("Consulta realizada");
                    return listUsers;
                }
            }
        }

        public Usuario SetUsers(Usuario usuario)
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
                        return GetUser(Convert.ToInt32(result));
                    }
                }
                return null;
            }
        }

        public Boolean DeleteUsers(int id)
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

        public Boolean UpdateUser(Usuario usuario)
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
