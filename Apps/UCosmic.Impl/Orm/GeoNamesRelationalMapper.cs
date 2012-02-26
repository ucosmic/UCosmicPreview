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

                Property(p => p.Id).IsUnicode(false);
            }
        }

        private class GeoNamesFeatureClassOrm : EntityTypeConfiguration<GeoNamesFeatureClass>
        {
            internal GeoNamesFeatureClassOrm()
            {
                ToTable(typeof(GeoNamesFeatureClass).Name, DbSchemaName.Places);

                // map primary key as char
                Property(p => p.Code).IsUnicode(false).IsFixedLength();
            }
        }

        private class GeoNamesFeatureOrm : EntityTypeConfiguration<GeoNamesFeature>
        {
            internal GeoNamesFeatureOrm()
            {
                ToTable(typeof(GeoNamesFeature).Name, DbSchemaName.Places);

                // map primary key as varchar
                Property(p => p.Code).IsUnicode(false);

                // Feature * <---> 1 FeatureClass
                HasRequired(d => d.Class)
                    .WithMany()
                    .HasForeignKey(d => d.ClassCode)
                    .WillCascadeOnDelete(false);
            }
        }

        private class GeoNamesToponymOrm : EntityTypeConfiguration<GeoNamesToponym>
        {
            internal GeoNamesToponymOrm()
            {
                ToTable(typeof(GeoNamesToponym).Name, DbSchemaName.Places);

                Property(p => p.GeoNameId) // do not generate geoname id
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                // map ascii && fixed length properties
                Property(p => p.ContinentCode).IsUnicode(false).IsFixedLength();
                Property(p => p.CountryCode).IsUnicode(false).IsFixedLength();
                Property(p => p.Admin1Code).IsUnicode(false);
                Property(p => p.Admin2Code).IsUnicode(false);
                Property(p => p.Admin3Code).IsUnicode(false);
                Property(p => p.Admin4Code).IsUnicode(false);

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

                Property(p => p.GeoNameId) // do not generate geoname id
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                // map ascii && fixed length properties
                Property(p => p.ContinentCode).IsUnicode(false).IsFixedLength();
                Property(p => p.Code).IsUnicode(false).IsFixedLength();
                Property(p => p.IsoAlpha3Code).IsUnicode(false).IsFixedLength();
                Property(p => p.FipsCode).IsUnicode(false).IsFixedLength();
                Property(p => p.AreaInSqKm).IsUnicode(false);
                Property(p => p.CurrencyCode).IsUnicode(false).IsFixedLength();

                // Country 0..1 <---> 1 Toponym
                HasRequired(d => d.AsToponym)
                    .WithOptional(p => p.AsCountry)
                    .WillCascadeOnDelete();

                // rename complex type properties
                Property(p => p.BoundingBox.Northeast.Latitude).HasColumnName("NorthLatitude");
                Property(p => p.BoundingBox.Northeast.Longitude).HasColumnName("EastLongitude");
                Property(p => p.BoundingBox.Southwest.Latitude).HasColumnName("SouthLatitude");
                Property(p => p.BoundingBox.Southwest.Longitude).HasColumnName("WestLongitude");
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

                // (AlternateName) * <---> 1 (Toponym)
                HasRequired(d => d.Toponym)
                    .WithMany(p => p.AlternateNames)
                    .HasForeignKey(d => d.GeoNameId)
                    .WillCascadeOnDelete();
            }
        }
    }
}
