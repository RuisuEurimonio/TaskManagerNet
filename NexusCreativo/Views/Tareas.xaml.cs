using NexusCreativo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NexusCreativo.Views
{
    /// <summary>
    /// Lógica de interacción para Tareas.xaml
    /// </summary>
    public partial class Tareas : Window
    {
        public MainWindow MainW { get; set; }
        public ObservableCollection<Tarea> TareasDB { get; set; }
        public ObservableCollection<Usuario> UsuarioDB { get; set; }
        public ObservableCollection<Proyecto> ProyectosDB { get; set; }
        private Controller.TareaC tareaControlador;
        private Controller.ProyectoC proyectoControlador;
        private Controller.UsuarioC usuarioControlador;

        public Tareas()
        {
            InitializeComponent();
        }

        public Tareas(MainWindow mainWindows)
        {
            InitializeComponent();
            MainW = mainWindows;
            tareaControlador = new Controller.TareaC();
            proyectoControlador = new Controller.ProyectoC();
            usuarioControlador = new Controller.UsuarioC();
            CargarDatos();
            DataContext = this;
        }



        private void CargarDatos()
        {
            TareasDB = new ObservableCollection<Tarea>(tareaControlador.ObtenerTareas());
            UsuarioDB = new ObservableCollection<Usuario>(usuarioControlador.getUsers());
            ProyectosDB = new ObservableCollection<Proyecto>(proyectoControlador.getProjects());
        }

        private void EliminarTarea(object sender, RoutedEventArgs e)
        {
            var tareaAEliminar = TareasDB.FirstOrDefault(u => u.Id == int.Parse(idTxt.Text));
            Boolean result = tareaControlador.Delete(idTxt.Text);
            if (result)
            {
                MessageBox.Show("Proyecto eliminado", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                TareasDB.Remove(tareaAEliminar);
                resetTable();
                return;
            }
            MessageBox.Show("Proyecto no eliminado, ha sucedido un error", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void CleanInputs()
        {
            idTxt.Text = "";
            nombreTxt.Text = "";
            descripcionTxt.Text = "";
            UsuarioBox.Text = "";
            ProyectoBox.Text = ""; 
        }

        private void resetTable()
        {
            TareasDB.Clear();
            foreach (var item in tareaControlador.ObtenerTareas())
            {
                TareasDB.Add(item);
            };
            CleanInputs();
        }

        private void ActualizarTarea(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void CrearTarea(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void LimpiarInputs(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("hola");
        }
    }
}
