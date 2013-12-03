using FluentMigrator.VersionTableInfo;

namespace BetterCms.Module.Sales.Models.Migrations
{
    [VersionTableMetaData]
    public class NewsletterVersionTableMetaData : IVersionTableMetaData
    {
        public string SchemaName
        {
            get
            {
                return "bcms_" + SalesModuleDescriptor.ModuleName;
            }
        }

        public string TableName
        {
            get
            {
                return "VersionInfo";
            }
        }

        public string ColumnName
        {
            get
            {
                return "Version";
            }
        }

        public string UniqueIndexName
        {
            get
            {
                return "uc_VersionInfo_Version_" + SalesModuleDescriptor.ModuleName;
            }
        }
    }
}