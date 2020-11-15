using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class CustomerCls
    {
        public int AccNo { get; set; }
        public string AccName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
        public int Branch { get; set; }
        public byte[] Photo { get; set; }
    }
}
