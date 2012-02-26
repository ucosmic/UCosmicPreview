using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Languages;

namespace UCosmic.Orm
{
    public static class LanguagesRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LanguageOrm());
            modelBuilder.Configurations.Add(new LanguageNameOrm());
        }

        private class LanguageOrm : EntityTypeConfiguration<Language>
        {
            internal LanguageOrm()
            {
                ToTable(typeof(Language).Name, DbSchemaName.Languages);

                Property(p => p.TwoLetterIsoCode).IsFixedLength();
                Property(p => p.ThreeLetterIsoCode).IsFixedLength();
                Property(p => p.ThreeLetterIsoBibliographicCode).IsFixedLength();

                Property(p => p.TwoLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoBibliographicCode).IsUnicode(false);
            }
        }

        private class LanguageNameOrm : EntityTypeConfiguration<LanguageName>
        {
            internal LanguageNameOrm()
            {
                ToTable(typeof(LanguageName).Name, DbSchemaName.Languages);

                Property(p => p.AsciiEquivalent).IsUnicode(false);

                // has one language it is the name for
                HasRequired(d => d.NameForLanguage)
                    .WithMany(p => p.Names)
                    .Map(d => d.MapKey("NameForLanguageId"))
                    .WillCascadeOnDelete(true);

                // has one language it is the translation to
                HasRequired(d => d.TranslationToLanguage)
                    .WithMany()
                    .Map(d => d.MapKey("TranslationToLanguageId"))
                    .WillCascadeOnDelete(false);
            }
        }
    }
}
