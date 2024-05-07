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
    public partial class Form6_icerik : Form
    {
        public Form6_icerik()
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






        private void Form6_icerik_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;
            // başlıklar gelen menüye göre düzenleniyor
            this.Text = karsilama_label.Text = Form6.giden_menu_ismi;


            timer6_icerik.Enabled = true;
            timer6_icerik.Start();

           
            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti1.Open();
            int gelen_menu_id = Form6.giden_menu_id;
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '6-"+gelen_menu_id+"-%' AND LEN(menu_id)=5", baglanti1);
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
            // telefon gurubu
            baska_hat_label.Visible = false;
            baska_hat_kutusu.Visible = false;
            tel_onayla_button.Visible = false;

            // seçiminiz grubu
            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

        private void timer6_icerik_Tick(object sender, EventArgs e)
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
                }
                else
                // ses sases_sayaci++;
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
                        timer6_icerik.Enabled = false;
                        seciminiz_kutusu.Focus(); // otomatik odak
                    }

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
                        // en dıştaki ifler gelen menüye göre buttonları değiştiriyo bu şekilde yaptım çünkü yeni bir form açmak istemedim
                        if (Form6.giden_menu_ismi == "Kayıp veya Çalıntı") // gelen menü kayıp veya çalıntı ise
                        {
                            baglanti1.Open();
                            SqlCommand sql3 = new SqlCommand("SELECT kayip_calinti FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                            SqlDataReader oku3 = sql3.ExecuteReader();
                            while (oku3.Read())
                            {
                                bool kayip_calinti = Convert.ToBoolean(oku3[0]); // Yurdışı servisini değişkene aktarıyoruz
                                if (kayip_calinti) // kullanıcının kayio ise
                                {
                                    DialogResult kullanici_sor = MessageBox.Show("Hattınız kullanıma kapatılacak yinede Kayıp veya Çalıntı işlemini yapmak istediğinize emin misiniz ?", karsilama_label.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                    if (kullanici_sor == DialogResult.Yes)
                                    {
                                        baglanti2.Open();
                                        SqlCommand ac_sql = new SqlCommand("UPDATE kullanicilar SET kayip_calinti=0 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                        ac_sql.ExecuteNonQuery();
                                        baglanti2.Close();
                                        MessageBox.Show("Kayıp veya Çalıntı işleminiz onaylanmıştır. En kısa sürede hattınız kullanıma kapatılcaktır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (kullanici_sor == DialogResult.No)
                                    {
                                        this.Hide();

                                    }
                                }
                                else // kullanıcının kayıp veya çalıntı işlemi kapalı ise
                                {
                                    DialogResult kullanici_sor = MessageBox.Show("Hattınız Kayıp veya Çalıntı işlemi nedeniyle kullanıma kapatılmıştır, kullanıma açmak istiyor musunuz ?", karsilama_label.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                    if (kullanici_sor == DialogResult.Yes)
                                    {
                                        baglanti2.Open();
                                        SqlCommand ac_sql = new SqlCommand("UPDATE kullanicilar SET kayip_calinti=1 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                        ac_sql.ExecuteNonQuery();
                                        baglanti2.Close();

                                        MessageBox.Show("Hattınız kullanıma açılmıştır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (kullanici_sor == DialogResult.No || kullanici_sor == DialogResult.Cancel)
                                    {
                                        this.Hide();

                                    }
                                }

                            }
                            baglanti1.Close();

                        } else
                            if (Form6.giden_menu_ismi == "Puk bilgisi almak")
                            {
                               
                                   
                                    baska_hat_label.Visible = true;
                                    baska_hat_kutusu.Visible = true;
                                    seciminiz_kutusu.Visible = false;
                                    seciminiz_label.Visible = false;
                                    onayla_button.Visible = false;
                                    giris.ne_calayim("uyari_mesajlari", "on_haneli_olarak_tuslayiniz"); // telefon label ve kutusu geldiği zaman kullanıcıyı bilgilendirsin
                                   
                            }


                        if (Form6.giden_menu_ismi == "Ürün ve servisleri")
                        {
                            baglanti1.Open();
                            SqlCommand sql4 = new SqlCommand("SELECT gizlino_engelleme FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                            SqlDataReader oku4 = sql4.ExecuteReader();
                            while (oku4.Read())
                            {
                                bool gizlino_engelleme = Convert.ToBoolean(oku4[0]); // Yurdışı servisini değişkene aktarıyoruz
                                if (gizlino_engelleme) //kullanıcının gizlino aktif ise
                                {
                                    DialogResult kullanici_sor = MessageBox.Show("Gizli numara servisiniz aktiftir.Kapatmak istiyor musunuz ?", karsilama_label.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                    if (kullanici_sor == DialogResult.Yes)
                                    {
                                        baglanti2.Open();
                                        SqlCommand gizlino_ac_sql = new SqlCommand("UPDATE kullanicilar SET kayip_calinti=0 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                        gizlino_ac_sql.ExecuteNonQuery();
                                        baglanti2.Close();
                                        MessageBox.Show("Gizli numara servisiniz kapatılmıştır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else if (kullanici_sor == DialogResult.No)
                                    {
                                        this.Hide();

                                    }
                                }
                                else // kullanıcının gizli no servisi kapalı ise
                                {
                                    DialogResult kullanici_sor = MessageBox.Show("Hattınızın Gizli Numara Servisi kapalıdır. Açmak istiyormusunuz ?", karsilama_label.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                    if (kullanici_sor == DialogResult.Yes)
                                    {
                                        baglanti2.Open();
                                        SqlCommand ac_sql = new SqlCommand("UPDATE kullanicilar SET kayip_calinti=1 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                        ac_sql.ExecuteNonQuery();
                                        baglanti2.Close();

                                        MessageBox.Show("Gizli numara servisiniz açılmışıtr...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (kullanici_sor == DialogResult.No || kullanici_sor == DialogResult.Cancel)
                                    {
                                        this.Hide();

                                    }
                                }

                            }
                            baglanti1.Close();
                        }
                        break;
                    case '2':
                        if (Form6.giden_menu_ismi == "Kayıp veya Çalıntı") // farklı bir işlem yapmak için
                        {// gelen menü kayıp veya çalıntı ise
                            this.Close();
                        }
                        else
                            if (Form6.giden_menu_ismi == "Puk bilgisi almak") // kullanıcının puk numarası
                            {
                                baglanti1.Open();
                                SqlCommand puk_sql = new SqlCommand("SELECT puk_bilgisi FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                                SqlDataReader puk_oku = puk_sql.ExecuteReader();
                                while (puk_oku.Read())
                                {
                                    MessageBox.Show("Hattınızın Puk numarası: " + puk_oku[0], karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                baglanti1.Close();
                            }else
                                if (Form6.giden_menu_ismi == "Ürün ve servisleri") // Lira paylaş servisi uzun olduğu için yeni bir formda seçenekleri gösterelim
                                {
                                    Form6_icerik_icerik lira_paylas_formu = new Form6_icerik_icerik();
                                    lira_paylas_formu.Show();
                                }
  
                        break;
                    case '3':
                            //ringa servisini program aracılığı ile yapamayız onu açıklamada belirtilen şekilde arama yaparak yada bayie giderek yaptırabiliriz
                            baglanti1.Open();
                            SqlCommand sql5 = new SqlCommand("SELECT * FROM menuler WHERE menu_id='6-4-3-0'", baglanti1);
                            SqlDataReader oku5 = sql5.ExecuteReader();
                            while (oku5.Read())
                            {
                                string ringa_aciklama = Convert.ToString(oku5[1]);
                                giris.ne_calayim("ses_dosyalari","6-4-3-0");
                                MessageBox.Show(ringa_aciklama, "Ringa Servisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            baglanti1.Close();
                        break;
                    case '#':
                        Form6 form6 = new Form6();
                        this.Close();
                        form6.Show();
                        break;
                    case '9':
                        anasayfa form1 = new anasayfa();
                        this.Close();
                        form1.Show();
                        break;
                    default:
                        MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;




                }
            }
            else MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
       
        }


        private void baska_hat_kutusu_TextChanged(object sender, EventArgs e) // bu class her bir tuşa basıldığında tetiklenir
        {
            if (baska_hat_kutusu.TextLength == 10) // başka hat numarası 10 haneli olduğunda doğrudur ve diğer menüler gelebilir böylece numara kontrolü yapılmış olur
            {
                tel_onayla_button.Visible = true;
                seciminiz_kutusu.Text = "";
                seciminiz_kutusu.Visible = true;
                seciminiz_label.Visible = true;
                onayla_button.Visible = true;
            }
            else
            {
                // değilse gelemezler
                tel_onayla_button.Visible = false;
                seciminiz_kutusu.Visible = false;
                seciminiz_label.Visible = false;
                onayla_button.Visible = false;
            }
        }

        private void tel_onayla_button_Click(object sender, EventArgs e)
        {
            baglanti1.Open();
            SqlCommand puk_sql = new SqlCommand("SELECT puk_bilgisi FROM kullanicilar WHERE tel_no='"+baska_hat_kutusu.Text+"'", baglanti1); // girilen numara yanlış da girilse(format olarak) veritabanında yoksa uyarı mesajı veriyoruz 
            SqlDataReader puk_oku = puk_sql.ExecuteReader();
            if (puk_oku.Read())
            {
              
                    MessageBox.Show(baska_hat_kutusu.Text + " numaralı hattın Puk numarası: " + puk_oku[0], karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else MessageBox.Show(baska_hat_kutusu.Text + " numaralı hat bizim operatörümüze kayıtlı değilidir !", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            baglanti1.Close();
        }
    }
}
