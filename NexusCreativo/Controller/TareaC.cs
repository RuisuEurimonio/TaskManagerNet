﻿using NexusCreativo.Models.Models.DAO;
using System;
using System.Collections.Generic;

namespace NexusCreativo.Controller
{
    internal class TareaC
    {


        public TareaDAO TareaDao { get; set; }
        public ProyectoDAO ProyectoDao { get; set; }
        public UsuarioDAO UsuarioDao { get; set; }

        public TareaC()
        {
            this.TareaDao = new TareaDAO();
        }

        public List<Models.Tarea> ObtenerTareas()
        {
            return TareaDao.ObtenerTareas();
        }

        public Models.Tarea ObtenerTareaById(string id)
        {
            return TareaDao.ObtenerTarea(int.Parse(id));
        }

        public Models.Tarea Crear(string nombre, string descripcion, bool isCompleted, DateTime period, int usuarioId, int proyectoId)
        {
            if (nombre != null && descripcion != null && nombre != "" && descripcion != "")
            {

                Models.Tarea nuevaTarea = new Models.Tarea(nombre, descripcion, isCompleted, period, UsuarioDao.GetUser(usuarioId), ProyectoDao.GetProject(proyectoId));
                Models.Tarea result = TareaDao.CrearTarea(nuevaTarea);
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
                TareaDao.EliminarTarea(int.Parse(id));
                return true;
            }
            return false;
        }

        public Boolean Actualizar(string id, string nombre, string descripcion, bool isCompleted, DateTime period, int usuarioId, int proyectoId)
        {
            if (id == null || id == "")
            {
                return false;
            }
            Models.Tarea tareaParaActualizar = TareaDao.ObtenerTarea(int.Parse(id));
            if (tareaParaActualizar == null)
            {
                return false;
            }
            if (nombre != null && descripcion != null && nombre != "" && descripcion != "")
            {
                return false;
            }
            Models.Tarea tareaActualizada = new Models.Tarea(int.Parse(id), nombre, descripcion, isCompleted, period, UsuarioDao.GetUser(usuarioId), ProyectoDao.GetProject(proyectoId));
            Boolean result = TareaDao.ActualizarTarea(tareaActualizada);
            return true;
        }

    }
}