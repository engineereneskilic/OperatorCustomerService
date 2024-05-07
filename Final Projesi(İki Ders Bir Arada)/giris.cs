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
    // Genel Açıklama: Projedeki tüm form designları birbiriyle neredeyse aynıdır ve çoğu formda autosize gibi değişik özellikler aktif hale getirilmiştir onların açıklamasını yapmıyacam zaten biraz inceleyince form ayarlarını ne değiştini anlayabilirsiniz
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        //
        // Formumuzda yüklendiğinde veritabanına da erişmiş olması gerek fakat her bilgisayarda veritabanı yolu farklı bu yolu kullanıcının yolu ile değiştirelim 
        
        // DEĞİŞKENLER ASLINDA PROGRAM AYARLAr
        public static int ses_sayaci = 0, ses_sayaci_limit; // sesleri sıra ile saymasını ve limitinde durması için gerek değişkenler
        public static string baglanticumlesi = @"Data Source="+veritabani_ayari.secilen_server_name+@";Initial Catalog=Operator_Musteri_Hizmetleri;Integrated Security=true;"; // veritabanı ayarları
        SqlConnection baglanti1 = new SqlConnection(baglanticumlesi); // iç içe sql çalıştırmak için ayrı 3 bağlantı oluşturuyoruz
        SqlConnection baglanti2 = new SqlConnection(baglanticumlesi);
        SqlConnection baglanti3 = new SqlConnection(baglanticumlesi);
        public static int menu_id_count = 8;// veritabanından gelen idler tutulcak olan dizinin toplam elemanı
        public static int menu_isim_count = 8; // veritabanından gelen isimler tutulcak olan dizinin toplam elemanı

        anasayfa anasayfa = new anasayfa(); // bu formda heryerde kullanalım

        private void giris_Load(object sender, EventArgs e)
        {
            

            rastgele_label.Text = rasgeleUret();
            
        }

        public static string rasgeleUret()
        {
            // rasgele harfler üretelim 8 haneli
            Random rastgele = new Random();
            string  karakterler=""; // boş değer atamamız gerekiyor çünkü program hata veriyor.
            for (int i = 1; i <= 4; i++) { 

                int ascii = rastgele.Next(65, 91); // 65 91 arası rastgele sayı seçiyor
                karakterler+= Convert.ToString(ascii);
            }
            return karakterler;
        }
       

        public static string kullanici_isim,kullanici_tel; // bütün formlardan gerekirse ulaşmak için kullanıcınn ismini ve telefon_no yani idsini alalalım

        private void rastgele_yenile_button_Click(object sender, EventArgs e)
        {
            rastgele_label.Text = rasgeleUret();
        }

        private void cikis_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
       
     
        private void giris_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti1.Open();// sql bir daha geldiğinde açılıp kapanma kontrolü
                SqlCommand sql = new SqlCommand("SELECT tel_no,sifre,isim_soyisim FROM kullanicilar", baglanti1);
                SqlDataReader oku = sql.ExecuteReader();
                bool bilgiler_dogrumu = false;
                bool robot_testi = false;
                while (oku.Read())
                {

                    if (sifre_kutusu.Text.Trim() == Convert.ToString(oku[1]).Trim() && tel_no_kutusu.Text.Trim() == Convert.ToString(oku[0]).Trim())
                    { bilgiler_dogrumu = true; kullanici_isim = Convert.ToString(oku[2]).Trim(); kullanici_tel = Convert.ToString(oku[0]).Trim(); }

                    if (rastgele_label.Text == rastgele_kutusu.Text.Trim()) robot_testi = true;


                }
                baglanti1.Close();
                if (!bilgiler_dogrumu) MessageBox.Show("Telefon numarası veya şifre yanlış !", this.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if(!robot_testi) MessageBox.Show("Güvenlik kodunu lütfen doğru giriniz !", this.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (bilgiler_dogrumu && robot_testi)
                {
                    // Bu kısım ödevi bu formdan kontrol etmeye başlayınca doğal olarak anlayamayabilirsiniz. Lira paylaş servisi menüsüne geldiğiniz zaman anlıyacaksınız.
                    // Burada başka bir kullanıcı bu kullanıcıdan lira isteyebilir isterse burada hangi kullanıcı telefon numarası ne kadar istiyor onun sorgularını yapıyoruz
                    baglanti1.Open();
                    SqlCommand listele_sql = new SqlCommand("SELECT * FROM lira_paylas WHERE lira_istenilen_tel_no='" + kullanici_tel + "'", baglanti1);
                    SqlDataReader listele = listele_sql.ExecuteReader();
                    int kac_tane = 0; // kaç kayıt var hesaplayalım NOT: sql komutuma COUNT(*) koyarak da bulunabilir ama çok uğraştım olmuyo yani hem COUNT(*) HEM diğer sütun isimleri birden gelmiyor.
                    while (listele.Read()) // kullanıcıya ait bütün lira is
                    {
                        kac_tane++;
                        int ID = Convert.ToInt32(listele[0]); // kullanıcının bir den fazla lira talebi geldiyse ayırt etmek için id şart.
                        long lira_istenilen_tel_no = Convert.ToInt64(listele[1]); // lira_paylas_talebini değişkene aktarıyoruz sqlde daha rahat kullanabilmek için
                        long lira_isteyen_tel_no = Convert.ToInt64(listele[2]); // lira_isteyen_tel_no değişkene aktarıyoruz sqlde daha rahat kullanabilmek için
                        int lira_paylas_taleb_tel_miktar = Convert.ToInt32(listele[3]); // lira_paylas_taleb_tel_miktar değişkene aktarıyoruz sqlde daha rahat kullanabilmek için

                        DialogResult kullanici_sor = MessageBox.Show(lira_isteyen_tel_no + " hat sahibi sizden " + lira_paylas_taleb_tel_miktar + "TL göndermenizi talep ediyor.Göndermeyi kabul ediyor musunuz?", karsilama_label.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (kullanici_sor == DialogResult.Yes)
                        {
                            baglanti2.Open();
                            SqlCommand taleb_sil_sql = new SqlCommand("DELETE FROM lira_paylas WHERE id='" + ID + "'", baglanti2); // kullanıcı lira gönderme isteğini kabul ettiğine göre artık tablodan bu isteği silmeliyiz
                            taleb_sil_sql.ExecuteNonQuery();
                            baglanti2.Close();

                            // Şimdi kullanıcıdan istenen tl miktarı acaba kullanıcının bakiyesinde varmı ona göre göndercez yada uyarı vericez
                            baglanti3.Open();
                            SqlCommand lirasi_varmi_sql = new SqlCommand("SELECT bakiye FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti3);
                            SqlDataReader lirasi_varmi = lirasi_varmi_sql.ExecuteReader();
                            lirasi_varmi.Read();
                            if (Convert.ToInt32(lirasi_varmi[0]) >= lira_paylas_taleb_tel_miktar) // kullanıcının yeterli bakiyesi varmı göndermek için varsa göndersin yoksa uyarı messagebox gelsin
                            {



                                // bu kallanıcıdan istenen tl miktarı kadar tl düşülüyor ve bu tl miktarını isteyen kişiye gönderiyoruz..
                                baglanti2.Open();
                                SqlCommand lira_gonder_sql = new SqlCommand("UPDATE kullanicilar SET bakiye=bakiye - '" + lira_paylas_taleb_tel_miktar + "' WHERE tel_no='" + lira_istenilen_tel_no + "';UPDATE kullanicilar SET bakiye=bakiye + '" + lira_paylas_taleb_tel_miktar + "' WHERE tel_no='" + lira_isteyen_tel_no + "'", baglanti2);

                                if (lira_gonder_sql.ExecuteNonQuery() != -1) // -1 değilse komut çalışmış ise..
                                {


                                    MessageBox.Show("Keni hattınızdan " + lira_isteyen_tel_no + " numaralı hatta " + lira_paylas_taleb_tel_miktar + " TL gönderilmiştir...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    anasayfa.Show(); this.Hide();
                                }
                                baglanti2.Close();

                            }
                            else { MessageBox.Show("Hattınızda yeterli bakiye bulunmadığı için işleminizi gerçekleştiremiyoruz..", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning); anasayfa.Show(); this.Hide(); }
                            baglanti3.Close();
                        }
                        else if (kullanici_sor == DialogResult.No)
                        {
                            // kullanıcı ben tl göndermek istemiyorum derse lira_paylas tablomuza kullanıcı bu isteği reddettiği için kalabalık etmesin diye siliyoruz. Lira_paylas tablosunda sadece aktif lira isteme istekleri mevcut olarak kalsın. Kabul edilen veya red edilen istekleri işi bitmiştir silinsin..
                            baglanti2.Open();
                            SqlCommand taleb_sil_sql = new SqlCommand("DELETE FROM lira_paylas WHERE id='" + ID + "'", baglanti2); // kullanıcı lira gönderme isteğini kabul ettiğine göre artık tablodan bu isteği silmeliyiz
                            taleb_sil_sql.ExecuteNonQuery();
                            baglanti2.Close();
                            MessageBox.Show(lira_isteyen_tel_no + " numaralı hattan gelen TL isteği reddedildi.", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            anasayfa.Show(); this.Hide(); // kullanıcı kabul etmese form1'e gitsin

                        }

                    }
                    baglanti1.Close();
                    if (kac_tane == 0) anasayfa.Show(); this.Hide(); // lira paylaş talebi yoksa kullanıcının direk form1'e gitsin
                }
            }
            catch
            {          // veritabanına bağlantı başarısız olduğu zaman verilecek uyarılarımız..
                MessageBox.Show("Veritabanınızı bulamadık! Sql Server 2012'de veritabanınız doğru bir şekilde çalışmıyor yada Servername yanlış seçmiş olabilirsiniz ! ", "VERİTABANI BAĞLANTI HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Veritabanınızın düzgün bir şekilde çalıştığını ve Servername'nizin doğru olduğunu düşünüyorsanız programda sorun devam ederse bir de bilgisayarınızı yeniden başlatmayı denerseniz seviniriz. Bunları yaptınız ve yine sorun düzelmedi ise program yetkilisine başvurunuz. Anlayışınız için teşekkür ederiz :)", "VERİTABANI BAĞLANTI HATASI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static void ne_calayim(string klasor, string ses_ismi) // public her yerden ulaşmam gerek
        {
            SoundPlayer player = new SoundPlayer();
            // aşağıda yazdığım kodu değişkene atayım açıklamasını yazıcaktım fakat değişlene atayınca hata veriyor. Aşağıda yazdığım System.Windows.Forms.SystemInformation.UserName kodu windows işletim sistemini kullanan kullanıcının ismini verir. Örneğin benim Enes sizin Hilal
            player.SoundLocation = @"C:\Users\" + System.Windows.Forms.SystemInformation.UserName + @"\Documents\Visual Studio 2015\Projects\Final Projesi(İki Ders Bir Arada)\Final Projesi(İki Ders Bir Arada)\" + klasor + @"\" + ses_ismi + ".wav";
            player.Play();


        }
    }
}
