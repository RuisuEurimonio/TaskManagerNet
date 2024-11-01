using NexusCreativo.Controller;
using NexusCreativo.Models;
using NexusCreativo.Models.Models.DAO;
using Org.BouncyCastle.Utilities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NexusCreativo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Usuario> UsuariosDB { get; set; }
        private UsuarioC UsuarioController;

        public MainWindow()
        {
            this.UsuarioController = new UsuarioC();
            InitializeComponent();
            CargarDatos();
            DataContext = this;
        }

        private void CargarDatos()
        {
            this.UsuariosDB = new ObservableCollection<Usuario>(UsuarioController.getUsers());
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Models.Usuario result = UsuarioController.Create(nameTxt.Text, rolTxt.Text);
            if (result != null)
            {
                MessageBox.Show("Usuario creado", "Crear", MessageBoxButton.OK, MessageBoxImage.Information);
                UsuariosDB.Add(result);
                CleanInputs();
                return;
            }
            MessageBox.Show("Usuario no creado, ha sucedido un error", "Crear", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var usuarioAEliminar = UsuariosDB.FirstOrDefault(u => u.Id == int.Parse(idTxt.Text));
            Boolean result = UsuarioController.Delete(idTxt.Text);
            if (result)
            {
                MessageBox.Show("Usuario eliminado", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information);
                UsuariosDB.Remove(usuarioAEliminar);
                CleanInputs();
                return;
            }
            MessageBox.Show("Usuario no eliminado, ha sucedido un error", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            Boolean result = UsuarioController.Actualizar(idTxt.Text, nameTxt.Text, rolTxt.Text);
            if (result)
            {
                MessageBox.Show("Usuario actualizado", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Usuario no actualizado, ha sucedido un error", "Actualizar", MessageBoxButton.OK, MessageBoxImage.Warning);
            CargarDatos();
        }

        private void openTaks(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void openProjects(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hola");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is Usuario selectedUser)
            {
                int selectedId = selectedUser.Id;
                Usuario usuarioSelected = UsuarioController.getUserById(selectedId.ToString());
                idTxt.Text = usuarioSelected.Id + "";
                nameTxt.Text = usuarioSelected.Name;
                rolTxt.Text = usuarioSelected.Rol;
            }
            
        }

        private void Clean(object sender, RoutedEventArgs e)
        {
            CleanInputs();
        }

        private void CleanInputs()
        {
            idTxt.Text = "";
            nameTxt.Text = "";
            rolTxt.Text = "";
        }

        private void idTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
