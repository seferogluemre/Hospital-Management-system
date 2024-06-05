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

namespace Hastane_Sistemi
{
    public partial class FrmDoktorİşlem : Form
    {
        public FrmDoktorİşlem()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        void AracTemizle()
        {
            textBoxId.Clear();
            textBoxad.Clear();
            txtSoyad.Clear();
            txtBranş.Clear();
            txtTc.Clear();
            txtŞifre.Clear();
        }

        void DoktorListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Doktorlar", bgl.bgl());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmDoktorİşlem_Load(object sender, EventArgs e)
        {
            DoktorListesi();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AracTemizle();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand kmt = new SqlCommand("insert into Doktorlar (DoktorAd,DoktorSoyad,DoktorBranş,DoktorTc,DoktorŞifre) values (@p1,@p2,@p3,@p4,@p5)", bgl.bgl());
                kmt.Parameters.AddWithValue("@p1", textBoxad.Text);
                kmt.Parameters.AddWithValue("@p2", txtSoyad.Text);
                kmt.Parameters.AddWithValue("@p3", txtBranş.Text);
                kmt.Parameters.AddWithValue("@p4", txtTc.Text);
                kmt.Parameters.AddWithValue("@p5", txtŞifre.Text);
                kmt.ExecuteNonQuery();
                MessageBox.Show("Listeye Doktor Eklendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DoktorListesi();
            }
            catch (Exception errorMessage)
            {
                MessageBox.Show(errorMessage.Message);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçen = dataGridView1.SelectedCells[0].RowIndex;
            textBoxId.Text = dataGridView1.Rows[seçen].Cells[0].Value.ToString();
            textBoxad.Text = dataGridView1.Rows[seçen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[seçen].Cells[2].Value.ToString();
            txtBranş.Text = dataGridView1.Rows[seçen].Cells[3].Value.ToString();
            txtTc.Text = dataGridView1.Rows[seçen].Cells[4].Value.ToString();
            txtŞifre.Text = dataGridView1.Rows[seçen].Cells[5].Value.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut1 = new SqlCommand("Delete from Doktorlar where DoktorTc=@p1", bgl.bgl());
                komut1.Parameters.AddWithValue("@p1", txtTc.Text);
                komut1.ExecuteNonQuery();
                bgl.bgl().Close();
                MessageBox.Show("Kayıt silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DoktorListesi();
                AracTemizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Programda Hata meydana geldi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Doktorlar set DoktorAd=@p2,DoktorSoyad=@p3,DoktorBranş=@p4,DoktorŞifre=@p5 where DoktorTc=@p6", bgl.bgl());
            komut.Parameters.AddWithValue("@p6", txtTc.Text);
            komut.Parameters.AddWithValue("@p2", textBoxad.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p4", txtBranş.Text);
            komut.Parameters.AddWithValue("@p5", txtŞifre.Text);
            komut.ExecuteNonQuery();
            bgl.bgl().Close();
            MessageBox.Show("Bilgiler güncellendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DoktorListesi();
            
        }
    }
}
