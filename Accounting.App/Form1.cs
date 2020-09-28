using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            frmNewTransaction frmNewTransaction = new frmNewTransaction();
            frmNewTransaction.ShowDialog();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            
            frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();

        }

        private void btnReprtPay_Click(object sender, EventArgs e)
        {
            
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 2;

            frmReport.ShowDialog();
        }

        private void btnReportReceive_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 1 ;

            frmReport.ShowDialog();
        }
    }
}
