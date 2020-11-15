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
    public partial class PrintCTransaction : Form
    {
        List<CTransactionCls> insideList;
        public PrintCTransaction(List<CTransactionCls> outsideList )
        {
            InitializeComponent();
            insideList = outsideList;
        }

        private void PrintCTransaction_Load(object sender, EventArgs e)
        {
            CTransactionReport rpt = new CTransactionReport();
            rpt.SetDataSource(insideList);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
