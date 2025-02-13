using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Battleship2
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Programming projects\c sharp sxoli\Battleship\BattleshipDB\BattleshipFinal-main\Battleship2\NewFolder1\Database2.mdf"";Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            CenterToScreen();
            LoadPlayerData();
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


        public void LoadPlayerData()
        {            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Players";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }


    }
}
