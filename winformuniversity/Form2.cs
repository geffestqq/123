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
    public partial class Form2 : Form
    {
        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        SqlDataAdapter adapter;
        string sql = "SELECT * FROM Student";
        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Student],  [Familiya_Student], [Name_Student], [Otchestvo_Student], [State1]  FROM [dbo].[Student]", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Student]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Student]"];

        }

  
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dgFill(QR);
            dataGridView1.Columns["ID_Student"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Фамилия студента";
            dataGridView1.Columns[2].HeaderCell.Value = "Имя Студента";
            dataGridView1.Columns[3].HeaderCell.Value = "Отчество Студента";
            dataGridView1.Columns[4].HeaderCell.Value = "Статус";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(textBox2.Text);
            Dolgnost_Insert1.Add(textBox3.Text);
            Dolgnost_Insert1.Add(textBox4.Text="статус1");
            procedure.procedure_Execution("Student_inser", Dolgnost_Insert1);

            Form2 ps2 = new Form2();
            ps2.Show();
            Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value == null ? "" : cell.Value.ToString();
                    pdfTable.AddCell(value);
                }
            }

            //Exporting to PDF
            string folderPath = "C:\\PDFs\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
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
                    state.Text = row.Cells[4].Value.ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "export.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                Export_Data_To_Word(dataGridView1, sfd.FileName);
            }
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Microsoft.Office.Interop.Word.Document oDoc = new Microsoft.Office.Interop.Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Microsoft.Office.Interop.Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "your header text";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            Student_update1.Add(textBox1.Text);
            Student_update1.Add(textBox2.Text);
            Student_update1.Add(textBox3.Text);
            Student_update1.Add(state.Text);
            procedure.procedure_Execution("Student_updated", Student_update1);
            Form2 ps2 = new Form2();
            ps2.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (state.Text== "Используется")
            {
                state.Text = "На удалении";
                var result = MessageBox.Show("Поместить на удаление?","Удаление",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    

                    Procedure_Class procedure = new Procedure_Class();
                    ArrayList Student_update1 = new ArrayList();
                    Student_update1.Add(ID.Text);
                    Student_update1.Add(textBox1.Text);
                    Student_update1.Add(textBox2.Text);
                    Student_update1.Add(textBox3.Text);
                    Student_update1.Add(state.Text);
                    procedure.procedure_Execution("Student_updated", Student_update1);
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
                procedure.procedure_Execution("Student_delete", Student_update1);
                dgFill(QR);
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
        //Поиск данных
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

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
