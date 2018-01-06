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
    public partial class Form5 : Form
    {
        public Form5(string _UserID,int[] _books)
        {
            books = _books;
            UserID = _UserID;
            InitializeComponent();
        }
        private string UserID;
        private int[] books;
        
        private void button1_Click(object sender, EventArgs e)
        {
            bool book = false;
            string Id = textBox1.Text;
            string bookName = textBox2.Text;
            int booksNumber = 0;
            for (; booksNumber < 3; booksNumber++)
            {
                if (books[booksNumber] == Convert.ToInt32(Id))
                {
                    book = true;
                    break;
                }
                    

            }
            if (book == false)
            {
                MessageBox.Show("你当前未借阅该书籍！", "提示");
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
                
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select ID from books where ID like '" + Id + "'", conn);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows == false)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("无此ID的书籍!", "结果");
                    return;
                }
                comm.Dispose();
                reader.Close();
                comm = new SqlCommand("select ID,书名 from books where ID like '" + Id + "' and 书名 like '" + bookName + "'", conn);
                reader = comm.ExecuteReader();
                if (reader.HasRows == false)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("ID与书名不同！请重新输入！", "结果");
                    return;
                }
                comm.Dispose();
                reader.Close();
                comm = new SqlCommand("update books set 库存 = 库存+1  where ID = '" + Id + "'", conn);
                reader = comm.ExecuteReader();
                comm.Dispose();
                reader.Close();
                comm = new SqlCommand( "update users set 当前借阅"+ (3-booksNumber)+ "=0"+ "where 账号 like '" + UserID + "'", conn);
                reader = comm.ExecuteReader();
                MessageBox.Show("还书成功！");
                Close();
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

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
