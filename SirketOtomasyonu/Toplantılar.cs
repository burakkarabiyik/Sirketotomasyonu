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
    public partial class Toplantılar : Form
    {
        public Toplantılar()
        {
            InitializeComponent();
            vericek();
            textBox1.Hide();
            button1.Hide();
            button2.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();

        }


        DataSet table = new DataSet();
        SqlConnection con;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        public void vericek()
        {

            
            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            dataAdapter = new SqlDataAdapter("Select Konusu,Tarihi From ToplantiBilgisi", con);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            table = new DataSet();
            table.Tables.Clear();
            dataAdapter.Fill(table, "ToplantiBilgisi");
            dataGridView1.DataSource = table.Tables["ToplantiBilgisi"]; 

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



        int toplantiid;
        SqlCommand cmd;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            string sorun = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            textBox1.Text = sorun;
            textBox1.Show();
            button1.Show();
            button2.Show();
            pictureBox2.Show();
            pictureBox3.Show();

            con = new SqlConnection("Data Source=.;Initial Catalog=SirketDb;Integrated Security=True");
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("Select *From ToplantiBilgisi where Konusu = @id", con);
           
            SqlDataReader dt;
            cmd.Parameters.AddWithValue("@id", sorun);
            dt = cmd.ExecuteReader();
            if (dt.HasRows)
                while (dt.Read())
                {
                    toplantiid = int.Parse(dt["ToplantiId"].ToString());

                }
            dt.Close();

        }
        Form1 fr = (Form1)Application.OpenForms["Form1"];

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cd = new SqlCommand("select COUNT(KimlikNo) from Katilacaklar where KimlikNo=@v1 and ToplantiId=@v2", con);
            cd.Parameters.AddWithValue("v1", fr.Id);
            cd.Parameters.AddWithValue("v2", toplantiid);
            int _parameter = Convert.ToInt32(cd.ExecuteScalar());
            con.Close();

            if (_parameter == 0)
            {
                
               
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cm = new SqlCommand("Insert Into Katilacaklar(ToplantiId,KimlikNo,KatilmaDurumu) Values(@v1,@v2,@v3)", con);
                    cm.Parameters.AddWithValue("@v1", toplantiid);
                    cm.Parameters.AddWithValue("@v2", fr.Id);
                    cm.Parameters.AddWithValue("@v3", 'e');
                    cm.ExecuteNonQuery();
              
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "update Katilacaklar set KatilmaDurumu=@v1 where ToplantiId=@v2 and KimlikNo=@v3";
                SqlCommand cm = new SqlCommand(kayit, con);
                cm.Parameters.AddWithValue("v1", 'e');
                cm.Parameters.AddWithValue("v2", toplantiid);
                cm.Parameters.AddWithValue("v3", fr.Id);
                cm.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cd = new SqlCommand("select COUNT(KimlikNo) from Katilacaklar where ToplantiId=@v1 and KimlikNo=@v2", con);
            cd.Parameters.AddWithValue("v1", toplantiid);
            cd.Parameters.AddWithValue("v2", fr.Id);
            int _parameter = Convert.ToInt32(cd.ExecuteScalar());
            con.Close();

            if (_parameter == 0)
            {

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    Form1 fr = (Form1)Application.OpenForms["Form1"];
                    SqlCommand cm = new SqlCommand("Insert Into Katilacaklar(ToplantiId,KimlikNo,KatilmaDurumu) Values(@v1,@v2,@v3)", con);
                    cm.Parameters.AddWithValue("@v1", toplantiid);
                    cm.Parameters.AddWithValue("@v2", fr.Id);
                    cm.Parameters.AddWithValue("@v2", 'h');
                    cm.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "update Katilacaklar set KatilmaDurumu=@v1 where ToplantiId=@v2 and KimlikNo=@v3";
                SqlCommand cm = new SqlCommand(kayit, con);
                cm.Parameters.AddWithValue("v1", 'h');
                cm.Parameters.AddWithValue("v2", toplantiid);
                cm.Parameters.AddWithValue("v3", fr.Id);
                cm.ExecuteNonQuery();
            }
        }

        CalisanProfili fr2 = (CalisanProfili)Application.OpenForms["CalisanProfili"];
        private void Toplantılar_FormClosed(object sender, FormClosedEventArgs e)
        {
            fr2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
