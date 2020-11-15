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
    public partial class Profile : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public Profile()
        {
            InitializeComponent();
            LoadGridView();
        }
        private void LoadGridView()
        {
            using (var conn = new SqlConnection(conStr))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select*from Registration";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);
                dgvProfile.DataSource = dt;

                if (dt != null)
                {
                    dgvProfile.DataSource = dt;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            MainPage mPage = new MainPage();
            mPage.Show();
        }
    }
}
