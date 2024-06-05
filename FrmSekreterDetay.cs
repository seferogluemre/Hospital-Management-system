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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();

        public string SekreterTc;
        public string SekreterAd;

        void AracTemizle()
        {
            txtİD.Clear();
            msktxtTARİH.Clear();
            maskedTextTARİHS2.Clear();
            cmbBRANŞ.SelectedItem = -1;
            cmbDOKTOR.SelectedItem = -1;
            checkBoxDURUM.Checked = false;
        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            labelSekreterTc.Text=SekreterTc;
            SqlCommand kmt = new SqlCommand("Select * from Sekreterler where SekreterTc=@p1", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", labelSekreterTc.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                labelSekreterTc.Text = dr["SekreterTc"].ToString();
                labelSekreterAdsoyad.Text = dr["SekreterAdSoyad"].ToString();
            }
            bgl.bgl().Close();


            //Doktorları liste aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Doktorlar", bgl.bgl());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branş comboboxa aktarma
            SqlCommand komut=new SqlCommand("Select BranşAd from Branşlar", bgl.bgl());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                cmbBRANŞ.Items.Add(reader["BranşAd"].ToString());
            }
            bgl.bgl().Close();

            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand("Select BranşAd from Branşlar", bgl.bgl());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("insert into Duyurular (Duyuru) values (@p1)", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", richTextBoxDuyuru.Text);
            kmt.ExecuteNonQuery();
            MessageBox.Show("Duyuru Oluşturuldu", "Duyuru Verildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.bgl().Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmDoktorİşlem frmDoktorİşlem = new FrmDoktorİşlem();
            frmDoktorİşlem.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FrmRandevular frmRandevular = new FrmRandevular();
            frmRandevular.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmBranşİşlem frmBranşİşlem = new FrmBranşİşlem();
            frmBranşİşlem.Show();
        }

        private void btnTemz_Click(object sender, EventArgs e)
        {
            AracTemizle();
        }

        private void btnKAYDET_Click(object sender, EventArgs e)
        {
            if (txtİD.Text == "" || msktxtTARİH.Text == "" || maskedTextTARİHS2.Text == "" || cmbBRANŞ.Text == "" || cmbDOKTOR.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayınız");

            }
            else
            {
                SqlCommand komutkaydet = new SqlCommand("insert into Randevular (RandevuId,RandevuTarih,RandevuSaat,RandevuBranş,RandevuDoktor) values(@r1,@r2,@r3,@r4,@r5)", bgl.bgl());
                komutkaydet.Parameters.AddWithValue("@r1", txtİD.Text);
                komutkaydet.Parameters.AddWithValue("@r2", msktxtTARİH.Text);
                komutkaydet.Parameters.AddWithValue("@r3", maskedTextTARİHS2.Text);
                komutkaydet.Parameters.AddWithValue("@r4", cmbBRANŞ.Text);
                komutkaydet.Parameters.AddWithValue("@r5", cmbDOKTOR.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.bgl().Close();
                MessageBox.Show("Randevu oluşturuldu");
            }
        }

        private void cmbBRANŞ_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDOKTOR.Items.Clear();
            SqlCommand kmt = new SqlCommand("Select DoktorAd,DoktorSoyad From Doktorlar Where DoktorBranş='" + cmbBRANŞ.Text + "'", bgl.bgl());
            SqlDataReader dataReader = kmt.ExecuteReader();
            while (dataReader.Read())
            {
                cmbDOKTOR.Items.Add(dataReader[0] + " " + dataReader[1]);
            }
            bgl.bgl().Close();
        }

        private void cmbDOKTOR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
