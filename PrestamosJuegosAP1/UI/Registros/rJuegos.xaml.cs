using PrestamosJuegosAP1.BLL;
using PrestamosJuegosAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestamosJuegosAP1.UI.Registros
{
    /// <summary>
    /// Interaction logic for rJuegos.xaml
    /// </summary>
    public partial class rJuegos : Window
    {
        private Juegos juegos = new Juegos();

        public rJuegos()
        {
            InitializeComponent();
            this.DataContext = juegos;
        }
        public void Limpiar()
        {
            juegos = new Juegos();
            this.DataContext = juegos;
        }

        //Metodo validar
        public bool esValido()
        {
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no Valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DescripcionTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0)
            {
                MessageBox.Show("Campos vacíos.", "Campos vacíos",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

           
            return true;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no Valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var encontrado = JuegosBLL.Buscar(int.Parse(JuegoIdTextBox.Text));
            if (encontrado != null)
            {
                juegos = encontrado;
                this.DataContext = juegos;
            }
            else
            {
                MessageBox.Show("No se encontro el juego.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!esValido())
                return;

            if (JuegosBLL.Guardar(juegos))
            {
                Limpiar();
                MessageBox.Show("Guardado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se Guardo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (JuegosBLL.Eliminar(int.Parse(JuegoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar este Registro.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
