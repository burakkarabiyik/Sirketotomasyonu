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
    public partial class MusteriProfil : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        int Id;
        public MusteriProfil(int musterino)
        {
            InitializeComponent();
            button1.Hide();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select *From Musteri where Musteri_No = @id", con);
            Id = musterino;
            SqlDataReader dt;
            cmd.Parameters.AddWithValue("@id", musterino);
            dt = cmd.ExecuteReader();
            if (dt.HasRows)
                while (dt.Read())
                {
                    textBox3.Text = dt["Musteri_Telno"].ToString();
                    textBox1.Text = dt["Musteri_Ad"].ToString();
                    textBox2.Text = dt["Musteri_Soyad"].ToString();
                    textBox4.Text = dt["Musteri_Tc"].ToString();
                    textBox6.Text = dt["Musteri_Adres"].ToString();
                    
                }
            dt.Close();
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

        private void Duyuru_
            (object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kaydet

            string kayit = "update Musteri set Musteri_Ad=@v1,Musteri_Soyad=@v2,Musteri_Tc=@v3,Musteri_Telno=@v4,Musteri_Adres=@v5 where Musteri_No="+Id;
            SqlCommand cm = new SqlCommand(kayit, con);
            cm.Parameters.AddWithValue("@v1", textBox1.Text);
            cm.Parameters.AddWithValue("@v2", textBox2.Text);
            cm.Parameters.AddWithValue("@v3", textBox4.Text);
            cm.Parameters.AddWithValue("@v4", textBox3.Text);
            cm.Parameters.AddWithValue("@v5", textBox6.Text);
            cm.ExecuteNonQuery();
            MessageBox.Show("Kaydedildi");
        }
        bool cek = true;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cek)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;
                button1.Show();
                cek = false;
            }
            else
            {
                cek = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox6.Enabled = false;
                button1.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SatınAlınanUrunler urunler = new SatınAlınanUrunler(Id);
            urunler.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Urun urn = new Urun();
            urn.part = 2;
            urn.Show(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sifre sf = new Sifre();
            sf.Show();
            this.Hide();
        }
 

        private void button6_Click(object sender, EventArgs e)
        {
            Oneri oneri = new Oneri();
            oneri.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MusteriProfil_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
