using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformuniversity
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            foreach (char dost in Configuration_class.strDostup)
            {
               // if (dost == '0')
                {
                    button1.Visible = false;
                }




            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
