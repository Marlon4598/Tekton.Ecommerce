SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='VirtualStore')
		drop database VirtualStore
GO

CREATE DATABASE VirtualStore;
GO

USE VirtualStore
GO


CREATE TABLE Products (
ProductID   int identity(1,1) NOT NULL ,
Name	    varchar(80) NOT NULL ,
Status      bit NOT NULL, 
Stock       smallint NULL DEFAULT (0), 
Description varchar(150) NOT NULL,
Price       money NULL DEFAULT (0),
CONSTRAINT "PK_Products" PRIMARY KEY  CLUSTERED 
	(
		"ProductID"
	),
	CONSTRAINT "CK_Products_Price" CHECK (Price >= 0),
)
GO


CREATE PROCEDURE ProductInsert
(
	@Name varchar(100),
	@Status bit,
	@Stock smallint,
	@Description varchar(150),
	@Price money
)
AS
BEGIN
	INSERT INTO Products
           (Name
           ,Status
           ,Stock
           ,Description
           ,Price)
     VALUES
           (
			@Name,
			@Status,
			@Stock,
			@Description,
			@Price
		   )
END
GO


CREATE PROCEDURE ProductUpdate
(
	@ProductID int,
	@Name varchar(80),
	@Status bit,
	@Stock smallint,
	@Description varchar(150),
	@Price money
)
AS
BEGIN
	UPDATE Products
	set 
		Name=@Name,
		Status=@Status,
		Stock=@Stock,
		Description=@Description,
		Price=@Price
	where 
		ProductID=@ProductID
END
GO


CREATE PROCEDURE ProductDelete
(
	@ProductID int
)
AS
BEGIN
	DELETE Products
	where 
		ProductID=@ProductID
END
GO


CREATE PROCEDURE ProductGetByID
(
	@ProductID int
)
AS
BEGIN
	Select
		ProductID, Name, Status, Stock, Description, Price
	from
		Products
	where 
		ProductID=@ProductID
END
GO


CREATE PROCEDURE ProductList
AS
BEGIN
	Select
		ProductID, Name, Status, Stock, Description, Price
	from
		Products
END
GO

-- The following adds data to the tables just created.
Insert into Products(Name, Status, Stock, Description, Price) values('Webcam', 1, 50, 'Teraware W19, 2K Full HD', 30.00)
Insert into Products(Name, Status, Stock, Description, Price) values('Keyboard', 1, 80, 'Gamer Havit One Hand', 20.00)
Insert into Products(Name, Status, Stock, Description, Price) values('Tablet', 1, 20, 'Huawei MediaPad T3 Kids ', 335.00)
GO






