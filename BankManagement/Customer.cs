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
using System.IO;

namespace BankManagement
{
    public partial class Customer : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        string imgLocation = "";
        public Customer()
        {
            InitializeComponent();
            LoadCombo();
            LoadDataInGrid();
            dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                dr.ItemArray = new object[] { 0, "---SELECT---" };
                dt.Rows.InsertAt(dr, 0);

                cmbBranch.ValueMember = "BranchID";
                cmbBranch.DisplayMember = "Location";
                cmbBranch.DataSource = dt;
            }
        }
        private void LoadDataInGrid()
        {
            using (var conn = new SqlConnection(conStr))
            {
                string query = "select AccountNo,AccountName,Phone,Address,Balance,BranchID,Photo from Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dgvCustomer.DataSource = dt;

                if (dt != null)
                {
                    dgvCustomer.DataSource = dt;
                }
                dgvCustomer.DataSource = dt;
                dgvCustomer.RowTemplate.Height = 80;
                DataGridViewImageColumn imgColm = new DataGridViewImageColumn();
                imgColm = (DataGridViewImageColumn)dgvCustomer.Columns[7];
                imgColm.ImageLayout = DataGridViewImageCellLayout.Stretch;
                dataAdapter.Dispose();
            }   
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {            
            using (var conn = new SqlConnection(conStr))
            {
                CustomerCls customer = new CustomerCls();
                try
                {
                    customer.AccNo = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Account no needed");
                }
                customer.AccName = textAccName.Text;

                customer.Phone = textPhone.Text;

                customer.Address = textAddress.Text;
                try
                {
                    customer.Balance = Convert.ToDecimal(textBalance.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter your amount");
                }
                try
                {
                    int branches = Convert.ToInt32(cmbBranch.SelectedValue);
                    customer.Branch = branches;
                }
                catch (Exception)
                {
                    MessageBox.Show("Branch needed");
                }
                customer.Photo = null;
                FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader binRead = new BinaryReader(stream);
                customer.Photo = binRead.ReadBytes((int)stream.Length);
                string file = imgLocation;
                string[] files = file.Split('\\');
                string fileName = files[files.Length - 1];
                string filePath = @"E:\Evidence\Module6 XML ADO.NET CristalReport\Project\Project ADO.Net\BankManagementSolution\BankManagement\Pictures" + fileName;
                File.Copy(file, filePath, true);
               
                string query= "Insert into Customer (AccountNo,AccountName,Phone, Address,Balance,BranchID,ImageName,ImagePath,Photo) " +
                    "values ('" + customer.AccNo + "','" + customer.AccName + "','" + customer.Phone + "','" + customer.Address + "','"+customer.Balance + "','" + customer.Branch + "','" + fileName + "','" + filePath + "',@pics)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@pics", customer.Photo));
                conn.Open();

                int count = 0;
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
                CustomerCls customer = new CustomerCls();
                try
                {
                    customer.AccNo = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customer.AccName = textAccName.Text;

                customer.Phone = textPhone.Text;

                customer.Address = textAddress.Text;
                try
                {
                    customer.Balance = Convert.ToDecimal(textBalance.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    int branches = Convert.ToInt32(cmbBranch.SelectedValue);
                    customer.Branch = branches;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customer.Photo = null;
                FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader binRead = new BinaryReader(stream);
                customer.Photo = binRead.ReadBytes((int)stream.Length);

                string file = imgLocation;
                string[] files = file.Split('\\');
                string fileName = files[files.Length - 1];
                string filePath = @"E:\Evidence\Module6 XML ADO.NET CristalReport\Project\Project ADO.Net\BankManagementSolution\BankManagement\Pictures" + fileName;
                File.Copy(file, filePath, true);

                string query = "UPDATE Customer SET AccountName='" + customer.AccName + "', Phone='"+customer.Phone+"' ," +
                    "Address='" + customer.Address + "', Balance='" + customer.Balance + "' ," +
                    " BranchID='" + customer.Branch + "',ImageName='" + fileName + "', ImagePath='" + filePath + "', Photo=@pics  WHERE AccountNo='" + customer.AccNo + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@pics", customer.Photo));
                conn.Open();

                int count = 0;
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
                CustomerCls customer = new CustomerCls();
                try
                {
                    customer.AccNo = Convert.ToInt32(textAccNo.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customer.AccName = textAccName.Text;

                customer.Phone = textPhone.Text;

                customer.Address = textAddress.Text;
                try
                {
                    decimal blnc = Convert.ToDecimal(textBalance.Text);
                    customer.Balance = blnc;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    int branches = Convert.ToInt32(cmbBranch.SelectedValue);
                    customer.Branch = branches;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Customer where AccountNo="+customer.AccNo+" and  AccountName='"+customer.AccName+"'";

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
        private void ClearTextBox()
        {
            textAccNo.Text = "";
            textAccName.Text = "";
            textPhone.Text = "";
            textAddress.Text = "";
            textBalance.Text = "";
            cmbBranch.Text = "";
            pictureBoxPhoto.Image =null;
        }
        private void ClearAll()
        {
            textAccNo.Text = "";
            textAccName.Text = "";
            textPhone.Text = "";
            textAddress.Text = "";
            textBalance.Text = "";
            cmbBranch.Text = ""; 
            pictureBoxPhoto.Image = null;
            dgvCustomer.DataSource = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            LoadDataInGrid();
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
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellId = e.RowIndex;
            DataGridViewRow row = dgvCustomer.Rows[cellId];

            int cID = Convert.ToInt32(row.Cells[1].Value);
            textAccNo.Text = cID.ToString();

            textAccName.Text = row.Cells[2].Value.ToString();

            textPhone.Text = row.Cells[3].Value.ToString();

            textAddress.Text = row.Cells[4].Value.ToString();
         
            textBalance.Text = row.Cells[5].Value.ToString();

            cmbBranch.Text = row.Cells[6].Value.ToString();

            byte[] data = (byte[])dgvCustomer.SelectedRows[0].Cells[7].Value;
            MemoryStream ms = new MemoryStream(data);
            pictureBoxPhoto.Image = Image.FromStream(ms);
        }
       private void PickImageURL_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBoxPhoto.ImageLocation = imgLocation;
            }
        }
    }
}
 