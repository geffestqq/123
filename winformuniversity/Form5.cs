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
using Microsoft.Office.Interop.Excel;

namespace winformuniversity
{
    public partial class Form5 : Form
    {

        string connect = "Data Source = DESKTOP-T3ECMD0\\GEFFEST; Initial Catalog = Universitet_Bekov; Persist Security Info = true; User ID = sa; Password = \"c2f5i4f53\"";
        private string QR = "";
        SqlDataAdapter adapter;
        string sql = "SELECT * FROM Diplom";
        private void dgFill(string qr)
        {
            DataSet ds = new DataSet();
            SqlConnection dataBaseConnection = new SqlConnection(connect);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select [ID_Diplom],  [Date_Diplom], [Student_Familiya], [Ocenka_Diplom], [Kurator],[Fakultet_Name]  FROM [dbo].[Diplom]", dataBaseConnection);
            dataAdapter.Fill(ds, "[dbo].[Diplom]");
            dataGridView1.DataSource = ds.Tables["[dbo].[Diplom]"];

        }
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            lbFill();
            dgFill(QR);
            dataGridView1.Columns["ID_Diplom"].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Дата диплома";
            dataGridView1.Columns[2].HeaderCell.Value = "Фамилия Студента";
            dataGridView1.Columns[3].HeaderCell.Value = "Оценка";
            dataGridView1.Columns[4].HeaderCell.Value = "Куратор";
            dataGridView1.Columns[5].HeaderCell.Value = "Факультет";
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
            cmd.CommandText = "select * from Prepod";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox3.Items.Add(dr["Familiya_Prepod"].ToString());

            }
            con.Close();

            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Fakultet";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox2.Items.Add(dr["Name_Fakultet"].ToString());

            }
            con.Close();

            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Student";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["Familiya_Student"].ToString());

            }
            con.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();

            ArrayList Dolgnost_Insert1 = new ArrayList();
            Dolgnost_Insert1.Add(textBox1.Text);
            Dolgnost_Insert1.Add(listBox1.SelectedItem.ToString());
            Dolgnost_Insert1.Add(textBox2.Text);
            Dolgnost_Insert1.Add(listBox2.SelectedItem.ToString());
            Dolgnost_Insert1.Add(listBox3.SelectedItem.ToString());
            procedure.procedure_Execution("Diplom_insert", Dolgnost_Insert1);
            Form5 ps2 = new Form5();
            ps2.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            Student_update1.Add(textBox1.Text);
            Student_update1.Add(listBox1.SelectedItem.ToString());
            Student_update1.Add(textBox2.Text);
            Student_update1.Add(listBox2.SelectedItem.ToString());
            Student_update1.Add(listBox3.SelectedItem.ToString());
            procedure.procedure_Execution("Diplom_update", Student_update1);
            Form5 ps2 = new Form5();
            ps2.Show();
            Hide();
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
                    listBox1.SelectedItem = row.Cells[2].Value.ToString();
                    textBox2.Text = row.Cells[3].Value.ToString();
                    listBox2.SelectedItem = row.Cells[4].Value.ToString();
                    listBox3.SelectedItem = row.Cells[5].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Procedure_Class procedure = new Procedure_Class();
            adapter = new SqlDataAdapter(sql, connect);
            ArrayList Student_update1 = new ArrayList();
            Student_update1.Add(ID.Text);
            procedure.procedure_Execution("Diplom_delete", Student_update1);
            dgFill(QR);
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

        private void button7_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application objexcelapp = new Microsoft.Office.Interop.Excel.Application();
            objexcelapp.Application.Workbooks.Add(Type.Missing);
            objexcelapp.Columns.ColumnWidth = 25;
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                objexcelapp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            /*For storing Each row and column value to excel sheet*/
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        objexcelapp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            MessageBox.Show("Your excel file exported successfully at D:\\" + "test322" + ".xlsx");
            objexcelapp.ActiveWorkbook.SaveCopyAs("C:\\PDFs\\" + "test322" + ".xlsx");
            objexcelapp.ActiveWorkbook.Saved = true;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream("pdftest.pdf", FileMode.Create));

            //Открываем документ
            doc.Open();

            //Определение шрифта необходимо для сохранения кириллического текста
            //Иначе мы не увидим кириллический текст
            //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Обход по всем таблицам датасета (хотя в данном случае мы можем опустить
            //Так как в нашей бд только одна таблица)

            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

            //Adding Header row

            //Adding Header row



            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,font));
                //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                table.AddCell(cell);
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
              
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    table.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), font));
                }
               
            }

             //Creating iTextSharp Table from the DataTable data
           

            //Добавляем таблицу в документ
            doc.Add(table);

            //Закрываем документ
            doc.Close();

        }
    }
}
