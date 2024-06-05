using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Sistemi
{
    public partial class FrmHastaneGiriş : Form
    {
        public FrmHastaneGiriş()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 frHastagirş = new Form1();
            frHastagirş.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Sistemde Girişiniz Bulunamadı?", "E-Nabız", MessageBoxButtons.OK, MessageBoxIcon.Question);
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FrmDoktorGiriş frmDoktor = new FrmDoktorGiriş();
            frmDoktor.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRandevuSorgu frmRandevu = new frmRandevuSorgu();
            frmRandevu.Show();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            DialogResult tepki = new DialogResult();
            tepki = MessageBox.Show("Çıkış yapıyorsunuz", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tepki==DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FrmSekreterGiriş sekreterGiriş = new FrmSekreterGiriş();
            sekreterGiriş.Show();
            this.Hide();
        }
    }
}
