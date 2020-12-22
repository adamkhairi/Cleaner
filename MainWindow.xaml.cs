//using Microsoft.Extensions.Caching.Memory;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace Ccleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double scanSize;

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

            if (pbar.Value >= 100)
            {
                MessageBox.Show("Scan Complete");

                var temp = Path.GetTempPath();
                DirectoryInfo dir = new DirectoryInfo(temp);
                var filesInDir = dir.EnumerateFiles();
                foreach (var file in filesInDir)
                {
                    if (file.Length > 0)
                    {
                        Console.WriteLine(file.Length.ToString());
                        scanSize += file.Length / 1024 * 2;

                    }
                    else
                    {
                        scanSize = 0;
                    }
                }

                Console.WriteLine(pbar.Value);
                btnClean.IsEnabled = true;
                btnHistory.IsEnabled = true;
                btnUpdate.IsEnabled = true;

                bigTitle.Content = "Scan Completed";
                statisic.Visibility = Visibility.Visible;
                infos.Visibility = Visibility.Hidden;
                progress.Visibility = Visibility.Hidden;

                lastUpdate1.Content = "2 Days ago";
                resultSize.Content = scanSize + " Mo";
                lastScan1.Content = "10/11/2010 20:10:22";
                pcName.Visibility = Visibility.Visible;
                userName.Visibility = Visibility.Visible;
                pcOs.Visibility = Visibility.Visible;

                pcName.Content += $" {Environment.MachineName}.";
                userName.Content += $" {Environment.UserName}.";
                pcOs.Content += $" {Environment.OSVersion}";
            }


        }

        private string[] PBarSleep()
        {

            var temp = Path.GetTempPath();
            var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);
            // var length = files.Length;

            var length = 50;

            for (int i = 0; i <= length; i++)
            {

                Thread.Sleep(80);
                Dispatcher.Invoke(() =>
                {
                    result.Text += $" {i}- {files[i]} {Environment.NewLine}";
                });

                Dispatcher.Invoke(() =>
                {
                    //pbar.Value = CalcPer(i, length);
                    pbar.Value += 100 / length;
                    textProg.Text = Math.Round(pbar.Value, 1) + "%";
                    result.ScrollToEnd();
                });

            }
            return files;
        }
        double CalcPer(double newNum, double originalNum)
        {
            var increase = originalNum - newNum;
            var incPer = increase / originalNum * 100;
            return 100 - incPer;
        }

        private void webSite_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("https://github.com/adamkhairi/Cleaner");

        }
        private void hideInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Hidden;
        }
        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Visible;

        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to Clean files?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var temp = Path.GetTempPath();
                var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);

                using (StreamWriter sw = new StreamWriter("c:/names.txt"))
                {

                    foreach (string s in files)
                    {
                        sw.WriteLine(s);
                    }
                }



                Clear.ClearTempData();
                StreamReader sr = new StreamReader("c:/jamaica.txt");
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    MessageBox.Show(line);
                }
            }
            else
            {
                // close the window 

            }


        }
    }
}
