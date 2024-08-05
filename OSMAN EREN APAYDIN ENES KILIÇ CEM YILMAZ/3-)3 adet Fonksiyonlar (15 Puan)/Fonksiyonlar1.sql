

-------------------------------------Fonksiyonlar-------------------------------------------------------------------------------------------------

--Fonksiyonlar:Fonksiyonlar tamamen i�imizi kolayla�t�rmak ad�na s�rekli olarak tekrarlad���m�z
-- sql sorgular�na tek bir noktadan eri�memizi sa�lar. Buda bize h�zl� bir eri�im imkan�
--h�zl� bir hata kontrol mekanizmas�, �abuk m�dahale, sorgu tekrarlamama gibi imkanlar� verir.
---------------------------------------------------------------------------------------------------------------------------------------------------
--Say�sal De�erli Fonksiyonlar (Scalar-Valued Functions)
--Scalar = ad�ndan da anla��ld��� gibi say�sal de�er d�nd�ren bir fonksiyondur.
-------------------

Create Function KitaplarAdet  --Kitaplar tablosunda Kitap Adedinin(ID Baz�nda) Toplam�n� Ekrana yazd�rmaktad�r.
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




