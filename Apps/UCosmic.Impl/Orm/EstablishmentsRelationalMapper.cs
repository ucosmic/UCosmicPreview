using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Email;
using UCosmic.Domain.Establishments;

namespace UCosmic.Orm
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

        private class EstablishmentOrm : EntityTypeConfiguration<Establishment>
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

                // name complex type properties
                Property(p => p.PublicContactInfo.Phone).HasColumnName("PublicPhone");
                Property(p => p.PublicContactInfo.Fax).HasColumnName("PublicFax");
                Property(p => p.PublicContactInfo.Email).HasColumnName("PublicEmail");
                Property(p => p.PartnerContactInfo.Phone).HasColumnName("PartnerPhone");
                Property(p => p.PartnerContactInfo.Fax).HasColumnName("PartnerFax");
                Property(p => p.PartnerContactInfo.Email).HasColumnName("PartnerEmail");
                Property(p => p.InstitutionInfo.UCosmicCode).HasColumnName("UCosmicCode");
                Property(p => p.InstitutionInfo.CollegeBoardDesignatedIndicator).HasColumnName("CollegeBoardDesignatedIndicator");
            }
        }

        private class EstablishmentLocationOrm : EntityTypeConfiguration<EstablishmentLocation>
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

                Property(p => p.MetadataXml).HasColumnType("ntext");
                //Property(p => p.SigningCertificate).HasColumnType("ntext");
                //Property(p => p.EncryptionCertificate).HasColumnType("ntext");
            }
        }

        private class EstablishmentAddressOrm : EntityTypeConfiguration<EstablishmentAddress>
        {
            internal EstablishmentAddressOrm()
            {
                ToTable(typeof(EstablishmentAddress).Name, DbSchemaName.Establishments);

                HasRequired(d => d.TranslationToLanguage)
                    .WithMany()
                    .Map(d => d.MapKey("TranslationToLanguageId"));
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

        private class EstablishmentTypeOrm : EntityTypeConfiguration<EstablishmentType>
        {
            internal EstablishmentTypeOrm()
            {
                ToTable(typeof(EstablishmentType).Name, DbSchemaName.Establishments);

                // has one category
                HasRequired(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .WillCascadeOnDelete(false); // do not delete type if category is deleted
            }
        }

        private class EstablishmentCategoryOrm : EntityTypeConfiguration<EstablishmentCategory>
        {
            internal EstablishmentCategoryOrm()
            {
                ToTable(typeof(EstablishmentCategory).Name, DbSchemaName.Establishments);

                Property(c => c.Code).HasColumnType("char");
            }
        }

        private class EstablishmentEmailDomainOrm : EntityTypeConfiguration<EstablishmentEmailDomain>
        {
            internal EstablishmentEmailDomainOrm()
            {
                ToTable(typeof(EstablishmentEmailDomain).Name, DbSchemaName.Establishments);
            }
        }

        private class EstablishmentNameOrm : EntityTypeConfiguration<EstablishmentName>
        {
            internal EstablishmentNameOrm()
            {
                ToTable(typeof(EstablishmentName).Name, DbSchemaName.Establishments);

                HasOptional(d => d.TranslationToLanguage)
                    .WithMany()
                    .Map(d => d.MapKey("TranslationToLanguageId"));
            }
        }

        private class EstablishmentUrlOrm : EntityTypeConfiguration<EstablishmentUrl>
        {
            internal EstablishmentUrlOrm()
            {
                ToTable(typeof(EstablishmentUrl).Name, DbSchemaName.Establishments);
            }
        }

        private class EmailTemplateOrm : EntityTypeConfiguration<EmailTemplate>
        {
            internal EmailTemplateOrm()
            {
                ToTable(typeof(EmailTemplate).Name, DbSchemaName.Establishments);

                // may have an establishment
                HasOptional(d => d.Establishment)
                    .WithMany()
                    .HasForeignKey(d => d.EstablishmentId)
                    .WillCascadeOnDelete(true);

                Property(t => t.Instructions).HasColumnType("ntext");
                Property(t => t.BodyFormat).HasColumnType("ntext");
            }
        }
    }
}
