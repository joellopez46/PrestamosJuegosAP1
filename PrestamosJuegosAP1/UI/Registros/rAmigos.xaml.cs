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
    /// Interaction logic for rAmigos.xaml
    /// </summary>
    public partial class rAmigos : Window
    {
        Amigos amigos = new Amigos();
        public rAmigos()
        {
            InitializeComponent();
            this.DataContext = amigos;
        }

        private void Limpiar()
        {
            this.amigos = new Amigos();
            this.DataContext = amigos;
        }
        public bool Validar()
        {
            if (!Regex.IsMatch(AmigoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (NombresTextBox.Text.Length == 0 || EmailTextBox.Text.Length == 0 || CelularTextBox.Text.Length == 0 ||
                DireccionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Existen Campos vacíos..", "Campos vacíos",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!Regex.IsMatch(NombresTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo carácteres alfabeticos.", "Nombre no válido.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!Regex.IsMatch(EmailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                MessageBox.Show("Este correo electrónico no es válida.", "Campo Email.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (TelefonoTextBox.Text.Length != 0 && !Regex.IsMatch(TelefonoTextBox.Text, @"\d{3}\-\d{3}\-\d{4}"))
            {
                MessageBox.Show("No tiene el Formato: 000-000-0000", "Número Teléfono no válido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!Regex.IsMatch(CelularTextBox.Text, @"\d{3}\-\d{3}\-\d{4}"))
            {
                MessageBox.Show("No tiene el Formato: 000-000-0600", "Número celular no válido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(AmigoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no Valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var encontrado = AmigosBLL.Buscar(int.Parse(AmigoIdTextBox.Text));
            if (encontrado != null)
            {
                amigos = encontrado;
                this.DataContext = amigos;
            }
            else
            {
                MessageBox.Show("Amigo No existe.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (AmigosBLL.Guardar(amigos))
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
            if (!Regex.IsMatch(AmigoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (AmigosBLL.Eliminar(int.Parse(AmigoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Se Elimino el Registro.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar este registro.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}