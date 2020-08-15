﻿using PrestamosJuegosAP1.BLL;
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
    /// Interaction logic for rPrestamos.xaml
    /// </summary>
    public partial class rPrestamos : Window
    {
        private Prestamos prestamos = new Prestamos();

        public rPrestamos()
        {
            InitializeComponent();
            this.DataContext = prestamos;
            JuegoIdComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Descripcion";

            AmigoIdComboBox.ItemsSource = AmigosBLL.GetAmigos();
            AmigoIdComboBox.SelectedValuePath = "AmigoId";
            AmigoIdComboBox.DisplayMemberPath = "Nombres";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = prestamos;
        }

        private void Limpiar()
        {
            this.prestamos = new Prestamos();
            this.DataContext = prestamos;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Prestamos esValido = PrestamosBLL.Buscar(prestamos.PrestamoId);
            return (esValido != null);
        }

        public bool ValidandoAgregar()
        {
            if (JuegoIdComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No a seleccionado un juego.",
                   "Campo Juego", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(CantidadTextBox.Text, "^[1-9]+${1,9}"))
            {
                MessageBox.Show("Cantidad no validad.",
                    "Cantidad no validad", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public bool Validar()
        {
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (AmigoIdComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un amigo.",
                   "Campo Amigo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var encontrado = PrestamosBLL.Buscar(int.Parse(PrestamoIdTextBox.Text));
            if (encontrado != null)
            {
                prestamos = encontrado;
                this.DataContext = prestamos;
            }
            else
            {
                MessageBox.Show("El prestamo no existe.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidandoAgregar())
                return;

            var detalle = new PrestamosDetalle
            {
                Id = 0,
                PrestamoId = int.Parse(PrestamoIdTextBox.Text),
                JuegoId = int.Parse(JuegoIdComboBox.SelectedValue.ToString()),
                Cantidad = int.Parse(CantidadTextBox.Text)
            };

            prestamos.Detalles.Add(detalle);
            Cargar();
            CantidadTextBox.Clear();

        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            var detalle = (PrestamosDetalle)DetalleDataGrid.SelectedItem;
            prestamos.Detalles.RemoveAt(DetalleDataGrid.SelectedIndex);
            Cargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (PrestamosBLL.Guardar(prestamos))
            {
                Limpiar();
                MessageBox.Show("Guardado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Guardar.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PrestamosBLL.Eliminar(int.Parse(PrestamoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar el Registro.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
