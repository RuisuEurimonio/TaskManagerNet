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

        public List<Models.Usuario> ObtenerUsuarios()
        {
            return this.UsuarioDao.ObtenerUsuarios();
        }

        public Models.Usuario ObtenerUsuarioById(string id)
        {
            return UsuarioDao.ObtenerUsuario(int.Parse(id));   
        }

        public Models.Usuario Crear(string name, string rol)
        {
            if(name != null && rol != null && name != "" && rol != "")
            {
                Models.Usuario nuevoUsuario = new Models.Usuario(name, rol);
                Models.Usuario resultado = UsuarioDao.CrearUsuario(nuevoUsuario);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            return null;
        }

        public Boolean Eliminar(string id)
        {
            if(id != null || id != "")
            {
                UsuarioDao.EliminarUsuario(int.Parse(id));
                return true;
            }
            return false;
        }

        public Boolean Actualizar(string id, string name, string rol)
        {
            if (id == null || id == "")
            {
                return false;
            }
            Models.Usuario usuarioPorActualizar = UsuarioDao.ObtenerUsuario(int.Parse(id));
            if (usuarioPorActualizar == null)
            {
                return false;
            }
            if (name == null || rol == null || name == "" || rol == "")
            {
                return false;
            }
            Models.Usuario usuarioActualizado = new Models.Usuario(int.Parse(id), name, rol);
            Boolean resultado = UsuarioDao.ActualizarUsuario(usuarioActualizado);
            if (!resultado)
            {
                return false;
            }
            return true;        
        }

    }


}
