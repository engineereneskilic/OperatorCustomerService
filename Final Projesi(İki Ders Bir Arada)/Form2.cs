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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // giriş formumuzdan gerekli değişkenleri çekiyoruz ve bu formda rahat kullanabilmek için değişkenlere atıyoruz

        int ses_sayaci = giris.ses_sayaci;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak



        private void Form2_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;
            
            timer2.Enabled = true;
            timer2.Start();

           
            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti1.Open();
            SqlCommand sql = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '2-%' AND LEN(menu_id)=3", baglanti1);// bu sorgu veritabanındaki menü listemden bu formda kullanmak istediğim menüleri seçmem için idlere bakarak istenilen menüleri alıyoruz
            SqlDataReader oku = sql.ExecuteReader();
            int index = 0;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

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

        private void timer2_Tick(object sender, EventArgs e)
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
                    timer2.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }
        }

     
        private void onayla_button_Click(object sender, EventArgs e)
        {
            anasayfa anasayfa = new anasayfa();
    
            if (seciminiz_kutusu.Text.Length == 1)
            {

                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        baglanti1.Open();
                         SqlCommand sql2 = new SqlCommand("SELECT kalan_dk,kalan_mb,kalan_sms FROM kullanicilar WHERE tel_no='"+giris.kullanici_tel+"'", baglanti1);
                         SqlDataReader oku2 = sql2.ExecuteReader();
                        while(oku2.Read()){
                            MessageBox.Show("Hattınızda toplam " + oku2[0] + " DK sesli arama " + oku2[1] + " MB internet kullanımı " + oku2[2] + " SMS hakkınız bulunmaktadır. Kullanım detayları ile ilgili ücretsiz mesaj telefonunuza sms olarak gönderilmiştir.", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        baglanti1.Close();
                        break;
                    case '2':
                        baglanti1.Open();
                         SqlCommand sql3 = new SqlCommand("SELECT yurdisi_servisi FROM kullanicilar WHERE tel_no='"+giris.kullanici_tel+"'", baglanti1);
                         SqlDataReader oku3 = sql3.ExecuteReader();
                        while(oku3.Read()){
                            bool yurdisi_servisi = Convert.ToBoolean(oku3[0]); // Yurdışı servisini değişkene aktarıyoruz
                            if (!yurdisi_servisi) // kullanıcının yurtdışı servisi kapalı ise
                            {
                                DialogResult kullanici_sor = MessageBox.Show("Uluslararası servisleriniz kapalıdır. Hattınızı yurtdışındayken kullanıma açmak için yes butonuna basınız.", karsilama_label.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (kullanici_sor == DialogResult.Yes)
                                {
                                    baglanti2.Open();
                                    SqlCommand ac_sql = new SqlCommand("UPDATE kullanicilar SET yurdisi_servisi=1 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                    ac_sql.ExecuteNonQuery();
                                    baglanti2.Close();
                                    MessageBox.Show("Uluslararası servisleriniz aktif edilmişitir...", karsilama_label.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                else if (kullanici_sor == DialogResult.No)
                                {
                                    anasayfa.Show();
                                    this.Close();
                                    
                                }
                            }
                            else // kullanıcının yurtdışı servisi açık ise
                            {
                                DialogResult kullanici_sor = MessageBox.Show("Uluslararası servisleriniz açıktır. Hattınızı yurtdışındayken kullanıma kapatmak için yes butonuna basınız.", karsilama_label.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (kullanici_sor == DialogResult.Yes)
                                {
                                    baglanti2.Open();
                                    SqlCommand ac_sql = new SqlCommand("UPDATE kullanicilar SET yurdisi_servisi=0 WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                                    ac_sql.ExecuteNonQuery();
                                    baglanti2.Close();

                                    MessageBox.Show("Uluslararası servisleriniz kapatılmıştır...", karsilama_label.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (kullanici_sor == DialogResult.No)
                                {
                                    anasayfa.Show();
                                    this.Close();
                                }
                            }

                        }
                        baglanti1.Close();
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
