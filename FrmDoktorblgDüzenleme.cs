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
    public partial class FrmDoktorblgDüzenleme : Form
    {
        public FrmDoktorblgDüzenleme()
        {
            InitializeComponent();
        }

        public string DoktorTc;

        Sql bgl = new Sql();
        private void buttonDüzenle_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("update Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorTc=@p3,DoktorŞifre=@p4", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", TxtAd.Text);
            kmt.Parameters.AddWithValue("@p2", txtSoyad.Text);
            kmt.Parameters.AddWithValue("@p3", txtTckmlk.Text);
            kmt.Parameters.AddWithValue("@p4", txtŞifre.Text);
            kmt.ExecuteNonQuery();
            MessageBox.Show("Verileriniz Güncellendi", "Gizliliginiz Saglanmaktadır", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.bgl().Close();




        }

        private void FrmDoktorblgDüzenleme_Load(object sender, EventArgs e)
        {
            txtTckmlk.Text = DoktorTc;

            SqlCommand sorgu = new SqlCommand("Select * from Doktorlar Where DoktorTc='" + txtTckmlk.Text + "'", bgl.bgl());
            SqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                textBoxId.Text = dr["Id"].ToString();
                TxtAd.Text = dr["DoktorAd"].ToString();
                txtSoyad.Text = dr["DoktorSoyad"].ToString();
                txtTckmlk.Text = dr["DoktorTc"].ToString();
                txtŞifre.Text = dr["DoktorŞifre"].ToString();
            }
            bgl.bgl().Close();


        }
    }
}
