using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
   public  class TransactionCls
    {
        public int TransID { get; set; }
        public int AccNO { get; set; }
        public int BranchID { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdraw { get; set; }
        public DateTime TransacDate { get; set; }
    }
}
