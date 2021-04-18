CREATE DATABASE QL_NhaHang
GO

USE QL_NhaHang
GO

CREATE TABLE Account
(
DisplayName nvarchar(100) NOT NULL,
UserName nvarchar(100) PRIMARY KEY,
[Password] varchar(100) NOT NULL,
Phone varchar(20),
[Address] nvarchar(200),
Sex bit NOT NULL,
[Type] int NOT NULL DEFAULT 0 -- 1:admin ; 0:staff
)
GO

CREATE TABLE FoodCategory
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name nvarchar(100) NOT NULL DEFAULT N'Chưa đặt tên',
IdCategory int NOT NULL,
Price float NOT NULL DEFAULT 0,
Unit nvarchar(50),  -- Dơn vị tính
ImageFood image null,
[Status] bit 
FOREIGN KEY (IdCategory) references dbo.FoodCategory(Id)
)
GO


CREATE TABLE Bill
(
Id int IDENTITY(1,1) PRIMARY KEY,
DateCheckIn datetime NOT NULL DEFAULT GETDATE(),
DateCheckOut datetime NULL,
TableName nvarchar(100),
QuantityTable int DEFAULT 1,
Discount int DEFAULT 0,
TotalPrice float DEFAULT 0,
[Status] int NOT NULL DEFAULT 0, -- 1:đã thanh toán ; 0:chưa thanh toán
)
GO

CREATE TABLE BillInfo
(
Id int IDENTITY(1,1) PRIMARY KEY,
IdBill int NOT NULL,
IdFood int NOT NULL,
[Count] int NOT NULL DEFAULT 0,
Price float NOT NULL DEFAULT 0

FOREIGN KEY (IdBill) references dbo.Bill(Id),
FOREIGN KEY (IdFood) references dbo.Food(Id)
)
GO

INSERT INTO Account VALUES
('Admin Manager','admin','1','0999999999',N'Hà Nội',1,1),
(N'Trần Văn Vững','vung','1','0355967964',N'Ba Vì',1,0)
GO
-- Thêm FoodCategory
INSERT INTO FoodCategory(Name) VALUES
(N'Hải sản'),
(N'Đồ quay nướng'),
(N'Súp Canh'),
(N'Tráng miệng'),
(N'Demo 1'),
(N'Demo 2'),
(N'Demo 3'),
(N'Demo 4'),
(N'Demo danh mục 5')
go

-- thêm món ăn
INSERT INTO Food(Name,IdCategory,Price,Unit,ImageFood,[Status]) VALUES
(N'Lẩu hải sản chua cay',1,7500000,N'Nồi',null,1),
(N'Salat Hoa Quả',4,110000,N'Đĩa',null,1),
(N'Cua Alaska hấp xả (Con)',1,4500000,N'Con',null,1),
(N'Cua Alaska hấp xả (Đĩa)',1,13500000,N'Đĩa',null,1),
(N'Khâu phục',2,400000,N'Bát',null,1),
(N'Vịt nướng lá mật (Con)',2,500000,N'Con',null,1),
(N'Vịt nướng lá mật (Đĩa)',2,250000,N'Đĩa',null,1),
(N'Dưa hấu',4,40000,N'Đĩa',null,1),
(N'Nước ngọt cocacola',5,15000,N'Lon',null,1),
(N'Canh ngũ vị hầm xương',3,70000,N'Bát',null,1),
(N'Lẩu ếch',1,600000,N'Nồi',null,1),
(N'Salat Rau củ',4,80000,N'Đĩa',null,1),
(N'Tôm Hùm hấp xả (Đĩa)',1,10500000,N'Đĩa',null,1),
(N'Tôm Hùm hấp xả (Con)',1,3500000,N'Con',null,1),
(N'Lợn quay',2,400000,N'Đĩa',null,1),
(N'Vịt nướng xả',2,500000,N'Con',null,1),
(N'Dưa chuột',4,20000,N'Đĩa',null,1),
(N'Nước cam',5,15000,N'Lon',null,1),
(N'Canh khoai tây',3,10000,N'Bát',null,1)
GO


CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO


CREATE PROC LoginApp
@UserName nvarchar(100),
@Password varchar(100)
AS
BEGIN
  SELECT * FROM Account WHERE UserName = @UserName AND [Password] = @Password
END
GO

CREATE PROC GetAccountByUserName
@UserName nvarchar(100)
AS
BEGIN
  SELECT * FROM Account WHERE UserName = @UserName
END
GO

CREATE PROC GetFoodCategoryList
@Name nvarchar(100)
AS
BEGIN 
  IF(@Name = '' OR @Name IS NULL)
   BEGIN
    SELECT * FROM FoodCategory as f
   END
  ELSE
  BEGIN
    SELECT * FROM FoodCategory WHERE dbo.fuConvertToUnsign1(Name) LiKE '%'+dbo.fuConvertToUnsign1(@Name)+'%'
   END
END
GO

CREATE PROC GetFoodCategoryById
@Id int
AS
BEGIN 
SELECT * FROM FoodCategory Where Id = @Id
END
GO

CREATE PROC GetFoodCategoryByName
@Name nvarchar(100)
AS
BEGIN 
SELECT * FROM FoodCategory Where Name = @Name
END
GO

CREATE PROC GetListFood
@Name nvarchar(100)
AS
BEGIN 
  IF(@Name = '' OR @Name IS NULL)
   BEGIN
    SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id
   END
  ELSE
  BEGIN
     SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id 
	 AND (dbo.fuConvertToUnsign1(Food.Name) LiKE '%'+dbo.fuConvertToUnsign1(@Name)+'%' 
	 OR dbo.fuConvertToUnsign1(FoodCategory.Name) LiKE '%'+dbo.fuConvertToUnsign1(@Name)+'%')
   END
END
GO

CREATE PROC GetFoodByCategoryId
@IdCategory int
AS
BEGIN 
SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id AND IdCategory = @IdCategory AND Food.Status = 1
END
GO

CREATE PROC GetFoodByNameFoodAndIdFoodCat
@Name nvarchar(100),
@IdFoodCategory int
AS
BEGIN 
  IF(@Name = '' OR @Name IS NULL)
   BEGIN
    SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id AND FoodCategory.Id = @IdFoodCategory
   END
  ELSE
  BEGIN
     SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id AND FoodCategory.Id = @IdFoodCategory
	 AND (dbo.fuConvertToUnsign1(Food.Name) LiKE '%'+dbo.fuConvertToUnsign1(@Name)+'%' 
	 OR dbo.fuConvertToUnsign1(FoodCategory.Name) LiKE '%'+dbo.fuConvertToUnsign1(@Name)+'%')
   END
END
GO

CREATE PROC GetFoodById
@Id int
AS
BEGIN 
SELECT *, FoodCategory.Name as NameCategory FROM Food,FoodCategory WHERE Food.IdCategory = FoodCategory.Id AND Food.Id = @Id
END
GO

CREATE PROC InsertFood
@Name nvarchar(100),
@IdCategory int,
@Price float,
@Unit nvarchar(50),
@ImageFood image,
@Status bit
AS
BEGIN 
INSERT INTO Food(Name,IdCategory,Price,Unit,ImageFood,[Status]) VALUES(@Name,@IdCategory,@Price,@Unit,@ImageFood,@Status)
END
GO

CREATE PROC UpdateFood
@Id int,
@Name nvarchar(100),
@IdCategory int,
@Price float,
@Unit nvarchar(50),
@ImageFood image,
@Status bit
AS
BEGIN 
 UPDATE Food SET Name = @Name, IdCategory= @IdCategory, Price = @Price, Unit = @Unit, ImageFood = @ImageFood, [Status] = @Status WHERE Id = @Id
END
GO

CREATE PROC DeleteFood
@IdFood int
AS
BEGIN 
DELETE Food WHERE Id = @IdFood
END
GO

CREATE PROC InsertFoodCategory
@Name nvarchar(100)
AS
BEGIN 
INSERT INTO FoodCategory(Name) VALUES(@Name)
END
GO

CREATE PROC UpdateFoodCategory
@Id int,
@Name nvarchar(100)
AS
BEGIN 
 UPDATE FoodCategory SET Name = @Name WHERE Id = @Id
END
GO

CREATE PROC DeleteFoodCategory
@Id int
AS
BEGIN 
DELETE FoodCategory WHERE Id = @Id
END
GO

CREATE PROC GetListAccount
@Key nvarchar(100)
AS
BEGIN 
  IF(@Key = '' OR @Key IS NULL)
   BEGIN
   SELECT * FROM Account
   END
  ELSE
  BEGIN
    SELECT * FROM Account WHERE dbo.fuConvertToUnsign1(DisplayName) LiKE '%'+dbo.fuConvertToUnsign1(@Key)+'%' 
	OR dbo.fuConvertToUnsign1(UserName) LiKE '%'+dbo.fuConvertToUnsign1(@Key)+'%' 
	OR dbo.fuConvertToUnsign1(Phone) LiKE '%'+dbo.fuConvertToUnsign1(@Key)+'%'
	OR dbo.fuConvertToUnsign1([Address]) LiKE '%'+dbo.fuConvertToUnsign1(@Key)+'%'
   END
END
GO

CREATE PROC InsertAccount
@DisplayName nvarchar(100),
@UserName nvarchar(100),
@Password varchar(100),
@Phone varchar(20),
@Address nvarchar(200),
@Sex bit,
@Type int
AS
BEGIN 
INSERT Account VALUES(@DisplayName,@UserName,@Password,@Phone,@Address,@Sex,@Type)
END
GO

CREATE PROC UpdateAccount
@DisplayName nvarchar(100),
@UserName nvarchar(100),
@Password varchar(100),
@Phone varchar(20),
@Address nvarchar(200),
@Sex bit,
@Type int
AS
BEGIN 

IF(@Password = null OR @Password = '')
	BEGIN
       UPDATE Account SET DisplayName = @DisplayName, Phone = @Phone , [Address] = @Address , Sex = @Sex , [Type] = @Type WHERE UserName = @UserName
	END
ELSE
    BEGIN
      UPDATE Account SET DisplayName = @DisplayName, [Password] = @Password , Phone = @Phone , [Address] = @Address , Sex = @Sex , [Type] = @Type WHERE UserName = @UserName
    END
END
GO

CREATE PROC UpdatePasswordAccount
@UserName nvarchar(100),
@PasswordNew varchar(100)
AS
BEGIN 
  UPDATE Account SET [Password] = @PasswordNew WHERE UserName = @UserName
END
GO

CREATE PROC DeleteAccount
@UserName nvarchar(100)
AS
BEGIN 
DELETE Account WHERE UserName = @UserName
END
GO

CREATE PROC GetListBillByDate
@DateCheckIn datetime,
@DateCheckOut datetime
AS
BEGIN 
select * FROM Bill WHERE DateCheckIn >= @DateCheckIn AND DateCheckOut <= @DateCheckOut AND [Status] = 1
END
go

CREATE PROC GetListBillUnCheckOut
AS
BEGIN 
select * FROM Bill WHERE [Status] = 0
END
go

CREATE PROC GetListBillInfoByIdBill
@IdBill int
AS
BEGIN 
select *,Food.Name,Food.Price as 'PriceFood',(Count*Food.Price) as 'ToTalPrice' FROM BillInfo,Food WHERE BillInfo.IdFood = Food.Id AND IdBill = @IdBill
END
go

CREATE PROC GetBillInfoById
@Id int
AS
BEGIN 
select * FROM BillInfo WHERE Id = @Id
END
go

CREATE PROC GetBillInfoByIdFood
@IdFood int
AS
BEGIN 
select count(*) FROM BillInfo WHERE IdFood = @IdFood
END
go

CREATE PROC DeleteBill
@Id int
AS
BEGIN 
   DELETE BillInfo WHERE IdBill = @Id
   DELETE Bill WHERE Id = @Id
END
GO

CREATE PROC InsertBill
@TableName nvarchar(100),
@QuantityTable int
AS
BEGIN
   INSERT INTO Bill(DateCheckIn,DateCheckOut,TableName,QuantityTable,Discount,TotalPrice,[Status]) VALUES(GETDATE(),null,@TableName,@QuantityTable,0,0,0) 
END
GO

CREATE PROC UpdateBill
@Id int,
@TableName nvarchar(100),
@QuantityTable int
AS
BEGIN
   UPDATE Bill SET TableName = @TableName , QuantityTable = @QuantityTable WHERE Id = @Id
END
GO

CREATE PROC InsertBillInfo
@IdBill int,
@IdFood int,
@Count int
AS
BEGIN 
     DECLARE @isExitsBillInfo int
	 DECLARE @foodCount int = 1
	 DECLARE @PriceFood float

	 SELECT  @isExitsBillInfo = Id, @foodCount = b.Count FROM BillInfo as b Where IdBill = @IdBill AND IdFood = @IdFood
	 SELECT @PriceFood = Price from Food where Id = @IdFood

	 DECLARE @newCount int = @foodCount + @Count
 IF(@isExitsBillInfo > 0)
	 BEGIN
	   IF(@newCount > 0)
	    UPDATE BillInfo SET Count = @newCount, Price = (@newCount*@PriceFood) Where IdFood = @IdFood AND IdBill = @IdBill
		ELSE
		DELETE BillInfo Where IdBill = @IdBill AND IdFood = @IdFood
	 END
 ELSE
 BEGIN
  IF(@newCount > 1)
     BEGIN
      INSERT INTO BillInfo(IdBill,IdFood,[Count],Price) VALUES(@IdBill,@IdFood,@Count,(@Count*@PriceFood))
	 END
 END
END
GO

CREATE PROC GetMaxBillId
AS
BEGIN 
SELECT MAX(Id) FROM Bill
END
GO

CREATE PROC CheckOut
@Id int,
@Discount int,
@TotalPrice float
AS
BEGIN 
Update Bill Set DateCheckOut = GETDATE() , Status = 1, Discount = @Discount, TotalPrice = @TotalPrice WHERE Id= @Id
END
GO

CREATE PROC GetBillDetailByIdBill
@IdBill int
AS
BEGIN
SELECT b.Id, b.DateCheckIn, b.DateCheckOut, b.TableName, b.QuantityTable, b.Discount, b.TotalPrice as 'FinalToTalPrice', f.Name as 'FoodName',f.Unit,bi.Count,(bi.Price/bi.Count) as 'Price',  bi.Price as 'TotalPrice' FROM Bill as b
JOIN BillInfo as bi ON b.Id = bi.IdBill
JOIN Food as f ON bi.IdFood = f.Id WHERE b.Id = @IdBill
END
GO