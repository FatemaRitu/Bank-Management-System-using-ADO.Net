using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void btnBranch_Click(object sender, EventArgs e)
        {
            Branch branc = new Branch();
            this.Hide();
            branc.Show();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            this.Hide();
            customer.Show();
        }
        private void btnTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            Transaction transaction = new Transaction();
            transaction.Show();
        }
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee employee = new Employee();
            employee.Show();
        }
        private void btnRegInfo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Profile prof = new Profile();
            prof.Show();
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login logs = new Login();
            this.Hide();
            logs.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports rpt = new Reports();
            rpt.Show();
            this.Hide();
        }
    }
}
