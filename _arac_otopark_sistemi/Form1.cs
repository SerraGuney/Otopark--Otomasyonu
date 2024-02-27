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
namespace _arac_otopark_sistemi
{
    public partial class Form1 : Form
    {
        private void OpenFormAnimation()
        {
            this.Opacity = 0; // Formun başlangıçta görünürlüğü sıfır olsun

            for (double i = 0; i <= 1; i += 0.1)
            {
                this.Opacity = i;
                this.Refresh();
                System.Threading.Thread.Sleep(50);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=DESKTOP-SAFRJB1\\SQLEXPRESS;Initial Catalog=DB_arac_otopark_sistemi;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);
        private void button1_Giriş_Click(object sender, EventArgs e)
        {
            OpenFormAnimation();
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                string kayit = "insert into TBL_KAYIT(plaka,giriş_tarihi,ad,soyad,marka,araç_rengi,telefon,email,adres)values(@plaka,@giriş_tarihi,@ad,@soyad,@marka,@araç_rengi,@telefon,@email,@adres)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@plaka", textPlaka.Text);
                lblGeliş.Text = DateTime.Now.ToString();
                komut.Parameters.AddWithValue("@giriş_tarihi", DateTime.Now);
                komut.Parameters.AddWithValue("@ad", textAd.Text);
                komut.Parameters.AddWithValue("@soyad", textSoyad.Text);
                komut.Parameters.AddWithValue("@marka", textMarka.Text);
                komut.Parameters.AddWithValue("@araç_rengi", textRenk.Text);
                komut.Parameters.AddWithValue("@telefon", textTel.Text);
                komut.Parameters.AddWithValue("@email", textEmail.Text);
                richAdres.Text = textRich.Text;
                komut.Parameters.AddWithValue("@adres", richAdres.Text);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt eklendi.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi:" + hata.Message);
            }


        }

        private void button1_Çıkış_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                string kayit = "insert into TBL_UCRETLER(çıkış_tarihi,süre,tutar)values(@çıkış_tarihi,@süre,@tutar)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                lblÇıkış.Text = DateTime.Now.ToString();
                komut.Parameters.AddWithValue("çıkış_tarihi", DateTime.Now);
                DateTime geliş, çıkış;
                geliş = DateTime.Parse(lblGeliş.Text);
                çıkış = DateTime.Parse(lblÇıkış.Text);
                TimeSpan fark;
                fark = çıkış - geliş;
                lblSüre.Text = fark.TotalSeconds.ToString("0.00");
                komut.Parameters.AddWithValue("@süre", lblSüre.Text);
                lblTutar.Text = (double.Parse(lblSüre.Text) * (0.75)).ToString("0.00");
                komut.Parameters.AddWithValue("@tutar", lblTutar.Text);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt çıkışı yapıldı.");

            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi:" + hata.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}


