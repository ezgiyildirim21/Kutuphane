using Kutuphane.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class Form1 : Form
    {
        KullaniciYoneticisi kullaniciYoneticisi;
        public Form1()
        {
            InitializeComponent();
            try
            {
                string json = File.ReadAllText("veriKullanici.json");
                kullaniciYoneticisi = JsonConvert.DeserializeObject<KullaniciYoneticisi> (json);

            }
            catch (Exception)
            {

                kullaniciYoneticisi = new KullaniciYoneticisi();
            }
        }

        private void linkLblKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayitOlForm kayitOlForm = new KayitOlForm(kullaniciYoneticisi);
            kayitOlForm.ShowDialog();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            Kullanici girisYapan = kullaniciYoneticisi.OturumAcma(txtKullaniciAdi.Text.Trim(), txtParola.Text);
            if(girisYapan == null)
                MessageBox.Show("Kullanıcı adı yada şifre hatalı!");
            else
            {
                KutuphaneForm kutuphaneForm = new KutuphaneForm(girisYapan);
                kutuphaneForm.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string json = JsonConvert.SerializeObject(kullaniciYoneticisi);
            File.WriteAllText("veriKullanici.json", json);
        }
    }
}
