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
    public partial class Form3 : Form
    {
        public Form3()
        {
           
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("select * from books where 书名 like '" + textBox1.Text + "%'", conn);
                SqlDataReader reader = comm.ExecuteReader();
                string str = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    str += reader.GetName(i) + "         ";
                }
                str += "\n";
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        str += reader[i] + "  ";
                    str += "\n";
                }
                if (reader.HasRows)
                    MessageBox.Show(str, "查询结果");
                else MessageBox.Show("无匹配记录！", "查询结果");
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
    }
}
