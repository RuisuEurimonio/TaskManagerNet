using NexusCreativo.Models.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NexusCreativo.Controller
{
    internal class UsuarioC
    {

        public UsuarioDAO UsuarioDao { get; set; }
        public UsuarioC() { 
            this.UsuarioDao = new UsuarioDAO();
        }

        public List<Models.Usuario> getUsers()
        {
            return this.UsuarioDao.GetUsers();
        }

        public Models.Usuario getUserById(string id)
        {
            return UsuarioDao.GetUser(int.Parse(id));   
        }

        public Models.Usuario Create(string name, string rol)
        {
            if(name != null && rol != null && name != "" && rol != "")
            {
                int id = UsuarioDao.GetUsers().Count;
                Models.Usuario newUsuario = new Models.Usuario(id+1, name, rol);
                Boolean result = UsuarioDao.SetUsers(newUsuario);
                if (result)
                {
                    return newUsuario;
                }
            }
            return null;
        }

        public Boolean Delete(string id)
        {
            if(id == null)
            {
                Boolean result = UsuarioDao.DeleteUsers(int.Parse(id));
                return true;
            }
            return false;
        }

        public Boolean Actualizar(string id, string name, string rol)
        {
            if (id == null || id == "")
            {
                Console.WriteLine("hola");
                return false;
            }
            Models.Usuario userByUpdate = UsuarioDao.GetUser(int.Parse(id));
            if (userByUpdate == null)
            {
                return false;
            }
            if (name == null || rol == null || name == "" || rol == "")
            {
                return false;
            }
            Models.Usuario usuarioUpdated = new Models.Usuario(int.Parse(id), name, rol);
            Boolean result = UsuarioDao.UpdateUser(usuarioUpdated);
            return true;        
        }

    }


}
