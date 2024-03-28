using CrystalDecisions.CrystalReports.Engine;
using StudentLibraryProject.App_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentLibraryProject
{
    public partial class BookListForm : Form
    {

        BookRepository bookRepository;
        public BookListForm()
        {
            InitializeComponent();
            bookRepository = new BookRepository();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportDocument report = new ReportDocument();


            report.Load($"{Application.StartupPath}\\BookReport.rpt");

            if (report.IsLoaded)
            {


                report.SetDataSource(bookRepository.GetReportData());

            }



            ReportViewerForm form = new ReportViewerForm();

            form.crystalReportViewer.ReportSource = report;



            form.ShowDialog(this);
        }

        private void BookListForm_Load(object sender, EventArgs e)
        {


            DataLoad();
        }

       

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var id = StudentGrid.SelectedRows[0].Cells[0].Value.ToString();


            if (int.Parse(id) > 0)
            {
                StudentEntryForm form = new StudentEntryForm();

                form.StudentID = int.Parse(id);


                form.ShowDialog(this);
            }

        }

        private void DataLoad()
        {
           studentMasterBindingSource.DataSource =  bookRepository.GetStudents();
        }
    }
}
