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
    public partial class CalisanDetay : Form
    {
        int Id;
        SqlConnection con;
        public CalisanDetay(int id,string ad,string soyad)
        {
            InitializeComponent();
            Id = id;
            label3.Text = ad;
            label4.Text = soyad;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");

            if (con.State == ConnectionState.Closed)
                con.Open();
            string kayit = "update Maas set Maas=@ad where CalisanKodu=@kod";
            SqlCommand cm = new SqlCommand(kayit, con);
            cm.Parameters.AddWithValue("kod", Id);
            cm.Parameters.AddWithValue("ad", textBox1.Text); 
            cm.ExecuteNonQuery();
            MessageBox.Show("Kaydedildi"); 
        }

        private void CalisanDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Maas frm = (Maas)Application.OpenForms["Maas"];
            CalisanProfili frm1 = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            frm.Close();
            frm1.Show();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
