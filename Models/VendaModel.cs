using System;
using System.Collections.Generic;
using System.Text;

namespace CWI_SellAnalytics.Models
{
    class VendaModel : Base
    {
        public int SaleID { get; set; }
        public List<ItemModel> Itens { get; set; }
        public string SalesmanName { get; set; }

    }
}
