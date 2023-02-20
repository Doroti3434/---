using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace крестики_нолики_new
{
    public partial class Form2 : Form
    {
        public bool mode;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //один игрок
        {
            mode = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //два игрока
        {
            mode = false;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
