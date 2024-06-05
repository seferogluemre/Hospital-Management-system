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
    public partial class Form1 : Form
    {
        public Form1()
        {
        
            InitializeComponent();
        }

        //Sql sınıfı
        Sql bgl = new Sql();
        ErrorProvider error = new ErrorProvider();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxTc.Text.Length!=11)
            {
                error.SetError(textBoxTc, "Tc Alanı 11 karakter olmalı");
            }
            else
            {
                error.Clear();
            }
            SqlCommand kmt = new SqlCommand("select * from Hastalar where HastaTc=@p1 and HastaŞifre=@p2", bgl.bgl());
            kmt.Parameters.AddWithValue("@p1", textBoxTc.Text);
            kmt.Parameters.AddWithValue("@p2", textBoxŞifre.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                frmHastaDetay frmHastaDetay = new frmHastaDetay();
                frmHastaDetay.hastaTc = textBoxTc.Text;
                MessageBox.Show("Hoşgeldiniz", "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmHastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Tc Veya Şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.bgl().Close();

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxŞifre.PasswordChar = '*';
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxŞifre.PasswordChar = '\0';
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmKayıt fr = new frmKayıt();
            fr.Show();
        }
    }
}
