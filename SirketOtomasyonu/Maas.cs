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
    public partial class Maas : Form
    {
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        DataSet table;
        SqlCommandBuilder commandBuilder;
        int Id;
        public Maas(int id)
        {
            InitializeComponent();
             vericek();
            Id = id;
            if(id==1)
            { button1.Hide();
              //pictureBox1.Hide();
            }
            else { button1.Show();
                pictureBox1.Show();
            }
        }

        void vericek()
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select * From Calisan", con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table, "Calisan");
            dataGridView1.DataSource = table.Tables["Calisan"];
            dataGridView1.Refresh();
        }
        CalisanProfili frm = (CalisanProfili)Application.OpenForms["CalisanProfili"];

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Id == 1)
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ad = dataGridView1.Rows[secili].Cells[1].Value.ToString();
                string soyad = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string id = dataGridView1.Rows[secili].Cells[0].Value.ToString();

                CalisanDetay dty = new CalisanDetay(int.Parse(id), ad, soyad);
                dty.Show();
                this.Hide();
            }
            else
            {

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

        private void button1_Click(object sender, EventArgs e)
        {
            YeniCalisan yeni = new YeniCalisan();
            yeni.Show();
            this.Hide();
        }

        private void Maas_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
