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
    public partial class CustomerPrintReport : Form
    {
        List<CustomerCls> _list;
        public CustomerPrintReport(List<CustomerCls> customer)
        {
            InitializeComponent();
            _list = customer;
        }

        private void CustomerPrintReport_Load(object sender, EventArgs e)
        {
            CustomerReport rpt = new CustomerReport();
            rpt.SetDataSource(_list);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
