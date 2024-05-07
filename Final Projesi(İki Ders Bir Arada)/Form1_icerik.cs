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
    public partial class Form1_icerik : Form
    {
        public Form1_icerik()
        {
            InitializeComponent();
        }

        // giriş formumuzdan gerekli değişkenleri çekiyoruz ve bu formda rahat kullanabilmek için değişkenlere atıyoruz

        int ses_sayaci = giris.ses_sayaci -1;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak

      

        //piravete yazsak bile bu formdaki  classların içinde kullanılmıyor her classın içinde değişkene atamayada gerek yok
        /*
        private static string gelen_menu_isim = Form1.giden_menu_isim;
        private static int gelen_menu_id = Form1.giden_menu_id;
        */
        private void Form1_icerik_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;

            // gelen menüye göre başlık değişssin
            karsilama_label.Text = Form1.giden_menu_isim;

            // gelen menüye göre lazım olan değişkenlerimizi tanımlayalım
           

            


            Menu_haric_gorunmez();

         
            baglanti1.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '1-"+Form1.giden_menu_id+"-%' AND LEN(menu_id) = 5", baglanti1); // gelen menü idye göre menülerimizi seçiyoruz
            SqlDataReader oku = sql.ExecuteReader();
            int index = -1;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

            }
            baglanti1.Close();
            ses_sayaci_limit = index; // ses sayacı sorgu sayısınca çalışmalı

            // 3 menününde ayrı ayrı kaç dakikada bir tetikleneceğini karar verelim..
            if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme") { timer1_icerik_.Interval = 5000; }
            else
               if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri") { timer1_icerik_.Interval = 5000; }
            else
               if (Form1.giden_menu_isim == "Numara İşlemleri") { timer1_icerik_.Interval = 25000; }

            timer1_icerik_.Enabled = true;
            timer1_icerik_.Start();
        }

        private void Menu_haric_gorunmez()
        {
            aciklama_label.Text = "";
            aciklama_label.Visible = false;
            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

       

        public static int gidecek_tl_miktarı; // kredi kartı sayfasına gidicek yani kullanıcnın seçtiği tl miktarı
        private void secilenTutar( int tl_miktari)
        {
            gidecek_tl_miktarı = tl_miktari;
            DialogResult kullanici_sor = MessageBox.Show(tl_miktari+" TL'yi onaylıyor musunuz?", karsilama_label.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (kullanici_sor == DialogResult.Yes)
            {
                // Kredi kartı sayfasına gidiyor...
                Form1_icerik_kredi_karti kredi_karti_formu = new Form1_icerik_kredi_karti();

                kredi_karti_formu.Show();

            }
            else if (kullanici_sor == DialogResult.No)
            {
                // bir üst menüye gidiyor...
                this.Close();
                anasayfa.Show();

            }

        }

        private void tarifeDegistir( string secilen_tarife_ismi)
        {
            baglanti1.Open();
            SqlCommand tarife_id_sor = new SqlCommand("SELECT * FROM menuler WHERE menu_isim='"+secilen_tarife_ismi+"'", baglanti1);
            SqlDataReader tarife_id = tarife_id_sor.ExecuteReader();
            tarife_id.Read();

            baglanti2.Open();
            SqlCommand tarife_bilgisi_sor = new SqlCommand("SELECT * FROM menuler WHERE menu_id='" + tarife_id[0] + "-0'", baglanti2); // bulduğumuz idye göre -0 ekleyim o tarifenin bilgisi buluyoruz bu şekilde
            SqlDataReader tarife_bilgisi = tarife_bilgisi_sor.ExecuteReader();
            tarife_bilgisi.Read();
            string secilen_tarife_bilgisi = Convert.ToString(tarife_bilgisi[1]);
            baglanti2.Close();
            baglanti1.Close();
            DialogResult kullanici_sor = MessageBox.Show(secilen_tarife_bilgisi+" "+secilen_tarife_ismi+" Tarifesinine geçmek istiyor musunuz ?", secilen_tarife_ismi+" Tarifesi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (kullanici_sor == DialogResult.Yes)
            {
                baglanti1.Open();
                SqlCommand tarife_degistir_sql = new SqlCommand("UPDATE kullanicilar SET tarife_ismi='" + secilen_tarife_ismi + "' WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                if (tarife_degistir_sql.ExecuteNonQuery() != -1) // komut çalıştıyse
                {
                    MessageBox.Show("Tarifeniz başarılı bir şekilde değiştirildi...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    anasayfa.Show();
                }
                baglanti1.Close();

            }
           // no durumunda birşey yapmasın
            
           
        }

        private void numaraDegistir(string yurdisi_telno)
        {
            string eski_tel_sonkisim = giris.kullanici_tel.Substring(yurdisi_telno.Length - 1, 4);
            //yurdışı telin uzunluğundan başla(ör:5539972  6 haneli), kullanıcının tel no uzunluğu kadar kes.Ör: yurtdışı:5539972 , kullanici tel:5434087551 burda amaç yurt dışı 6 haneli mesela  6.haneden başla kullanıcı telin yani 7 numarasından başlanı kullanıcı telin sonuna kadar kes sonuc:7551
            // yeni telefon numaramızın 2 parçalı olduğunu düşünün ilk parça yurtdışı teli kalanı eski telin son kısmı; yani 5539972 ilk parça 7551 ise ikinci parça yani yeni tel:55399727551
            string yeni_telno = Convert.ToString(yurdisi_telno + "" + eski_tel_sonkisim); // birinci parça ve ikinci parça birleşiyor yeni tel artık oluşuyor.
            MessageBox.Show(Convert.ToString(yeni_telno));
        
               
            DialogResult kullanici_sor = MessageBox.Show("Yeni numaranız: "+yeni_telno+". Onaylıyor musunuz?", karsilama_label.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (kullanici_sor == DialogResult.Yes)
            {
                baglanti1.Open();
                SqlCommand lirasi_varmi_sql = new SqlCommand("SELECT bakiye FROM kullanicilar WHERE tel_no='"+giris.kullanici_tel+"'",baglanti1);
                SqlDataReader lirasi_varmi = lirasi_varmi_sql.ExecuteReader();
                lirasi_varmi.Read();
                if (Convert.ToInt32(lirasi_varmi[0]) >= 20) // kullanıcı numarasını değiştirmek için 20 tl ödemek zorunda ödeyebilcek mi onun kontrolü
                {
                    baglanti2.Open();
                    SqlCommand tarife_degistir_sql = new SqlCommand("UPDATE kullanicilar SET tel_no='" + yeni_telno + "',bakiye=bakiye-20 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                    if (tarife_degistir_sql.ExecuteNonQuery() != -1) // komut çalıştıyse
                    {
                        MessageBox.Show("İşleminiz tamamlandı. Yeni numaranızı artık kullanabilirsiniz...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        anasayfa.Show();
                    }
                    baglanti2.Close();
                }
                else { MessageBox.Show("Hattınızda yeterli bakiye bulunmadığı için işleminizi gerçekleştiremiyoruz..", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                baglanti1.Close();
            }
            
        }

        private void timer1_icerik__(object sender, EventArgs e)
        {
            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {

                if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme" || Form1.giden_menu_isim == "Numara İşlemleri") { giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]); } // Tarife Bilgisi İşlemleri Menüsünde ses dosyalarımız olmadığı için çalmasın
                menu_label.Visible = true;
                if (ses_sayaci == 0)
                {
                    if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme" || Form1.giden_menu_isim == "Numara İşlemleri") // Kredi kartı ve numara işlemlerinde ön açıklama yazısı yazmak istiyorum
                    {
                        aciklama_label.Visible = true;
                        aciklama_label.Text = "Açıklama: " + menu_isim[ses_sayaci] + "\n";

                        
                    }
                    else
                    if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri") // gelen menu ismi tarife bilgisi ve işlemleri ise bu kullanıcının tarife bilgilerini başa yazsın..
                    {
                        aciklama_label.Visible = true;
                        baglanti1.Open();
                        SqlCommand tarife_bul = new SqlCommand("SELECT menu_id,tarife_ismi FROM menuler,kullanicilar WHERE menu_isim=tarife_ismi AND tel_no='" + giris.kullanici_tel + "'", baglanti1); // kullanıcının tarife ismi kullanicilar tablosundan bulunup menuler tablosundaki tarife ismi(menu_isim) ile eşleştiriliyor böylece tarife idsi alınıyor buna bağlı olarak tarife bilgisi geliyor 
                        SqlDataReader oku1 = tarife_bul.ExecuteReader();
                        oku1.Read();

                        baglanti2.Open();
                        SqlCommand tarife_bilgisi_sql = new SqlCommand("SELECT menu_isim FROM menuler WHERE menu_id='" + oku1["menu_id"] + "-0'", baglanti2); // bulduğumuz idye göre -0 ekleyim o tarifenin bilgisi buluyoruz
                        SqlDataReader oku2 = tarife_bilgisi_sql.ExecuteReader();
                        oku2.Read();
                        aciklama_label.Text += "Mecvut tarifeniz: " + oku1["tarife_ismi"] + " Tarifesi\n";
                        aciklama_label.Text += "Tarife Bilgisi: " + oku2[0] + "\n";
                        aciklama_label.Text += "\n";// bir satır aşşağı
                        baglanti2.Close();

                        baglanti1.Close();


                    }
                }
                else
                {
                    menu_label.AutoSize = true; // otomatik boyut almayı etkinleştirelim form başlarken elle yükseklik ve genişlik verdik çünkü açıklama kısmı çok uzun bu şekil yaparsak alta iner şimdide bunu düzeltelim tektat otomatik boyut versin
                    if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme")
                    {
                        menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                    }
                    else
                    if (Form1.giden_menu_isim == "Numara İşlemleri")
                    {
                        timer1_icerik_.Interval = 6000; // ilk ayarladığım ayar kredi kartı içindi ve çok bekletiyor. Bunu 6 saniyeye ayarlayalım kullanıcılar beklemesin..
                        menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                    }
                    else
                    if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri")
                    {
                     
                        menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " Tarifesi için\n";
                    }
                }
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
                }
                else
                // ses sayacı limitinden iki fazla daha gittiği zaman anamenü dön seçeneği
                if (ses_sayaci == ses_sayaci_limit + 2)
                {

                    giris.ne_calayim("tekrar_edenler", "anamenuye_donmek");
                    menu_label.Text += "9.Anamenüye dönmek için\n";
                }
                else
                // ses sayacı limitinden üç fazla daha gittiği zaman artık secim gurubu gözükebilir..
                if (ses_sayaci == ses_sayaci_limit + 3)
                {
                    // Seçiminiz:

                    seciminiz_label.Visible = true;
                    seciminiz_kutusu.Visible = true;
                    onayla_button.Visible = true;
                    timer1_icerik_.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }
            }
        }

        anasayfa anasayfa = new anasayfa();
        private void onayla_button_Click_1(object sender, EventArgs e)
        {
            if (seciminiz_kutusu.Text.Length == 1)
            {

                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        // seçilen menü ne ise ona göre switch durumuda değişmeli
                        if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme")
                        {
                            secilenTutar(15);
                            this.Close();
                        }
                        else
                        if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri")
                        {
                            tarifeDegistir(menu_isim[1]);
                        }else
                        if(Form1.giden_menu_isim == "Numara İşlemleri")
                        {

                            numaraDegistir(Convert.ToString(menu_isim[1]));
                        }

                        break;
                    case '2':
                        if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme")
                        {
                            secilenTutar(25);
                            this.Close();
                        }
                        else
                       if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri")
                        {
                            tarifeDegistir(menu_isim[2]);
                        }else
                        if (Form1.giden_menu_isim == "Numara İşlemleri")
                        {

                            numaraDegistir(Convert.ToString(menu_isim[2]));
                        }

                        break;
                    case '3':
                        if (Form1.giden_menu_isim == "Kredi Kartı ile Bakiye Yükleme")
                        {
                            secilenTutar(35);
                            this.Close();
                        }
                        else
                        if (Form1.giden_menu_isim == "Tarife Bilgisi ve İşlemleri")
                        {
                            tarifeDegistir(menu_isim[3]);
                        }else
                        if (Form1.giden_menu_isim == "Numara İşlemleri")
                        {

                            numaraDegistir(Convert.ToString(menu_isim[3]));
                        }
                        break;
                    case '4':
                        if (Form1.giden_menu_isim == "Numara İşlemleri")
                        {

                            numaraDegistir(Convert.ToString(menu_isim[4]));
                        }
                        else
                        {
                            MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case '5':
                        if (Form1.giden_menu_isim == "Numara İşlemleri")
                        {

                            numaraDegistir(Convert.ToString(menu_isim[5]));
                        }
                        else
                        {
                            MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case '#':
                        this.Close();
                        Form1 form1 = new Form1();
                        form1.Show();
                        break;
                    case '9':
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
