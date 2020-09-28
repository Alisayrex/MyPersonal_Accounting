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
using ValidationComponents;



namespace Accounting.App
{
    public partial class frmNewTransaction : Form
    {
        public int AccountID = 0; 

        UnitOfWork db = new UnitOfWork();
        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {

            dgvCustomers2.AutoGenerateColumns = false;
            dgvCustomers2.DataSource = db.CustomerRepository.GetNameCustomers();
            if(AccountID != 0)
            {
                var account = db.AccountingRepository.GetById(AccountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description;
                txtName.Text = db.CustomerRepository.GetCustomerNameById(account.CustomerID); 
                if(account.TypeID ==1)
                {
                    rbReceive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
            }
        }

        private void dgvCustomers2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers2.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
        }

        private void dgvCustomers2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers2.CurrentRow.Cells[0].Value.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbPay.Checked || rbReceive.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting()
                    {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        CustomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                        TypeID = (rbReceive.Checked) ? 1 : 2,
                        DateTitle = DateTime.Now,
                        Description = txtDescription.Text

                    };
                    if(AccountID == 0)
                    {
                        db.AccountingRepository.Insert(accounting);
                    }
                    else
                    {
                        accounting.ID = AccountID;
                        db.AccountingRepository.Update(accounting);
                    }
                   
                    db.Save();
                    DialogResult = DialogResult.OK; 
                }
                else
                {
                    MessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
                }
            }
        }
    }
}
