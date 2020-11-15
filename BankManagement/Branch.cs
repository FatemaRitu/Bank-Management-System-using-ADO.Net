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
    public partial class Branch : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public Branch()
        {
            InitializeComponent();
            LoadDataInGrid();
        }

        private void LoadDataInGrid()
        {

            using (var conn = new SqlConnection(conStr))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select*from Branch";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);
                dgvBranch.DataSource = dt;

                if (dt != null)
                {
                    dgvBranch.DataSource = dt;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            using (var conn = new SqlConnection(conStr))
            {
                BranchCls branch = new BranchCls();
                try
                {
                    branch.BranchID = Convert.ToInt16(textBranchID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                branch.BranchCode = textBranchCode.Text;
                branch.Location = textLocation.Text;
                try
                {
                    branch.TotalEmployee = Convert.ToInt16(textTotalemp.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Branch (BranchID,BranchCode,Location,TotalEmployee) " +
                    "values('"+branch.BranchID+"','"+branch.BranchCode+"','"+branch.Location+"','"+branch.TotalEmployee+"')";
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

        private void dgvBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellId = e.RowIndex;
            DataGridViewRow row = dgvBranch.Rows[cellId];
            int bID = Convert.ToInt16(row.Cells[1].Value.ToString());
            textBranchID.Text = bID.ToString();
            textBranchCode.Text = row.Cells[2].Value.ToString();
            textLocation.Text = row.Cells[3].Value.ToString();
            int bTotalEmp = Convert.ToInt16(row.Cells[4].Value.ToString());
            textTotalemp.Text = bTotalEmp.ToString();
        }

        private void ClearTextBox()
        {
            textBranchID.Text = "";
            textBranchCode.Text = "";
            textLocation.Text = "";
            textTotalemp.Text = "";
        }

        private void ClearAll()
        {
            textBranchID.Text = "";
            textBranchCode.Text = "";
            textLocation.Text = "";
            textTotalemp.Text = "";
            dgvBranch.DataSource = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
       
            ClearAll();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadDataInGrid();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            using (var conn = new SqlConnection(conStr))
            {
                BranchCls branch = new BranchCls();
                try
                {
                    branch.BranchID = Convert.ToInt16(textBranchID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                branch.BranchCode = textBranchCode.Text;
                branch.Location = textLocation.Text;
                try
                {
                    branch.TotalEmployee = Convert.ToInt16(textTotalemp.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Branch SET BranchCode='" + branch.BranchCode + "', " +
                    "Location='" + branch.Location + "', TotalEmployee='" + branch.TotalEmployee + "'" +
                    " WHERE BranchID='" + branch.BranchID + "'";
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
                BranchCls branch = new BranchCls();
                try
                {
                    branch.BranchID = Convert.ToInt16(textBranchID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                branch.BranchCode = textBranchCode.Text;
                branch.Location = textLocation.Text;
                try
                {
                    branch.TotalEmployee = Convert.ToInt16(textTotalemp.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Branch WHERE BranchID='" + branch.BranchID + "'";
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBack_Click_1(object sender, EventArgs e)
        {
            MainPage mPage = new MainPage();
            this.Hide();
            mPage.Show();
        }
    }
}
