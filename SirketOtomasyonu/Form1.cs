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
    public partial class Form1 : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
             
        }
        public int Id;
        SqlDataReader dt;
        MusteriProfil frm;
        CalisanProfili cs;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Kimlik No Giriniz");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Şifre Giriniz");
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd = new SqlCommand("Select *From Kullanicilar where KullaniciAdi = @id and Sifre = @Sifre", con);

                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Sifre", textBox2.Text);
                dt = cmd.ExecuteReader();
                
                if (dt.HasRows)
                {
                    while (dt.Read())
                    {

                        if (dt["turu"].ToString() == "m")
                        {

                            frm = new MusteriProfil(int.Parse(textBox1.Text));
                            Id = int.Parse(textBox1.Text);
                            frm.Show();
                        }
                        else if(dt["turu"].ToString()=="c")
                        {
                            cs = new CalisanProfili(int.Parse(textBox1.Text));
                            Id = int.Parse(textBox1.Text);
                            cs.Show();

                        }
                        else
                        {
                            cs = new CalisanProfili(int.Parse(textBox1.Text));
                            cs.yetkili();
                            Id = int.Parse(textBox1.Text);
                            cs.Show();
                        }

                        this.Hide();
                    }
                }
                else if (!dt.HasRows)
                {
                    MessageBox.Show("Hatalı Giriş Yaptınız");
                }

                dt.Close();
            }
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //    Application.Exit();
        //}

        private Point MouseDownLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //minimum
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //---------------
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            kayit kyt = new kayit();
            kyt.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_2(object sender, EventArgs e)
        {

        }
    }
}
