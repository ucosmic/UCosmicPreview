using System.Data.Entity;
using UCosmic.Domain.Files;

namespace UCosmic.Orm
{
    public static class FilesRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LooseFileOrm());
        }

        private class LooseFileOrm : RevisableEntityTypeConfiguration<LooseFile>
        {
            internal LooseFileOrm()
            {
                ToTable(typeof(LooseFile).Name, DbSchemaName.Files);
            }
        }
    }
}
