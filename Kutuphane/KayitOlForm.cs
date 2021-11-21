using Kutuphane.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KayitOlForm : Form
    {
        private readonly KullaniciYoneticisi kullaniciYoneticisi;
        public KayitOlForm(KullaniciYoneticisi kullaniciYoneticisi)
        {
            InitializeComponent();
            this.kullaniciYoneticisi = kullaniciYoneticisi;
        }               

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            string adSoyad = txtAdSoyad.Text.Trim();
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string parola = txtParola.Text;
            if(ParolaDogrula() || kullaniciYoneticisi.KullaniciVarMi(kullaniciAdi) || string.IsNullOrEmpty(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Hatalı işlem");
                return;
            }
            kullaniciYoneticisi.KayitOlma(adSoyad, kullaniciAdi, parola);
            MessageBox.Show("Kayıt işlemi başarıyla gerçekleştirildi.");
            Close();
        }

        private bool ParolaDogrula()
        {
            return (txtParola.Text != txtParolaTekrar.Text) || string.IsNullOrEmpty(txtParola.Text) || string.IsNullOrEmpty(txtParolaTekrar.Text);
        }

        private void txtParolaTekrar_TextChanged(object sender, EventArgs e)
        {
            if(txtParola.Text != txtParolaTekrar.Text)
            {
                lblDogrulama.ForeColor = Color.Red;
                lblDogrulama.Text = "Parolalar eşleşmedi.";
            }
            else
            {
                lblDogrulama.ForeColor = Color.Green;
                lblDogrulama.Text = "Parolalar eşleşti.";
            }
        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if(kullaniciYoneticisi.KullaniciVarMi(txtKullaniciAdi.Text.Trim()) || string.IsNullOrEmpty(txtKullaniciAdi.Text))
            {
                lblDogrulama.ForeColor = Color.Red;
                lblDogrulama.Text = "Kullanıcı adı uygun değil.";
            }            
            else
            {
                lblDogrulama.ForeColor = Color.Green;
                lblDogrulama.Text = "Kullanıcı adı uygun.";
            }
        }
    }
}
