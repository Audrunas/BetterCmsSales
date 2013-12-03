using System;

using BetterCms.Core.DataAccess.DataContext.Migrations;
using BetterCms.Core.Models;
using FluentMigrator;

namespace BetterCms.Module.Sales.Models.Migrations
{
    /// <summary>
    /// Module initial database structure creation.
    /// </summary>
    [Migration(201312010000)]
    public class InitialSetup : DefaultMigration
    {
        /// <summary>
        /// The root module schema name.
        /// </summary>
        private readonly string rootModuleSchemaName;

        /// <summary>
        /// The media manager schema name.
        /// </summary>
        private readonly string mediaManagerSchemaName;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialSetup"/> class.
        /// </summary>
        public InitialSetup()
            : base(SalesModuleDescriptor.ModuleName)
        {
            rootModuleSchemaName = (new Root.Models.Migrations.RootVersionTableMetaData()).SchemaName;
            mediaManagerSchemaName = (new MediaManager.Models.Migrations.MediaManagerVersionTableMetaData()).SchemaName;
        }

        public override void Up()
        {
            CreatePartnersTable();
            CreateBuyersTable();
            CreateSuppliersTable();

            CreateUnitsTable();
            CreateProductsTable();

            CreateSaleStatusesTable();
            CreatePurchaseStatusesTable();
            CreatePurchasesTable();
            CreatePurchaseProductsTable();
            CreateSalesTable();
            CreateSaleProductsTable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

        private void CreatePartnersTable()
        {
            Create
                .Table("Partners").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("Name").AsString(MaxLength.Name).NotNullable()
                .WithColumn("Email").AsString(MaxLength.Email).Nullable()
                .WithColumn("Address").AsString(MaxLength.Text).Nullable()
                .WithColumn("PhoneNumber").AsString(MaxLength.Name).Nullable();
        }

        private void CreateBuyersTable()
        {
            Create
                .Table("Buyers").InSchema(SchemaName)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey();

            Create
                .ForeignKey("FK_Cms_Buyers_Id_Partners_Id")
                .FromTable("Buyers").InSchema(SchemaName).ForeignColumn("Id")
                .ToTable("Partners").InSchema(SchemaName).PrimaryColumn("Id");
        }

        private void CreateSuppliersTable()
        {
            Create
                .Table("Suppliers").InSchema(SchemaName)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey();

            Create
                .ForeignKey("FK_Cms_Suppliers_Id_Partners_Id")
                .FromTable("Suppliers").InSchema(SchemaName).ForeignColumn("Id")
                .ToTable("Partners").InSchema(SchemaName).PrimaryColumn("Id");
        }

        private void CreateUnitsTable()
        {
            Create
                .Table("Units").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("Title").AsString(MaxLength.Name).NotNullable()
                .WithColumn("ShortTitle").AsString(MaxLength.Name).NotNullable();

            Create
                .UniqueConstraint("UX_Cms_Units_Title")
                .OnTable("Units").WithSchema(SchemaName)
                .Columns(new[] { "Title", "DeletedOn" });

            Create
                .UniqueConstraint("UX_Cms_Units_ShortTitle")
                .OnTable("Units").WithSchema(SchemaName)
                .Columns(new[] { "ShortTitle", "DeletedOn" });
        }

        private void CreateProductsTable()
        {
            Create
                .Table("Products").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("Name").AsString(MaxLength.Name).NotNullable()
                .WithColumn("Price").AsFloat().NotNullable()
                .WithColumn("CategoryId").AsGuid().Nullable()
                .WithColumn("MediaImageId").AsGuid().Nullable()
                .WithColumn("UnitId").AsGuid().NotNullable();

            Create
                .ForeignKey("FK_Cms_Products_CategoryId_Categories_Id")
                .FromTable("Products").InSchema(SchemaName).ForeignColumn("CategoryId")
                .ToTable("Categories").InSchema(rootModuleSchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_Products_MediaImageId_MediaImages_Id")
                .FromTable("Products").InSchema(SchemaName).ForeignColumn("MediaImageId")
                .ToTable("MediaImages").InSchema(mediaManagerSchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_Products_UnitId_Units_Id")
                .FromTable("Products").InSchema(SchemaName).ForeignColumn("UnitId")
                .ToTable("Units").InSchema(SchemaName).PrimaryColumn("Id");
        }

        private void CreateSaleStatusesTable()
        {
            Create
               .Table("SaleStatuses").InSchema(SchemaName)
               .WithColumn("Id").AsInt16().NotNullable().PrimaryKey()
               .WithColumn("Name").AsString(MaxLength.Name).NotNullable();
        }

        private void CreatePurchaseStatusesTable()
        {
            Create
               .Table("PurchaseStatuses").InSchema(SchemaName)
               .WithColumn("Id").AsInt16().NotNullable().PrimaryKey()
               .WithColumn("Name").AsString(MaxLength.Name).NotNullable();
        }

        private void CreatePurchasesTable()
        {
            Create
                .Table("Purchases").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("SupplierId").AsGuid().NotNullable()
                .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(PurchaseStatus.New);

            Create
                .ForeignKey("FK_Cms_Purchases_SupplierId_Suppliers_Id")
                .FromTable("Purchases").InSchema(SchemaName).ForeignColumn("SupplierId")
                .ToTable("Suppliers").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_Purchases_Status_PurchaseStatuses_Id")
                .FromTable("Purchases").InSchema(SchemaName).ForeignColumn("Status")
                .ToTable("PurchaseStatuses").InSchema(SchemaName).PrimaryColumn("Id");
        }

        private void CreateSalesTable()
        {
            Create
                .Table("Sales").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("BuyerId").AsGuid().NotNullable()
                .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(SaleStatus.New);

            Create
                .ForeignKey("FK_Cms_Sales_BuyerId_Buyers_Id")
                .FromTable("Sales").InSchema(SchemaName).ForeignColumn("BuyerId")
                .ToTable("Buyers").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_Sales_Status_SaleStatuses_Id")
                .FromTable("Sales").InSchema(SchemaName).ForeignColumn("Status")
                .ToTable("SaleStatuses").InSchema(SchemaName).PrimaryColumn("Id");
        }

        private void CreatePurchaseProductsTable()
        {
            Create
                .Table("PurchaseProducts").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("PurchaseId").AsGuid().NotNullable()
                .WithColumn("ProductId").AsGuid().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("Price").AsFloat().NotNullable();

            Create
                .ForeignKey("FK_Cms_PurchaseProducts_ProductId_Products_Id")
                .FromTable("PurchaseProducts").InSchema(SchemaName).ForeignColumn("ProductId")
                .ToTable("Products").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_PurchaseProducts_PurchaseId_Purchases_Id")
                .FromTable("PurchaseProducts").InSchema(SchemaName).ForeignColumn("PurchaseId")
                .ToTable("Purchases").InSchema(SchemaName).PrimaryColumn("Id");
        }
        
        private void CreateSaleProductsTable()
        {
            Create
                .Table("SaleProducts").InSchema(SchemaName)
                .WithCmsBaseColumns()

                .WithColumn("SaleId").AsGuid().NotNullable()
                .WithColumn("ProductId").AsGuid().NotNullable()
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("Price").AsFloat().NotNullable();

            Create
                .ForeignKey("FK_Cms_SaleProducts_ProductId_Products_Id")
                .FromTable("SaleProducts").InSchema(SchemaName).ForeignColumn("ProductId")
                .ToTable("Products").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_SaleProducts_SaleId_Sales_Id")
                .FromTable("SaleProducts").InSchema(SchemaName).ForeignColumn("SaleId")
                .ToTable("Sales").InSchema(SchemaName).PrimaryColumn("Id");
        }
    }
}