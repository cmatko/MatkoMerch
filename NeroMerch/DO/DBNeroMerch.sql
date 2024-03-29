USE [master]
GO
/****** Object:  Database [IN31_NeroMerch_Webshop]    Script Date: 30.10.2019 13:47:28 ******/
CREATE DATABASE [IN31_NeroMerch_Webshop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IN31_NeroMerch_Webshop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IN31_NeroMerch_Webshop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IN31_NeroMerch_Webshop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IN31_NeroMerch_Webshop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )

CREATE DATABASE [IN31_NeroMerch_Webshop]
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IN31_NeroMerch_Webshop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ARITHABORT OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET  MULTI_USER 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET QUERY_STORE = OFF
GO
USE [IN31_NeroMerch_Webshop]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [IN31_NeroMerch_Webshop]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 30.10.2019 13:47:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[NetUnitPrice] [money] NOT NULL,
	[ImagePath] [varchar](300) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderLine]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[NetUnitPrice] [money] NOT NULL,
	[LinePrice] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseKey]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseKey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WasUsed] [bit] NOT NULL,
	[LicenseKey] [int] NOT NULL,
	[OrderLine_Id] [int] NULL,
	[KeyString] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[OrderContent]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[OrderContent]
AS
SELECT
	lk.Id as KeyId,
	ol.OrderId as OrderId,
	co.Name as Manufacturer,
	pr.ProductName as ProductName,
	ol.NetUnitPrice,
	lk.KeyString,
	isnull(lk.WasUsed,0) as WasUsed 
FROM 
	OrderLine ol 
	JOIN Product pr ON ol.ProductId = pr.Id
	JOIN Manufacturer co ON pr.ManufacturerId = co.Id
	LEFT JOIN LicenseKey lk ON lk.OrderLine_Id = ol.Id
GO
/****** Object:  Table [dbo].[Category]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductInfo]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create view [dbo].[ProductInfo]
as
	select 
		Pr.Id, 
		Pr.ProductName, 
		Pr.Description, 
		Pr.NetUnitPrice, 
		Pr.ImagePath, 
		Pr.ManufacturerId as ManufacturerId, 
		Pr.CategoryId as CategoryId, 
		Co.Name as Company, 
		Ca.Name as Category
	from Product Pr
		join Manufacturer Co on Pr.ManufacturerId = Co.Id
		join Category Ca on Pr.CategoryId = Ca.Id
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingAddress](
	[OrderId] [int] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](4) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [varchar](320) NOT NULL,
	[Street] [varchar](100) NOT NULL,
	[Zip] [char](4) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[PwHash] [binary](32) NOT NULL,
	[Salt] [binary](32) NOT NULL,
	[PwResetCode] [uniqueidentifier] NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiftCert]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiftCert](
	[LicenseKeyId] [int] NOT NULL,
	[RemainingValue] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LicenseKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PriceTotal] [money] NOT NULL,
	[DateOrdered] [datetime] NULL,
	[GiftCertId] [int] NULL,
	[PriceNet] [money] NOT NULL,
	[VoucherId] [int] NULL,
	[DatePaid] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Value] [tinyint] NOT NULL,
	[Comment] [nvarchar](160) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 30.10.2019 13:47:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](25) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[IsPercent] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Apparel and Caps')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Accessoires')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Actionfigures')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'sonstiges')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Manufacturer] ON 

INSERT [dbo].[Manufacturer] ([Id], [Name]) VALUES (1, N'Hasbro')
INSERT [dbo].[Manufacturer] ([Id], [Name]) VALUES (2, N'TheForceMayBe')
INSERT [dbo].[Manufacturer] ([Id], [Name]) VALUES (3, N'Star Wars ToyLine')
SET IDENTITY_INSERT [dbo].[Manufacturer] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (3, N'Joda Actionfigures', 66.0000, N'/Pictures/joda_action.jpg', N'Eine Special Edition vom  Meister Joda', 1, 3)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (8, N'Han Solo Actionfigures', 69.0000, N'/Pictures/Han_Solo_Action.jpg', N'SoloMania', 1, 3)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (9, N'Luke Skywalker', 70.0000, N'/Pictures/Luke_Action.jpg', N'The choosen one', 1, 3)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (10, N'Vader Toaster', 45.0000, N'/Pictures/vader_toast.jpg', N'Toaster', 2, 4)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (13, N'Cute Porg', 13.0000, N'/Pictures/prog.jpg', N'Toy', 3, 3)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (14, N'Lightsaber', 55.0000, N'/Pictures/lightsaber_2for1.jpg', N'Toy', 3, 3)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (15, N'Joda Shirt classic', 23.0000, N'/Pictures/joda_shirt_1.jpg', N'Kleidung', 2, 1)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (16, N'USB Schlüsselanhänger', 12.0000, N'/Pictures/SW_Key_AC_1.png', N'Accessoires', 2, 2)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (17, N'Manschettenknöpfe Darth Vader Maske', 19.0000, N'/Pictures/Vader_Mas_Kno.jpg', N'Manschettenknöpfe', 2, 2)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (18, N'Chewbacca Knuddle Rucksack', 35.0000, N'/Pictures/ChewBag.jpg', N'Rucksack Chewbacca ', 1, 4)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (19, N'Chewbacca T-Shirt', 20.0000, N'/Pictures/chew.jpeg', N'Chew-Shirt', 2, 1)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (20, N'Solo Börse', 30.0000, N'/Pictures/Han_Wallet.jpg', N'Wallet baby', 3, 2)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (21, N'MerrySithmas Pullover', 25.0000, N'/Pictures/MerrySithmas.jpg', N'MerrySithmas', 2, 1)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (22, N'R2D2 Bierkühler', 90.0000, N'/Pictures/R2D2BeerFridge.jpg', N'R2D2BeerFridge', 2, 4)
INSERT [dbo].[Product] ([Id], [ProductName], [NetUnitPrice], [ImagePath], [Description], [ManufacturerId], [CategoryId]) VALUES (23, N'Darth Mobil', 8000.0000, N'/Pictures/Star-Wars-Car.jpg', N'Darth car', 2, 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A9D105345F46E50F]    Script Date: 30.10.2019 13:47:30 ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [uq_OrderLine]    Script Date: 30.10.2019 13:47:30 ******/
ALTER TABLE [dbo].[OrderLine] ADD  CONSTRAINT [uq_OrderLine] UNIQUE NONCLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [uq_rating]    Script Date: 30.10.2019 13:47:30 ******/
ALTER TABLE [dbo].[Rating] ADD  CONSTRAINT [uq_rating] UNIQUE NONCLUSTERED 
(
	[ProductId] ASC,
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LicenseKey] ADD  DEFAULT ((0)) FOR [WasUsed]
GO
ALTER TABLE [dbo].[BillingAddress]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[LicenseKey]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_Id] FOREIGN KEY([OrderLine_Id])
REFERENCES [dbo].[OrderLine] ([Id])
GO
ALTER TABLE [dbo].[LicenseKey] CHECK CONSTRAINT [FK_OrderLine_Id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([Id])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 30.10.2019 13:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[GetAllCategories]
AS
	SELECT * FROM Category
GO
/****** Object:  StoredProcedure [dbo].[GetCatRevenueinYear]    Script Date: 30.10.2019 13:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc [dbo].[GetCatRevenueinYear]
(
	@year int
)
as 
begin
	select top (5) SUM(OL.Amount) as Stückzahl, p.[ProductName] 
	from Category C inner join Product P
		on P.CategoryId = C.Id inner join OrderLine OL
		on OL.ProductId = P.Id inner join [Order] O
		on O.Id = OL.OrderId
	where YEAR(O.DateOrdered) = @year
	group by p.[ProductName]
	order by Stückzahl desc
end
GO
/****** Object:  StoredProcedure [dbo].[SearchAndFilter]    Script Date: 30.10.2019 13:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[SearchAndFilter]
(
	@text varchar(100),
	@catId int
)
AS
BEGIN
	IF (@catId = -1)
		BEGIN
			SELECT * FROM ProductInfo
			WHERE 
				[ProductName] LIKE '%' + @text + '%' OR
				[Description] LIKE '%' + @text + '%' OR
				[ManufacturerId] LIKE '%' + @text + '%'
		END
	ELSE
		BEGIN
			SELECT * FROM ProductInfo
			WHERE 
				CategoryId = @catId AND 
				(
					[ProductName] LIKE '%' + @text + '%' OR
					[Description] LIKE '%' + @text + '%' OR
					[ManufacturerId] LIKE '%' + @text + '%'
				)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[Statistik]    Script Date: 30.10.2019 13:47:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   Procedure [dbo].[Statistik]
as 
begin
Select top 5 [ProductName] ,SUM(Amount) as TopVerkäufe, ProductId
FROM [dbo].[Product] join [dbo].[OrderLine]
ON Product.Id = OrderLine.ProductId
group by [ProductName],ProductId
Order by TopVerkäufe desc
end
GO
USE [master]
GO
ALTER DATABASE [IN31_NeroMerch_Webshop] SET  READ_WRITE 
GO
