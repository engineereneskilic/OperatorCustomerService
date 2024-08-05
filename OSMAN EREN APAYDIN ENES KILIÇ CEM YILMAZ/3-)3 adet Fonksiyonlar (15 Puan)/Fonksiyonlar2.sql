

-----------------------------------Fonksiyonlar-----------------------------------------------------------------------------------------------------------------------
--Tablo Döndüren Fonksiyonlar (Table-valued Functions)

--Sayýsal deðerli fonksiyondan tek farký döndürdüðü verinin tablo olmasýdýr.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------



CREATE FUNCTION fn_Fiyatagöresirala (@fiyat int )   --Kitap Fiyatý 20 tl tutarýndaki kitaplarý listelemektedir. 
RETURNS table
AS
RETURN Select KitapAdi,KitapYazari,KitapFiyati from KitapUrun where KitapFiyati=@fiyat
Go


Select * from dbo.fn_Fiyatagöresirala(20)