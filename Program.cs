using System;
using System.Threading;

namespace CWI_SellAnalytics
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileWatcher fileWatcher = new FileWatcher();
            fileWatcher.SearchFile();

            while (true)
            {
                Console.WriteLine("Buscando Arquivo");
                Thread.Sleep(5000);
            }
        }
    }
}
