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
    public partial class Login : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(conStr))
            {

                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string username=textUsername.Text;
                string password = textpassword.Text;
                cmd.CommandText = "Select Username,Password from Registration " +
                    "where Username='" + username+ "' and Password='" + password+"'";
                conn.Open();
                try
                {
                    var dt = new DataTable();
                    var rdr = cmd.ExecuteReader();
                    dt.Load(rdr, LoadOption.Upsert);
                    if (dt.Rows.Count > 0)
                    {
                        MainPage page = new MainPage();
                        this.Hide();
                        page.Show();
                    }
                    else
                    {
                        this.Show();
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Registration reg = new Registration();
           reg.Show();
        }
    }
}
