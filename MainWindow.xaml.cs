using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using Xamarin.Essentials;

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

        private async void scan_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Start scan");
            btnClean.IsEnabled = false;
            btnHistory.IsEnabled = false;
            btnUpdate.IsEnabled = false;

            bigTitle.Content = "Scan in progress...";
            scan.Visibility = Visibility.Hidden;
            statisic.Visibility = Visibility.Hidden;
            progress.Visibility = Visibility.Visible;

           await Task.Run(pBarSleep);
           
            if (pbar.Value == 100) { 
                   Console.WriteLine(pbar.Value);
                   btnClean.IsEnabled = true;
                   btnHistory.IsEnabled = true;
                   btnUpdate.IsEnabled = true;

                   bigTitle.Content = "Scan Completed";
                   scan.Visibility = Visibility.Visible;
                   statisic.Visibility = Visibility.Visible;
                   progress.Visibility = Visibility.Hidden;
                lastUpdate1.Content = "2 Days ago";
                resultSize.Content = "1.3 GB";
                lastScan1.Content = "Friday, December 18 2020";
               }
            
            
        }
        void pBarSleep()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(30);
                Dispatcher.Invoke(() =>
                {
                    pbar.Value = i;
                });
            }
        }

        private void webSite_Click(object sender, RoutedEventArgs e)
        {
    
           Process.Start("https://github.com/adamkhairi/Cleaner");
        }
    }
}
