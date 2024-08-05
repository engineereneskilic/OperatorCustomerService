-- Trigger 3 (  Ýade tablosuna veri girildiðinde alýnan tarihe göre geçmi getirmiþ erken mi getirmiþ yani kaçýncý günü getirmiþ )

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
 

   Select @IadeID=iadeID, @iade_UyeTC=iade_UyeTC, @iade_kitapID=iade_KitapID, @iade_Tarih=iade_Tarih FROM inserted -- tetiklenen verileri alalým

   SELECT @kontrol_alinan_tarih=alinan_tarih FROM Odunc_Verme WHERE @iade_UyeTC=alinan_UyeTC and @iade_kitapID=alinan_KitapID -- bakalým üyemiz kitabý ne zaman almýþ ?

   SELECT @kac_gungecti=DATEDIFF(day,@kontrol_alinan_tarih,@iade_Tarih) -- üyemizin aldýðý tarih ile bize getirdiði tarih arasýnda kaç gün geçmiþ ?

   if (@kac_gungecti > 15) BEGIN  PRINT N'Kitabý geç getirdiniz'; END ELSE BEGIN  PRINT N'Kitabý zamanýnda teslim ettiðiniz için teþekkür ederiz:)'; END  
     
	
-- Test

INSERT INTO Iade VALUES(20,'25467895610', 1, '2015-09-20')
