USE IN31_NeroMerch_Webshop

--DROP TABLE OrderLine
--DROP TABLE Product
--DROP TABLE Category
--DROP TABLE Manufacturer
--DROP TABLE [Order]
--DROP TABLE Customer

CREATE TABLE Customer
(
	Id				int				primary key identity	not null,
	Title			varchar(10)								not null,
	FirstName		nvarchar(50)							not null,
	LastName		nvarchar(50)							not null,
	Email			varchar(320)	unique					not null,
	Street			varchar(100)							not null,
	Zip				char(4)									not null,
	City			varchar(100)							not null,
	PwHash			binary(32)								not null,
	Salt			binary(32)								not null
)

CREATE TABLE [Order]
(
	Id				int				primary key identity	not null,
	CustomerId		int				references Customer		not null,
	PriceTotal		money										null,
	DateOrdered		datetime									null,
	Street			varchar(100)							not null, 
	Zip				char(4)									not null,
	City			varchar(100)							not null
)

CREATE TABLE Category
(
	Id				int				primary key identity	not null,
	[Name]			varchar(100)							not null,
	TaxRate			money									not null
)

CREATE TABLE Manufacturer
(
	Id				int				primary key identity	not null,
	[Name]			varchar(100)							not null
)

CREATE TABLE Product
(
	Id				int				primary key identity	not null,
	ProductName		varchar(100)							not null,
	NetUnitPrice	money									not null,
	ImagePath		varchar(300)							not null,
	Description		varchar(max)							not null,
	ManufacturerId	int				references Manufacturer	not null,
	CategoryId		int				references Category		not null
)

CREATE TABLE OrderLine
(
	Id				int				primary key identity	not null,
	OrderId			int				references [Order]		not null,
	ProductId		int				references Product		not null,
	Amount			int										not null,
	NetUnitPrice	money									not null,
	TaxRate			money									not null,
	CONSTRAINT uq_OrderLine UNIQUE(OrderId,ProductId)
)

