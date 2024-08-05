
--------Sorgular--------

Select(KitapAdi) AS Kitap_Adý from KitapUrun where KitapAdi like 'A%' -- KitapUrun tablosunda kitap_adý 'A' ile baþlayanlarý ekrana yazdýrmaktadýr.

Select kitapAdi,KitapFiyati  AS Kitap_Fiyatý From KitapUrun where KitapFiyati <10 --KitapUrun tablosunda kitap Fiyatý '10 Dan küçük' olan kitaplarý ekrana yazdýrmaktadýr.

Select Alinan_KitapID,alinan_UyeTC,Alinan_Tarih from Odunc_Verme where YEAR(Alinan_Tarih)=2016 AND MONTH(Alinan_Tarih)=09 AND DAY(Alinan_Tarih)=09 --Odunc_Verme Tablosunda (2016/09/09) Alýnan kitaplarý ekrana yazdýr.

Select (kitapAdi) AS Kitap_Adý From KitapUrun where KitapAdi between 'B' AND 'K' --Kitaplar tablosunda Kitap adý 'B' ve 'K' aralaðýndakileri ekrana yazdýrmaktadýr.

Select COUNT (DISTINCT KitapTuru) AS Farklý_Kitap_Türü  from KitapUrun  --Kitaplar  tablosunda Kaç Farklý kitap türü bulunduðunu ekrana yazdýrmaktadýr.

----Ýç Ýçe Sorgular---------

Select KitapFiyati,KitapAdi from KitapUrun
  Where KitapFiyati Between 20 and 21 (select MAX (KitapAdet)
   AS Stok_kitap_Adet  from KitapUrun) --Kitaplar Tablosunda listedeki  diðer fiyatlara göre yüksek olan kitap fiyatý ve tekrar Kitaplar tablosunda  diðer kitaplara göre en fazla olan stok adetini ekrana yazdýrmaktadýr.


Select KitapAdi,KitapYazari from KitapUrun where KitapAdet>
 (Select AVG (KitapAdet) from KitapUrun) -- Kitaplar Tablosunda KitapAdet(ID Bazýnda) arasýnda ortalama KitapAdet  daha çok olan kitaplarý(ID Bazýnda) ekrana yazdýrmaktadýr.

Select alinan_UyeTC from Odunc_Verme
 where Alinan_KitapID=10 
 (Select KitapAdi from KitapUrun where KitapID =10) 
 (Select UyeAd AS KitabýAlanUye from Uyeler,Odunc_Verme Where alinan_UyeTC=UyeTC AND alinan_KitapID=10 ) -- Kitaplar  tablosunda KÝtapýd'si 10 olan kitabýn Odunc_Verme tablosunda  kim tarafýndan alýndýðýný ekrana yazdýrmaktadýr.

 Select kitapAdi,KitapYazari
  from KitapUrun
  where KitapAdet>(Select AVG(KitapAdet) from KitapUrun)--Kitaplar Tablosunda  KitapAdetinin Ortalamasý üzerinde olan kitaplarý ekrana yazdýrmaktadýr.

 Select KitapAdi,KitapYazari,KitapAdet From KitapUrun where KitapAdet<ANY(
 Select KitapAdet from KitapUrun where KitapAdet=15) --Kitaplar tablosunda KitapAdet(ID Bazýnda) panelinde   enfazla ID'si yüksek olan kitaba göre daha Düþük ID'li  kitaplar  ekrana yazdýrmaktadýr.


