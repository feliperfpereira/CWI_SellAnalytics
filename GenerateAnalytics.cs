using CWI_SalesAnalytics.Models;
using CWI_SellAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CWI_SalesAnalytics
{
    class GenerateAnalytics
    {
        public SalesAnalyticsModel AnalyticProcess(List<VendaModel> vendas,List<ClienteModel> clientes, List<VendedorModel> vendedors)
        {
            SalesAnalyticsModel salesAnalytics = new SalesAnalyticsModel();

            salesAnalytics.QtdClients = getQtdClients(clientes);
            salesAnalytics.QtdSalesman = getQtdSalesman(vendedors);
            salesAnalytics.HighSaleID = getHighSale(vendas);
            salesAnalytics.WorstSalesman =  getWorstSalesman(vendas, vendedors);

            return salesAnalytics;
        }

        private VendedorModel getWorstSalesman(List<VendaModel> vendas, List<VendedorModel> vendedors)
        {
            int idHighSale = 0;
            string worst;
            ComputeData(vendas, out idHighSale, out worst);
            VendedorModel vendedor = new VendedorModel();

            return vendedors.FirstOrDefault(q => q.Name == worst);
        }

        private int getHighSale(List<VendaModel> vendas)
        {
            int idHighSale = 0;
            string worst;
            ComputeData(vendas,out idHighSale, out worst);

            return idHighSale;
        }

        private void ComputeData(List<VendaModel> vendas,out int idHighSale,out string worstSalesman)
        {
            idHighSale = 0;
            worstSalesman = "";
            decimal highSaleValue = 0;
            decimal lowSaleValue = 0;

            foreach (var venda in vendas)
            {
                decimal saleValue = 0;
                foreach (var item in venda.Itens)
                {
                    saleValue += item.Price * item.Quantity;
                }
                if (lowSaleValue == 0)
                {
                    lowSaleValue = saleValue;
                }

                if (highSaleValue < saleValue)
                {
                    highSaleValue = saleValue;
                    idHighSale = venda.SaleID;
                }

                if (lowSaleValue > saleValue)
                {
                    lowSaleValue = saleValue;
                    worstSalesman = venda.Name;
                }
            }
        }

        private int getQtdSalesman(List<VendedorModel> vendedors)
        {
            return vendedors.Count;
        }

        private int getQtdClients(List<ClienteModel> clientes)
        {
            return clientes.Count;
        }
    }
}
