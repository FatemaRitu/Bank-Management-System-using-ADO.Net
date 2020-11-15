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
    public partial class TransactionPrintReport : Form
    {
        List<TransactionCls> _list;
        public TransactionPrintReport(List<TransactionCls> transactions)
        {
            InitializeComponent();
            _list = transactions;
        }

        private void TransactionPrintReport_Load(object sender, EventArgs e)
        {

            TransactionReport rpt = new TransactionReport();
            rpt.SetDataSource(_list);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    
    }
}
