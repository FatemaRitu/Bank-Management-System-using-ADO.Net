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
    public partial class Transaction : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public Transaction()
        {
            InitializeComponent();
            LoadCombo();
            LoadDataInGrid();
        }
        private void LoadCombo()
        {
            using (var conn = new SqlConnection(conStr))
            {

                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT BranchID, Location FROM Branch";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);

                DataRow dr;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "--SELECT YOUR BRANCH--" };
                dt.Rows.InsertAt(dr, 0);



                textBranchID.ValueMember = "BranchID";
                textBranchID.DisplayMember = "Location";
                textBranchID.DataSource = dt;

              
            }

        }

        private void LoadDataInGrid()
        {

            using (var conn = new SqlConnection(conStr))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
               cmd.CommandText = "select TransactionID, AccountNo,BranchID,Deposit,Withdraw, TransactionDate " +
                    "from Transactions ";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);
                dgvTransaction.DataSource = dt;

                if (dt != null)
                {
                    dgvTransaction.DataSource = dt;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            

            using (var conn = new SqlConnection(conStr))
            {
                TransactionCls transaction = new TransactionCls();
                try
                {
                    transaction.TransID = Convert.ToInt32(textTranID.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Transaction ID  occured error");
                }

                try
                {
                    transaction.AccNO = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Account no. occured error");
                }

                try
                {
                    transaction.BranchID = Convert.ToInt32(textBranchID.SelectedValue);
                }
                catch (Exception)
                {

                    MessageBox.Show("Branch occured error");
                }

                try
                {
                    transaction.Deposit = Convert.ToDecimal(textDeposit.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Money occured error");
                }
                try
                {
                    transaction.Withdraw = Convert.ToDecimal(textWithdraw.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Money occured error");
                }
                try
                {
                    transaction.TransacDate = Convert.ToDateTime(dtTransDate.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid date format");
                }

                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Transactions (TransactionID,AccountNo,BranchID,Deposit, Withdraw,TransactionDate) " +
                    "values ('" + transaction.TransID + "','" + transaction.AccNO + "','" + transaction.BranchID + "','" + transaction.Deposit + "','" + transaction.Withdraw + "','" + transaction.TransacDate + "')";


                conn.Open();
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("INSERTED...");
                }
                else
                {
                    MessageBox.Show("ERROR!!!");
                }

            }
            LoadDataInGrid();
            ClearTextBox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           

            using (var conn = new SqlConnection(conStr))
            {
                TransactionCls transaction = new TransactionCls();
                try
                {
                    transaction.TransID = Convert.ToInt32(textTranID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.AccNO = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.BranchID = Convert.ToInt32(textBranchID.SelectedValue);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.Deposit = Convert.ToDecimal(textDeposit.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid money input");
                }
                try
                {
                    transaction.Withdraw = Convert.ToDecimal(textWithdraw.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid money input");
                }
                try
                {
                    transaction.TransacDate = Convert.ToDateTime(dtTransDate.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid date format");
                }

                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Transactions SET BranchID='" + transaction.BranchID + "', Deposit='" + transaction.Deposit + "', Withdraw='" + transaction.Withdraw + "' ,TransactionDate='" + transaction.TransacDate + "' WHERE TransactionID='" + transaction.TransID + "' and  AccountNo='" + transaction.AccNO + "'";

                conn.Open();
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("UPDATED...");
                }
                else
                {
                    MessageBox.Show("ERROR!!!");
                }

            }
            LoadDataInGrid();
            ClearTextBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           

            using (var conn = new SqlConnection(conStr))
            {
                TransactionCls transaction = new TransactionCls();
                try
                {
                    transaction.TransID = Convert.ToInt32(textTranID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.AccNO = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.BranchID = Convert.ToInt32(textBranchID.SelectedValue);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                try
                {
                    transaction.Deposit = Convert.ToDecimal(textDeposit.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid money input");
                }
                try
                {
                    transaction.Withdraw = Convert.ToDecimal(textWithdraw.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid money input");
                }
                try
                {
                    transaction.TransacDate = Convert.ToDateTime(dtTransDate.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid date format");
                }

                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Transactions where AccountNo=" + transaction.AccNO + " and  TransactionID='" + transaction.TransID + "'";

                conn.Open();
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("DELETED...");
                }
                else
                {
                    MessageBox.Show("ERROR!!!");
                }

            }
            LoadDataInGrid();
            ClearTextBox();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadDataInGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearTextBox()
        {
            textTranID.Text = "";
            textAccNo.Text = "";
            textBranchID.Text = "";
            textDeposit.Text = "";
            textWithdraw.Text = "";
            dtTransDate.Text = "";
        }
        private void ClearAll()
        {
            textTranID.Text = "";
            textAccNo.Text = "";
            textBranchID.Text = "";
            textDeposit.Text = "";
            textWithdraw.Text = "";
            dtTransDate.Text = "";
            dgvTransaction.DataSource = "";
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            MainPage mPage = new MainPage();
            this.Hide();
            mPage.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellId = e.RowIndex;
            DataGridViewRow row = dgvTransaction.Rows[cellId];

            int TranID = Convert.ToInt32(row.Cells[1].Value);
            textTranID.Text = TranID.ToString();

            int AccNo = Convert.ToInt32(row.Cells[2].Value);
            textAccNo.Text = AccNo.ToString();

            int BrancID = Convert.ToInt32(row.Cells[3].Value);
            textBranchID.Text = BrancID.ToString();

            textDeposit.Text = row.Cells[4].Value.ToString();

            textWithdraw.Text = row.Cells[5].Value.ToString();

            DateTime tDate = Convert.ToDateTime(row.Cells[6].Value);
            dtTransDate.Text =tDate.ToString();
        }
    }
}
