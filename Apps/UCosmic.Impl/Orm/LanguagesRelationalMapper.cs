using System.Data.Entity;
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

        private class LanguageOrm : RevisableEntityTypeConfiguration<Language>
        {
            internal LanguageOrm()
            {
                ToTable(typeof(Language).Name, DbSchemaName.Languages);

                Property(p => p.TwoLetterIsoCode).IsRequired().IsFixedLength().HasMaxLength(2);
                Property(p => p.ThreeLetterIsoCode).IsRequired().IsFixedLength().HasMaxLength(3);
                Property(p => p.ThreeLetterIsoBibliographicCode).IsRequired().IsFixedLength().HasMaxLength(3);

                Property(p => p.TwoLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoBibliographicCode).IsUnicode(false);
            }
        }

        private class LanguageNameOrm : RevisableEntityTypeConfiguration<LanguageName>
        {
            internal LanguageNameOrm()
            {
                ToTable(typeof(LanguageName).Name, DbSchemaName.Languages);

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

                Property(p => p.Text).IsRequired().HasMaxLength(150);
                Property(p => p.AsciiEquivalent).IsUnicode(false).HasMaxLength(150);
            }
        }
    }
}
