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
    public partial class FrmSekreterGiriş : Form
    {
        public FrmSekreterGiriş()
        {
            InitializeComponent();
        }
        Sql bgl = new Sql();
        

        private void FrmSekreterGiriş_Load(object sender, EventArgs e)
        {
            buttonGiriş.FlatStyle = FlatStyle.System;
            buttonGiriş.FlatAppearance.BorderSize = 0;
        }

        private void buttonGiriş_Click(object sender, EventArgs e)
        {
            SqlCommand kmt = new SqlCommand("SELECT * FROM Sekreterler WHERE SekreterTc = @Tc AND SekreterŞifre = @Sifre", bgl.bgl());
            kmt.Parameters.AddWithValue("@Tc", textBox1.Text);
            kmt.Parameters.AddWithValue("@Sifre", textBox2.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                FrmSekreterDetay sekreterDetay=new FrmSekreterDetay();
                sekreterDetay.SekreterTc=textBox1.Text;
                MessageBox.Show("Hoşgeldiniz", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sekreterDetay.Show();
                this.Hide();
            }
            bgl.bgl().Close();
        }
    }
}
