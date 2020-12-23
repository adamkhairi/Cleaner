//using Microsoft.Extensions.Caching.Memory;

using Humanizer.Bytes;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
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
        long totalSize;
        //double scanSize;

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


                Console.WriteLine(pbar.Value);
                btnClean.IsEnabled = true;
                btnHistory.IsEnabled = true;
                btnUpdate.IsEnabled = true;

                bigTitle.Content = "Scan Completed";
                statisic.Visibility = Visibility.Visible;
                infos.Visibility = Visibility.Hidden;
                progress.Visibility = Visibility.Hidden;

                lastUpdate1.Content = "2 Days ago";
                resultSize.Content = totalSize.ToPrettySize();
                lastScan1.Content = "10/11/2010 20:10:22";
                pcName.Visibility = Visibility.Visible;
                userName.Visibility = Visibility.Visible;
                pcOs.Visibility = Visibility.Visible;

                pcName.Content += $" {Environment.MachineName}.";
                userName.Content += $" {Environment.UserName}.";
                pcOs.Content += $" {Environment.OSVersion}";
            }


        }

        private void PBarSleep()
        {

            //var temp = Path.GetTempPath();
            //DirectoryInfo dir = new DirectoryInfo(temp);

            //var length = dir.GetFiles("*.*", SearchOption.AllDirectories).Length;
            //var filesInDir = dir.EnumerateFiles();
            //long i = 0;

            //foreach (var file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            //{
            //    Thread.Sleep(10);
            //    Dispatcher.Invoke(() =>
            //    {
            //        result.Text += $" {i}- {file.FullName} {Environment.NewLine}";
            //        pbar.Value += 100 / length;
            //        //pbar.Value = CalcPer(i, length);
            //        textProg.Text = Math.Round(pbar.Value, 1) + "%";
            //        result.ScrollToEnd();
            //        i++;
            //    });
            //    if (file.Length > 0)
            //    {
            //        Console.WriteLine(file.Length.ToString());    
            //        this.totalSize += Convert.ToInt64(file.Length);

            //    }
            //    else
            //    {
            //        this.totalSize = 0;
            //    }
            //}

            var temp = Path.GetTempPath();
            var dir = new DirectoryInfo(temp);
            var files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            //var length = files.Length;

            var length = 150;
            
            for (int i = 0; i <= length; i++)
            {

                Thread.Sleep(10);
                Dispatcher.Invoke(() =>
                {
                    result.Text += $" {i}- {files[i]} {Environment.NewLine}";
                    //pbar.Value += 100 / length;
                    pbar.Value = CalcPer(i, length);
                    textProg.Text = Math.Round(pbar.Value, 1) + "%";
                    result.ScrollToEnd();
                });

                if (length > 0)
                {
                    Console.WriteLine(files[i].Length.ToString());
                    this.totalSize += Convert.ToInt64(files[i].Length);

                }
                else
                {
                    this.totalSize = 0;
                }

            }

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



                Clear.ClearTempData();
                HistoryHundler();
            }
            else
            {
                // close the window 

            }


        }

        private void HistoryHundler()
        {
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            var time = DateTime.Now.ToString("HH-mm");
            var dir = Directory.CreateDirectory("c:/Cleaner-logs/" + date);

            try
            {
                var temp = Path.GetTempPath();
                var files = Directory.GetFiles(temp, "*.*", SearchOption.AllDirectories);

                Console.WriteLine(dir);

                using (StreamWriter sw = new StreamWriter($@"c:\Cleaner-logs\{dir}\{time}.txt "))

                {
                    //for (int i = 0; i < files.Length; i++)
                    //{
                    //    sw.WriteLine("[" + i + "]" + " -- " + files[i]);

                    //}
                    sw.WriteLine($"Last scan was {date} at {time} Totale Deleted was :{totalSize.ToPrettySize()}");
                }

                //Clear.ClearTempData();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                StreamWriter msg = File.AppendText($@"c:\Cleaner-logs\{dir}\[{time}].txt ");

                msg.WriteLine(e.Message);
                throw;
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            var dir = Directory.CreateDirectory("c:/Cleaner-logs/" + date);

            Process.Start(@"c:\Cleaner-logs");

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("yourpastebinlink").Contains("1.5.0"))
                {
                    if (MessageBox.Show("Looks like there is an update! Do you want to download it?", "Cleaner", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        using (var client = new WebClient())
                        {
                            
                            Process.Start("CleanerUpdater.exe");
                            this.Close();
                        }
                    }
                     
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
