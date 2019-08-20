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
    public partial class Oneri : Form
    {
        public Oneri()
        {
            InitializeComponent();
        }
        SqlConnection con;
        Form1 frm = (Form1)Application.OpenForms["Form1"];
        MusteriProfil frm1 = (MusteriProfil)Application.OpenForms["MusteriProfil"];
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cm = new SqlCommand("Insert Into Oneri(Oneri,MusteriKodu) Values(@v1,@v2)", con);
            cm.Parameters.AddWithValue("@v1", textBox1.Text);
            cm.Parameters.AddWithValue("@v2", frm.Id);
            cm.ExecuteNonQuery();  
            MessageBox.Show("Eklendi");

            this.Close();
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

        private void Oneri_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm1.Show();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
