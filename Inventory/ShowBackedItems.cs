﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class ShowBackedItems : Form
    {
        public ShowBackedItems()
        {
            InitializeComponent();
        }

       

      

      

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            MyContext myContext = new MyContext();
            var report = myContext.purchaseInvoiceProducts.Where(p => p.PurchaseInvoice.Date >= dp_startDate.Value && p.PurchaseInvoice.Date <= dp_endDate.Value && p.PurchaseInvoice.supplier == null).Select(p => new
            { SalesMan = p.PurchaseInvoice.SalesMan.Name, p.Product.Name,p.CountofProduct,p.PurchasingPrice , p.PriceofTotalProduct ,p.PurchaseInvoice.Date }).ToList();

            dataGridView1.DataSource = report;
        }
        
        private void BtnReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowBackedItems_Load(object sender, EventArgs e)
        {
            MyContext myContext = new MyContext();
            var report = myContext.purchaseInvoiceProducts.Where(p=> p.PurchaseInvoice.supplier == null).Select(p => new
            { SalesMan = p.PurchaseInvoice.SalesMan.Name, p.Product.Name, p.CountofProduct, p.PurchasingPrice, p.PriceofTotalProduct, p.PurchaseInvoice.Date }).ToList();

            dataGridView1.DataSource = report;
        }
    }
}
