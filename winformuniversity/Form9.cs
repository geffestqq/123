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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 ps2 = new Form8();
            ps2.Show();
            Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox3.Text != textBox2.Text)
            {
                label1.Text = "Пароли не совпадают";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.BackColor = System.Drawing.Color.Red;
                textBox3.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                Procedure_Class procedure = new Procedure_Class();
                ArrayList Dolgnost_Insert1 = new ArrayList();
                Dolgnost_Insert1.Add(textBox1.Text);
                Dolgnost_Insert1.Add(textBox2.Text);
                procedure.procedure_Execution("Admin_inser", Dolgnost_Insert1);
                MessageBox.Show("Учетная запись создана");
            }
        }
    }
}
