using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SirketOtomasyonu
{
    public partial class YeniCalisan : Form
    {
        public YeniCalisan()
        {
            InitializeComponent();
        }
        SqlConnection con;

        private void button1_Click(object sender, EventArgs e)
        {
            ///ekleenecekkkkkkk

            try
            {
                con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
                if (con.State == ConnectionState.Closed) con.Open();
                string kayit = "Insert Into Kullanicilar(KullaniciAdi,Sifre,turu) Values(@k,@s,@a)";
                SqlCommand cd = new SqlCommand(kayit, con);
                cd.Parameters.AddWithValue("k", textBox1.Text);
                cd.Parameters.AddWithValue("s", textBox1.Text);
                cd.Parameters.AddWithValue("a", 'c');
                cd.ExecuteNonQuery();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit2 = "Insert Into Calisan(Id,Calisan_Ad,Calisan_Soyad,Calisan_Telno,Calisan_Tc,Calisan_Dep,Calisan_Yetkisi) Values(@id,@ad,@soy,@tel,@tc,@dep,@yet)";
                SqlCommand cm = new SqlCommand(kayit2, con);
                cm.Parameters.AddWithValue("id", textBox1.Text);
                cm.Parameters.AddWithValue("ad", textBox2.Text);
                cm.Parameters.AddWithValue("soy", textBox3.Text);
                cm.Parameters.AddWithValue("tel", textBox4.Text);
                cm.Parameters.AddWithValue("tc", textBox5.Text);
                cm.Parameters.AddWithValue("dep", textBox6.Text);
                cm.Parameters.AddWithValue("yet", textBox7.Text);
                cm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Bu kimlikte Çalışan vardır");

            }
        }

        private void YeniCalisan_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalisanProfili frm = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            frm.Show(); 
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

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
