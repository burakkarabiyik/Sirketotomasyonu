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
    public partial class ToplantiEkle : Form
    {
        public ToplantiEkle()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        DateTime dt;

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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
                if (con.State == ConnectionState.Closed) con.Open();
                cmd = new SqlCommand("Insert Into ToplantiBilgisi(Konusu,Tarihi)Values(@v1,@v2)", con);
                cmd.Parameters.AddWithValue("v1", textBox1.Text);
                cmd.Parameters.AddWithValue("v2", dt);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Eklendi");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void ToplantiEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            CalisanProfili cd = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            cd.Show();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dt =DateTime.Parse(e.Start.ToShortDateString());
            MessageBox.Show(e.Start.Date.ToShortDateString());
        }
    }
}
