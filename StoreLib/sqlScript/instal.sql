USE [master]
GO

CREATE DATABASE [Store]
GO

USE [Store]
GO

CREATE LOGIN StoreUser WITH password='1234';
CREATE USER StoreUser FOR LOGIN StoreUser;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountType](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[TypeName] [nvarchar](100) NOT NULL,
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) PRIMARY KEY,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductPrice] [money] NOT NULL,
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDiscount](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[OnProductId] [int] NOT NULL,
	[ConditionProductId] [int] NOT NULL,
	[ConditionQuantity] [int] NOT NULL,
	[DiscountTypeId] [int] NOT NULL,
	[DiscountNumber] [float] NOT NULL,
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogData](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Data] [nvarchar](max) NOT NULL,
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Date] [datetime] NOT NULL,
	[message] [nvarchar](max) NOT NULL,
)
GO


SET IDENTITY_INSERT [DiscountType] ON
INSERT [dbo].[DiscountType] ([Id], [TypeName]) VALUES (1, N'Percent')
INSERT [dbo].[DiscountType] ([Id], [TypeName]) VALUES (2, N'GetFree')
SET IDENTITY_INSERT [DiscountType] OFF
GO

SET IDENTITY_INSERT [Product] ON
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductPrice]) VALUES (1, N'Butter', 0.8000)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductPrice]) VALUES (2, N'Milk', 1.1500)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductPrice]) VALUES (3, N'Bread', 1.0000)
SET IDENTITY_INSERT [Product] OFF
GO

SET IDENTITY_INSERT [ProductDiscount] ON
INSERT [dbo].[ProductDiscount] ([Id], [OnProductId], [ConditionProductId], [ConditionQuantity], [DiscountTypeId], [DiscountNumber]) VALUES (1, 3, 2, 2, 1, 50)
INSERT [dbo].[ProductDiscount] ([Id], [OnProductId], [ConditionProductId], [ConditionQuantity], [DiscountTypeId], [DiscountNumber]) VALUES (2, 2, 2, 3, 2, 1)
SET IDENTITY_INSERT [ProductDiscount] OFF
GO

ALTER TABLE [dbo].[ProductDiscount]  WITH CHECK ADD  CONSTRAINT [FK_ProductDiscount_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[ProductDiscount] CHECK CONSTRAINT [FK_ProductDiscount_DiscountType]
GO
ALTER TABLE [dbo].[ProductDiscount]  WITH CHECK ADD  CONSTRAINT [FK_ProductDiscount_Product] FOREIGN KEY([OnProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductDiscount] CHECK CONSTRAINT [FK_ProductDiscount_Product]
GO
ALTER TABLE [dbo].[ProductDiscount]  WITH CHECK ADD  CONSTRAINT [FK_ProductDiscount_Product1] FOREIGN KEY([ConditionProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductDiscount] CHECK CONSTRAINT [FK_ProductDiscount_Product1]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts] 
AS
BEGIN

	SET NOCOUNT ON;

	SELECT *
	FROM Product

END
GO
GRANT EXEC ON [GetAllProducts] TO StoreUser
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductDiscount] 
@ProductId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT *
	FROM ProductDiscount
	WHERE ConditionProductId = @ProductId

END
GO
GRANT EXEC ON [GetProductDiscount] TO StoreUser
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertData]
@Logdata nvarchar(MAX)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO LogData (Data)
	VALUES (@Logdata)

END
GO
GRANT EXEC ON [InsertData] TO StoreUser
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE WriteLog
	@Date datetime, 
	@Message nvarchar(MAX)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO  [dbo].[SystemLog]([Date],[message])
	VALUES (@Date, @Message)
END
GO
GRANT EXEC ON [WriteLog] TO StoreUser
GO