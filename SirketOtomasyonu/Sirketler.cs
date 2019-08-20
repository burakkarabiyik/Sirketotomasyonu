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
    public partial class Sirketler : Form
    {
        public Sirketler()
        {
            InitializeComponent();
            vericek();
        }

        DataSet table = new DataSet();
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        public void vericek()
        {


            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select SirketAdi,Turu as Türü,Alanı From AnlasmaliSirketler", con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table, "AnlasmaliSirketler");
            dataGridView1.DataSource = table.Tables["AnlasmaliSirketler"];

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Anlasmaekle ekle = new Anlasmaekle();
            ekle.Show();
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

        private void Sirketler_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalisanProfili cp = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            cp.Show();
            
        }
    }
}
