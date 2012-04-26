using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UCosmic.Domain.Establishments;

namespace UCosmic.Impl.Seeders
{
    public abstract class BaseEstablishmentSeeder : UCosmicDbSeeder
    {
        protected Establishment EnsureEstablishment(string officialName, bool isMember,
            Establishment parent, EstablishmentType type, string websiteUrl, string emailDomains,
            IEnumerable<EstablishmentName> alternateNames = null)
        {
            var establishment = Context.Establishments.Include(e => e.Location).ByOfficialName(officialName);
            if (!string.IsNullOrWhiteSpace(websiteUrl))
            {
                establishment =
                    Context.Establishments.SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals(websiteUrl));
            }
            if (establishment != null) return establishment;

            establishment = new Establishment
            {
                OfficialName = officialName,
                Parent = parent,
                WebsiteUrl = websiteUrl,
                IsMember = isMember,
                Type = type,
                Location = new EstablishmentLocation
                {
                    Addresses = new List<EstablishmentAddress>()
                },
            };
            establishment.EmailDomains = establishment.EmailDomains ?? new List<EstablishmentEmailDomain>();
            if (!string.IsNullOrWhiteSpace(emailDomains))
            {
                foreach (var emailDomain in emailDomains.Explode(";"))
                {
                    establishment.EmailDomains.Add(new EstablishmentEmailDomain { Value = emailDomain, });
                }
            }
            establishment.Names = establishment.Names ?? new List<EstablishmentName>();
            if (alternateNames != null)
            {
                foreach (var name in alternateNames)
                {
                    establishment.Names.Add(name);
                }
            }
            establishment.Urls = establishment.Urls ?? new List<EstablishmentUrl>();
            Context.Establishments.Add(establishment);
            Context.SaveChanges();
            return establishment;
        }

        #region Establishment types & categories

        private EstablishmentType GetEstablishmentType(string englishName,
            string englishPluralName, string categoryCode)
        {
            var type = Context.Establishments.Select(e => e.Type).Distinct()
                           .ByEnglishNameAndCategoryCode(englishName, categoryCode)
                       ?? new EstablishmentType
                          {
                              EnglishName = englishName,
                              EnglishPluralName = englishPluralName,
                              Category = GetEstablishmentCategory(categoryCode),
                          };
            return type;
        }

        private EstablishmentCategory GetEstablishmentCategory(string code)
        {
            var category = Context.Establishments.Select(e => e.Type.Category).Distinct().ByCode(code);
            if (category == null)
            {
                category = new EstablishmentCategory { Code = code.ToUpper() };
                switch (code)
                {
                    case EstablishmentCategoryCode.Inst:
                        category.EnglishName = "Institution";
                        category.EnglishPluralName = "Institutions";
                        break;
                    case EstablishmentCategoryCode.Corp:
                        category.EnglishName = "Business or Corporation";
                        category.EnglishPluralName = "Businesses & Corporations";
                        break;
                    case EstablishmentCategoryCode.Govt:
                        category.EnglishName = "Government";
                        category.EnglishPluralName = "Government";
                        break;
                }
            }
            return category;
        }

        protected EstablishmentType GetUniversitySystem()
        {
            return GetEstablishmentType("University System", "University Systems", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetUniversityCampus()
        {
            return GetEstablishmentType("University Campus", "University Campuses", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetUniversity()
        {
            return GetEstablishmentType("University", "Universities", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetGovernmentAdministration()
        {
            return GetEstablishmentType("Government Administration", "Government Administrations", EstablishmentCategoryCode.Govt);
        }

        protected EstablishmentType GetCollege()
        {
            return GetEstablishmentType("College", "Colleges", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetCommunityCollege()
        {
            return GetEstablishmentType("Community College", "Community Colleges", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetAcademicProgram()
        {
            return GetEstablishmentType("Academic Program", "Academic Programs", EstablishmentCategoryCode.Inst);
        }

        protected EstablishmentType GetRecruitmentAgency()
        {
            return GetEstablishmentType("Recruitment Agency", "Recruitment Agencies", EstablishmentCategoryCode.Corp);
        }

        protected EstablishmentType GetGenericBusiness()
        {
            var corpCategory = GetEstablishmentCategory(EstablishmentCategoryCode.Corp);
            return GetEstablishmentType(corpCategory.EnglishName, corpCategory.EnglishPluralName, corpCategory.Code);
        }

        protected EstablishmentType GetAssociation()
        {
            var corpCategory = GetEstablishmentCategory(EstablishmentCategoryCode.Corp);
            return GetEstablishmentType("Association", "Associations", corpCategory.Code);
        }

        #endregion
    }
}