

-------------------------------------Fonksiyonlar-------------------------------------------------------------------------------------------------

--Fonksiyonlar:Fonksiyonlar tamamen iþimizi kolaylaþtýrmak adýna sürekli olarak tekrarladýðýmýz
-- sql sorgularýna tek bir noktadan eriþmemizi saðlar. Buda bize hýzlý bir eriþim imkaný
--hýzlý bir hata kontrol mekanizmasý, çabuk müdahale, sorgu tekrarlamama gibi imkanlarý verir.
---------------------------------------------------------------------------------------------------------------------------------------------------
--Sayýsal Deðerli Fonksiyonlar (Scalar-Valued Functions)
--Scalar = adýndan da anlaþýldýðý gibi sayýsal deðer döndüren bir fonksiyondur.
-------------------

Create Function KitaplarAdet  --Kitaplar tablosunda Kitap Adedinin(ID Bazýnda) Toplamýný Ekrana yazdýrmaktadýr.
(
@sayi1 int,
@sayi2 int,
@sayi3 int,
@sayi4 int,
@sayi5 int,
@sayi6 int,
@sayi7 int,
@sayi8 int,
@sayi9 int,
@sayi10 int

)
Returns int
as
       Begin
	           Return @sayi1+@sayi2+@sayi3+@sayi4+@sayi5+@sayi6+@sayi7+@sayi8+@sayi9+@sayi10
			   end

go

Select dbo.KitaplarAdet(18,29,15,9,17,11,16,22,5,15) AS Elimizde_Bulunan_Kitap_Adetleri




