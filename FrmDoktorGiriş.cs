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
    public partial class FrmDoktorGiriş : Form
    {
        public FrmDoktorGiriş()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();

        private void FrmDoktorGiriş_Load(object sender, EventArgs e)
        {

        
        }

        private void FrmDoktorGiriş_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonGirişyap_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("Select * From Doktorlar where DoktorTc=@p1 and DoktorŞifre=@p2",bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", textBoxTckimlik.Text);
            kmt.Parameters.AddWithValue("@p2", textBoxParola.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                FrmDokdorDetay dokdorDetay = new FrmDokdorDetay();
                dokdorDetay.DoktorTc = textBoxTckimlik.Text;
                dokdorDetay.DoktorBranş = dr["DoktorBranş"].ToString();
                MessageBox.Show("Hoşgeldiniz", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dokdorDetay.Show();
                this.Hide();
                bgl.bgl().Close();
            }
        }
    }
}
