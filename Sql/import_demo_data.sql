-- units
if not exists (select 1 from bcms_sales.Units where isdeleted = 0 and shorttitle = 'vnt.')
	insert into bcms_sales.Units (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, title, ShortTitle)
	values (1, 0, getdate(), 'admin', getdate(), 'admin', 'Vienetai', 'vnt.')

if not exists (select 1 from bcms_sales.Units where isdeleted = 0 and shorttitle = 'm.')
	insert into bcms_sales.Units (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, title, ShortTitle)
	values (1, 0, getdate(), 'admin', getdate(), 'admin', 'Metrai', 'm.')

-- products
if not exists (select 1 from bcms_sales.Products where isdeleted = 0 and name = N'Žalia medžiaga')
	insert into bcms_sales.Products (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, name, price, unitid)
	values (1, 0, getdate(), 'admin', getdate(), 'admin',  N'Žalia medžiaga', '0', 
		(select top 1 id from bcms_sales.Units where isdeleted = 0 and shorttitle = 'm.'))

if not exists (select 1 from bcms_sales.Products where isdeleted = 0 and name = N'Raudona medžiaga')
	insert into bcms_sales.Products (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, name, price, unitid)
	values (1, 0, getdate(), 'admin', getdate(), 'admin',  N'Raudona medžiaga', '0', 
		(select top 1 id from bcms_sales.Units where isdeleted = 0 and shorttitle = 'm.'))

if not exists (select 1 from bcms_sales.Products where isdeleted = 0 and name = N'Sagutės')
	insert into bcms_sales.Products (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, name, price, unitid)
	values (1, 0, getdate(), 'admin', getdate(), 'admin',  N'Sagutės', '0', 
		(select top 1 id from bcms_sales.Units where isdeleted = 0 and shorttitle = 'vnt.'))

-- Suppliers
if not exists (select 1 from bcms_sales.Partners where isdeleted = 0 and name = N'Gariūnščikai')
begin
	insert into bcms_sales.Partners (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, Name, Email, PhoneNumber)
	values (1, 0, getdate(), 'admin', getdate(), 'admin',  N'Gariūnščikai', 'gariunai@turgus.lt', '866677789')

	insert into bcms_sales.Suppliers (id) values ((select top 1 id from bcms_sales.Partners where isdeleted = 0 and name = N'Gariūnščikai'))
end

if not exists (select 1 from bcms_sales.Partners where isdeleted = 0 and name = N'Vietiniai audiniai')
begin
	insert into bcms_sales.Partners (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, Name, Email, PhoneNumber)
	values (1, 0, getdate(), 'admin', getdate(), 'admin',  N'Vietiniai audiniai', 'vietiniai@audiniai.lt', '899911223')

	insert into bcms_sales.Suppliers (id) values ((select top 1 id from bcms_sales.Partners where isdeleted = 0 and name = N'Vietiniai audiniai'))
end

-- Purchases
if not exists (select 1 from bcms_sales.Purchases where isdeleted = 0 and createdByuser = N'purchasecreator1')
begin
	insert into bcms_sales.Purchases (version, isdeleted, createdon, CreatedByUser, ModifiedOn, ModifiedByUser, SupplierId, [Status])
	values (1, 0, getdate(), 'purchasecreator1', getdate(), 'purchasecreator1',  (select top 1 id from bcms_sales.Partners where isdeleted = 0 and name = N'Vietiniai audiniai'), 1)
end

select * from bcms_sales.Purchases
select * from bcms_sales.PurchaseStatuses
