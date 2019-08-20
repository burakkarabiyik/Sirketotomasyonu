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
    public partial class SirketBilgisi : Form
    {
        public SirketBilgisi()
        {
            InitializeComponent();
        }
        SqlConnection con;

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cd = new SqlCommand("select COUNT(Turu) from SirketBilgisi", con);
            int _parameter = Convert.ToInt32(cd.ExecuteScalar());
            
            if (_parameter == 0)
            {
                SqlCommand cm = new SqlCommand("Insert Into SirketBilgisi(SirketAdi,Turu,TelefonNo,Adres) Values(@v1,@v2,@v3,@v4)", con);
                cm.Parameters.AddWithValue("@v1", textBox1.Text);
                cm.Parameters.AddWithValue("@v2", textBox2.Text);
                cm.Parameters.AddWithValue("@v3", textBox3.Text);
                cm.Parameters.AddWithValue("@v4", textBox3.Text);
                cm.ExecuteNonQuery();
            }
            else
            {
                string kayit = "update SirketBilgisi set SirketAdi=@v1,Turu=@v2,TelefonNo=@v3,Adres=@v4 where Id=1";
                SqlCommand cm = new SqlCommand(kayit, con);
                cm.Parameters.AddWithValue("@v1", textBox1.Text);
                cm.Parameters.AddWithValue("@v2", textBox2.Text);
                cm.Parameters.AddWithValue("@v3", textBox3.Text);
                cm.Parameters.AddWithValue("@v4", textBox3.Text);
                cm.ExecuteNonQuery();
            }
        }
        CalisanProfili fr = (CalisanProfili)Application.OpenForms["CalisanProfili"];
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SirketBilgisi_FormClosed(object sender, FormClosedEventArgs e)
        {
            fr.Show();
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
