using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomers : Form
    {

        public int customerId = 0;


        public frmAddOrEditCustomers()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {

            if (txtMobile.Text != "" || txtName.Text != "")
            {

                

                using (UnitOfWork db = new UnitOfWork())
                {


                    string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                    string path = Application.StartupPath + "/Images/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    pcCustomer.Image.Save(path + imageName);


                    Customers customer = new Customers()
                    {
                        FullName = txtName.Text,
                        Mobile = txtMobile.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        CustomerImage = imageName
                    };



                    if (customerId == 0)
                    {

                        db.CustomerRepository.InsertCustomer(customer);
                    }
                    else
                    {
                        customer.CustomerID = customerId;
                        db.CustomerRepository.UpdateCustomer(customer);
                    }
                    db.Save();
                    DialogResult = DialogResult.OK;




                }

            }




        
            else
            {
                MessageBox.Show("نام و موبایل را وارد کنید");

            }

}

        private void btnSelectPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() ==DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }

        private void frmAddOrEditCustomers_Load(object sender, EventArgs e)
        {
            if (customerId != 0)
            {
                using (UnitOfWork db = new UnitOfWork())
                {


                    this.Text = "ویرایش شخص";
                    btnAddCustomer.Text = "ویرایش";
                    var customer = db.CustomerRepository.GetCustomerById(customerId);

                    txtAddress.Text = customer.Address;
                    txtEmail.Text = customer.Email;
                    txtMobile.Text = customer.Mobile;
                    txtName.Text = customer.FullName;

                    pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;


                }
            }

        }
    }
}
