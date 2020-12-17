using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ccleaner
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

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void scan_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Start scan");
         

            btnClean.IsEnabled = false;
            btnHistory.IsEnabled = false;
            btnUpdate.IsEnabled = false;

            bigTitle.Content = "Scan in progress...";
            scan.Visibility = Visibility.Hidden;
            statisic.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Visible;


            for (int i = 1; i <= 100; i++)
            {
                pbar.Value++;
            }


           


        }

        private void pbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        Duration duration = new Duration(TimeSpan.FromSeconds(2));
            DoubleAnimation doubleAnimation = new DoubleAnimation(pbar.Value + 10, duration);
            pbar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            //Thread.Sleep(10000);

        }
    }
}
