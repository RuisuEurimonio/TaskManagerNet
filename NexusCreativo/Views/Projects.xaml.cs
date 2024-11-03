using NexusCreativo.Models;
using NexusCreativo.Models.Models.DAO;
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
    /// Lógica de interacción para Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        public MainWindow MainW { set; get; }
        public ObservableCollection<Proyecto> ProyectoDB { set; get; }
        private Controller.ProyectoC proyectoController;

        public Projects()
        {
            this.proyectoController = new Controller.ProyectoC();
            InitializeComponent();
        }

        public Projects(MainWindow mainWindow)
        {
            this.MainW = mainWindow;
            this.proyectoController = new Controller.ProyectoC();
            CargarDatos();
            DataContext = this;
            InitializeComponent();
        }

        private void CargarDatos()
        {
            this.ProyectoDB = new ObservableCollection<Proyecto>(proyectoController.getProjects());
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            var proyectoAEliminar = ProyectoDB.FirstOrDefault(u => u.Id == int.Parse(idTxt.Text));
            Boolean result = proyectoController.Delete(idTxt.Text);
            if (result)
            {
                MessageBox.Show("Proyecto eliminado", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                ProyectoDB.Remove(proyectoAEliminar);
                resetTable();
                return;
            }
            MessageBox.Show("Proyecto no eliminado, ha sucedido un error", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ActualizarProject(object sender, RoutedEventArgs e)
        {
            Boolean result = proyectoController.Actualizar(idTxt.Text, projectTxt.Text, descriptionTxt.Text);
            if (result)
            {
                MessageBox.Show("Proyecto actualizado", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Information);
                resetTable();
                return;
            }
            MessageBox.Show("Proyecto no actualizado, ha sucedido un error", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private void CleanInputs()
        {
            idTxt.Text = "";
            projectTxt.Text = "";
            descriptionTxt.Text = "";
        }

        private void resetTable()
        {
            ProyectoDB.Clear();
            foreach (var item in proyectoController.getProjects())
            {
                ProyectoDB.Add(item);
            };
            CleanInputs();
        }

        private void CreateProject(object sender, RoutedEventArgs e)
        {
            Models.Proyecto result = proyectoController.Create(projectTxt.Text, descriptionTxt.Text);
            if (result != null)
            {
                MessageBox.Show("Proyecto creado", "Crear", MessageBoxButton.OK, MessageBoxImage.Information);
                ProyectoDB.Add(result);
                CleanInputs();
                return;
            }
            MessageBox.Show("Proyecto no creado, ha sucedido un error", "Crear", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is Usuario selectedUser)
            {
                int selectedId = selectedUser.Id;
                Proyecto usuarioSelected = proyectoController.getProjectById(selectedId.ToString());
                idTxt.Text = usuarioSelected.Id + "";
                projectTxt.Text = usuarioSelected.Nombre;
                descriptionTxt.Text = usuarioSelected.Description;
            }
        }

        private void CleanProjects(object sender, RoutedEventArgs e)
        {
            CleanInputs();
        }

        private void closeTask(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainW.Show();
        }
    }
}
