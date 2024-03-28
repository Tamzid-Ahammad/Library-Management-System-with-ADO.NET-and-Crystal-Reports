using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibraryProject.App_Data
{
    internal class vwBookInfo
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhoneNo { get; set; }
        public string BookName { get; set; }
        public decimal BookRentPrice { get; set; }
        public int BookQuantity { get; set; }
        public decimal BookTotal { get; set; }
    }
}
