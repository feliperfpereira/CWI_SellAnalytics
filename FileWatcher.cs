using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CWI_SellAnalytics
{
    public class FileWatcher
    {

        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);

        public void SearchFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = string.Format(@"{0}\Data\In", Environment.CurrentDirectory);

            watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;

        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {

            //Caso arquvios sejam muito grandes, consigo rodar vários ao mesmo tempo
            //Thread t = new Thread(() =>
            //{
            //    FileReader fileReader = new FileReader();
            //    fileReader.fileReader(e.FullPath);
            //});
            //t.Start();

            //if (semaphoreSlim.Wait(TimeSpan.FromSeconds(3)))
            //{
            //    Task.Factory.StartNew((index) =>
            //    {
            //            //Simulate processiong  
            //            FileReader fileReader = new FileReader();
            //        fileReader.fileReader(e.FullPath);
            //        Console.WriteLine(Path.GetFileNameWithoutExtension(e.FullPath));
            //    }, 0).ContinueWith(task => { semaphoreSlim.Release(); });
            //}
            //else
            //{
            //    Console.WriteLine("Wait time expired before semaphore released");
            //}


            int workerThreadCount;
            int ioThreadCount;

            ThreadPool.GetMinThreads(out workerThreadCount, out ioThreadCount);


            ThreadPool.SetMaxThreads(10, 10);

            Task t1 = Task.Run(() =>
            {
                FileReader fileReader = new FileReader();
                fileReader.fileReader(e.FullPath);
                Console.WriteLine(Path.GetFileNameWithoutExtension(e.FullPath));
            });

        }
    }
}
