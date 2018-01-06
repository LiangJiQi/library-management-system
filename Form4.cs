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
    public partial class Form4 : Form
    {
        public Form4(string _UserID,int[] _books)
        {
            books = _books;
            UserID = _UserID;
            InitializeComponent();
        }
        private string UserID;
        private int[] books;
        private void button1_Click(object sender, EventArgs e)
        {
            string Id = textBox1.Text;
            string bookName = textBox2.Text;
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select ID from books where ID like '" +Id+ "'", conn);
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
                comm = new SqlCommand("select * from books where ID like '" + Id + "' and 书名 like '"+ bookName+"'", conn);
                reader = comm.ExecuteReader();
                if (reader.HasRows == false)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("ID与书名不同！请重新输入！", "结果");
                    return;
                }
                reader.Read();
                if (Convert.ToInt32(reader[reader.FieldCount-1]) <= 0)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("已无库存！", "结果");
                    return;
                }

                comm.Dispose();
                reader.Close();
                comm = new SqlCommand("update books set 库存 = 库存-1  where ID = '" + Id + "'", conn);
                reader = comm.ExecuteReader();
                comm.Dispose();
                reader.Close();
                comm = new SqlCommand("select * from users where 账号 like '" + UserID + "'", conn);
                reader = comm.ExecuteReader();
                reader.Read();
                bool pushBook = false;
                int i = 1;
                for (; i <= 3; i++)
                {
                    if (Convert.ToInt32(Convert.ToString(reader.GetValue(reader.FieldCount - i))) <= 0 || Convert.ToInt32(Convert.ToString(reader.GetValue(reader.FieldCount - i))) > 6)
                    {
                        pushBook = true;
                        break;
                    }
                        
                }
                if (pushBook == false)
                {
                    MessageBox.Show("您的借阅数已满，请还书后重试！", "提示");

                    Close();
                    return;
                }
                    
                comm.Dispose();
                reader.Close();
                comm = new SqlCommand(" update users set 当前借阅"+(4-i)+" = '" + Id + "' "+ "where 账号 like '" + UserID + "'" , conn);
                reader = comm.ExecuteReader();
                MessageBox.Show("借出成功！");
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
        public int[] getBooks()
        {
            return books;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
