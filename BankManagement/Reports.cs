using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class Reports : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Reports()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(conStr))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select AccountNo,AccountName,Phone,Address,Balance,BranchID from Customer";
                conn.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                List<CustomerCls> customer = new List<CustomerCls>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CustomerCls obj = new CustomerCls();
                    obj.AccNo = Convert.ToInt32(dt.Rows[i]["AccountNo"].ToString());
                    obj.AccName = dt.Rows[i]["AccountName"].ToString();
                    obj.Address = dt.Rows[i]["Address"].ToString();
                    obj.Phone = dt.Rows[i]["Phone"].ToString();
                    obj.Balance = Convert.ToDecimal(dt.Rows[i]["Balance"].ToString());
                    obj.Branch = Convert.ToInt32(dt.Rows[i]["BranchID"].ToString());
                    customer.Add(obj);
                }
                using (CustomerPrintReport prForm = new CustomerPrintReport(customer))
                {
                    prForm.ShowDialog();
                }
            }
        }
        private void btnTransaction_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(conStr))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Transactions";
                conn.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                List<TransactionCls> transactions = new List<TransactionCls>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TransactionCls obj = new TransactionCls();
                    obj.TransID = Convert.ToInt32(dt.Rows[i]["TransactionID"].ToString());
                    obj.AccNO = Convert.ToInt32(dt.Rows[i]["AccountNo"].ToString());
                    obj.BranchID = Convert.ToInt32(dt.Rows[i]["BranchID"].ToString());
                    obj.Deposit = Convert.ToDecimal(dt.Rows[i]["Deposit"].ToString());
                    obj.Withdraw = Convert.ToDecimal(dt.Rows[i]["Withdraw"].ToString());
                    obj.TransacDate = Convert.ToDateTime(dt.Rows[i]["TransactionDate"].ToString());
                    transactions.Add(obj);
                }
                using (TransactionPrintReport prForm = new TransactionPrintReport(transactions))
                {
                    prForm.ShowDialog();
                }
            }
        }

        private void btnCTransactionReport_Click(object sender, EventArgs e)
        {
            CTransaction cTran = new CTransaction();
            cTran.Show();
            this.Hide();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainPage mainP = new MainPage();
            mainP.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            BankMangement bank = new BankMangement();
            bank.Show();
            this.Hide();
        }
    }
}
