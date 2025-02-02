using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.FormClosed += (s, args) => this.Close();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made By the best team");
        }
    }
}
