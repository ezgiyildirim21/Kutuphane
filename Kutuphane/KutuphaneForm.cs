using Kutuphane.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KutuphaneForm : Form
    {
        KutuphaneYoneticisi kutuphaneYoneticisi;
        // new yapmamamızın sebebi: her başladığında yeni oluşturuyor fakat bizde json varsa yapmasın yeniden
        private readonly Kullanici kullanici;

        public KutuphaneForm(Kullanici kullanici)
        {
            InitializeComponent();
            this.kullanici = kullanici; 
            VerileriOku();
            TurleriEkle();
            DataGuncelle(); // bağış yaptıktan sonra, kitap ödünç aldıktan sonra bu data güncelle metodunu kullanacağız. Kod tekrarı yapmamak için metot hale getiriyoruz.
        }

        private void VerileriOku()
        {
            try
            {
                // VARSA OKU
                string json = File.ReadAllText("veriKutuphane.json");
                kutuphaneYoneticisi = JsonConvert.DeserializeObject<KutuphaneYoneticisi>(json);
                                        // generic metot    <type> ne type yaparsak o olur.

            }
            catch (Exception)
            {
                // YOKSA YENİDEN OLUŞTUR
                kutuphaneYoneticisi = new KutuphaneYoneticisi();
            }
        }

        private void tsmiCikisYap_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiKitapOduncAl_Click(object sender, EventArgs e)
        {
            // Kutuphaneden seçili kitabı kaldıracağız ve o anki login olmuş kullanıcının ödünç aldığı kitaplara ekleyeceğim.
            // Ve o anki tarih bilgisini kitabın ödünç alınma tarihine yazıcam.

            // 1. kitabı bul
            // 2. kitabı ödünç alınan kitaplara ekle
            // 3. kitabı kütüphaneden sil.

            Kitap aranankitap = (Kitap)dgvKitaplar.SelectedRows[0].DataBoundItem;
            
            
            
            DataGuncelle();
        }

        private void dgvKitaplar_MouseClick(object sender, MouseEventArgs e)
        {
            // Sağ click olduysa
            if(e.Button == MouseButtons.Right)
            {
                var position = dgvKitaplar.HitTest(e.X, e.Y).RowIndex;  // sadece kitap olan yerlerde kitap ödünç al çalışsın.
                if (position >= 0)
                {
                    contextMenuStrip1.Show(dgvKitaplar, new Point(e.X, e.Y));
                    dgvKitaplar.Rows[position].Selected = true; // sağ click yaptığımızda dgvKitaplar daki seçimi değiştirmek için
                }                
            }
        }

        private void TurleriEkle()
        {
            cboTurler.Items.Add("Hepsi");
            //cboTurler.Items.Add(KitapTur.Psikoloji);
            //cboTurler.Items.Add(KitapTur.Fantastik);
            //cboTurler.Items.Add(KitapTur.Tarih);
            //cboTurler.Items.Add(KitapTur.BilimKurgu);
            //cboTurler.Items.Add(KitapTur.Polisiye);
            //cboTurler.Items.Add(KitapTur.Biyografi);
            //cboTurler.Items.Add(KitapTur.Korku);
            //cboTurler.Items.Add(KitapTur.Egitim); 
            foreach (var item in Enum.GetValues(typeof(KitapTur)))
            {
                cboTurler.Items.Add(item);
            }
            cboTurler.SelectedIndex = 0;    
            // İlk değer seçili gelsin dersek 0. Seçili gelmesin dersek -1 
                          
        }        
        private void DataGuncelle()
        {
            dgvKitaplar.DataSource = null;
            dgvKitaplar.DataSource = kutuphaneYoneticisi.Kitaplar;
            dgvKitaplar.Columns[0].Visible = false; // gizlemek için kullanıyoruz.
            dgvKitaplar.Columns[7].Visible = false;
        }

        private void tsmiBagisYap_Click(object sender, EventArgs e)
        {
            BagisYapForm bagisYapForm = new BagisYapForm(kutuphaneYoneticisi);
            bagisYapForm.ShowDialog();
            DataGuncelle();
        }      
        private void KutuphaneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerileriKaydet();
        }
        private void VerileriKaydet()
        {
            string json = JsonConvert.SerializeObject(kutuphaneYoneticisi);
            File.WriteAllText("veriKutuphane.json", json);
        }

        private void tsmHesabim_Click(object sender, EventArgs e)
        {
            HesabimForm hesabimForm = new HesabimForm(kullanici,kutuphaneYoneticisi);
            hesabimForm.ShowDialog();
        }
    }
}
