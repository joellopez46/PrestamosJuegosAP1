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
    /// Interaction logic for rEntradas.xaml
    /// </summary>
    public partial class rEntradas : Window
    {
        private Entradas entradas = new Entradas();
        public rEntradas()
        {
            InitializeComponent();
            this.DataContext = entradas;
            var Lista = JuegosBLL.GetList(x => true);
            this.JuegoIdComboBox.ItemsSource = Lista;
            this.JuegoIdComboBox.SelectedValuePath = "JuegoId";
            this.JuegoIdComboBox.DisplayMemberPath = "Descripcion";
            if (Lista.Count > 0)
                this.JuegoIdComboBox.SelectedIndex = 0;

        }
        public void Limpiar()
        {
            entradas = new Entradas();
            this.DataContext = entradas;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (EntradaIdTextBox.Text.Length == 0)
            {
                esValido = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("EntradaId vacio", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                EntradaIdTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }

            if (CantidadTextBox.Text.Length == 0)
            {
                esValido = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Cantidad vacio", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Focus();
                GuardarButton.IsEnabled = true;
            }



            return esValido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(EntradaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var encontrado = EntradasBLL.Buscar(int.Parse(EntradaIdTextBox.Text));
            if (encontrado != null)
            {
                entradas = encontrado;
                this.DataContext = entradas;
            }
            else
            {
                MessageBox.Show("No encontrado.", "No encontrado.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (EntradasBLL.Guardar(entradas))
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
            if (!Regex.IsMatch(EntradaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Id no valido.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (EntradasBLL.Eliminar(int.Parse(EntradaIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar este registro.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
