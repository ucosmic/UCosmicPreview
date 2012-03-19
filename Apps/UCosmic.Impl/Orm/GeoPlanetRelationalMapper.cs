using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Places;

namespace UCosmic.Orm
{
    public static class GeoPlanetRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GeoPlanetPlaceOrm());
            modelBuilder.Configurations.Add(new GeoPlanetPlaceNodeOrm());
            modelBuilder.Configurations.Add(new GeoPlanetPlaceTypeOrm());
            modelBuilder.Configurations.Add(new GeoPlanetPlaceBelongToOrm());
        }

        private class GeoPlanetPlaceTypeOrm : EntityTypeConfiguration<GeoPlanetPlaceType>
        {
            internal GeoPlanetPlaceTypeOrm()
            {
                ToTable(typeof(GeoPlanetPlaceType).Name, DbSchemaName.Places);

                HasKey(p => p.Code);
                Property(p => p.Code) // do not generate type code
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                // map properties
                Property(p => p.Uri).IsUnicode(false).IsRequired().HasMaxLength(200);
                Property(p => p.EnglishName).IsRequired().HasMaxLength(100);
                Property(p => p.EnglishDescription).IsRequired().HasMaxLength(500);
            }
        }

        private class GeoPlanetPlaceOrm : EntityTypeConfiguration<GeoPlanetPlace>
        {
            internal GeoPlanetPlaceOrm()
            {
                ToTable(typeof(GeoPlanetPlace).Name, DbSchemaName.Places);

                HasKey(p => p.WoeId);
                Property(p => p.WoeId) // do not generate woe id
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                // map ascii && fixed length properties
                Property(p => p.Uri).IsUnicode(false);

                // GeoPlanetPlace * <---> 1 GeoPlanetPlaceType
                HasRequired(d => d.Type)
                    .WithMany()
                    .Map(x => x.MapKey("TypeCode"))
                    .WillCascadeOnDelete();

                // GeoPlanetParentPlace 0..1 <---> * GeoPlanetChildPlace
                HasOptional(d => d.Parent)
                    .WithMany(p => p.Children)
                    .Map(x => x.MapKey("ParentWoeId"))
                    .WillCascadeOnDelete(false);

                // GeoPlanetPlace 0..1 <---> 0..1 Place
                HasOptional(d => d.Place)
                    .WithOptionalDependent(p => p.GeoPlanetPlace)
                    .Map(x => x.MapKey("PlaceId"))
                    .WillCascadeOnDelete(false);

                Property(p => p.EnglishName).IsRequired().HasMaxLength(200);
                Property(p => p.Uri).IsRequired().HasMaxLength(200);
                Property(p => p.Postal).HasMaxLength(50);

                // name complex type properties
                Property(p => p.Center.Latitude).HasColumnName("Latitude");
                Property(p => p.Center.Longitude).HasColumnName("Longitude");
                Property(p => p.BoundingBox.Northeast.Latitude).HasColumnName("NorthLatitude");
                Property(p => p.BoundingBox.Northeast.Longitude).HasColumnName("EastLongitude");
                Property(p => p.BoundingBox.Southwest.Latitude).HasColumnName("SouthLatitude");
                Property(p => p.BoundingBox.Southwest.Longitude).HasColumnName("WestLongitude");
                Property(p => p.Country.Code).HasColumnName("CountryCode").HasMaxLength(10);
                Property(p => p.Country.TypeName).HasColumnName("CountryType").HasMaxLength(50);
                Property(p => p.Country.Name).HasColumnName("CountryName").HasMaxLength(200);
                Property(p => p.Admin1.Code).HasColumnName("Admin1Code").HasMaxLength(10);
                Property(p => p.Admin1.TypeName).HasColumnName("Admin1Type").HasMaxLength(50);
                Property(p => p.Admin1.Name).HasColumnName("Admin1Name").HasMaxLength(200);
                Property(p => p.Admin2.Code).HasColumnName("Admin2Code").HasMaxLength(10);
                Property(p => p.Admin2.TypeName).HasColumnName("Admin2Type").HasMaxLength(50);
                Property(p => p.Admin2.Name).HasColumnName("Admin2Name").HasMaxLength(200);
                Property(p => p.Admin3.Code).HasColumnName("Admin3Code").HasMaxLength(10);
                Property(p => p.Admin3.TypeName).HasColumnName("Admin3Type").HasMaxLength(50);
                Property(p => p.Admin3.Name).HasColumnName("Admin3Name").HasMaxLength(200);
                Property(p => p.Locality1.Name).HasColumnName("Locality1Name").HasMaxLength(200);
                Property(p => p.Locality1.TypeName).HasColumnName("Locality1Type").HasMaxLength(50);
                Property(p => p.Locality2.Name).HasColumnName("Locality2Name").HasMaxLength(200);
                Property(p => p.Locality2.TypeName).HasColumnName("Locality2Type").HasMaxLength(50);
            }
        }

        private class GeoPlanetPlaceNodeOrm : EntityTypeConfiguration<GeoPlanetPlaceNode>
        {
            internal GeoPlanetPlaceNodeOrm()
            {
                ToTable(typeof(GeoPlanetPlaceNode).Name, DbSchemaName.Places);

                HasKey(p => new { p.AncestorId, p.OffspringId });

                // Ancestor * <---> * Offspring
                HasRequired(d => d.Ancestor)
                    .WithMany(p => p.Offspring)
                    .HasForeignKey(d => d.AncestorId)
                    .WillCascadeOnDelete(false);
                HasRequired(d => d.Offspring)
                    .WithMany(p => p.Ancestors)
                    .HasForeignKey(d => d.OffspringId)
                    .WillCascadeOnDelete(false);
            }
        }

        private class GeoPlanetPlaceBelongToOrm : EntityTypeConfiguration<GeoPlanetPlaceBelongTo>
        {
            internal GeoPlanetPlaceBelongToOrm()
            {
                ToTable(typeof(GeoPlanetPlaceBelongTo).Name, DbSchemaName.Places);

                HasKey(p => new { p.PlaceWoeId, p.Rank });

                // GeoPlanetPlace * <---> * BelongsToAnotherGeoPlanetPlace
                HasRequired(d => d.GeoPlanetPlace)
                    .WithMany(p => p.BelongTos)
                    .HasForeignKey(d => d.PlaceWoeId)
                    .WillCascadeOnDelete(false);
                HasRequired(d => d.BelongsTo)
                    .WithMany()
                    .HasForeignKey(d => d.BelongToWoeId)
                    .WillCascadeOnDelete(false);
            }
        }
    }
}
