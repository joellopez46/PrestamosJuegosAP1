﻿using PrestamosJuegosAP1.BLL;
using PrestamosJuegosAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestamosJuegosAP1.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cEntradas.xaml
    /// </summary>
    public partial class cEntradas : Window
    {
        public cEntradas()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
       {
            var listado = new List<Entradas>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //EntradaId
                        listado = EntradasBLL.GetAmigos(a => a.EntradaId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 1: //JuegoId                       
                        listado = EntradasBLL.GetAmigos(a => a.JuegoId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 2: //Cantidad                       
                        listado = EntradasBLL.GetAmigos(a => a.Cantidad == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = EntradasBLL.GetAmigos(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = EntradasBLL.GetAmigos(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = EntradasBLL.GetAmigos(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
