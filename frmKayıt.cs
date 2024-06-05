using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Hastane_Sistemi
{
    public partial class frmKayıt : Form
    {
        public frmKayıt()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();
        string HastaCinsiyet;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                HastaCinsiyet = "Erkek";
            }
            else
            {
                HastaCinsiyet = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBxAd.Text != "" && txtSoyad.Text != "" && txtTc.Text != "" && maskedTextBox1.MaskCompleted == true && txtSifre.Text != "" && radioButton1.Checked != false || radioButton2.Checked != false)
            {
                SqlCommand kmt = new SqlCommand("insert into Hastalar (HastaAd,HastaSoyad,HastaTc,HastaTel,HastaŞifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.bgl());
                kmt.Parameters.AddWithValue("@p1", txtBxAd.Text);
                kmt.Parameters.AddWithValue("@p2", txtSoyad.Text);
                kmt.Parameters.AddWithValue("@p3", txtTc.Text);
                kmt.Parameters.AddWithValue("@p4", maskedTextBox1.Text);
                kmt.Parameters.AddWithValue("@p5", txtSifre.Text);
                kmt.Parameters.AddWithValue("@p6", HastaCinsiyet);
                kmt.ExecuteNonQuery();
                MessageBox.Show("Kaydınız Yapıldı", "Başarılı Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bgl.bgl().Close();
            }
            else
            {
                MessageBox.Show("Hatalı deger girişi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                HastaCinsiyet = "Kadın";
            }
            else
            {
                HastaCinsiyet = "";
            }
        }
    }
}
