using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace BankManagement
{
    public partial class Registration : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        
        public Registration()
        {
            InitializeComponent();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            RegistrationCls reg = new RegistrationCls();
            reg.FirstName = txtFirstName.Text;
            reg.LastName = txtLastName.Text;
            reg.DOB = Convert.ToDateTime(dtDOB.Text);
            if (rbMale.Checked)
            {
                reg.Gender = rbMale.Text;
            }
            else
            {
                reg.Gender = rbFemale.Text;
            }

            reg.Address = txtAddress.Text;
            reg.ContactNo = txtPhone.Text;
            reg.Email = txtEmail.Text;
            reg.Username = txtUsername.Text;
            reg.Password = txtPassword.Text;

            if (txtUsername.Text == "" || txtPassword.Text == "")
                MessageBox.Show("This field is required");
            else if (txtPassword.Text != txtPassConfrm.Text)
                MessageBox.Show("Pasword doesn't match");
            else if (txtPassword.Text.Length < 4)
                MessageBox.Show("Password must contain more than 8 characters...");
            else if (txtPhone.Text.Length != 11)
                MessageBox.Show("Phone number contains 11 digits. ");
            else
            {

                using (var con = new SqlConnection(conStr))
                {
                    int count = 0;
                    var cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText= "Insert into Registration(FirstName, LastName,DateOfBirth,Gender, Address,ContactNo,EmailAddress,Username, Password)" +
                    "VALUES('" + reg.FirstName + "','" + reg.LastName + "','" + reg.DOB + "','" + reg.Gender + "','" + 
                    reg.Address + "','" + reg.ContactNo + "','" + reg.Email + "','" + reg.Username + "','" + reg.Password + "')";
                    con.Open();

                    count = cmd.ExecuteNonQuery();
                    if (count>0)
                    {
                        MessageBox.Show("Congratulation! Registration Completed successfully.");

                    }
                    else
                    {
                        MessageBox.Show("An error occured...");

                    }
                }
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }       

    }
}