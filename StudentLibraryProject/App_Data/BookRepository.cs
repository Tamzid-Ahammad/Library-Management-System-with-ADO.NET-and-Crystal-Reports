using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentLibraryProject.App_Data
{
    internal class BookRepository
    {
        string conString = $"server = (LOCALDB)\\MSSQLLOCALDB; attachdbfilename = {Application.StartupPath}\\App_Data\\Library.mdf; trusted_connection= true; ";

        public BookRepository()
        {

        }



        public List<StudentMaster> GetStudents()
        {


            List<StudentMaster> students = new List<StudentMaster>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                var cmd = con.CreateCommand();


               
                cmd.CommandText = "Select * from StudentMaster";

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                con.Open();
                sda.Fill(ds);


                if (ds.Tables.Count > 0)
                {



                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        StudentMaster student = new StudentMaster();
                        student.StudentId = Convert.ToInt32(dr["StudentId"]);
                       
                        student.StudentName = dr["StudentName"].ToString();
                        
                        student.StudentAddress = dr["StudentAddress"]?.ToString();
                        student.StudentPhoneNo = dr["StudentPhoneNo"].ToString();
                        students.Add(student);
                    }

                }

            }

            return students;
        }

        public StudentMaster GetStudent(int StudentId)
        {

            StudentMaster student = new StudentMaster();
            using (SqlConnection con = new SqlConnection(conString))
            {
                var cmd = con.CreateCommand();


                
                cmd.CommandText = $"Select * from StudentMaster where StudentId={StudentId}; Select * from BookDetails where StudentId={StudentId}; ";

                

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                sda.Fill(ds);


                if (ds.Tables.Count > 0)
                {

                    var dr = ds.Tables[0].Rows[0];





                    student.StudentId = Convert.ToInt32(dr["StudentId"]);

                    student.StudentName = dr["StudentName"].ToString();
                    student.StudentPhoneNo = dr["StudentPhoneNo"].ToString();
                    student.StudentAddress = dr["StudentAddress"]?.ToString();

                    foreach (DataRow row in ds.Tables[1].Rows)
                    {

                        BookDetails item = new BookDetails();


                        item.BookName = row["BookName"].ToString();
                        item.BookRentPrice = Convert.ToDecimal(row["BookRentPrice"]);
                        item.BookQuantity = (int)Convert.ToUInt32(row["BookQuantity"]);

                        student.Books.Add(item);
                    }



                }

            }
            return student;
        }





        public int SaveStudent(StudentMaster student)
        {
            int rowNo = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                var cmd = con.CreateCommand();

                cmd.Transaction = tran;



                try
                {


                    cmd.CommandText = "select isnull(max(studentid), 0) + 1 as StudentId from StudentMaster ";


                    string StudentID = cmd.ExecuteScalar()?.ToString();



                    cmd.CommandText = $"INSERT INTO [dbo].[StudentMaster]([StudentId],[StudentName],[StudentAddress],[StudentPhoneNo]) VALUES (  {StudentID}, '{student.StudentName}', '{student.StudentAddress}', '{student.StudentPhoneNo}'   )";


                    rowNo = cmd.ExecuteNonQuery();


                    if (rowNo > 0)
                    {

                        foreach (BookDetails book in student.Books)
                        {
                            cmd.CommandText = $"INSERT INTO [dbo].[BookDetails] ([StudentId] ,[BookName] ,[BookRentPrice] ,[BookQuantity])  VALUES ({StudentID} ,'{book.BookName}' , '{book.BookRentPrice}' , '{book.BookQuantity}')";


                            int r1 = cmd.ExecuteNonQuery();
                        }

                    }

                    tran.Commit();
                }
                catch (SqlException e)
                {

                    tran.Rollback();
                    MessageBox.Show(e.Message);
                    return 0;
                }
            }
            return rowNo;
        }


        public int UpdateStudent(StudentMaster Student)
        {
            int rowNo = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                var cmd = con.CreateCommand();

                cmd.Transaction = tran;



                try
                {




                    cmd.CommandText = $"UPDATE [dbo].[StudentMaster]   SET  [StudentName] = '{Student.StudentName}',[StudentAddress] = '{Student.StudentAddress}',[StudentPhoneNo] = '{Student.StudentPhoneNo}' where StudentId = {Student.StudentId}";

                    rowNo = cmd.ExecuteNonQuery();


                    if (rowNo > 0)
                    {
                        cmd.CommandText = $"delete from [dbo].[BookDetails] where StudentId = {Student.StudentId}";


                        if (cmd.ExecuteNonQuery() >= 0)
                        {
                            foreach (var item in Student.Books)
                            {
                                cmd.CommandText = $"INSERT INTO [dbo].[BookDetails] ([StudentId] ,[BookName] ,[BookRentPrice] ,[BookQuantity])  VALUES ({Student.StudentId} ,'{item.BookName}' , '{item.BookRentPrice}' , '{item.BookQuantity}')";


                                cmd.ExecuteNonQuery();
                            }
                        }



                    }

                    tran.Commit();
                }
                catch (SqlException e)
                {

                    tran.Rollback();
                    MessageBox.Show(e.Message);
                    return 0;
                }
            }
            return rowNo;
        }

        public int DeleteStudent(string StudentId)
        {
            int rowNo = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                var cmd = con.CreateCommand();

                cmd.Transaction = tran;




                try
                {


                    cmd.CommandText = $"delete from [dbo].[StudentMaster]   where StudentId = {StudentId}";

                    rowNo = cmd.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException e)
                {
                    tran.Rollback();
                    MessageBox.Show(e.Message);
                    return 0;
                }
            }
            return rowNo;
        }

        internal List<vwBookInfo> GetReportData()
        {
            List<vwBookInfo> students = new List<vwBookInfo>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                var cmd = con.CreateCommand();


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * FROM vwBookInfo";



                DataTable dt = new DataTable();
                con.Open();



                dt.Load(cmd.ExecuteReader());




                foreach (DataRow dr in dt.Rows)
                {
                    vwBookInfo book = new vwBookInfo();
                    book.StudentId = Convert.ToInt32(dr["StudentId"]);
                    book.StudentName = dr["StudentName"].ToString();
                    book.StudentPhoneNo = dr["StudentPhoneNo"].ToString();
                    book.StudentAddress = dr["StudentAddress"]?.ToString();
                    book.BookName = dr["BookName"]?.ToString();
                    book.BookRentPrice = Convert.ToDecimal(dr["BookRentPrice"]);
                    book.BookQuantity = (int)Convert.ToUInt32(dr["BookQuantity"]);






                    students.Add(book);
                }



            }

            return students;
        }
    }
}
