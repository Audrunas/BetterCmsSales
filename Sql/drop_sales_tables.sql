IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'VersionInfo')
BEGIN
	DROP TABLE bcms_sales.VersionInfo
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'SaleProducts')
BEGIN
	DROP TABLE bcms_sales.SaleProducts
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Sales')
BEGIN
	DROP TABLE bcms_sales.Sales
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'PurchaseProducts')
BEGIN
	DROP TABLE bcms_sales.PurchaseProducts
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Purchases')
BEGIN
	DROP TABLE bcms_sales.Purchases
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Products')
BEGIN
	DROP TABLE bcms_sales.Products
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Units')
BEGIN
	DROP TABLE bcms_sales.Units
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Buyers')
BEGIN
	DROP TABLE bcms_sales.Buyers
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Suppliers')
BEGIN
	DROP TABLE bcms_sales.Suppliers
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'Partners')
BEGIN
	DROP TABLE bcms_sales.Partners
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'SaleStatuses')
BEGIN
	DROP TABLE bcms_sales.SaleStatuses
END
GO

IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'bcms_sales' AND TABLE_NAME = N'PurchaseStatuses')
BEGIN
	DROP TABLE bcms_sales.PurchaseStatuses
END
GO