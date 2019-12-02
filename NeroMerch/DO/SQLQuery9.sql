USE [IN31_NeroMerch_Webshop]
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


