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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // giriş formumuzdan gerekli değişkenleri çekiyoruz ve bu formda rahat kullanabilmek için değişkenlere atıyoruz

        int ses_sayaci = giris.ses_sayaci; // kaç tane ses olduğunu giriş sayfasından belirliyoruz
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi); // iç içe sorgular için iki bağlantı açmamız gerek yada kaç içiçe olcaksa o kadar bağlantı bize 2 tane yeter
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak

        // Bu iki diziyi yapmamın sebebi veritabanındaki verileri çekip diziye atmak. Listele işlemini while döngüsü ile yapabilirdim fakat while döngüsü çok hızlı sorgu yapıyo yani indexlerini kontrol edemiyorum tabi bir yöntemi vardır da bilmiyorum.
        // Veritabanından çekip diziye attıktan sonra timera göre listelemesini istiyorum. Amaç seslerin zaman süresini bilmek ona göre yazıyı bekletmek istiyorum. Örneğin kadın sesi Enes diyosa sözünü bitirdikten sonra Enes kelimesi ekranda gözüksün istiyorum. Kadın söylemediği hiç bir şey ne erken yazılsın ne geç yazılsın tam lafı bittiğinde gelsin istiyorum..
        // bütün formlarda aynı mantık olduğu için hepsinde bu açıklamı yazmiyacam..
        private void Form1_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;

            // timer aktif hale getirelim ve başlatalım
            timer1.Enabled = true;
            timer1.Start();

            // başlangıçta hiç bişey söylemesin kadın söylemeye başlayınca yazılar gözükmeye başlasın
            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti1.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '1-%' AND LEN(menu_id)=3", baglanti1); // bu sorgu veritabanındaki menü listemden bu formda kullanmak istediğim menüleri seçmem için idlere bakarak istenilen menüleri alıyoruz
            SqlDataReader oku = sql.ExecuteReader();
            int index = 0;
            while (oku.Read())
            {
                // burada veritabanından gelen verileri dizimize aktarıyoruz
                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim(); // Trim sağ sol boşlukları siler veritabanında bazen nvarchar(50) diyorum ama 50 karakter tam yazmıyorum boşluk görüyor c#ta ama bu dediğim bazende olmuyo garanti olsun diye Trim ekledim

            }
            baglanti1.Close();
            ses_sayaci_limit = index; // ses sayacı sorgu sayısınca çalışmalı
        }

        private void Menu_haric_gorunmez()
        {

            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e) // bu class design sayfasında ayarlanan rakama göre mesela 5 saniye ise 5 saniyede bir çalışıyor..
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


                giris.ne_calayim("tekrar_edenler", "bir_ust_menu");
                menu_label.Text += "#.Bir üst menüye dönmek için\n";

                // ses sayacı limitinden iki fazla daha gittiği zaman artık secim gurubu gözükebilir..
                if (ses_sayaci <= ses_sayaci_limit + 2)
                {
                    // Seçiminiz:

                    seciminiz_label.Visible = true;
                    seciminiz_kutusu.Visible = true;
                    onayla_button.Visible = true;
                    timer1.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }
        }

        // menü sayımız çok olması nedeniyle çok form kullanmak istemedim. O yüzden seçilen menüye göre bir forma veri göndericez 
        public static int giden_menu_id;
        public static string giden_menu_isim;
        private void onayla_button_Click_1(object sender, EventArgs e)
        {
            if (seciminiz_kutusu.Text.Length == 1) // switch char seçtiğimiz için fazla karakterde program çöküyo bunu önlemek için switche girmeden tek karakter hata mesajı versin
            {
                Form1_icerik form1_icerik = new Form1_icerik();
                anasayfa anasayfa = new anasayfa();
                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        giden_menu_id = 1;
                        giden_menu_isim = "Kredi Kartı ile Bakiye Yükleme";
                        form1_icerik.Show();
                        this.Close();
                        break;
                    case '2':
                        giden_menu_id = 2;
                        giden_menu_isim = "Tarife Bilgisi ve İşlemleri";
                        form1_icerik.Show();
                        this.Close();
                        break;
                    case '3':
                        giden_menu_id = 3;
                        giden_menu_isim = "Numara İşlemleri";
                        form1_icerik.Show();
                        this.Close();
                        break;
                    case '4':
                        giden_menu_id = 4;
                        giden_menu_isim = "Kredi Kartı ile Fatura Ödeme";
                        form1_icerik.Show();
                        this.Close();
                        break;
                    case '#':
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
    }
}
