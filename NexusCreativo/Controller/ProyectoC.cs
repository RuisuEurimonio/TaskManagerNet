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

        public List<Models.Proyecto> getProjects()
        {
            return this.ProyectoDao.GetProjects();
        }

        public Models.Proyecto getProjectById(string id)
        {
            return ProyectoDao.GetProject(int.Parse(id));
        }

        public Models.Proyecto Create(string name, string rol)
        {
            if (name != null && rol != null && name != "" && rol != "")
            {
                Models.Proyecto newProject = new Models.Proyecto(name, rol);
                Models.Proyecto result = ProyectoDao.SetProjects(newProject);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public Boolean Delete(string id)
        {
            if (id != null || id != "")
            {
                ProyectoDao.DeleteProjects(int.Parse(id));
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
            Models.Proyecto proyectoByUpdate = ProyectoDao.GetProject(int.Parse(id));
            if (proyectoByUpdate == null)
            {
                return false;
            }
            if (name == null || rol == null || name == "" || rol == "")
            {
                return false;
            }
            Models.Proyecto proyectoUpdated = new Models.Proyecto(int.Parse(id), name, rol);
            Boolean result = ProyectoDao.UpdateProjects(proyectoUpdated);
            return true;
        }
    }
}
