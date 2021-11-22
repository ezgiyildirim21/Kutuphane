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
    public partial class HesabimForm : Form
    {
        private readonly Kullanici kullanici;
        private readonly KutuphaneYoneticisi kutuphaneYoneticisi;

        public HesabimForm(Kullanici kullanici, KutuphaneYoneticisi kutuphaneYoneticisi)
        {
            InitializeComponent();
            this.kullanici = kullanici;
            this.kutuphaneYoneticisi = kutuphaneYoneticisi;
            KullaniciBilgileriniGoster();
        }

        private void KullaniciBilgileriniGoster()
        {
            lblID.Text = $"ID: {kullanici.Id}";
            lblAdSoyad.Text = $"ID: {kullanici.AdSoyad}";
            lblKullaniciAdi.Text = $"ID: {kullanici.KullaniciAdi}";
            lblParola.Text = $"ID: {kullanici.Parola}";

            dgvOduncAlinanKitaplar.DataSource = kullanici.OduncAlinanKitaplar;
        }

        private void btnKitapTeslimEt_Click(object sender, EventArgs e)
        {
            kullanici.OduncAlinanKitaplar.Remove((Kitap)dgvOduncAlinanKitaplar.DataSource);
            kutuphaneYoneticisi.Kitaplar.Add((Kitap)dgvOduncAlinanKitaplar.DataSource);
        }
    }
}
