using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text11_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Id = "";
            password = "";
            root = false;
            books = null;
        }
        private Form2 form2;
        private Form3 form3;
        private Form4 form4;
        private Form5 form5;
        private Form6 form6;
        private User user;

        private string Id;
        private string password;
        private bool root;                  //  是否是管理员
        private int[] books;

        private void button1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.ShowDialog();
            if (form2.DialogResult == DialogResult.OK)
                textBoxTextchange();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                form3 = new Form3();
                form3.Show();
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (books == null)
            {
                MessageBox.Show("您尚未登录！", "提示");
                return;
            }
            form4 = new Form4(Id,books);
            form4.ShowDialog();
            if (form4.DialogResult == DialogResult.OK)
                textBoxTextchange();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (books == null)
            {
                MessageBox.Show("您尚未登录！", "提示");
                return;
            }
            bool booksTrueOrFalse = false;
            for(int i = 0; i < 3; i++)
            {
                if (books[i] <= 0 && books[i] > 6)
                {
                    MessageBox.Show("您尚未登录或未借阅任何书籍！", "提示");
                }
                else booksTrueOrFalse = true;

            }
             if(booksTrueOrFalse == true)
                {
                form5 = new Form5(Id,books);
                form5.ShowDialog();
                if (form5.DialogResult == DialogResult.OK)
                    textBoxTextchange();
            }

        }
       public void textBoxTextchange()
        {
            Id = form2.getUserId();
            password = form2.getUserPass();
            root = form2.getUserRoot();
            if (root == true)
                button5.Visible = true;
            books = form2.getBooks();
            user = new User(Id, password, root, books);
            textBox1.Text = "                        欢迎使用图书管理系统！\r\n";
            textBox1.Text += "账号：" + Id + "\r\n" + "管理员：" + root+"\r\n\r\n"+"当前借阅："+"\r\n";
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand comm;
                comm = new SqlCommand("select 账号,密码,管理员,当前借阅1,当前借阅2,当前借阅3 from users" + " where 账号 like '"+Id+"'", conn);
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                
                //if(books != null)
                for (int j = 1; j <= 3; j++)
                {
                    if (reader.GetValue(reader.FieldCount - j) != null)
                        books[j - 1] = Convert.ToInt32(reader.GetValue(reader.FieldCount - j));
                }
                comm.Dispose();
                reader.Close();
                for (int i = 0; i < 3; i++)
                {
                    comm = new SqlCommand("select ID,书名 from books where ID like '" + books[i] + "'", conn);
                    reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        textBox1.Text = textBox1.Text +"ID : "+books[i]+"   书名："+ Convert.ToString( reader.GetValue(reader.FieldCount -1)) + "\r\n";
                    }
                    comm.Dispose();
                    reader.Close();
                }
               
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();

            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            textBoxTextchange();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "                        欢迎使用图书管理系统！\r\n";
            textBox1.Text += "\r\n账号：" + Id + "\r\n" + "管理员：" + root + "\r\n\r\n" + "当前借阅：" + "\r\n";
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form6 = new Form6();
            form6.ShowDialog();
        }
    }
}
