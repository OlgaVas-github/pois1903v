﻿CREATE TABLE [dbo].[User]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Role INT NOT NULL,
	Email NVARCHAR(50) NOT NULL,
	FirstName NVARCHAR(50) NULL,
	LastName NVARCHAR(50) NULL,
	Password NVARCHAR(50) NOT NULL
);


ALTER TABLE [User] ADD Country nvarchar(10) NULL;


CREATE TABLE [dbo].[Company]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(50) NOT NULL,
	Address NVARCHAR(50) NULL
);

CREATE TABLE [dbo].[Product] 
(
    [ID] INT IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [Price] DECIMAL (18,2)  NULL,
    [CompanyID] INT NOT NULL REFERENCES [Company] (ID), 
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Order]
(
    [ID] INT IDENTITY (1, 1) NOT NULL,
    [OrderID] NVARCHAR(50) NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [ShipDate] DATETIME NULL,
    [Price] DECIMAL (18,2) NOT NULL, 
    [ProductID] INT NOT NULL REFERENCES [Product] (ID), 
    PRIMARY KEY CLUSTERED ([ID] ASC)
)