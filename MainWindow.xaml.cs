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

            await Task.Run(pBarSleep);

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


        void pBarSleep()
        {

            var temp = Path.GetTempPath();
            var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i <= files.Length; i++)
            {
                Thread.Sleep(120);

                Dispatcher.Invoke(() =>
                {


                    for (var j = 0; j < files.Length; j++)
                    {
                        //scan_info.Document.Blocks.Add(files[j]);
                        //scan_info.AppendText(files[j]);

                        scan_info.Items.Add(files[j]);
                    }

                });
                Dispatcher.Invoke(() =>
               {
                   var valueAct = (files.Length * i / 100);


                   pbar.Value = Convert.ToInt32(valueAct);

               });
            }
        }

        private void webSite_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("https://github.com/adamkhairi/Cleaner");

        }
        private void hideInfo(object sender, RoutedEventArgs e)
        {
            scan_info.Visibility = Visibility.Hidden;
        }
        private void showInfo(object sender, RoutedEventArgs e)
        {
            scan_info.Visibility = Visibility.Visible;

        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
