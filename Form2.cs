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
    public delegate string calculate(string user);
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Id = "";
            pass = "";
            root = false;
            IdTrueOrFalse = false;
            passTrueOrFalse = false;
            books = null;
        }
        private string Id;
        private string pass;
        private bool root;
        private bool IdTrueOrFalse;
        private bool passTrueOrFalse;
        private int[] books;
        private void button1_Click(object sender, EventArgs e)
        {
            Id = textBox1.Text;
            pass = textBox2.Text;
            root = false;
            IdTrueOrFalse = false;
            passTrueOrFalse = false;
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";

            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();

                //SqlCommand comm = new SqlCommand("select 账号 from user", conn);
                //SqlDataReader reader = comm.ExecuteReader();
                //while (reader.Read())
                //{

                //    if(Id == reader.GetString(i++))
                //        IdTrueOrFalse = true;
                //}
                //comm = new SqlCommand("select 密码 from user", conn);
                //reader = comm.ExecuteReader();
                //while (reader.Read())
                //{
                //    int i = 1;
                //    if (pass == reader.GetString(i++))
                //        passTrueOrFalse = true;
                //}
                //if (IdTrueOrFalse == true && passTrueOrFalse == true)
                //    MessageBox.Show("登入成功！");
                SqlCommand comm = new SqlCommand("select 账号,密码,管理员,当前借阅1,当前借阅2,当前借阅3 from users", conn);
                SqlDataReader reader = comm.ExecuteReader();
                int i = 0;
               while(reader.Read())
                {
                    string l = reader.GetString(i);
                    string m = Convert.ToString(reader.GetValue(reader.FieldCount - 3));
                    if (Id == reader.GetString(i))
                        IdTrueOrFalse = true;
                    if (pass == Convert.ToString(reader.GetValue(reader.FieldCount - 5)))
                        passTrueOrFalse = true;
                    else continue;
                    if (IdTrueOrFalse == true && passTrueOrFalse == true && Convert.ToInt32(reader.GetValue(reader.FieldCount - 4)) == 1)
                    {
                        root = true;
                    }
                    books = new int[3];
                    for (int j = 1; j <= 3; j++)
                    {
                        if (reader.GetValue(reader.FieldCount - j) != null)
                            books[j - 1] = Convert.ToInt32(reader.GetValue(reader.FieldCount - j));
                    }

                }
                if (IdTrueOrFalse == true && passTrueOrFalse == true)
                {
                    
                    MessageBox.Show("登录成功！");
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    Id = "";
                    pass = "";
                    IdTrueOrFalse = false;
                    passTrueOrFalse = false;
                    root = false;
                    MessageBox.Show("账号或密码错误请重新输入！");
                }
                reader.Close();

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
        public string getUserId()
        {
           
            return Id;
        }
        public string getUserPass()
        {
            return pass;
        }
        public bool getUserRoot()
        {
            return root;
        }
        public int[] getBooks()
        {
            return books;
        }

    }
}
