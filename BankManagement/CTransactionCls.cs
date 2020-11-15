using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
   public  class CTransactionCls
    {
        public int AccNo { get; set; }
        public string AccName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdraw { get; set; }
        public string Branch { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransacDate { get; set; }
    }
}
