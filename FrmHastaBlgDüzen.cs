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
    public partial class FrmHastaBlgDüzen : Form
    {
        public FrmHastaBlgDüzen()
        {
            InitializeComponent();
        }
        
        Sql bgl = new Sql();
        public string tc ;

        string HastaCins;

        private void FrmHastaBlgDüzen_Load(object sender, EventArgs e)
        {
            txtTc.Text = tc;

            SqlCommand kmt = new SqlCommand("Select * from Hastalar where HastaTc='" + txtTc.Text + "'", bgl.bgl());
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                txtBxAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                txtTc.Text = dr[3].ToString();
                maskedTextBox1.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();

                if (dr[6].ToString()=="Erkek")
                {
                    radioButtonErkek.Checked = true;
                }
                if (dr[6].ToString() == "Kadın")
                {
                    radioButtonKadın.Checked = true;
                }
                bgl.bgl().Close();
            }
        }

        private void buttonKayıt_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("update Hastalar set HastaAd=@HastaAd,HastaSoyad=@HastaSoyad,HastaTel=@HastaTel,HastaŞifre=@HastaŞifre,HastaCinsiyet=@HastaCinsiyet", bgl.bgl());
            kmt.Parameters.AddWithValue("@HastaAd", txtBxAd.Text);
            kmt.Parameters.AddWithValue("@HastaSoyad", txtSoyad.Text);
            kmt.Parameters.AddWithValue("@HastaTel", maskedTextBox1.Text);
            kmt.Parameters.AddWithValue("@HastaŞifre", txtSifre.Text);
            kmt.Parameters.AddWithValue("@HastaCinsiyet", HastaCins);
            kmt.ExecuteNonQuery();
            MessageBox.Show("Bilgileriniz Güncellendi", "Gizliliginiz Saglanmaktadır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.bgl().Close();
        }

        private void radioButtonErkek_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonErkek.Checked==true)
            {
                HastaCins = "Erkek";
            }
            else
            {
                HastaCins = "";
            }
        }

        private void radioButtonKadın_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKadın.Checked == true)
            {
                HastaCins = "Kadın";
            }
            else
            {
                HastaCins = "";
            }
        }
    }
}
