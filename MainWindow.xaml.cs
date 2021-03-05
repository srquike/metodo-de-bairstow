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

namespace metodo_de_bairstow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private IList<Iteracion> iteraciones;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtR.Text) && !string.IsNullOrEmpty(TxtS.Text))
            {
                if (double.TryParse(TxtR.Text, out double r) && double.TryParse(TxtS.Text, out double s))
                {
                    Iteracion iteracion, iteracionAnterior;
                    int i;
                    const double a0 = 3.3, a1 = 0.5, a2 = 2.3, a3 = -1.1, a4 = 1;

                    iteraciones = new List<Iteracion>();
                    iteracionAnterior = new Iteracion();
                    i = 0;

                    do
                    {
                        iteracion = new Iteracion();
                        iteracion.I = i;

                        if (iteracion.I == 0)
                        {
                            iteracion.R = r;
                            iteracion.S = s;
                        }
                        else
                        {
                            iteracion.S = iteracionAnterior.S + iteracionAnterior.ẟS;
                            iteracion.R = iteracionAnterior.R + iteracionAnterior.ẟR;
                        }

                        iteracion.B4 = a4;
                        iteracion.B3 = a3 + (iteracion.R * iteracion.B4);
                        iteracion.B2 = a2 + (iteracion.R * iteracion.B3) + (iteracion.S * iteracion.B4);
                        iteracion.B1 = a1 + (iteracion.R * iteracion.B2) + (iteracion.S * iteracion.B3);
                        iteracion.B0 = a0 + (iteracion.R * iteracion.B1) + (iteracion.S * iteracion.B2);

                        iteracion.C4 = iteracion.B4;
                        iteracion.C3 = iteracion.B3 + (iteracion.R * iteracion.C4);
                        iteracion.C2 = iteracion.B2 + (iteracion.R * iteracion.C3) + (iteracion.S * iteracion.C4);
                        iteracion.C1 = iteracion.B1 + (iteracion.R * iteracion.C2) + (iteracion.S * iteracion.C3);

                        iteracion.ẟR = ((iteracion.C3 * iteracion.B0) - (iteracion.C2 * iteracion.B1)) / (Math.Pow(iteracion.C2, 2) - (iteracion.C1 * iteracion.C3));
                        iteracion.ẟS = ((iteracion.C1 * iteracion.B1) - (iteracion.C2 * iteracion.B0)) / (Math.Pow(iteracion.C2, 2) - (iteracion.C1 * iteracion.C3));

                        iteraciones.Add(iteracion);
                        iteracionAnterior = iteracion;
                        i++;

                    } while (Math.Round(iteracion.ẟR, 3) != 0 && Math.Round(iteracion.ẟS, 3) != 0);

                    DgIteraciones.ItemsSource = iteraciones;


                }
            }
        }

        private void AplicarBairstow(Iteracion iteracion)
        {

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtR.Clear();
            TxtS.Clear();
            DgIteraciones.ItemsSource = null;
            TxtR.Focus();
        }
    }
}
