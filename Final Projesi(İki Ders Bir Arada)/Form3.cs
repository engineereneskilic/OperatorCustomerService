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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        int ses_sayaci = giris.ses_sayaci;
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak


        private void Form3_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;     

            timer3.Enabled = true;
            timer3.Start();

            Menu_haric_gorunmez();

            menu_id[0] = "";
            menu_isim[0] = "";
            baglanti.Open();
            SqlCommand sql= new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '3-%' AND LEN(menu_id)=3", baglanti);
            SqlDataReader oku = sql.ExecuteReader();
            int index = 0;
            while (oku.Read())
            {

                index++;
                menu_id[index] = Convert.ToString(oku[0]).Trim(); menu_isim[index] = Convert.ToString(oku[1]).Trim();

            }
            baglanti.Close();
            ses_sayaci_limit= index; // ses sayacı sorgu sayısınca çalışmalı
        }


        private void Menu_haric_gorunmez()
        {

            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }



        private void timer3_Tick(object sender, EventArgs e)
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
                    timer3.Enabled = false;
                    seciminiz_kutusu.Focus(); // otomatik odak
                }

            }
        }
        // burda yine önceki formlarda olduğu gibi seçilen menüye göre forma veri gönderiyoruz
        public static string giden_paket_ismi;
        public static int giden_paket_id;
        private void onayla_button_Click_1(object sender, EventArgs e)
        {
            Form3_icerik form3_icerik = new Form3_icerik();
            if (seciminiz_kutusu.Text.Length == 1)
            {

                switch (Convert.ToChar(seciminiz_kutusu.Text))
                {

                    case '1':
                        giden_paket_ismi = menu_isim[1];
                        giden_paket_id = 1;
                        form3_icerik.Show();
                        break;
                    case '2':
                        giden_paket_ismi = menu_isim[2];
                        giden_paket_id = 2;
                        form3_icerik.Show();
                        break;
                    case '3':
                        giden_paket_ismi = menu_isim[3];
                        giden_paket_id = 3;
                        form3_icerik.Show();
                        break;
                    case '4':
                        giden_paket_ismi = menu_isim[4];
                        giden_paket_id = 4;
                        form3_icerik.Show();
                        break;
                    case '5':
                        giden_paket_ismi = menu_isim[5];
                        giden_paket_id = 5;
                        form3_icerik.Show();
                        break;
                    case '6':
                        giden_paket_ismi = menu_isim[6];
                        giden_paket_id = 6;
                        form3_icerik.Show();
                        break;
                    case '#':
                        anasayfa anasayfa = new anasayfa();
                        this.Close();
                        anasayfa.Show();
                        break;
                    default:
                        MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label3.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;




                }
            }
            else MessageBox.Show("Lütfen doğru seçim yapınız !", karsilama_label3.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);     
        
        }
     
    }
}
