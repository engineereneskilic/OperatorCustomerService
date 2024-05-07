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
    public partial class Form3_icerik : Form
    {
        public Form3_icerik()
        {
            InitializeComponent();
        }

        int ses_sayaci = giris.ses_sayaci -1; // açıklama satırı olduğu zaman sayaç 0 dan başlasın
        int ses_sayaci_limit = giris.ses_sayaci_limit;
        char secim_ses;
        SqlConnection baglanti1 = new SqlConnection(giris.baglanticumlesi);
        SqlConnection baglanti2 = new SqlConnection(giris.baglanticumlesi);
        string[] menu_id = new string[giris.menu_id_count]; // veritabanından gelen idler tutulcak
        string[] menu_isim = new string[giris.menu_isim_count]; // veritabanından gelen isimler tutulcak


        private void Form3_icerik_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini alalım ve sağ alttakı kullanıcı_label ımıza aktaralım hoş görünsün
            kullanici_label.Text = "Hoşgeldiniz " + giris.kullanici_isim;     


           karsilama_label.Text = Form3.giden_paket_ismi;


            timer3_icerik.Enabled = true;
            timer3_icerik.Start();

           
            Menu_haric_gorunmez();

          
            baglanti1.Open();
            SqlCommand sql3 = new SqlCommand("SELECT * FROM menuler WHERE menu_id LIKE '3-"+Form3.giden_paket_id+"-%'", baglanti1);
            SqlDataReader oku3 = sql3.ExecuteReader();
            int index = -1;
             while (oku3.Read())
            {

                index++;
               menu_id[index] = Convert.ToString(oku3[0]).Trim(); menu_isim[index] = Convert.ToString(oku3[1]).Trim();

            }
            baglanti1.Close();
            ses_sayaci_limit = index; // ses sayaci limiti ile sorgu sayısı sayısı aynı olcak 
        }

        private void Menu_haric_gorunmez()
        {

            menu_label.Text = "";
            menu_label.Visible = false;
            seciminiz_label.Visible = false;
            seciminiz_kutusu.Visible = false;
            onayla_button.Visible = false;
        }

        private void timer3_icerik_Tick(object sender, EventArgs e)
        {
            ses_sayaci++;
            if (ses_sayaci <= ses_sayaci_limit)
            {
               
                if (ses_sayaci == 0)
                {
                    giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                    menu_label.Text += "Açıklama: " + menu_isim[ses_sayaci]+"\n";
                    menu_label.Visible = true;
                   
                }
                else
                {
                    timer3_icerik.Interval = 3000; // ses beklemesi çok uzun sürüyo beklemsin diye kısaltalım
                    giris.ne_calayim("ses_dosyalari", menu_id[ses_sayaci]);
                    menu_label.Text += ses_sayaci + "." + menu_isim[ses_sayaci] + " için\n";
                    menu_label.Visible = true;
                }
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
                    timer3_icerik.Enabled = false;
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
                        // gelen paket id ye göre veritabanımızdaki paketlerle eşleştirip paketin içeriğini alalım
                        baglanti1.Open();
                        SqlCommand paket_sql = new SqlCommand("SELECT * FROM paketler WHERE paket_id='"+Form3.giden_paket_id+"'", baglanti1);
                        SqlDataReader paket_listele = paket_sql.ExecuteReader();
                        paket_listele.Read();

                        int paket_id = Convert.ToInt32(paket_listele[0]);
                        string paket_isim = Convert.ToString(paket_listele[1]);
                        int paket_dk= Convert.ToInt32(paket_listele[2]);
                        int paket_sms = Convert.ToInt32(paket_listele[3]);
                        int paket_mb = Convert.ToInt32(paket_listele[4]);
                        int paket_ucret = Convert.ToInt32(paket_listele[5]);
                        baglanti1.Close();


                        // kullanıcının bakiye bilgisini alıyoruz
                        baglanti1.Open();
                        SqlCommand lirasi_varmi_sql = new SqlCommand("SELECT bakiye FROM kullanicilar WHERE tel_no='" + giris.kullanici_tel + "'", baglanti1);
                        SqlDataReader lirasi_varmi = lirasi_varmi_sql.ExecuteReader();
                        lirasi_varmi.Read();
                        if (Convert.ToInt32(lirasi_varmi[0]) >= paket_ucret) // kullanıcının yeterli bakiyesi varmı paketi almak için varsa alsın yoksa uyarı messagebox gelsin
                        {
                            baglanti2.Open();
                            SqlCommand tarife_degistir_sql = new SqlCommand("UPDATE kullanicilar SET paket_ismi='"+Form3.giden_paket_ismi+"',kalan_dk=kalan_dk+'"+paket_dk+"',kalan_sms=kalan_sms+'"+paket_sms+"',kalan_mb=kalan_mb+'"+paket_mb+"',bakiye=bakiye-'"+paket_ucret+"' WHERE tel_no='" + giris.kullanici_tel + "'", baglanti2);
                            if (tarife_degistir_sql.ExecuteNonQuery() != -1) // komut çalıştıyse
                            {
                                MessageBox.Show("Paketiniz başarı ile satın alınmıştır...", karsilama_label.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                              
                            }
                            baglanti2.Close();
                        }
                        else { MessageBox.Show("Hattınızda yeterli bakiye bulunmadığı için işleminizi gerçekleştiremiyoruz..", karsilama_label.Text.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                        baglanti1.Close();


                        this.Close();
                        anasayfa from1 = new anasayfa();
                        from1.Show();
                        break;

                    case '#':
                        Form3 form3 = new Form3();
                        this.Close();
                        form3.Show();
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
