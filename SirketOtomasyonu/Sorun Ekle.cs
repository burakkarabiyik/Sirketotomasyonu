using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Data.SqlClient;

namespace SirketOtomasyonu
{
    public partial class Sorun_Ekle : Form
    {
        SqlConnection con;
        int id;
        public Sorun_Ekle(int Id)
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");

            id = Id;
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

        Sorun srn = (Sorun)Application.OpenForms["Sorun"];
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cm = new SqlCommand("Insert Into Sorunlar(SorunDetayları,Calisan_Kodu,Giderildimi) Values(@v1,@v2,@v3)", con);
                cm.Parameters.AddWithValue("@v1", textBox1.Text);
                cm.Parameters.AddWithValue("@v2", id);
                cm.Parameters.AddWithValue("@v3", "0");
                cm.ExecuteNonQuery();
                srn.vericek();
                srn.dataGridView1.Refresh();
                this.Close();
                MessageBox.Show("Eklendi");

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
