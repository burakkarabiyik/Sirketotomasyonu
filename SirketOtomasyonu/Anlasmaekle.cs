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
    public partial class Anlasmaekle : Form
    {
        public Anlasmaekle()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection con;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cd = new SqlCommand("select COUNT(Turu) from AnlasmaliSirketler where SirketAdi='" + textBox1.Text + "'", con);
            int _parameter = Convert.ToInt32(cd.ExecuteScalar());
            con.Close();

            if (_parameter == 0)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cm = new SqlCommand("Insert Into AnlasmaliSirketler(SirketAdi,Turu,Alanı) Values(@v1,@v2,@v3)", con);
                cm.Parameters.AddWithValue("@v1", textBox1.Text);
                cm.Parameters.AddWithValue("@v2", textBox2.Text);
                cm.Parameters.AddWithValue("@v3", textBox3.Text);
                cm.ExecuteNonQuery();
                MessageBox.Show("Anlaşma Sağlandı");
                this.Close();
            }
            else
            {
                MessageBox.Show("Anlaşmanız Zaten Var");
            }
        }

        private void Anlasmaekle_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalisanProfili F = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            F.Show();
        }
    }
}
