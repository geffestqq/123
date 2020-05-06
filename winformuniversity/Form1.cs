using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
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
        //test git
        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        SqlDataAdapter adapter;
        string sql = "SELECT * FROM Prepod";
        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Prepod],  [Familiya_Prepod], [Name_Prepod], [Otchestvo_Prepod]  FROM [dbo].[Prepod]", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Prepod]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Prepod]"];

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(textBox2.Text);
            Dolgnost_Insert1.Add(textBox3.Text);
            procedure.procedure_Execution("Prepod_ins", Dolgnost_Insert1);

            Form1 ps2 = new Form1();
            ps2.Show();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgFill(QR);
            dataGridView1.Columns["ID_Prepod"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Фамилия Преподователя";
            dataGridView1.Columns[2].HeaderCell.Value = "Имя Преподователя";
            dataGridView1.Columns[3].HeaderCell.Value = "Отчество Преподователя";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            Student_update1.Add(textBox1.Text);
            Student_update1.Add(textBox2.Text);
            Student_update1.Add(textBox3.Text);
            procedure.procedure_Execution("Prepod_update", Student_update1);
            Form1 ps2 = new Form1();
            ps2.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            adapter = new SqlDataAdapter(sql, connect);
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            procedure.procedure_Execution("Prepod_delet", Student_update1);
            dgFill(QR);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                if (row != null)
                {
                    ID.Text = row.Cells[0].Value.ToString();
                    textBox1.Text = row.Cells[1].Value.ToString();
                    textBox2.Text = row.Cells[2].Value.ToString();
                    textBox3.Text = row.Cells[3].Value.ToString();
                }
            }
        }
    }
}
