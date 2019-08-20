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
    public partial class Sorun : Form
    {
        int Id;
        public Sorun(int id)
        {
            InitializeComponent();
            Id = id;
            vericek();
        }
        DataSet table = new DataSet();
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        SqlDataReader a;



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

        public void vericek()
        {

            //SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            //SqlCommand cm = new SqlCommand("select *From Sorunlar where Calisan_Kodu=" + Id, con);
            //SqlDataReader dr = cm.ExecuteReader();
            //List<string> liste = new List<string>();
            //List<int> liste2 = new List<int>();
            //if (dr.HasRows)
            //{
            //    while(dr.Read())
            //    {
            //        listView1.Columns.Add(dr["SorunDetayları"].ToString(),5);
            //        MessageBox.Show("Test");
            //        liste.Add(dr["SorunDetayları"].ToString());
            //        liste2.Add(int.Parse(dr["Giderildimi"].ToString()));
            //    }
            //} 
             con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select SorunDetayları From Sorunlar where Calisan_Kodu=" + Id, con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table,"Sorunlar");
            dataGridView1.DataSource = table.Tables["Sorunlar"];
            dataGridView1.Refresh();
    
        }
        private void Sorun_FormClosed(object sender, FormClosedEventArgs e)
        {

            CalisanProfili f1 = (CalisanProfili)Application.OpenForms["CalisanProfili"];
            f1.Show();
        }

        private void Sorun_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Visible = true;
            label3.Visible = true;
            textBox1.Visible = true;
            Height = 433;
            pictureBox2.Visible = true;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            string sorun = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            textBox1.Text = sorun;
            SqlCommand cm = new SqlCommand("Select giderildimi from Sorunlar where SorunDetayları='"+sorun+"'",con);
            
             a= cm.ExecuteReader();
            if(a.HasRows)
            {
                while(a.Read())
                {
                    if(a["giderildimi"].ToString()=="1")
                    {
                        pictureBox2.Show();
                        pictureBox1.Hide();
                    }
                    else
                    {

                        pictureBox1.Show();
                        pictureBox2.Hide();
                    }
                }
            }
            a.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sorun_Ekle srn = new Sorun_Ekle(Id);
            srn.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
