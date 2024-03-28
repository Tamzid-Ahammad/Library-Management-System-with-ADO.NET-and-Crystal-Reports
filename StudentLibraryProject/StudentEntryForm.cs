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
    public partial class StudentEntryForm : Form
    {
        BookRepository repository = new BookRepository();
        public int StudentID { get; set; } = 0;
        public StudentEntryForm()
        {
            InitializeComponent();
        }

       

        private void StudentEntryForm_Load(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
        }
        void ResetForm()
        {
            txtId.Text = null;
            
            txtName.Text = null;
            txtAddress.Text = null;
            txtPhoneNo.Text = null;
            gridItem.Rows.Clear();
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                StudentMaster student = new StudentMaster();

                if (txtId.Text.Length > 0)
                    student.StudentId = Convert.ToInt32(txtId.Text);

                
                student.StudentPhoneNo = txtPhoneNo.Text;
                student.StudentName = txtName.Text;
                student.StudentAddress = txtAddress.Text;





                foreach (DataGridViewRow item in gridItem.Rows)
                {

                    if (item.IsNewRow) continue;

                    BookDetails bookDetails = new BookDetails();

                    bookDetails.BookName = item.Cells[0].Value.ToString();
                    bookDetails.BookRentPrice = Convert.ToDecimal(item.Cells[1].Value);
                    bookDetails.BookQuantity = (int)Convert.ToUInt32(item.Cells[2].Value);
                    student.Books.Add(bookDetails);
                }

                if (txtId.Text.Length > 0)
                {

                    int rw = repository.UpdateStudent(student);


                    if (rw > 0)
                    {
                        MessageBox.Show("Data updated successfully");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    int rw = repository.SaveStudent(student);


                    if (rw > 0)
                    {
                        MessageBox.Show("Data saved successfully");
                    }
                    else
                    {
                        return;
                    }
                }


                ResetForm();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Length > 0)
            {

                var dialog = MessageBox.Show("Delete record", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (dialog == DialogResult.OK)
                {
                    int rw = repository.DeleteStudent(txtId.Text);


                    if (rw > 0)
                    {
                        MessageBox.Show("Data deleted successfully");
                    }
                }




            }

        }

       

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void LoadData()
        {
            if (StudentID > 0)
            {
                var student = repository.GetStudent(StudentID);

                txtId.Text = student.StudentId.ToString();
                txtName.Text = student.StudentName;
                txtPhoneNo.Text = student.StudentPhoneNo;
               
                txtAddress.Text = student.StudentAddress;


                bookDetailsBindingSource.DataSource = student.Books;



            }
            


        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
