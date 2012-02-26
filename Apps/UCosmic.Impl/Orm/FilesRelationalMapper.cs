using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Files;

namespace UCosmic.Orm
{
    public static class FilesRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RevisableFileOrm());
        }

        private class RevisableFileOrm : EntityTypeConfiguration<LooseFile>
        {
            internal RevisableFileOrm()
            {
                ToTable(typeof(LooseFile).Name, DbSchemaName.Files);
            }
        }
    }
}
