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
    public partial class SatınAlınanUrunler : Form
    {
        int Id;
        public SatınAlınanUrunler(int id)
        {
            InitializeComponent();
            Id = id;
            vericek();
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


        private void SatınAlınanUrunler_Load(object sender, EventArgs e)
        {

        }
        DataSet table = new DataSet();
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder; 
        public void vericek()
        { 
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select Satilan_Urun,Fiyat,Adres From Odemeler where Satınalankisi=" + Id, con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table, "Odemeler");
            dataGridView1.DataSource = table.Tables["Odemeler"];
            dataGridView1.Refresh();
            dataGridView1.Columns["Satilan_Urun"].HeaderText = "Satınalınan Ürün";

        }

        private void SatınAlınanUrunler_FormClosed(object sender, FormClosedEventArgs e)
        {
            MusteriProfil mf = (MusteriProfil)Application.OpenForms["MusteriProfil"];
            mf.Show(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
