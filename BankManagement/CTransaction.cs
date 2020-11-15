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
    public partial class CTransaction : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public CTransaction()
        {
            InitializeComponent();
            LoadGridView(0);
            LoadCombo();
        }

        private void LoadCombo()
        {
            using (var conn = new SqlConnection(conStr))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select AccountNo,AccountName from Customer  where AccountNo in (select AccountNo from Transactions) order By AccountNo ASC";
                conn.Open();

                DataTable dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);

                DataRow dr;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "--All Accounts--" };
                dt.Rows.InsertAt(dr, 0);

                cbAccNo.ValueMember = "AccountName";
                cbAccNo.DisplayMember = "AccountNo";
                cbAccNo.DataSource = dt;
            }
        }
        private void LoadGridView(int AccNo)
        {
            using (var conn = new SqlConnection(conStr))
            {
                DateTime fromDate = dtStart.Value;
                DateTime ToDate = dtEnd.Value;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (AccNo == 0)
                {
                    cmd.CommandText = "Select Customer.AccountNo,AccountName,Address,Phone,Balance," +
                    " Withdraw,Deposit,TransactionID,TransactionDate,Location  from Customer " +
                    "join Transactions on Customer.AccountNo=Transactions.AccountNo " +
                    "join Branch on Customer.BranchID=Branch.BranchID";
                }
                else
                {
                    cmd.CommandText = "Select Customer.AccountNo,AccountName,Address,Phone,Balance," +
                   " Withdraw,Deposit,TransactionID,TransactionDate,Location from Customer " +
                   "join Transactions on Customer.AccountNo=Transactions.AccountNo " +
                   "join Branch on Customer.BranchID=Branch.BranchID where Customer.AccountNo = '" + AccNo + "'";
                }
                conn.Open();
                DataTable dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);
                dgvCusTrans.DataSource = dt;
            }
        }

        private void cbAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int AccNo = Convert.ToInt32(cbAccNo.Text);
            LoadGridView(AccNo);
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtStart.Value;
            DateTime ToDate = dtEnd.Value;
            using (var conn = new SqlConnection(conStr))
            {
                int AccNo = Convert.ToInt32(cbAccNo.Text);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (AccNo == 0)
                {

                    cmd.CommandText = "Select Customer.AccountNo,AccountName,Address,Phone,Balance," +
                    " Withdraw,Deposit,TransactionID,TransactionDate,Location  from Customer " +
                    "join Transactions on Customer.AccountNo=Transactions.AccountNo " +
                    "join Branch on Customer.BranchID=Branch.BranchID where TransactionDate Between('" + fromDate + "') AND ('" + ToDate + "')";
                }
                else
                {
                    cmd.CommandText = "Select Customer.AccountNo,AccountName,Address,Phone ,Balance," +
                   " Withdraw,Deposit,TransactionID,TransactionDate,Location from Customer " +
                   "join Transactions on Customer.AccountNo=Transactions.AccountNo " +
                   "join Branch on Customer.BranchID=Branch.BranchID where Customer.AccountNo = '" + AccNo + "' and TransactionDate Between('" + fromDate + "') AND ('" + ToDate + "')";
                }
                conn.Open();

                DataTable dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);

                List<CTransactionCls> list = new List<CTransactionCls>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CTransactionCls obj = new CTransactionCls();
                    obj.AccNo = Convert.ToInt32(dt.Rows[i]["AccountNo"].ToString());
                    obj.AccName = dt.Rows[i]["AccountName"].ToString();
                    obj.Address = dt.Rows[i]["Address"].ToString();
                    obj.Phone = dt.Rows[i]["Phone"].ToString();
                    obj.Balance = Convert.ToDecimal(dt.Rows[i]["Balance"].ToString());
                    obj.Deposit = Convert.ToDecimal(dt.Rows[i]["Deposit"].ToString());
                    obj.Withdraw = Convert.ToDecimal(dt.Rows[i]["Withdraw"].ToString());
                    obj.Branch = dt.Rows[i]["Location"].ToString();
                    obj.TransactionId = Convert.ToInt32(dt.Rows[i]["TransactionID"].ToString());
                    obj.TransacDate = Convert.ToDateTime(dt.Rows[i]["TransactionDate"].ToString());
                    list.Add(obj);

                }
                using (PrintCTransaction print = new PrintCTransaction(list))
                {
                    print.ShowDialog();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainPage mPage = new MainPage();
            this.Hide();
            mPage.Show();
        }
    }
}
