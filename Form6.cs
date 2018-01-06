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
    public partial class Form6 : Form
    {
        public Form6()
        {
            adapter = null;
            dataset = null;
            InitializeComponent();


        }
        private SqlDataAdapter adapter;
        private DataSet dataset;
        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“myDatabaseDataSet.books”中。您可以根据需要移动或删除它。
            //this.booksTableAdapter.Fill(this.myDatabaseDataSet.books);
            string ConnectionString = @"Data Source=127.0.0.1/;Initial Catalog=MyDatabase;" + "Integrated Security=SSPI";
            adapter = new SqlDataAdapter("select * from books", ConnectionString);
            dataset = new DataSet();
            adapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder scd = new SqlCommandBuilder(adapter);
            try
            {
                adapter.Update(dataset);
                MessageBox.Show("修改成功！","提示");
            }
            catch(SqlException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
