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
using System.Diagnostics;

namespace Hastane_Sistemi
{
    public partial class FrmDokdorDetay : Form
    {
        public FrmDokdorDetay()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();
        public string DoktorTc;
        public string DoktorAdSoyad;
        public string DoktorBranş;

        void DoktorAitRandevular()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Randevular where RandevuDoktor='" + lbldktrAdsoyad.Text + "'", bgl.bgl());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmDokdorDetay_Load(object sender, EventArgs e)
        {
            labelHastaadsyd.Text = "Null";
            labelHastaTc.Text = "Null";


            lbldktrTc.Text = DoktorTc;
            lbldktrKlinik.Text = DoktorBranş;
            SqlCommand sorgu = new SqlCommand("Select DoktorAd,DoktorSoyad from Doktorlar where DoktorTc=@p1", bgl.bgl());
            sorgu.Parameters.AddWithValue("@p1", lbldktrTc.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                lbldktrAdsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.bgl().Close();

            DoktorAitRandevular();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçen = dataGridView1.SelectedCells[0].RowIndex;
            rchTxtŞikayet.Text = dataGridView1.Rows[seçen].Cells[7].Value.ToString();

            int seçen2 = dataGridView1.SelectedCells[0].RowIndex;
            labelHastaTc.Text = dataGridView1.Rows[seçen2].Cells[6].Value.ToString();


            SqlCommand kmt = new SqlCommand("Select HastaAd,HastaSoyad from Hastalar where HastaTc=@p1", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", labelHastaTc.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                label6.Text = dr["HastaAd"] + " " + dr["HastaSoyad"];
            }
            bgl.bgl().Close();
            
        }

        private void checkRandevu_CheckedChanged(object sender, EventArgs e)
        {
            if (labelHastaadsyd.Text!="" && rchTxtŞikayet.Text!="")
            {
                if (checkRandevu.Checked==true)
                {
                    SqlCommand kmt = new SqlCommand("Delete from Randevular Where HastaTc='" + labelHastaTc.Text + "'", bgl.bgl());
                    kmt.ExecuteNonQuery();
                    MessageBox.Show("Randevu Tamamlandı", "Randevu Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bgl.bgl().Close();
                    labelHastaadsyd.Text = "Null";
                    labelHastaTc.Text = "Null";
                    rchTxtŞikayet.Clear();
                    checkRandevu.Checked = false;
                    DoktorAitRandevular();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com.tr/?hl=tr");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmDoktorblgDüzenleme frmDoktorblg = new FrmDoktorblgDüzenleme();
            frmDoktorblg.DoktorTc = lbldktrTc.Text;
            frmDoktorblg.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult tepki = new DialogResult();
            tepki = MessageBox.Show("Uygulamadan Ayrılıyorsunuz", "Emin Misiniz", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tepki==DialogResult.Yes)
            {
                Application.Exit();
            }
      
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

    
    }
}
