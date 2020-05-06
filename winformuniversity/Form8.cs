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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Procedure_Class procedures = new Procedure_Class();
            //  Configuration_class connection = new Configuration_class();
            // connection.dbEnter(textBox1.Text, textBox2.Text);
            // switch (Configuration_class.IDuser)
            // {
            //   case (0):
               textBox1.BackColor = System.Drawing.Color.Red;
              textBox2.BackColor = System.Drawing.Color.Red;
              label1.Text = "Введён не верный логнин или пароль!";
            textBox1.Text = "";
             textBox2.Text = "";
            //      break;
            //    default:
            //     Configuration_class.strDostup = procedures.fDostup(textBox1.Text, textBox2.Text);

               //    Form7 ps2 = new Form7();
               //   ps2.Show();
             //   Hide();

            //      break;
            // }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 ps2 = new Form9();
            ps2.Show();
            Hide();
        }
    }
}
