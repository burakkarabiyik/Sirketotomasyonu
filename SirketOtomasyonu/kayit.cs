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
    public partial class kayit : Form
    {
        SqlConnection con;
        public kayit()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");

        }

        SqlCommand cmd;
        SqlDataReader dr;

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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Form1 frm = (Form1)Application.OpenForms["Form1"];
        private void kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) con.Open();
            cmd = new SqlCommand("Select *from Kullanicilar where KullaniciAdi=" + textBox1.Text, con);
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                MessageBox.Show("Bu Müşteri No ile bir kullanıcı vardır !");
                dr.Close();
            } 
            else
            {
                dr.Close();
                cmd = new SqlCommand("Insert Into Kullanicilar(KullaniciAdi,Sifre,Turu) Values(@v1,@v2,@v3)", con);
                cmd.Parameters.AddWithValue("v1", textBox1.Text);
                cmd.Parameters.AddWithValue("v2", textBox2.Text);
                cmd.Parameters.AddWithValue("v3", 'm');
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Insert Into Musteri(Musteri_No,Musteri_Ad,Musteri_Soyad,Musteri_Tc,Musteri_Telno,Musteri_Adres) Values(@v1,@v2,@v3,@v4,@v5,@v6)", con);
                cmd.Parameters.AddWithValue("v1", textBox1.Text);
                cmd.Parameters.AddWithValue("v2", textBox3.Text);
                cmd.Parameters.AddWithValue("v3", textBox4.Text);
                cmd.Parameters.AddWithValue("v4", textBox5.Text);
                cmd.Parameters.AddWithValue("v5", textBox6.Text);
                cmd.Parameters.AddWithValue("v6", textBox7.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanici Eklendi");
                frm.textBox1.Text = textBox1.Text;
                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        
    }
}
