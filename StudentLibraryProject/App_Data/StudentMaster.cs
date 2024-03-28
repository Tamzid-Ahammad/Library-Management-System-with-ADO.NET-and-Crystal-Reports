using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibraryProject.App_Data
{
    internal class StudentMaster
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhoneNo { get; set; }
        public List<BookDetails> Books { get; set; } = new List<BookDetails>();
    }
}
