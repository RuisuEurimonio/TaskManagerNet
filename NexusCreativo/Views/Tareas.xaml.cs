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
            UsuarioDB = new ObservableCollection<Usuario>(usuarioControlador.ObtenerUsuarios());
            ProyectosDB = new ObservableCollection<Proyecto>(proyectoControlador.ObtenerProyectos());
        }

        private void EliminarTarea(object sender, RoutedEventArgs e)
        {
            var tareaAEliminar = TareasDB.FirstOrDefault(u => u.Id == int.Parse(idTxt.Text));
            Boolean result = tareaControlador.Eliminar(idTxt.Text);
            if (result)
            {
                MessageBox.Show("Tarea eliminada", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                TareasDB.Remove(tareaAEliminar);
                resetTable();
                return;
            }
            MessageBox.Show("Tarea no eliminada, ha sucedido un error", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            Boolean result = tareaControlador.Actualizar(idTxt.Text, nombreTxt.Text, descripcionTxt.Text, (bool)Estado.IsChecked, (DateTime)fechVencimiento.SelectedDate, UsuarioBox.SelectedValue+"", ProyectoBox.SelectedValue+"");
            if (result)
            {
                MessageBox.Show("Tarea actualizado", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Information);
                resetTable();
                return;
            }
            MessageBox.Show("Tarea no actualizado, ha sucedido un error", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Warning);


        }

        private void CrearTarea(object sender, RoutedEventArgs e)
        {
            Models.Tarea result = tareaControlador.Crear(nombreTxt.Text, descripcionTxt.Text, (bool)Estado.IsChecked, (DateTime)fechVencimiento.SelectedDate, UsuarioBox.SelectedValue + "", ProyectoBox.SelectedValue + "");
            if (result != null)
            {
                MessageBox.Show("Tarea creado", "Crear", MessageBoxButton.OK, MessageBoxImage.Information);
                TareasDB.Add(result);
                CleanInputs();
                return;
            }
            MessageBox.Show("Tarea no creado, ha sucedido un error", "Crear", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void LimpiarInputs(object sender, RoutedEventArgs e)
        {
            CleanInputs();
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainW.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is Tarea tareaSeleccionada)
            {
                int selectedId = tareaSeleccionada.Id;
                Tarea tareaSelected = tareaControlador.ObtenerTareaById(selectedId.ToString());
                idTxt.Text = tareaSelected.Id + "";
                nombreTxt.Text = tareaSelected.Nombre;
                descripcionTxt.Text = tareaSelected.Description;
                UsuarioBox.SelectedValue = tareaSelected.Usuario.Id;
                ProyectoBox.SelectedValue = tareaSelected.Proyecto.Id;
                if (tareaSelected.isCompleted == true)
                {
                    Estado.IsChecked = true;
                }
                fechVencimiento.SelectedDate = tareaSeleccionada.Period;
            }
        }
    }
}
