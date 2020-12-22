using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Ccleaner
{
    class Clear
    {
        public static void ClearTempData()
        {

            var tempF = Path.GetTempPath();

            //var tempt = @"‪C:\Windows\Temp";
            MessageBox.Show(tempF);
            DirectoryInfo dir = new DirectoryInfo(tempF);
            var filesInDir = dir.GetFiles();


            foreach (var file in filesInDir)
            {
                try
                {
                    //file.Delete();
                    //dir.Delete();
                    Console.WriteLine(file.FullName);
                    Console.WriteLine(file.DirectoryName);

                    File.Delete(file.FullName);
                    Directory.Delete(file.DirectoryName);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
