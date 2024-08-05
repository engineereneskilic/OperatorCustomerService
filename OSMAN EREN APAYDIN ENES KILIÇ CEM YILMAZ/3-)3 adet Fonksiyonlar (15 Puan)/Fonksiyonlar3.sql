--Fn_CinsiyeteGoreKitapTuru

Create Function Fn_CinsiyeteGoreKitap1(@cinsiyet nvarchar(50),@kitapturu nvarchar(50)) -- se�ilen cinsiyete ve istenen kitap t�r�ne g�re hangi cinsiyet  hangi kitap t�r�nden ka� adet kitap �d�n� alm��
Returns int
As
Begin
    Declare @kactanesonuc int
    Select @kactanesonuc=COUNT(*) FROM Odunc_Verme,Uyeler,KitapUrun WHERE alinan_UyeTC=UyeTC AND alinan_KitapID=KitapID AND UyeCinsiyet=@cinsiyet AND KitapTuru=@kitapturu
	
	return @kactanesonuc
End

--TEST
SELECT dbo.Fn_CinsiyeteGoreKitapTuru('Kad�n','Piskoloji') AS Sonu�;