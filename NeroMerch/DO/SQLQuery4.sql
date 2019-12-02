USE [IN31_NeroMerch_Webshop]
GO

INSERT INTO [dbo].[Customer]
           ([Title]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Street]
           ,[Zip]
           ,[City]
           ,[PwHash]
           ,[Salt])
     VALUES
           ('Mr',
           'Andre',
           'Fonhousen',           
		   'A.fon@gmail.com',
           'WallmilkStreet 1/5',
           5500,
           'Chicago',
           135790,
           24680)
GO

INSERT INTO [dbo].[Manufacturer]
           ([Name])
     VALUES
           ('Hasbro')
GO



INSERT INTO [dbo].[Category]
           ([Name]
           ,[TaxRate])
     VALUES
           ('Actionfigures',20)
GO



select *
from [dbo].[Customer]


INSERT INTO [dbo].[Order]
           ([CustomerId]
           ,[PriceTotal]
           ,[DateOrdered]
           ,[Street]
           ,[Zip]
           ,[City])
     VALUES
           (3,
           300,
           12.12,
           'Jawohlgasse 13',
           1100,
           'Chicago')
GO
INSERT INTO [dbo].[OrderLine]
           ([OrderId]
           ,[ProductId]
           ,[Amount]
           ,[NetUnitPrice]
           ,[TaxRate])
     VALUES
           (1,
           1,
           1,
           300,
           20)
GO

INSERT INTO [dbo].[Product]
           ([ProductName]
           ,[NetUnitPrice]
           ,[ImagePath]
           ,[Description]
           ,[ManufacturerId]
           ,[CategoryId])
     VALUES
           ('Joda Actionfigures',
           300,
           'D:\VisualStudio2017\LAP\Bilder\Joda_action',
           'Eine Special Edition vom  Meister Joda',
           1,
           2)
GO



