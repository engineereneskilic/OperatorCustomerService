-- Trigger 3 (  �ade tablosuna veri girildi�inde al�nan tarihe g�re ge�mi getirmi� erken mi getirmi� yani ka��nc� g�n� getirmi� )

Create Trigger TRG_IadeKontrolu
On Iade
After insert
As
   DECLARE @IadeID INT

   DECLARE @iade_UyeTC numeric(11, 0)

   DECLARE @iade_kitapID INT

   DECLARE @iade_Tarih DATE

   DECLARE @kontrol_alinan_tarih DATE

   DECLARE @kac_gungecti int
 

   Select @IadeID=iadeID, @iade_UyeTC=iade_UyeTC, @iade_kitapID=iade_KitapID, @iade_Tarih=iade_Tarih FROM inserted -- tetiklenen verileri alal�m

   SELECT @kontrol_alinan_tarih=alinan_tarih FROM Odunc_Verme WHERE @iade_UyeTC=alinan_UyeTC and @iade_kitapID=alinan_KitapID -- bakal�m �yemiz kitab� ne zaman alm�� ?

   SELECT @kac_gungecti=DATEDIFF(day,@kontrol_alinan_tarih,@iade_Tarih) -- �yemizin ald��� tarih ile bize getirdi�i tarih aras�nda ka� g�n ge�mi� ?

   if (@kac_gungecti > 15) BEGIN  PRINT N'Kitab� ge� getirdiniz'; END ELSE BEGIN  PRINT N'Kitab� zaman�nda teslim etti�iniz i�in te�ekk�r ederiz:)'; END  
     
	
-- Test

INSERT INTO Iade VALUES(20,'25467895610', 1, '2015-09-20')
