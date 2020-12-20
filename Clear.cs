using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ccleaner
{
    class Clear
    {
        public static void ClearTempData(string[] temp)
        {
            foreach (var file in temp)
            {
                try
                {
                   File.Delete(file);
                   Directory.Delete(file);

                    Console.WriteLine(file);
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

        }
    }
}
