//using Microsoft.Extensions.Caching.Memory;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


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
            infos.Visibility = Visibility.Visible;
            // Clear.ClearTempData(temp);

            await Task.Run(PBarSleep);

            if (pbar.Value > 100)
            {
                MessageBox.Show("Scan Complete");

                Console.WriteLine(pbar.Value);
                btnClean.IsEnabled = true;
                btnHistory.IsEnabled = true;
                btnUpdate.IsEnabled = true;

                bigTitle.Content = "Scan Completed";
                scan.Visibility = Visibility.Visible;
                statisic.Visibility = Visibility.Visible;
                infos.Visibility = Visibility.Hidden;
                progress.Visibility = Visibility.Hidden;

                lastUpdate1.Content = "2 Days ago";
                resultSize.Content = "1.3 GB";
                lastScan1.Content = "Friday, December 18 2020";
            }


        }

        private string[] PBarSleep()
        {

            var temp = Path.GetTempPath();
            var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);
            var length = files.Length;
            //var length = 150;

            for (int i = 0; i <= length; i++)

            {
                Thread.Sleep(100);

                Dispatcher.Invoke(() =>
                {
                    result.Text += $" {i}- {files[i]} {Environment.NewLine}";
                });

                Dispatcher.Invoke(() =>
                {
                    pbar.Value = CalcPer(i, length);
                    textProg.Text = Math.Round(pbar.Value , 1) + "%";
                    result.ScrollToEnd();
                });

            }
            return files;
        }
        double CalcPer(double a, double b)
        {
            var i = b - a;
            var inc = i / b * 100;
            return 100 - inc;
        }

        private void webSite_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("https://github.com/adamkhairi/Cleaner");

        }
        private void hideInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Hidden;
        }
        private void showInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;

        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            //var temp = Path.GetTempPath();
            //var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);

            //Clear.ClearTempData(files);
        }
    }
}
