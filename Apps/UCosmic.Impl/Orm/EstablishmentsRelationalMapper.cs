using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;

namespace UCosmic.Impl.Orm
{
    public static class EstablishmentsRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EstablishmentOrm());
            modelBuilder.Configurations.Add(new EstablishmentSamlSignOnOrm());
            modelBuilder.Configurations.Add(new EstablishmentLocationOrm());
            modelBuilder.Configurations.Add(new EstablishmentAddressOrm());
            modelBuilder.Configurations.Add(new EstablishmentNodeOrm());
            modelBuilder.Configurations.Add(new EstablishmentTypeOrm());
            modelBuilder.Configurations.Add(new EstablishmentCategoryOrm());
            modelBuilder.Configurations.Add(new EstablishmentEmailDomainOrm());
            modelBuilder.Configurations.Add(new EstablishmentNameOrm());
            modelBuilder.Configurations.Add(new EstablishmentUrlOrm());
            modelBuilder.Configurations.Add(new EmailTemplateOrm());
        }

        private class EstablishmentOrm : RevisableEntityTypeConfiguration<Establishment>
        {
            internal EstablishmentOrm()
            {
                ToTable(typeof(Establishment).Name, DbSchemaName.Establishments);

                // ParentEstablishment 0..1 <---> * ChildEstablishment
                HasOptional(d => d.Parent)
                    .WithMany(p => p.Children)
                    .Map(d => d.MapKey("ParentId"))
                    .WillCascadeOnDelete(false); // do not delete establishment if parent is deleted

                // Establishment * <---> 1 EstablishmentType
                HasRequired(d => d.Type)
                    .WithMany()
                    .Map(d => d.MapKey("TypeId"))
                    .WillCascadeOnDelete(false); // do not delete establishment if type is deleted

                // has many alternate names
                HasMany(p => p.Names)
                    .WithRequired(d => d.ForEstablishment)
                    .Map(d => d.MapKey("ForEstablishmentId"))
                    .WillCascadeOnDelete(true);

                // Establishment 1 <---> * EstablishmentUrl
                HasMany(p => p.Urls)
                    .WithRequired(d => d.ForEstablishment)
                    .Map(d => d.MapKey("ForEstablishmentId"))
                    .WillCascadeOnDelete(true);

                // has many email domains
                HasMany(p => p.EmailDomains)
                    .WithRequired(d => d.Establishment)
                    .HasForeignKey(d => d.EstablishmentId)
                    .WillCascadeOnDelete(true);

                // has many affiliates
                HasMany(p => p.Affiliates)
                    .WithRequired(d => d.Establishment)
                    .HasForeignKey(d => d.EstablishmentId)
                    .WillCascadeOnDelete(true);

                // has many ancestors
                HasMany(p => p.Ancestors)
                    .WithRequired(d => d.Offspring)
                    .HasForeignKey(d => d.OffspringId)
                    .WillCascadeOnDelete(false);

                // has many offspring
                HasMany(p => p.Offspring)
                    .WithRequired(d => d.Ancestor)
                    .HasForeignKey(d => d.AncestorId)
                    .WillCascadeOnDelete(false);

                // Establishment 1 <---> 1 EstablishmentLocation
                HasRequired(p => p.Location)
                    .WithRequiredPrincipal(d => d.ForEstablishment);

                // Establishment 1 <---> 0..1 EstablishmentSamlSignOn
                HasOptional(p => p.SamlSignOn)
                    .WithRequired();

                Property(p => p.OfficialName).IsRequired().HasMaxLength(400);
                Property(p => p.WebsiteUrl).HasMaxLength(200);

                // name complex type properties
                Property(p => p.PublicContactInfo.Phone).HasMaxLength(50).HasColumnName("PublicPhone");
                Property(p => p.PublicContactInfo.Fax).HasMaxLength(50).HasColumnName("PublicFax");
                Property(p => p.PublicContactInfo.Email).HasMaxLength(256).HasColumnName("PublicEmail");

                Property(p => p.PartnerContactInfo.Phone).HasMaxLength(50).HasColumnName("PartnerPhone");
                Property(p => p.PartnerContactInfo.Fax).HasMaxLength(50).HasColumnName("PartnerFax");
                Property(p => p.PartnerContactInfo.Email).HasMaxLength(256).HasColumnName("PartnerEmail");

                Property(p => p.InstitutionInfo.UCosmicCode).HasColumnName("UCosmicCode")
                    .IsFixedLength().HasMaxLength(6).IsUnicode(false);
                Property(p => p.InstitutionInfo.CollegeBoardDesignatedIndicator)
                    .HasColumnName("CollegeBoardDesignatedIndicator").IsFixedLength().HasMaxLength(6).IsUnicode(false);
            }
        }

        private class EstablishmentLocationOrm : RevisableEntityTypeConfiguration<EstablishmentLocation>
        {
            internal EstablishmentLocationOrm()
            {
                ToTable(typeof(EstablishmentLocation).Name, DbSchemaName.Establishments);

                // EstablishmentLocation * <---> * Place
                HasMany(p => p.Places)
                    .WithMany()
                    .Map(x => x
                        .MapLeftKey("EstablishmentLocationId")
                        .MapRightKey("PlaceId")
                        .ToTable("EstablishmentLocationInPlace", DbSchemaName.Establishments));

                // EstablishmentLocation 1 <---> * EstablishmentAddress
                // EstablishmentLocation 1 <---> 1 NativeAddress
                // EstablishmentLocation 1 <---> 0..1 TranslatedAddress
                HasMany(p => p.Addresses)
                    .WithRequired()
                    .Map(x => x.MapKey("EstablishmentLocationId"));

                Property(p => p.Center.Latitude).HasColumnName("CenterLatitude");
                Property(p => p.Center.Longitude).HasColumnName("CenterLongitude");
                Property(p => p.BoundingBox.Northeast.Latitude).HasColumnName("BoundingBoxNorthLatitude");
                Property(p => p.BoundingBox.Northeast.Longitude).HasColumnName("BoundingBoxEastLongitude");
                Property(p => p.BoundingBox.Southwest.Latitude).HasColumnName("BoundingBoxSouthLatitude");
                Property(p => p.BoundingBox.Southwest.Longitude).HasColumnName("BoundingBoxWestLongitude");
            }
        }

        private class EstablishmentSamlSignOnOrm : EntityTypeConfiguration<EstablishmentSamlSignOn>
        {
            internal EstablishmentSamlSignOnOrm()
            {
                ToTable(typeof(EstablishmentSamlSignOn).Name, DbSchemaName.Establishments);

                Property(p => p.EntityId).IsRequired().HasMaxLength(2048);
                Property(p => p.MetadataUrl).IsRequired().HasMaxLength(2048);
                Property(p => p.MetadataXml).HasColumnType("ntext");
                Property(p => p.SsoLocation).HasMaxLength(2048);
                Property(p => p.SsoBinding).HasMaxLength(50);
            }
        }

        private class EstablishmentAddressOrm : RevisableEntityTypeConfiguration<EstablishmentAddress>
        {
            internal EstablishmentAddressOrm()
            {
                ToTable(typeof(EstablishmentAddress).Name, DbSchemaName.Establishments);

                HasRequired(d => d.TranslationToLanguage)
                    .WithMany()
                    .Map(d => d.MapKey("TranslationToLanguageId"));

                Property(e => e.Text).IsRequired().HasMaxLength(500);
            }
        }

        private class EstablishmentNodeOrm : EntityTypeConfiguration<EstablishmentNode>
        {
            internal EstablishmentNodeOrm()
            {
                ToTable(typeof(EstablishmentNode).Name, DbSchemaName.Establishments);

                HasKey(p => new { p.AncestorId, p.OffspringId });
            }
        }

        private class EstablishmentTypeOrm : RevisableEntityTypeConfiguration<EstablishmentType>
        {
            internal EstablishmentTypeOrm()
            {
                ToTable(typeof(EstablishmentType).Name, DbSchemaName.Establishments);

                // has one category
                HasRequired(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .WillCascadeOnDelete(false); // do not delete type if category is deleted

                Property(p => p.EnglishName).IsRequired().HasMaxLength(150);
                Property(p => p.EnglishPluralName).HasMaxLength(150);
            }
        }

        private class EstablishmentCategoryOrm : RevisableEntityTypeConfiguration<EstablishmentCategory>
        {
            internal EstablishmentCategoryOrm()
            {
                ToTable(typeof(EstablishmentCategory).Name, DbSchemaName.Establishments);

                Property(c => c.EnglishName).IsRequired().HasMaxLength(150);
                Property(c => c.EnglishPluralName).HasMaxLength(150);
                Property(c => c.Code).HasColumnType("char").HasMaxLength(4);
            }
        }

        private class EstablishmentEmailDomainOrm : RevisableEntityTypeConfiguration<EstablishmentEmailDomain>
        {
            internal EstablishmentEmailDomainOrm()
            {
                ToTable(typeof(EstablishmentEmailDomain).Name, DbSchemaName.Establishments);

                Property(p => p.Value).IsRequired().HasMaxLength(256);
            }
        }

        private class EstablishmentNameOrm : RevisableEntityTypeConfiguration<EstablishmentName>
        {
            internal EstablishmentNameOrm()
            {
                ToTable(typeof(EstablishmentName).Name, DbSchemaName.Establishments);

                HasOptional(d => d.TranslationToLanguage)
                    .WithMany()
                    .Map(d => d.MapKey("TranslationToLanguageId"));

                Property(p => p.TranslationToHint).HasMaxLength(15);
                Property(p => p.Text).IsRequired().HasMaxLength(400);
                Property(p => p.AsciiEquivalent).HasMaxLength(400);
            }
        }

        private class EstablishmentUrlOrm : RevisableEntityTypeConfiguration<EstablishmentUrl>
        {
            internal EstablishmentUrlOrm()
            {
                ToTable(typeof(EstablishmentUrl).Name, DbSchemaName.Establishments);

                Property(p => p.Value).IsRequired().HasMaxLength(200);
            }
        }

        private class EmailTemplateOrm : RevisableEntityTypeConfiguration<EmailTemplate>
        {
            internal EmailTemplateOrm()
            {
                ToTable(typeof(EmailTemplate).Name, DbSchemaName.Establishments);

                // may have an establishment
                HasOptional(d => d.Establishment)
                    .WithMany()
                    .HasForeignKey(d => d.EstablishmentId)
                    .WillCascadeOnDelete(true);

                Property(t => t.Name).IsRequired().HasMaxLength(150);
                Property(t => t.SubjectFormat).IsRequired().HasMaxLength(250);
                Property(t => t.FromAddress).HasMaxLength(256);
                Property(t => t.FromDisplayName).HasMaxLength(150);
                Property(t => t.ReplyToAddress).HasMaxLength(256);
                Property(t => t.ReplyToDisplayName).HasMaxLength(150);
                Property(t => t.BodyFormat).IsRequired();
                Property(t => t.Instructions).HasColumnType("ntext");
                Property(t => t.BodyFormat).HasColumnType("ntext");
            }
        }
    }
}
