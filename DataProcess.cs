using CWI_SellAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CWI_SalesAnalytics
{
    class DataProcess
    {
        private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
        public VendedorModel SalesmanProcess(string[] data)
        {
            VendedorModel salesman = new VendedorModel();
            for (int i = 0; i < data.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        int identifier;
                        int.TryParse(data[i], out identifier);
                        salesman.Identifier = identifier;
                        break;
                    case 1:
                        salesman.Document = data[i];
                        break;
                    case 2:
                        salesman.Name = data[i];
                        break;
                    case 3:
                        decimal salary;
                        decimal.TryParse(data[i], NumberStyles.AllowDecimalPoint, culture, out salary);
                        salesman.Salary = salary;
                        break;
                    default:
                        break;
                }
            }


            return salesman;
        }

        public ClienteModel ClientProcess(string[] data)
        {
            ClienteModel client = new ClienteModel();

            for (int i = 0; i < data.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        int identifier;
                        int.TryParse(data[i], out identifier);
                        client.Identifier = identifier;
                        break;
                    case 1:
                        client.Document = data[i];
                        break;
                    case 2:
                        client.Name = data[i];
                        break;
                    case 3:
                        client.BussinessArea = data[i];
                        break;
                    default:
                        break;
                }
            }

            return client;
        }

        public VendaModel SaleProcess(string[] data)
        {
            VendaModel sale = new VendaModel();

            for (int i = 0; i < data.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        int identifier;
                        int.TryParse(data[i], out identifier);
                        sale.Identifier = identifier;
                        break;
                    case 1:
                        int SaleID;
                        int.TryParse(data[i], out SaleID);
                        sale.SaleID = SaleID;
                        break;
                    case 2:
                        sale.Itens = itemProcess(data[i]);
                        break;
                    case 3:
                        sale.Name = data[i];
                        break;
                    default:
                        break;
                }
            }

            return sale;
        }

        public List<ItemModel> itemProcess(string data)
        {
            /*
             [1-10-100,2-30-2.50,3-40-3.10]
             */
            List<ItemModel> item = new List<ItemModel>();
            data = removeStr(data, "[", "]");
            var dataArr = data.Split(',');

            foreach (var vd in dataArr)
            {
                ItemModel itn = new ItemModel();
                var linha = vd.Split('-');
                for (int i = 0; i < linha.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            int ItemID;
                            int.TryParse(linha[i], out ItemID);
                            itn.ItemID = ItemID;
                            break;
                        case 1:
                            int Quantity;
                            int.TryParse(linha[i], out Quantity);
                            itn.Quantity = Quantity;
                            break;
                        case 2:
                            decimal price;
                            decimal.TryParse(linha[i], NumberStyles.AllowDecimalPoint, culture, out price);
                            itn.Price = price;
                            break;
                        default:
                            break;
                    }
                }

                item.Add(itn);
            }


            return item;
        }

        public string removeStr(string sourceString, string removeString1, string removeString2)
        {
            int index = sourceString.IndexOf(removeString1);
            string cleanPath = (index < 0)
                ? sourceString
                : sourceString.Remove(index, removeString1.Length);

            int index1 = cleanPath.IndexOf(removeString2);
            cleanPath = (index1 < 0)
                ? cleanPath
                : cleanPath.Remove(index1, removeString2.Length);


            return cleanPath;

        }
    }
}
