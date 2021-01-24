using CWI_SellAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CWI_SalesAnalytics.Models
{
    class SalesAnalyticsModel
    {
        public int QtdClients { get; set; }
        public int QtdSalesman { get; set; }
        public int HighSaleID { get; set; }
        public VendedorModel WorstSalesman { get; set; }
    }
}
