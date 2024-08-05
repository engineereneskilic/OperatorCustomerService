
--------Sorgular--------

Select(KitapAdi) AS Kitap_Ad� from KitapUrun where KitapAdi like 'A%' -- KitapUrun tablosunda kitap_ad� 'A' ile ba�layanlar� ekrana yazd�rmaktad�r.

Select kitapAdi,KitapFiyati  AS Kitap_Fiyat� From KitapUrun where KitapFiyati <10 --KitapUrun tablosunda kitap Fiyat� '10 Dan k���k' olan kitaplar� ekrana yazd�rmaktad�r.

Select Alinan_KitapID,alinan_UyeTC,Alinan_Tarih from Odunc_Verme where YEAR(Alinan_Tarih)=2016 AND MONTH(Alinan_Tarih)=09 AND DAY(Alinan_Tarih)=09 --Odunc_Verme Tablosunda (2016/09/09) Al�nan kitaplar� ekrana yazd�r.

Select (kitapAdi) AS Kitap_Ad� From KitapUrun where KitapAdi between 'B' AND 'K' --Kitaplar tablosunda Kitap ad� 'B' ve 'K' arala��ndakileri ekrana yazd�rmaktad�r.

Select COUNT (DISTINCT KitapTuru) AS Farkl�_Kitap_T�r�  from KitapUrun  --Kitaplar  tablosunda Ka� Farkl� kitap t�r� bulundu�unu ekrana yazd�rmaktad�r.

----�� ��e Sorgular---------

Select KitapFiyati,KitapAdi from KitapUrun
  Where KitapFiyati Between 20 and 21 (select MAX (KitapAdet)
   AS Stok_kitap_Adet  from KitapUrun) --Kitaplar Tablosunda listedeki  di�er fiyatlara g�re y�ksek olan kitap fiyat� ve tekrar Kitaplar tablosunda  di�er kitaplara g�re en fazla olan stok adetini ekrana yazd�rmaktad�r.


Select KitapAdi,KitapYazari from KitapUrun where KitapAdet>
 (Select AVG (KitapAdet) from KitapUrun) -- Kitaplar Tablosunda KitapAdet(ID Baz�nda) aras�nda ortalama KitapAdet  daha �ok olan kitaplar�(ID Baz�nda) ekrana yazd�rmaktad�r.

Select alinan_UyeTC from Odunc_Verme
 where Alinan_KitapID=10 
 (Select KitapAdi from KitapUrun where KitapID =10) 
 (Select UyeAd AS Kitab�AlanUye from Uyeler,Odunc_Verme Where alinan_UyeTC=UyeTC AND alinan_KitapID=10 ) -- Kitaplar  tablosunda K�tap�d'si 10 olan kitab�n Odunc_Verme tablosunda  kim taraf�ndan al�nd���n� ekrana yazd�rmaktad�r.

 Select kitapAdi,KitapYazari
  from KitapUrun
  where KitapAdet>(Select AVG(KitapAdet) from KitapUrun)--Kitaplar Tablosunda  KitapAdetinin Ortalamas� �zerinde olan kitaplar� ekrana yazd�rmaktad�r.

 Select KitapAdi,KitapYazari,KitapAdet From KitapUrun where KitapAdet<ANY(
 Select KitapAdet from KitapUrun where KitapAdet=15) --Kitaplar tablosunda KitapAdet(ID Baz�nda) panelinde   enfazla ID'si y�ksek olan kitaba g�re daha D���k ID'li  kitaplar  ekrana yazd�rmaktad�r.


