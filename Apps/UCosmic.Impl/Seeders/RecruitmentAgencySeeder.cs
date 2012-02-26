using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using NGeo.Yahoo.PlaceFinder;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Orm;
using Place = UCosmic.Domain.Places.Place;

namespace UCosmic.Seeders
{
    // ReSharper disable UnusedMember.Global
    public class RecruitmentAgencySeeder : BaseEstablishmentSeeder
    // ReSharper restore UnusedMember.Global
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public override void Seed(UCosmicContext context)
        {
            if (WebConfig.IsDeployedToCloud) return;

            Context = context;

            var configurationManager = new DotNetConfigurationManager();
            var objectCommander = new ObjectCommander(context);
            var geoNames = new GeoNamesClient();
            var geoPlanet = new GeoPlanetClient();
            var placeFactory = new PlaceFactory(context, objectCommander, geoPlanet, geoNames, configurationManager);
            var placeFinderClient = DependencyInjector.Current.GetService<IConsumePlaceFinder>();
            double latitude;
            double longitude;
            Result result;
            Place place;
            List<Place> places;

            #region EduGlobal headquarters and branches

            var eduGlobalHeaqdquarters = Context.Establishments.SingleOrDefault(a =>
                a.WebsiteUrl == "www.eduglobalchina.com" && a.Parent == null
                && a.Type.EnglishName == "Recruitment Agency");
            if (eduGlobalHeaqdquarters == null)
            {
                #region EduGlobal Beijing (HQ)

                latitude = 39.898716;
                longitude = 116.417877;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                eduGlobalHeaqdquarters = new Establishment
                {
                    Type = GetRecruitmentAgency(),
                    OfficialName = "EduGlobal Beijing",
                    WebsiteUrl = "www.eduglobalchina.com",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "7F North Office Tower, Beijing New World Centre\r\n3B Chongwenmenwai St\r\n100062 Beijing\r\nPR China",
                            },
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "zh"),
                                Text = "中国北京崇文区崇文门外大街3号B\r\n北京新世界中心写字楼B座7层\r\n邮编：100062",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (10) 6708 0808",
                        Fax = "+86 (10) 6708 2541",
                        Email = "infobeijing@eduglobal.com",
                    },
                };
                Context.Establishments.Add(eduGlobalHeaqdquarters);
                Context.SaveChanges();

                #endregion
                #region EduGlobal Changchun (Branch)

                latitude = 43.891129;
                longitude = 125.310471;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                var eduGlobalChangchun = new Establishment
                {
                    Parent = eduGlobalHeaqdquarters,
                    Type = GetRecruitmentAgency(),
                    OfficialName = "EduGlobal Changchun",
                    WebsiteUrl = "www.eduglobalchina.com",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "Songhuajiang University,\r\nNo.758 Qianjin Street, Changchun City,\r\nJilin Province\r\n130000, P.R.China",
                            },
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "zh"),
                                Text = "吉林省长春市前进大街758号松花江大学\r\n邮编：130000",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 431 85111566",
                        Fax = "+86 431-85111566",
                        Email = "lina.wang@eduglobal.com",
                    },
                };
                Context.Establishments.Add(eduGlobalChangchun);

                #endregion

                Context.SaveChanges();
            }

            #endregion
            #region EIC headquarters and branches

            var eicHeaqdquarters = Context.Establishments.SingleOrDefault(a =>
                a.WebsiteUrl == "www.eic.org.cn" && a.Parent == null
                && a.Type.EnglishName == "Recruitment Agency");
            if (eicHeaqdquarters == null)
            {
                #region EIC Beijing (HQ)

                latitude = 39.905983;
                longitude = 116.459373;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                eicHeaqdquarters = new Establishment
                {
                    Type = GetRecruitmentAgency(),
                    OfficialName = "EIC Group Beijing",
                    WebsiteUrl = "www.eic.org.cn",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "Room 1203, Block A, Jianwai SOHO\r\n39 East 3rd Ring Road\r\nChaoyang District, Beijing\r\nChina  100022",
                            },
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "zh"),
                                Text = "北京市朝阳区东三环中路39号建外SOHO A座\r\n12,15层国贸办公区",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (10) 5878 1616",
                        Fax = "+86 (10) 5869 4393",
                        Email = "beijing@eic.org.cn",
                    },
                };
                Context.Establishments.Add(eicHeaqdquarters);

                #endregion
                #region EIC Changsha (Branch)

                latitude = 28.194132;
                longitude = 112.976715;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                var eicChangsha = new Establishment
                {
                    Parent = eicHeaqdquarters,
                    Type = GetRecruitmentAgency(),
                    OfficialName = "EIC Group Changsha",
                    WebsiteUrl = "www.eic.org.cn",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "Floor 24, Pinghetang Business Mansion\r\nNo. 88 Huangxing Middle Road\r\nChangsha City, Hunan Province\r\nChina",
                            },
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "zh"),
                                Text = "长沙市黄兴中路88号平和堂商务楼24楼启德教育\r\n中心",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (731) 8448 8495",
                        Fax = "+86 (731) 8448 3835",
                        Email = "changsha@eic.org.cn",
                    },
                };
                Context.Establishments.Add(eicChangsha);

                #endregion

                Context.SaveChanges();
            }

            #endregion
            #region Can-Achieve headquarters and branches

            var canachieveHeadquarters = Context.Establishments.SingleOrDefault(a =>
                a.WebsiteUrl == "www.can-achieve.com.cn" && a.Parent == null
                && a.Type.EnglishName == "Recruitment Agency");
            if (canachieveHeadquarters == null)
            {
                #region Can-Achieve Beijing (HQ)

                latitude = 39.905605;
                longitude = 116.459831;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                canachieveHeadquarters = new Establishment
                {
                    Type = GetRecruitmentAgency(),
                    OfficialName = "Can Achieve Group Beijing",
                    WebsiteUrl = "www.can-achieve.com.cn",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "802, Tower B, JianWai SOHO, Office Building\r\nChaoyang District\r\nBeijing China, 100022",
                            },
                        },

                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (10) 5869 9445",
                        Fax = "+86 (10) 5869 4171",
                        Email = "",
                    },

                };
                Context.Establishments.Add(canachieveHeadquarters);

                #endregion
                #region Can-Achieve Nanjing (Branch)

                latitude = 32.044769;
                longitude = 118.789917;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                var canAchieveNanjing = new Establishment
                {
                    Parent = canachieveHeadquarters,
                    Type = GetRecruitmentAgency(),
                    OfficialName = "Can Achieve Group Nanjing",
                    WebsiteUrl = "www.can-achieve.com.cn",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "A 12F Deji Mansion, No. 188 Changjiang Road\r\nNanjing, Jiangsu Province\r\nChina, 210018",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (25) 8681 6111",
                        Fax = "",
                        Email = "",
                    },
                };
                Context.Establishments.Add(canAchieveNanjing);

                #endregion
                #region Can-Achieve Guangzhou (Branch)

                latitude = 23.138937;
                longitude = 113.328751;
                result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                Debug.Assert(result.WoeId != null);
                place = placeFactory.FromWoeId(result.WoeId.Value);
                places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                var canAchieveGuangzhou = new Establishment
                {
                    Parent = canachieveHeadquarters,
                    Type = GetRecruitmentAgency(),
                    OfficialName = "Can Achieve Group Guangzhou",
                    WebsiteUrl = "www.can-achieve.com.cn",
                    Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                        Addresses = new List<EstablishmentAddress>
                        {
                            new EstablishmentAddress
                            {
                                TranslationToLanguage = context.Languages.Current().Single(l => l.TwoLetterIsoCode == "en"),
                                Text = "Room 511, Nanfang Securities Building\r\nNo.140-148, Tiyu Dong Road",
                            },
                        },
                    },
                    PublicContactInfo = new EstablishmentContactInfo
                    {
                        Phone = "+86 (20) 2222 0066",
                        Fax = "",
                        Email = "",
                    },
                };
                Context.Establishments.Add(canAchieveGuangzhou);

                #endregion
            }

            #endregion

        }
    }
}