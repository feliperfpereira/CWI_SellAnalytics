using CWI_SalesAnalytics;
using CWI_SalesAnalytics.Models;
using CWI_SellAnalytics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace CWI_SellAnalytics
{
    class FileReader
    {
        private string line;
        DataProcess DataProcess = new DataProcess();
        GenerateAnalytics generateAnalytics = new GenerateAnalytics();

        public void fileReader(string path)
        {

            List<VendedorModel> Salesman = new List<VendedorModel>();
            List<ClienteModel> Client = new List<ClienteModel>();
            List<VendaModel> Sale = new List<VendaModel>();
            Stream stream = null;

            try
            {
                FileInfo fl = new FileInfo(path); 
                while (IsFileLocked(fl))
                {
                    Thread.Sleep(2000);
                }

                stream = new FileStream(path, FileMode.OpenOrCreate);

                // Open the text file using a stream reader.
                using (var sr = new StreamReader(stream))
                {
                    stream = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var lineArr = line.Split("ç");
                        if (line.Length > 0)
                        {
                            switch (line.Split("ç")[0])
                            {

                                case "001":
                                    Salesman.Add(DataProcess.SalesmanProcess(lineArr));
                                    break;
                                case "002":
                                    Client.Add(DataProcess.ClientProcess(lineArr));
                                    break;
                                case "003":
                                    Sale.Add(DataProcess.SaleProcess(lineArr));
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;

                            }
                        }


                    }

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

            createOutFile(generateAnalytics.AnalyticProcess(Sale, Client, Salesman), Path.GetFileNameWithoutExtension(path));
        }

        private void createOutFile(SalesAnalyticsModel salesAnalyticsModel, string fileNameIN)
        {

            System.IO.Directory.CreateDirectory(string.Format(@"{0}\Data\Out", Environment.CurrentDirectory));
            string pathString = string.Format(@"{0}\Data\Out", Environment.CurrentDirectory);
            string fileName = string.Format("{0}_OUT.json", fileNameIN);

            pathString = System.IO.Path.Combine(pathString, fileName);

            if (!System.IO.File.Exists(pathString))
            {

                var responseData = salesAnalyticsModel;
                string jsonData = JsonConvert.SerializeObject(responseData, Formatting.None);
                System.IO.File.WriteAllText(pathString, jsonData);
            }
            else
            {
                Console.WriteLine("Aquivo \"{0}\" já existe.", fileName);
                return;
            }
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

    }
}

