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
    public partial class Employee : Form
    {
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public Employee()
        {
            InitializeComponent();
            LoadComboBranch();
            LoadComboDesignation();
            LoadDataInGrid();
        }

        private void LoadComboDesignation()
        {
            using (var conn = new SqlConnection(conStr))
            {

                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select Id, Name from Designation";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);

                DataRow dr;
                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "---SELECT---" };
                dt.Rows.InsertAt(dr, 0);

                cmbDesignation.ValueMember = "Id";
                cmbDesignation.DisplayMember = "Name";
                cmbDesignation.DataSource = dt;
            }

        }

        private void LoadComboBranch()
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
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select EmployeeID, FirstName,LastName, Designation,Salary,BranchID from  Employee";
                conn.Open();

                var dt = new DataTable();
                var rdr = cmd.ExecuteReader();
                dt.Load(rdr, LoadOption.Upsert);
                dgvEmployee.DataSource = dt;

                if (dt != null)
                {
                    dgvEmployee.DataSource = dt;
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {

            using (var conn = new SqlConnection(conStr))
            {

                EmployeeCls employee = new EmployeeCls();
                try
                {
                    employee.EmployeeID = Convert.ToInt32(textEmpID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                employee.FirstName = textEmpFirstName.Text;

                employee.LastName = textEmpLastName.Text;

                try
                {
                    employee.Designation = Convert.ToInt32(cmbDesignation.SelectedValue);


                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get designation");
                }

                try
                {
                    employee.salary = Convert.ToDecimal(textSalary.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                try
                {
                    employee.Branch = Convert.ToInt32(cmbBranch.SelectedValue);

                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get branch value");
                }

                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employee (EmployeeID,FirstName,LastName,Designation,Salary,BranchID) " +
                    "values ('" + employee.EmployeeID + "','" + employee.FirstName + "','" + employee.LastName + "','" +
                    employee.Designation + "','" + employee.salary + "','" + employee.Branch + "')";

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
                EmployeeCls employee = new EmployeeCls();
                try
                {
                    employee.EmployeeID = Convert.ToInt32(textEmpID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                employee.FirstName = textEmpFirstName.Text;

                employee.LastName = textEmpLastName.Text;

                try
                {
                    employee.Designation = Convert.ToInt32(cmbDesignation.SelectedValue);


                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get designation");
                }

                try
                {
                    employee.salary = Convert.ToDecimal(textSalary.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                try
                {
                    employee.Branch = Convert.ToInt32(cmbBranch.SelectedValue);

                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get branch value");
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee set FirstName='" + employee.FirstName + "',LastName='" + employee.LastName + "'," +
                 "Designation='" + employee.Designation + "',Salary='" + employee.salary + "',BranchID='" + employee.Branch + "'" +
                 " where EmployeeID='" + employee.EmployeeID + "' ";


                conn.Open();
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("Updated...");
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
                EmployeeCls employee = new EmployeeCls();
                try
                {
                    employee.EmployeeID = Convert.ToInt32(textEmpID.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                employee.FirstName = textEmpFirstName.Text;

                employee.LastName = textEmpLastName.Text;

                try
                {
                    employee.Designation = Convert.ToInt32(cmbDesignation.SelectedValue);


                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get designation");
                }

                try
                {
                    employee.salary = Convert.ToDecimal(textSalary.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                try
                {
                    employee.Branch = Convert.ToInt32(cmbBranch.SelectedValue);

                }
                catch (Exception)
                {

                    MessageBox.Show("Can not get branch value");
                }
                int count = 0;
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Employee where EmployeeID='" + employee.EmployeeID + "'  ";

                conn.Open();
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("Deleted...");
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
            textEmpID.Text = "";
            textEmpFirstName.Text = "";
            textEmpLastName.Text = "";
            cmbDesignation.Text = "";
            textSalary.Text = "";
            cmbBranch.Text = "";
        }
        private void ClearAll()
        {
            textEmpID.Text = "";
            textEmpFirstName.Text = "";
            textEmpLastName.Text = "";
            cmbDesignation.Text = "";
            textSalary.Text = "";
            cmbBranch.Text = "";
            dgvEmployee.DataSource = "";
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

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int cellId = e.RowIndex;
            DataGridViewRow row = dgvEmployee.Rows[cellId];

            int Id = Convert.ToInt32(row.Cells[1].Value);
            textEmpID.Text = Id.ToString();

            textEmpFirstName.Text = row.Cells[2].Value.ToString();

            textEmpLastName.Text = row.Cells[3].Value.ToString();

            cmbDesignation.Text = row.Cells[4].Value.ToString();

            textSalary.Text = row.Cells[5].Value.ToString();

            cmbBranch.Text = row.Cells[6].Value.ToString();

        }
    }
}
