--Fn_CinsiyeteGoreKitapTuru

Create Function Fn_CinsiyeteGoreKitap1(@cinsiyet nvarchar(50),@kitapturu nvarchar(50)) -- seçilen cinsiyete ve istenen kitap türüne göre hangi cinsiyet  hangi kitap türünden kaç adet kitap ödünç almýþ
Returns int
As
Begin
    Declare @kactanesonuc int
    Select @kactanesonuc=COUNT(*) FROM Odunc_Verme,Uyeler,KitapUrun WHERE alinan_UyeTC=UyeTC AND alinan_KitapID=KitapID AND UyeCinsiyet=@cinsiyet AND KitapTuru=@kitapturu
	
	return @kactanesonuc
End

--TEST
SELECT dbo.Fn_CinsiyeteGoreKitapTuru('Kadýn','Piskoloji') AS Sonuç;