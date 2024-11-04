using NexusCreativo.Models.Models.DAO;
using System;
using System.Collections.Generic;

namespace NexusCreativo.Controller
{
    internal class ProyectoC
    {

        public ProyectoDAO ProyectoDao { get; set; }
        public ProyectoC()
        {
            this.ProyectoDao = new ProyectoDAO();
        }

        public List<Models.Proyecto> ObtenerProyectos()
        {
            return this.ProyectoDao.ObtenerProyectos();
        }

        public Models.Proyecto ObtenerProyectoById(string id)
        {
            return ProyectoDao.ObtenerProyecto(int.Parse(id));
        }

        public Models.Proyecto Crear(string name, string rol)
        {
            if (name != null && rol != null && name != "" && rol != "")
            {
                Models.Proyecto nuevoProyecto = new Models.Proyecto(name, rol);
                Models.Proyecto resultado = ProyectoDao.CrearProyecto(nuevoProyecto);
                if (resultado != null)
                {
                    return resultado;
                }
            }
            return null;
        }

        public Boolean Eliminar(string id)
        {
            if (id != null || id != "")
            {
                ProyectoDao.EliminarProyecto(int.Parse(id));
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
            Models.Proyecto proyectoParaActualizar = ProyectoDao.ObtenerProyecto(int.Parse(id));
            if (proyectoParaActualizar == null)
            {
                return false;
            }
            if (name == null || rol == null || name == "" || rol == "")
            {
                return false;
            }
            Models.Proyecto proyectoActualizado = new Models.Proyecto(int.Parse(id), name, rol);
            Boolean resultado = ProyectoDao.ActualizarProyecto(proyectoActualizado);
            if(!resultado)
            {
                return false;
            }
            return true;
        }
    }
}
