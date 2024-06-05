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

namespace Hastane_Sistemi
{
    public partial class frmHastaDetay : Form
    {
        public frmHastaDetay()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();
        public string hastaTc;
        private void frmHastaDetay_Load(object sender, EventArgs e)
        {
            lblHastaTC.Text = hastaTc;
            try
            {
                //Hastanın Tcsine göre AD Soyad bilgisini aktarma
                SqlCommand sqlCommand = new SqlCommand("Select HastaAd,HastaSoyad from Hastalar where HastaTc=@p1", bgl.bgl());
                sqlCommand.Parameters.AddWithValue("@p1", lblHastaTC.Text);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    labelHastaADSOYAD.Text = sqlDataReader[0] + " " + sqlDataReader[1];
                }
                bgl.bgl().Close();

                //Hastanın TC göre randevu geçmişini çekme
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from Randevular where HastaTc=" + hastaTc, bgl.bgl());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                bgl.bgl().Close();

                //Comboboxa branş aktarma
                SqlCommand kmt = new SqlCommand("Select BranşAd from Branşlar", bgl.bgl());
                SqlDataReader dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    cmbBRANŞ.Items.Add(dr[0]);
                }
                bgl.bgl().Close();

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult tepki = new DialogResult();
            tepki = MessageBox.Show("Çıkış Yapıyorsunuz", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tepki==DialogResult.OK)
            {
                Application.Exit();
            }
        }

       

        private void btnRANDEVUAL_Click(object sender, EventArgs e)
        {
            //Sekreterin oluşturdugu aktif randevuları görünütleyip Hastanın randevu almasını saglama
            SqlCommand kmt = new SqlCommand("Update Randevular Set RandevuDurum=1,HastaTc=@p1,HastaŞikayet=@p2 where RandevuId=@p3", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", lblHastaTC.Text);
            kmt.Parameters.AddWithValue("@p2", richŞİKAYT.Text);
            kmt.Parameters.AddWithValue("@p3", txtID.Text);
            kmt.ExecuteNonQuery();
            bgl.bgl().Close();
            MessageBox.Show("Randevu alındı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRANDEVUAL_Click_1(object sender, EventArgs e)
        {
            if (cmbDOKTOR.SelectedItem==null)
            {
                MessageBox.Show("Doktor Alanını Seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                if (richŞİKAYT.Text=="")
                {
                    MessageBox.Show("Şikayetinizi Belirtiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    if (cmbDOKTOR.SelectedItem != null && richŞİKAYT.Text != "")
                    {
                        SqlCommand randevu = new SqlCommand("update Randevular set RandevuDurum=1,HastaTc=@HastaTc,HastaŞikayet=@HastaŞikayet where RandevuId=@p1", bgl.bgl());
                        randevu.Parameters.AddWithValue("@HastaTc", lblHastaTC.Text);
                        randevu.Parameters.AddWithValue("HastaŞikayet", richŞİKAYT.Text);
                        randevu.Parameters.AddWithValue("@p1", txtID.Text);
                        randevu.ExecuteNonQuery();
                        MessageBox.Show("Randevu Kaydınız Yapıldı", "Kayıt Yapıldı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bgl.bgl().Close();

                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seç = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[seç].Cells[0].Value.ToString();
        }

        private void cmbBRANŞ_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Seçilen Branşa göre doktor listeleme
            cmbDOKTOR.Items.Clear();
            SqlCommand kmt = new SqlCommand("select DoktorAd,DoktorSoyad from Doktorlar where DoktorBranş='" + cmbBRANŞ.Text + "'", bgl.bgl());
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                cmbDOKTOR.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.bgl().Close();
        }

        private void cmbDOKTOR_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Seçilen doktora göre aktif randevuları listeleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Randevular where RandevuBranş='" + cmbBRANŞ.Text + "'" + " and RandevuDoktor='" + cmbDOKTOR.Text + "' and RandevuDurum=0", bgl.bgl());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void linkLBİLGİDÜZENLE_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBlgDüzen hastaBlgDüzen = new FrmHastaBlgDüzen();
            hastaBlgDüzen.tc = hastaTc;
            hastaBlgDüzen.Show();
        }
    }
}
