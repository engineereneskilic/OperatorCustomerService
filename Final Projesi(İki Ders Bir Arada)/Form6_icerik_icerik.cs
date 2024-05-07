using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;  // media kütüphanesini çağırıyoruz
using System.Data.SqlClient; // sql kütüphanesini çağırıyoruz

namespace Final_Projesi_İki_Ders_Bir_Arada_
{
    public partial class Form6_icerik_icerik : Form
    {
        public Form6_icerik_icerik()
        {
            InitializeComponent();
        }

        int ses_sayaci = giris.ses_sayaci;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak


        private void Form6_icerik_icerik_Load(object sender, EventArgs e)
        {

            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;


            timer6_icerik_icerik.Enabled = true;
            timer6_icerik_icerik.Start();


            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti1.Open();
            int gelen_menu_id = Form6.giden_menu_id;
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '6-4-2-%'", baglanti1);
            SqlDataReader oku = sql.ExecuteReader();

            int index = 0;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

            }
            baglanti1.Close();
            ses_sayaci_limit = index;
        }

        private void Menu_haric_gorunmez()
        {
            // telefon grubu
            baska_hat_label.Visible = false;
            baska_hat_kutusu.Visible = false;
            tl_miktari_label.Visible = false;
            tl_miktari_kutusu.Visible = false;
            tel_onayla_button.Visible = false;

            // seçiminiz kutuları
            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;

     
        }

        private void timer6_icerik_icerik_Tick(object sender, EventArgs e)
        {
            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {

                giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                menu_label.Visible = true;

            }
            else
            {
                // Bilgi: ses_sayaci_limit değişkeni aslında veritabanı limitidir.
                // Aşağıda işlemler veritabanından exta gerçekleşen işlemler o yüzden limit dışı.

                // ses sayacı limitinden iki fazla daha gittiği zaman bir üst menü seçeneği
                if (ses_sayaci == ses_sayaci_limit + 1)
                {
                    giris.ne_calayim("tekrar_edenler", "bir_ust_menu");
                    menu_label.Text += "#.Bir üst menüye dönmek için\n";
                } else
                // ses sayacı limitinden iki fazla daha gittiği zaman anamenü dön seçeneği
                if (ses_sayaci == ses_sayaci_limit + 2)
                {

                    giris.ne_calayim("tekrar_edenler", "anamenuye_donmek");
                    menu_label.Text += "9.Anamenüye dönmek için\n";
                } else
                // ses sayacı limitinden üç fazla daha gittiği zaman artık secim gurubu gözükebilir..
                if (ses_sayaci == ses_sayaci_limit + 3)
                {
                    // Seçiminiz:

                    seciminiz_label.Visible = true;
                    seciminiz_kutusu.Visible = true;
                    onayla_button.Visible = true;
                    timer6_icerik_icerik.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }
        }

        private void onayla_button_Click(object sender, EventArgs e)
        {
            if (seciminiz_kutusu.Text.Length == 1)
            {

                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                         baglanti1.Open();
                         SqlCommand lira_paylas_ac_sql = new SqlCommand("UPDATE kullanicilar SET lira_paylas=1 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                         lira_paylas_ac_sql.ExecuteNonQuery();
                         baglanti1.Close();

                         MessageBox.Show("Lira Paylaş Servisiniz Açılmışıtır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case '2':
                         baglanti1.Open();
                         SqlCommand lira_paylas_kapa_sql = new SqlCommand("UPDATE kullanicilar SET lira_paylas=0 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                         lira_paylas_kapa_sql.ExecuteNonQuery();
                         baglanti1.Close();

                         MessageBox.Show("Lira Paylaş Servisiniz Kapatılmıştır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case '3':
                         // lira gönderme menüsü seçildiğinde gerekli şeyler gelsin
                            // telefon grubu
                            baska_hat_label.Visible = true;
                            baska_hat_kutusu.Visible = true;
                            tl_miktari_label.Visible = true;
                            tl_miktari_kutusu.Visible = true;
                            tel_onayla_button.Visible = true;

                            // seçiminiz kutuları
                            menu_label.Text = "";
                            menu_label.Visible = true;
                            seciminiz_label.Visible = true;
                            seciminiz_kutusu.Visible = true;
                            onayla_button.Visible = true;

                        baska_hat_kutusu.Focus(); // başka hat kutusu odaklansın
                            giris.ne_calayim("uyari_mesajlari", "on_haneli_olarak_tuslayiniz"); // telefon label ve kutusu geldiği zaman kullanıcıyı bilgilendirsin

                        break;
                    case '4':
                        // lira isteme menüsü gerekli şeyler gelsin

                        // telefon grubu
                        baska_hat_label.Visible = true;
                        baska_hat_kutusu.Visible = true;
                        tl_miktari_label.Visible = true;
                        tl_miktari_kutusu.Visible = true;
                        tel_onayla_button.Visible = true;

                        // seçiminiz kutuları
                        menu_label.Text = "";
                        menu_label.Visible = true;
                        seciminiz_label.Visible = true;
                        seciminiz_kutusu.Visible = true;
                        onayla_button.Visible = true;

                        baska_hat_kutusu.Focus(); // başka hat kutusu odaklansın
                        giris.ne_calayim("uyari_mesajlari", "on_haneli_olarak_tuslayiniz"); // telefon label ve kutusu geldiği zaman kullanıcıyı bilgilendirsin

                        break;
                    case '#':
                        Form6_icerik form6_icerik = new Form6_icerik();
                        this.Close();
                        form6_icerik.Show();
                        break;
                    case '9':
                        anasayfa anasayfa = new anasayfa();
                        this.Close();
                        anasayfa.Show();
                        break;
                    default:
                        MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;




                }
            }
            else MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
       
        }
        int seciminiz_no;
        private void baska_hat_kutusu_TextChanged(object sender, EventArgs e)
        {
            seciminiz_no = Convert.ToInt32(seciminiz_kutusu.Text);
            if (baska_hat_kutusu.TextLength == 10)
            {
                tel_onayla_button.Visible = true;
                seciminiz_kutusu.Visible = true;
                seciminiz_label.Visible = true;
                onayla_button.Visible = true;
            }
            else
            {

                tel_onayla_button.Visible = false;
                seciminiz_kutusu.Visible = false;
                seciminiz_label.Visible = false;
                onayla_button.Visible = false;
            }
        }

        private void tel_onayla_button_Click(object sender, EventArgs e)
        {
            // seçilen menü ya 3 yada 4 olcaktır yani ya lira göndercek yada istiycek
            if (seciminiz_no == 3) // 3 ise lira gönderme işlemlerini başlatalım
            {
                baglanti1.Open();
                SqlCommand lira_gonder_sql = new SqlCommand("UPDATE kullanicilar SET bakiye=bakiye - '" + Convert.ToInt32(tl_miktari_kutusu.Text) + "' WHERE tel_no='" + giris.kullanici_tel + "';UPDATE kullanicilar SET bakiye=bakiye + '" + Convert.ToInt32(tl_miktari_kutusu.Text) + "' WHERE tel_no='" + baska_hat_kutusu.Text + "'", baglanti1); // şuan giriş yapan kullanıcının hesabından lirayı düşüp göndermek istediği kişinin hesabına lirayı gönderiyoruz

                if (lira_gonder_sql.ExecuteNonQuery() != -1) // -1 değilse komut çalışmıştır..
                {
                    MessageBox.Show("Keni hattınızdan " + baska_hat_kutusu.Text + " numaralı hatta " + tl_miktari_kutusu.Text + " TL gönderilmiştir...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else MessageBox.Show(baska_hat_kutusu.Text + " numaralı hat geçersiz yada ECO'ya kayıtlı değildir!", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglanti1.Close();
            }
            else if (seciminiz_no == 4) // 4 ise lira istiyecek
            {
                
                baglanti1.Open();
                SqlCommand sql1 = new SqlCommand("SELECT lira_paylas FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1); // kullanıcımızın lira paylaş servisi aktif mi ona bakıyoruz normalde operatörümüz gelen müşterilerimizin lira paylaş servisini kapalı tutar fakat kullanıcı ister ise kendisi bu servisi aktif edebilir..
                SqlDataReader oku1 = sql1.ExecuteReader();
                while (oku1.Read())
                {
                    bool lira_paylas = Convert.ToBoolean(oku1[0]); // lira_paylas_talebini değişkene aktarıyoruz
                  
                    if (lira_paylas) //kullanıcıya lira paylaş açıksa ise
                    {
                        baglanti2.Open();
                        SqlCommand lira_talebi_yap_sql = new SqlCommand("INSERT INTO lira_paylas VALUES('"+baska_hat_kutusu.Text+"','"+giris.kullanici_tel+"','"+tl_miktari_kutusu.Text+"') ", baglanti2);

                        if (lira_talebi_yap_sql.ExecuteNonQuery() != -1) // komut çalışırsa
                        {
                            MessageBox.Show("Keni hattınızdan " + baska_hat_kutusu.Text + " numaralı hatta " + tl_miktari_kutusu.Text + " TL isteme talebi gönderilmiştir. Belirtilen numara onay verdiği taktirde Tl isteme talebiniz gerçekleştirilecektir...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // bir üst menüye atsın
                            Form6_icerik form6_icerik = new Form6_icerik();
                            this.Close();
                            form6_icerik.Show();
                        }
                        else MessageBox.Show("Bir hata nedeniyle işleminizi gerçekleştiremiyoruz", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        baglanti2.Close();
                    }
                    else { MessageBox.Show("Lira paylaş servisiniz aktif olmadığı için işleminizi gerçekleştiremiyoruz !", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                    //NOT: Kullanıcı bu servisi seçmiş olabilir fakat lira paylaş servisi kapalı ise işlem yapamayız bu program aracılığı ile yada şubelerimize gelerek lira paylaş servisini aktif hale getirmesi gerekmektedir..
                }

            }
            baglanti1.Close();
                
        }
    }
}
