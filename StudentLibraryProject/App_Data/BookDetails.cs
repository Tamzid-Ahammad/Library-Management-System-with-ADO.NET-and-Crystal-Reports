using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibraryProject.App_Data
{
    internal class BookDetails
    {
        public string BookName { get; set; }
        public decimal BookRentPrice { get; set; }
        public int BookQuantity { get; set; }
        public decimal BookTotal => BookRentPrice * BookQuantity;


    }
}
