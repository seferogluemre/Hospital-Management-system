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
using System.Data.SqlClient;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hastane_Sistemi
{
    public partial class FrmBranşİşlem : Form
    {
        public FrmBranşİşlem()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        void AracTemizle()
        {
            txtBranşAd.Clear();
            textBoxıd.Clear();  
        }

        void BranşListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Branşlar", bgl.bgl());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmBranşİşlem_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Branşlar", bgl.bgl());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçen = dataGridView1.SelectedCells[0].RowIndex;
            textBoxıd.Text = dataGridView1.Rows[seçen].Cells[0].Value.ToString();
            txtBranşAd.Text = dataGridView1.Rows[seçen].Cells[1].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand kmt = new SqlCommand("insert into Branşlar (BranşAd) values (@p1,@p2)", bgl.bgl());
                kmt.Parameters.AddWithValue("@p1", textBoxıd.Text);
                kmt.Parameters.AddWithValue("@p2", txtBranşAd.Text);
                kmt.ExecuteNonQuery();
                MessageBox.Show("Listeye Doktor Eklendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BranşListesi();
            }
            catch (Exception errorMessage)
            {
                MessageBox.Show(errorMessage.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut1 = new SqlCommand("Delete from Branşlar where BranşId=@p1", bgl.bgl());
                komut1.Parameters.AddWithValue("@p1", textBoxıd.Text);
                komut1.ExecuteNonQuery();
                bgl.bgl().Close();
                MessageBox.Show("Kayıt silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                BranşListesi();
                AracTemizle();
            }
            catch (Exception)
            {
                MessageBox.Show("Programda Hata meydana geldi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Branşlar set BranşAd=@p2 where BranşId=@p1", bgl.bgl());
            komut.Parameters.AddWithValue("@p1",textBoxıd.Text);
            komut.Parameters.AddWithValue("@p2",txtBranşAd.Text);
            komut.ExecuteNonQuery();
            bgl.bgl().Close();
            MessageBox.Show("Bilgiler güncellendi", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BranşListesi();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AracTemizle();
        }
    }
}
