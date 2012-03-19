using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Places;

namespace UCosmic.Orm
{
    public static class GeoNamesRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GeoNamesTimeZoneOrm());
            modelBuilder.Configurations.Add(new GeoNamesFeatureClassOrm());
            modelBuilder.Configurations.Add(new GeoNamesFeatureOrm());
            modelBuilder.Configurations.Add(new GeoNamesToponymOrm());
            modelBuilder.Configurations.Add(new GeoNamesCountryOrm());
            modelBuilder.Configurations.Add(new GeoNamesToponymNodeOrm());
            modelBuilder.Configurations.Add(new GeoNamesAlternateNameOrm());
        }

        private class GeoNamesTimeZoneOrm : EntityTypeConfiguration<GeoNamesTimeZone>
        {
            internal GeoNamesTimeZoneOrm()
            {
                ToTable(typeof(GeoNamesTimeZone).Name, DbSchemaName.Places);

                HasKey(p => p.Id);

                Property(p => p.Id).HasMaxLength(50);
                Property(p => p.Id).IsUnicode(false);
            }
        }

        private class GeoNamesFeatureClassOrm : EntityTypeConfiguration<GeoNamesFeatureClass>
        {
            internal GeoNamesFeatureClassOrm()
            {
                ToTable(typeof(GeoNamesFeatureClass).Name, DbSchemaName.Places);

                // map primary key as char
                HasKey(p => p.Code);
                Property(p => p.Code).HasMaxLength(1).IsUnicode(false).IsFixedLength();

                Property(p => p.Name).IsRequired().HasMaxLength(200);
            }
        }

        private class GeoNamesFeatureOrm : EntityTypeConfiguration<GeoNamesFeature>
        {
            internal GeoNamesFeatureOrm()
            {
                ToTable(typeof(GeoNamesFeature).Name, DbSchemaName.Places);

                // map primary key as varchar
                HasKey(p => p.Code);
                Property(p => p.Code).IsUnicode(false).HasMaxLength(5);

                // Feature * <---> 1 FeatureClass
                HasRequired(d => d.Class)
                    .WithMany()
                    .HasForeignKey(d => d.ClassCode)
                    .WillCascadeOnDelete(false);

                Property(e => e.ClassCode).IsRequired();
                Property(e => e.Name).IsRequired().HasMaxLength(200);
            }
        }

        private class GeoNamesToponymOrm : EntityTypeConfiguration<GeoNamesToponym>
        {
            internal GeoNamesToponymOrm()
            {
                ToTable(typeof(GeoNamesToponym).Name, DbSchemaName.Places);

                HasKey(p => p.GeoNameId);
                Property(p => p.GeoNameId) // do not generate geoname id
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                Property(p => p.Name).IsRequired().HasMaxLength(200);
                Property(p => p.ToponymName).IsRequired().HasMaxLength(200);

                // map ascii && fixed length properties
                Property(p => p.ContinentCode).IsUnicode(false).IsFixedLength().HasMaxLength(2);
                Property(p => p.CountryCode).IsUnicode(false).IsFixedLength().HasMaxLength(2);
                Property(p => p.CountryName).HasMaxLength(200);
                Property(p => p.Admin1Code).IsUnicode(false).HasMaxLength(15);
                Property(p => p.Admin1Name).HasMaxLength(200);
                Property(p => p.Admin2Code).IsUnicode(false).HasMaxLength(15);
                Property(p => p.Admin2Name).HasMaxLength(200);
                Property(p => p.Admin3Code).IsUnicode(false).HasMaxLength(15);
                Property(p => p.Admin3Name).HasMaxLength(200);
                Property(p => p.Admin4Code).IsUnicode(false).HasMaxLength(15);
                Property(p => p.Admin4Name).HasMaxLength(200);

                // Toponym * <---> 0..1 TimeZone
                HasOptional(d => d.TimeZone)
                    .WithMany()
                    .HasForeignKey(d => d.TimeZoneId)
                    .WillCascadeOnDelete(false);

                // Toponym * <---> 1 Feature
                HasRequired(d => d.Feature)
                    .WithMany()
                    .HasForeignKey(d => d.FeatureCode)
                    .WillCascadeOnDelete(false);

                // Toponym 0..1 <---> 0..1 Place
                HasOptional(p => p.Place)
                    .WithOptionalDependent(d => d.GeoNamesToponym)
                    .Map(x => x.MapKey("PlaceId"))
                    .WillCascadeOnDelete(false);

                // ParentToponym  0..1 <---> * ChildToponym
                HasOptional(d => d.Parent)
                    .WithMany(p => p.Children)
                    .Map(x => x.MapKey("ParentGeoNameId"))
                    .WillCascadeOnDelete(false);

                // name complex type properties
                Property(p => p.Center.Latitude).HasColumnName("Latitude");
                Property(p => p.Center.Longitude).HasColumnName("Longitude");
            }
        }

        private class GeoNamesCountryOrm : EntityTypeConfiguration<GeoNamesCountry>
        {
            internal GeoNamesCountryOrm()
            {
                ToTable(typeof(GeoNamesCountry).Name, DbSchemaName.Places);

                HasKey(p => p.GeoNameId);
                Property(p => p.GeoNameId) // do not generate geoname id
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                // map ascii && fixed length properties
                Property(p => p.ContinentCode).IsUnicode(false).IsFixedLength();
                Property(p => p.Code).IsUnicode(false).IsFixedLength().IsRequired().HasMaxLength(2);
                Property(p => p.Name).IsRequired().HasMaxLength(200);
                Property(p => p.ContinentCode).IsRequired().HasMaxLength(2);
                Property(p => p.ContinentName).IsRequired().HasMaxLength(200);
                Property(p => p.IsoNumericCode).IsRequired();
                Property(p => p.IsoAlpha3Code).IsUnicode(false).IsFixedLength().IsRequired().HasMaxLength(3);
                Property(p => p.FipsCode).IsUnicode(false).IsFixedLength().HasMaxLength(2);
                Property(p => p.AreaInSqKm).IsUnicode(false).HasMaxLength(15);
                Property(p => p.CapitalCityName).HasMaxLength(200);
                Property(p => p.CurrencyCode).IsUnicode(false).IsFixedLength().HasMaxLength(3);
                Property(p => p.Languages).HasMaxLength(150);

                // rename complex type properties
                Property(p => p.BoundingBox.Northeast.Latitude).HasColumnName("NorthLatitude");
                Property(p => p.BoundingBox.Northeast.Longitude).HasColumnName("EastLongitude");
                Property(p => p.BoundingBox.Southwest.Latitude).HasColumnName("SouthLatitude");
                Property(p => p.BoundingBox.Southwest.Longitude).HasColumnName("WestLongitude");

                // Country 0..1 <---> 1 Toponym
                HasRequired(d => d.AsToponym)
                    .WithOptional(p => p.AsCountry)
                    .WillCascadeOnDelete();
            }
        }

        private class GeoNamesToponymNodeOrm : EntityTypeConfiguration<GeoNamesToponymNode>
        {
            internal GeoNamesToponymNodeOrm()
            {
                ToTable(typeof(GeoNamesToponymNode).Name, DbSchemaName.Places);

                HasKey(p => new { p.AncestorId, p.OffspringId });

                // AncestorToponym * <---> * OffspringToponym
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

        private class GeoNamesAlternateNameOrm : EntityTypeConfiguration<GeoNamesAlternateName>
        {
            internal GeoNamesAlternateNameOrm()
            {
                ToTable(typeof(GeoNamesAlternateName).Name, DbSchemaName.Places);

                HasKey(p => p.AlternateNameId);

                // (AlternateName) * <---> 1 (Toponym)
                HasRequired(d => d.Toponym)
                    .WithMany(p => p.AlternateNames)
                    .HasForeignKey(d => d.GeoNameId)
                    .WillCascadeOnDelete();

                Property(p => p.Language).HasMaxLength(10);
                Property(p => p.Name).HasMaxLength(200).IsRequired();
            }
        }
    }
}
