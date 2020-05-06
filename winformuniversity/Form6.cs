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
    public partial class Form6 : Form
    {
        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        SqlDataAdapter adapter;
        string sql = "SELECT * FROM Vedomost";
        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Vedomost],  [Ocenka_Vedomosti], [Data_Vedomosti], [Disciplina_Name], [Studenta_Familiya]  FROM [dbo].[Vedomost]", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Vedomost]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Vedomost]"];

        }
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            lbFill();
            dgFill(QR);
            dataGridView1.Columns["ID_Vedomost"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Оценка Ведомость";
            dataGridView1.Columns[2].HeaderCell.Value = "Дата Ведомость";
            dataGridView1.Columns[3].HeaderCell.Value = "Дисциплина";
            dataGridView1.Columns[4].HeaderCell.Value = "Студент";
        }
        private void lbFill()
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-T3ECMD0\\GEFFEST;" +
                   " Initial Catalog = Universitet_Bekov; Persist Security Info = true;" +
                   " User ID = sa; Password = \"c2f5i4f53\"");
            SqlCommand cmd;
            SqlDataReader dr;

            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Disciplina";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["Name_Disciplina"].ToString());

            }
            con.Close();


            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Student";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox2.Items.Add(dr["Familiya_Student"].ToString());

            }
            con.Close();
        }
            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    listBox1.SelectedItem = row.Cells[3].Value.ToString();
                    listBox2.SelectedItem = row.Cells[4].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(textBox2.Text);
            Dolgnost_Insert1.Add(listBox1.SelectedItem.ToString());
            Dolgnost_Insert1.Add(listBox2.SelectedItem.ToString());
            procedure.procedure_Execution("Vedomost_insert", Dolgnost_Insert1);
            Form6 ps2 = new Form6();
            ps2.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            Student_update1.Add(textBox1.Text);
            Student_update1.Add(textBox2.Text);
            Student_update1.Add(listBox1.SelectedItem.ToString());
            Student_update1.Add(listBox2.SelectedItem.ToString());
            procedure.procedure_Execution("Vedomost_update", Student_update1);
            Form6 ps2 = new Form6();
            ps2.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            adapter = new SqlDataAdapter(sql, connect);
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            procedure.procedure_Execution("Vedomost_delete", Student_update1);
            dgFill(QR);
        }
    }
}
