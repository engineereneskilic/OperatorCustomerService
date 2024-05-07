using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient; // sql kütüphanesini çağırıyoruz

namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    public partial class Form1_icerik_kredi_karti : Form
    {
        public Form1_icerik_kredi_karti()
        {
            InitializeComponent();
        }

        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);

        private void Form1_icerik_kredi_karti_Load(object sender, EventArgs e)
        {
            // form1_icerik_dem seçilen tutarı tutar labele yazdırıyoruz..
            tutar_label.Text = Convert.ToString(Form1_icerik.gidecek_tl_miktarı)+" TL";
            // girişte kullandığımız rasgele karakterler üreten classımızı çağıralım
            rasgele_label.Text = giris.rasgeleUret();

        }

        private void onayla_button_Click(object sender, EventArgs e)
        {
            baglanti1.Open();// sql bir daha geldiğinde açılıp kapanma kontrolü
            SqlCommand kart_sorgu = new SqlCommand("SELECT * FROM kredi_karti_bilgileri WHERE kart_tel_no='"+giris.kullanici_tel+"' AND kart_no='"+kredi_karti_no_kutusu.Text+"' AND kart_sifre='"+kredi_karti_sifre_kutusu.Text+"' AND kart_son_tarih='"+kredi_karti_tarih_kutusu1.Text+"/"+kredi_karti_tarih_kutusu2.Text+"'", baglanti1); // burada tüm kart bilgileri veritabanı ile eşleştiriliyor bakalım doğrumu ?
            SqlDataReader oku = kart_sorgu.ExecuteReader();
            bool varmi = false;
            while(oku.Read())
            {
                varmi = true; // kredi kartı bilgileri doğru kullanıcı artık yükleme yapabilir
                baglanti2.Open();
                SqlCommand lira_yukle = new SqlCommand("UPDATE kullanicilar SET bakiye=bakiye+'"+ Form1_icerik.gidecek_tl_miktarı + "' WHERE tel_no='"+giris.kullanici_tel+"'", baglanti2); // Tl yüklemesini gerçekleştirelim..
                if (lira_yukle.ExecuteNonQuery() !=-1) // -1 değilse çalıştı
                {
                    if (rasgele_label.Text == rastgele_kutusu.Text)// kredi kartı bilgileri doğru ama rasgele sayıları da doğru girmişmi yani giren kişi insan mı robot mu?
                    {
                        MessageBox.Show(tutar_label.Text + " hattınıza başarılı bir şekilde yüklenmiştir..", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        form1_git();
                    }
                    else MessageBox.Show("Güvenlik kodunu lütfen doğru giriniz !", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }            
                baglanti2.Close();
            }
            if(!varmi) MessageBox.Show("Kredi kartı bilgileriniz yanlış yada sistemde kayıtlı değilir! Eğer sistemde kayıtlı olmadığını düşünüyorsanız en yakın ECO Bayine giderek kredi kartınızı mevcut hattınıza tanımlatabilirsiniz...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            baglanti1.Close();
        }

        private void iptal_button_Click(object sender, EventArgs e)
        {
            form1_git();
        }
        private void form1_git()
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
