using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Hastane_Sistemi
{
    public partial class frmRandevuSorgu : Form
    {
        public frmRandevuSorgu()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();
        DataSet1TableAdapters.RandevularTableAdapter datasetRandevu = new DataSet1TableAdapters.RandevularTableAdapter();
        DataSet1TableAdapters.Randevular1TableAdapter sorguData = new DataSet1TableAdapters.Randevular1TableAdapter();
        
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult tepki = new DialogResult();
            tepki = MessageBox.Show("Uygulamadan Ayrılcaksınız Attıgınız Sorgular Kaybolacak", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tepki==DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void frmRandevuSorgu_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayıt_durumu = false;

            if (txthastaTc.Text!="")
            {
                SqlCommand kmt = new SqlCommand("Select  * from  Randevular Where  (HastaTc = @p1)", bgl.bgl());
                kmt.Parameters.AddWithValue("@p1", txthastaTc.Text);
                SqlDataReader dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    MessageBox.Show("Sorgu Başarılı Kayıt Bulundu", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kayıt_durumu = true;
                    listboxHastaRandevu.Items.Add(dr["RandevuTarih"].ToString());
                    listboxHastaRandevu.Items.Add(dr["RandevuSaat"].ToString());
                    listboxHastaRandevu.Items.Add(dr["RandevuBranş"].ToString());
                    listboxHastaRandevu.Items.Add(dr["RandevuDoktor"].ToString());
                    listboxHastaRandevu.Items.Add(dr["HastaTc"].ToString());
                    listboxHastaRandevu.Items.Add(dr["HastaŞikayet"].ToString());
                }
                bgl.bgl().Close();
            }
            if (kayıt_durumu==false)
            {
                MessageBox.Show("Girilen Tcye Ait Sorgu Bulunamadı", "Bulamadık", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
           
        }
    }
}
