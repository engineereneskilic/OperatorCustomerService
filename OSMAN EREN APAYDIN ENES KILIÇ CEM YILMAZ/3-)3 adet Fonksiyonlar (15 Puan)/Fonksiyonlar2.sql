

-----------------------------------Fonksiyonlar-----------------------------------------------------------------------------------------------------------------------
--Tablo D�nd�ren Fonksiyonlar (Table-valued Functions)

--Say�sal de�erli fonksiyondan tek fark� d�nd�rd��� verinin tablo olmas�d�r.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------



CREATE FUNCTION fn_Fiyatag�resirala (@fiyat int )   --Kitap Fiyat� 20 tl tutar�ndaki kitaplar� listelemektedir. 
RETURNS table
AS
RETURN Select KitapAdi,KitapYazari,KitapFiyati from KitapUrun where KitapFiyati=@fiyat
Go


Select * from dbo.fn_Fiyatag�resirala(20)