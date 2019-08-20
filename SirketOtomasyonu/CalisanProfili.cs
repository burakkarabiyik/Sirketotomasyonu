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
    public partial class CalisanProfili : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        int Id; 
        public CalisanProfili(int id)
        {
            InitializeComponent();
            

            button1.Hide();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select *From Calisan where Id = @id", con);
            Id = id;
            SqlDataReader dt;
            cmd.Parameters.AddWithValue("@id", id);
            dt = cmd.ExecuteReader();
            if (dt.HasRows)
                while (dt.Read())
                {
                    textBox1.Text = dt["Calisan_Ad"].ToString();
                    textBox2.Text = dt["Calisan_Soyad"].ToString();
                    textBox3.Text = dt["Calisan_Telno"].ToString();
                    textBox4.Text = dt["Calisan_Tc"].ToString();
                    textBox6.Text = dt["Calisan_Dep"].ToString();

                }
            dt.Close();  
        }
        public void yetkili()
        {
            button5.Visible = true;
            button4.Visible = true;
            button6.Visible = true;
            button8.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;
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

        private void CalisanProfili_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private Point MouseDownLocation;

        private void Mouse1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void Mouse2(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sorun srn = new Sorun(Id);
            srn.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Urun urn = new Urun();
            urn.part = 1;
            urn.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Maas maas = new Maas(1);
            maas.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Maas ms = new Maas(0);
            ms.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ToplantiEkle t = new ToplantiEkle();
            t.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Toplantılar tp = new Toplantılar();
            tp.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Sirketler sr = new Sirketler();
            sr.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SirketBilgisi sr = new SirketBilgisi();
            sr.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Katilacaklarım kt = new Katilacaklarım();
            kt.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kayit = "update Calisan set Calisan_Ad=@v1,Calisan_Soyad=@v2,Calisan_Tc=@v3,Calisan_Telno=@v4,Calisan_Dep=@v5 where Id=" + Id;
            SqlCommand cm = new SqlCommand(kayit, con);
            cm.Parameters.AddWithValue("@v1", textBox1.Text);
            cm.Parameters.AddWithValue("@v2", textBox2.Text);
            cm.Parameters.AddWithValue("@v3", textBox4.Text);
            cm.Parameters.AddWithValue("@v4", textBox3.Text);
            cm.Parameters.AddWithValue("@v5", textBox6.Text);
            cm.ExecuteNonQuery();
            MessageBox.Show("Kaydedildi");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlCommand cd = new SqlCommand("CalisanSayisi", con);
            cd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cd.ExecuteReader();
            while(dr.Read())
            {
                MessageBox.Show("Çalışan Sayısı : "+dr["frm"].ToString());
            }
            dr.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SqlCommand cd = new SqlCommand("MusteriSayisi", con);
            cd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cd.ExecuteReader();
            while (dr.Read())
            {
                MessageBox.Show("Müşteri Sayısı : " + dr["kisi"].ToString());
            }
            dr.Close();
        }

        private void CalisanProfili_Load(object sender, EventArgs e)
        {

        }
    }
}
