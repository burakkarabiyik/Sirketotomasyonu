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
    public partial class UrunDetay : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        int ur;
        public UrunDetay(int Urun, int part)
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            ur = Urun;
            if (part == 1)
            {
                button1.Hide();
                button3.Hide();
                button2.Show();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd = new SqlCommand("Select *From Urun where Urun_Kodu = @id", con);

                SqlDataReader dt;
                cmd.Parameters.AddWithValue("@id", Urun);
                dt = cmd.ExecuteReader();
                if (dt.HasRows)
                    while (dt.Read())
                    {
                        textBox1.Text = dt["Urun_Adi"].ToString();
                        textBox2.Text = dt["Urun_Fiyat"].ToString();


                    }
                dt.Close();
            }
            else if (part == 0)
            {

                button2.Hide();
                button1.Show();
                button3.Hide();

            }
            else
            {
                button2.Hide();
                button1.Hide();
                button3.Show();
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd = new SqlCommand("Select *From Urun where Urun_Kodu = @id", con);

                SqlDataReader dt;
                cmd.Parameters.AddWithValue("@id", Urun);
                dt = cmd.ExecuteReader();
                if (dt.HasRows)
                    while (dt.Read())
                    {
                        textBox1.Text = dt["Urun_Adi"].ToString();
                        textBox2.Text = dt["Urun_Fiyat"].ToString();


                    }
                dt.Close();
            }
        }

        private void UrunDetay_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //kaydet
            if (con.State == ConnectionState.Closed)
                con.Open();
            string kayit = "update Urun set Urun_Adi=@ad,Urun_Fiyat=@fiyat where Urun_Kodu=@kod";
            SqlCommand cm = new SqlCommand(kayit, con);
            cm.Parameters.AddWithValue("kod",ur );
            cm.Parameters.AddWithValue("ad", textBox1.Text);
            cm.Parameters.AddWithValue("fiyat", textBox2.Text);
            cm.ExecuteNonQuery();
        }

        private void button1_Click(object sender, EventArgs e)
        { 

            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");

            SqlCommand cd = new SqlCommand("select COUNT(Urun_Adi) from Urun where Urun_Adi='" + textBox1.Text + "'", con);
            int _parameter = Convert.ToInt32(cd.ExecuteScalar());
            con.Close();

            if (_parameter == 0)
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "Insert Into Urun(Urun_Adi,Urun_Fiyat) Values(@ad,@fiyat)";
                SqlCommand cm = new SqlCommand(kayit, con);
                cm.Parameters.AddWithValue("ad", textBox1.Text);
                cm.Parameters.AddWithValue("fiyat", textBox2.Text);
                cm.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Böyle Bir Ürün var.");
            }
        }

        private void UrunDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Urun f1 = (Urun)Application.OpenForms["Urun"];
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             
            if (con.State == ConnectionState.Closed)
                con.Open();
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            cmd = new SqlCommand("Select *From Musteri where Musteri_No = @id", con);
            SqlDataReader dt;
            cmd.Parameters.AddWithValue("@id", frm.Id);
            dt = cmd.ExecuteReader();
            string adres="";
            if (dt.HasRows)
                while (dt.Read())
                {
                    adres = dt["Musteri_Adres"].ToString();
                }
            dt.Close();
            string kayit = "Insert Into Odemeler(Satilan_Urun,Satınalankisi,Fiyat,Adres) Values(@urun,@kisi,@fiyat,@adres)";
            SqlCommand cm = new SqlCommand(kayit, con);
            cm.Parameters.AddWithValue("urun", ur);
            cm.Parameters.AddWithValue("kisi", frm.Id);
            cm.Parameters.AddWithValue("adres", adres);
            cm.Parameters.AddWithValue("fiyat", textBox2.Text);
            cm.ExecuteNonQuery();
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
    }
}
