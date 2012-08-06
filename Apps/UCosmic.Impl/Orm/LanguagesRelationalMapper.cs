using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Languages;

namespace UCosmic.Impl.Orm
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
                HasKey(e => e.Id);

                Property(p => p.TwoLetterIsoCode).IsRequired().IsFixedLength().HasMaxLength(2);
                Property(p => p.ThreeLetterIsoCode).IsRequired().IsFixedLength().HasMaxLength(3);
                Property(p => p.ThreeLetterIsoBibliographicCode).IsRequired().IsFixedLength().HasMaxLength(3);

                Property(p => p.TwoLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoCode).IsUnicode(false);
                Property(p => p.ThreeLetterIsoBibliographicCode).IsUnicode(false);

                // Language (1) <-----> (0..*) LanguageName
                HasMany(p => p.Names)
                    .WithRequired()
                    .HasForeignKey(d => d.LanguageId)
                    .WillCascadeOnDelete(true)
                ;
            }
        }

        private class LanguageNameOrm : EntityTypeConfiguration<LanguageName>
        {
            internal LanguageNameOrm()
            {
                ToTable(typeof(LanguageName).Name, DbSchemaName.Languages);
                HasKey(x => new { x.LanguageId, x.Number });

                Property(p => p.Text).IsRequired().HasMaxLength(150);
                Property(p => p.AsciiEquivalent).IsUnicode(false).HasMaxLength(150);

                // LanguageName (0..*) <-----> (1) Language (name is a translation to a different language)
                HasRequired(d => d.TranslationToLanguage)
                    .WithMany()
                    .HasForeignKey(d => d.TranslationToLanguageId)
                    .WillCascadeOnDelete(false)
                ;
            }
        }
    }
}
