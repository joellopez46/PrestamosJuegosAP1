using PrestamosJuegosAP1.BLL;
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
    /// Interaction logic for cPrestamos.xaml
    /// </summary>
    public partial class cPrestamos : Window
    {
        public cPrestamos()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Prestamos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //PrestamoId
                        listado = PrestamosBLL.GetPrestamos(p => p.PrestamoId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 1: //AmigoId                       
                        listado = PrestamosBLL.GetPrestamos(p => p.AmigoId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 2: //Observacion                       
                        listado = PrestamosBLL.GetPrestamos(p => p.Observacion.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;

                    case 3: //CantidadJuegos                       
                        listado = PrestamosBLL.GetPrestamos(p => p.CantidadJuegos == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = PrestamosBLL.GetPrestamos(c => true);
            }

            if (DesdeDataPicker.SelectedDate != null)
                listado = PrestamosBLL.GetPrestamos(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = PrestamosBLL.GetPrestamos(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
