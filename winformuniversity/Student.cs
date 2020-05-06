using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformuniversity
{
    public partial class Form1 : Form
    {
        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Student], [Familiya_Student], [Name_Student], [Otchestvo_Student] FROM [dbo].[Student] ", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Student]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Student]"];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(textBox2.Text);
            Dolgnost_Insert1.Add(textBox3.Text);
            procedure.procedure_Execution("Student_inser", Dolgnost_Insert1);

            Form1 ps2 = new Form1();
            ps2.Show();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgFill(QR);
            dataGridView1.Columns["ID_Student"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Фамилия студента";
            dataGridView1.Columns[2].HeaderCell.Value = "Имя студента";
            dataGridView1.Columns[3].HeaderCell.Value = "Отчество студента";


        }
    }
}
