using PrestamosJuegosAP1.UI.Registros;
using System;
using System.Collections.Generic;
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

namespace PrestamosJuegosAP1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void Registroamigo_Click(object sender, RoutedEventArgs e)
        {
             rAmigos ramigos = new rAmigos();
            ramigos.Show();
        }

        private void Registroentrada_Click(object sender, RoutedEventArgs e)
        {
            rEntradas rentradas = new rEntradas();
            rentradas.Show();

        }

        private void Registrojuego_Click(object sender, RoutedEventArgs e)
        {
            rJuegos rjuegos = new rJuegos();
            rjuegos.Show();
        }

        private void Registroprestamo_Click(object sender, RoutedEventArgs e)
        {
            rPrestamos rprestamos = new rPrestamos();
            rprestamos.Show();
        }
    }
}
