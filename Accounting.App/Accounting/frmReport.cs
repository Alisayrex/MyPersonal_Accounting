using Accounting.DataLayer.Context;
using Accounting.Utility;
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
    public partial class frmReport : Form
    {

        public int TypeID = 0;


        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها";

            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            filter();

        }

        void filter()
        {


            using (UnitOfWork db = new UnitOfWork())
            {
                var result = db.AccountingRepository.Get(a => a.TypeID == TypeID);
                // dgReprt.AutoGenerateColumns = false;
                // dgReprt.DataSource = result; 
                dgReprt.Rows.Clear();
                foreach (var accounting in result)
                {
                    string customerName = db.CustomerRepository.GetCustomerNameById(accounting.CustomerID);
                    dgReprt.Rows.Add(accounting.ID, customerName, accounting.Amount, accounting.DateTitle.ToShamsi(), accounting.Description);


                }

            }



        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReprt.CurrentRow != null)
            {

                int id = int.Parse(dgReprt.CurrentRow.Cells[0].Value.ToString());
                if (MessageBox.Show("آیا از حذف مطمئن هستید ؟", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingRepository.Delete(id);
                        db.Save();
                        filter();

                    }



                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReprt.CurrentRow != null)
            {

                int id = int.Parse(dgReprt.CurrentRow.Cells[0].Value.ToString());
                frmNewTransaction frmNew = new frmNewTransaction();
                frmNew.AccountID = id; 
                if(frmNew.ShowDialog()== DialogResult.OK){
                    filter(); 
                }
            }
        }
    }
}
