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
    public partial class Form4 : Form
    {
        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        SqlDataAdapter adapter;
        string sql = "SELECT * FROM Disciplina";
        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Disciplina],  [Name_Disciplina], [Prepod_Familiyaa], [StateDisciplina1] FROM [dbo].[Disciplina]", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Disciplina]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Disciplina]"];

        }

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(comboBox1.SelectedValue.ToString());
            procedure.procedure_Execution("Disciplina_inserttt", Dolgnost_Insert1);

            Form4 ps2 = new Form4();
            ps2.Show();
            Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dgFill(QR);
            dataGridView1.Columns["ID_Disciplina"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Название дисциплины";
            dataGridView1.Columns[2].HeaderCell.Value = "Препод";
            dataGridView1.Columns[3].HeaderCell.Value = "Состояние";
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
                    comboBox1.SelectedItem= row.Cells[2].Value.ToString();
                    state.Text = row.Cells[3].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (state.Text == "Используется")
            {
                state.Text = "На удалении";
                var result = MessageBox.Show("Поместить на удаление?", "Удаление",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {


                    Procedure_Class procedure = new Procedure_Class();
                    ArrayList Student_update1 = new ArrayList();
                    Student_update1.Add(ID.Text);
                    Student_update1.Add(textBox1.Text);
                    Student_update1.Add(comboBox1.SelectedItem.ToString());
                    Student_update1.Add(state.Text);
                    procedure.procedure_Execution("Disciplina_update", Student_update1);
                    dgFill(QR);


                    state.Text = "Используется";

                }
            }
            if (state.Text == "На удалении")
            {
                Procedure_Class procedure = new Procedure_Class();
                adapter = new SqlDataAdapter(sql, connect);
                ArrayList Student_update1 = new ArrayList();
                Student_update1.Add(ID.Text);
                procedure.procedure_Execution("Disciplina_delete", Student_update1);
                dgFill(QR);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox4.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                if (dataGridView1[1, i].Value.ToString() != textBox4.Text)
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i--;
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            Student_update1.Add(textBox1.Text);
            Student_update1.Add(comboBox1.SelectedItem.ToString());
            Student_update1.Add(state.Text);
            procedure.procedure_Execution("Disciplina_update", Student_update1);
            dgFill(QR);
        }
    }
}
