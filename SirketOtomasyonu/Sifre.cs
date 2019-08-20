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
    public partial class Sifre : Form
    {
        public Sifre()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select Sifre From Kullanicilar where KullaniciAdi = @id", con);
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            SqlDataReader dt;
            cmd.Parameters.AddWithValue("@id", frm.Id);
            dt = cmd.ExecuteReader();
            int sifre=0;
            if (dt.HasRows)
                while (dt.Read())
                {
                    sifre = int.Parse(dt["Sifre"].ToString());

                }
            dt.Close();
            if(textBox1.Text!=sifre.ToString())
            {
                MessageBox.Show("Eski Şifre Hatalı");
            }
            else if(textBox2.Text!=textBox3.Text)
            {
                MessageBox.Show("Şifreler Eşleşmiyor");
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "update Kullanicilar set Sifre=@ad where KullaniciAdi=@kod";
                SqlCommand cm = new SqlCommand(kayit, con);
                cm.Parameters.AddWithValue("kod", frm.Id);
                cm.Parameters.AddWithValue("ad", textBox3.Text);
                cm.ExecuteNonQuery();
                MessageBox.Show("Şifre Başarılı bir şekilde Değişti");
                this.Close();
            }
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
        MusteriProfil ms = (MusteriProfil)Application.OpenForms["MusteriProfil"];
        private void Sifre_FormClosed(object sender, FormClosedEventArgs e)
        {
            ms.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
