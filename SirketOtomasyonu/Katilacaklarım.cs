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
    public partial class Katilacaklarım : Form
    {
        public Katilacaklarım()
        {
            InitializeComponent();
            vericek();
        }

        DataSet table = new DataSet();
        SqlConnection con; 

        Form1 fr = (Form1)Application.OpenForms["Form1"];
        SqlCommand cmd;
        public void vericek()
        {
            ////////////////katılacağım Toplantılarrrrrr
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select *From Katilacaklar where KimlikNo =" + fr.Id+" and KatilmaDurumu='e'", con);

            SqlDataReader dt;
            List<int> toplantilar = new List<int>();
            dt = cmd.ExecuteReader();
            int i=0;
            if (dt.HasRows)
                while (dt.Read())
                {
                    toplantilar.Add(int.Parse(dt["ToplantiId"].ToString()));
                    i++;
                }
            dt.Close();
            DataTable ds=new DataTable();
            ds.Columns.Add("Konusu");
            ds.Columns.Add("Tarihi");

            DataRow dr;

            for (int a = 0; a < i; a++)
            {
                cmd = new SqlCommand("Select *From ToplantiBilgisi where ToplantiId ="+toplantilar[a] , con);
                dr = ds.NewRow();
                dt = cmd.ExecuteReader();
                if (dt.HasRows)
                    while (dt.Read())
                    {
                        dr["Konusu"] = dt["Konusu"].ToString();
                        dr["Tarihi"] = dt["Tarihi"].ToString();
                        ds.Rows.Add(dr);
                    }
                dt.Close();
            }
            dataGridView1.DataSource = ds;
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

        CalisanProfili fs = (CalisanProfili)Application.OpenForms["CalisanProfili"];
        private void Katilacaklarım_FormClosed(object sender, FormClosedEventArgs e)
        {
            fs.Show();
        }
    }
}
