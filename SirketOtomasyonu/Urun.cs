using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SirketOtomasyonu
{
    public partial class Urun : Form
    {
        public Urun()
        {
            InitializeComponent();
            vericek();
        }

        private void Urun_Load(object sender, EventArgs e)
        {
            if(part==2)
            {
                button1.Hide();
            }
        }
        DataSet table = new DataSet();
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        public void vericek()
        {
 
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select * From Urun ", con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table, "Urun");
            dataGridView1.DataSource = table.Tables["Urun"];
            dataGridView1.Refresh();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int part;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            string sorun = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            UrunDetay dty = new UrunDetay(int.Parse(sorun),part);
            dty.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UrunDetay dty = new UrunDetay(0,0);
            dty.Show();
            this.Hide();
        }
        private Point MouseDownLocation;

        private void mous(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void mous2(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Urun_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
