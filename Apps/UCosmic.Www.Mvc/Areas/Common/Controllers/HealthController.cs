using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using NGeo.Yahoo.PlaceFinder;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;
using BoundingBox = UCosmic.Domain.Places.BoundingBox;
using Place = UCosmic.Domain.Places.Place;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class HealthController : Controller
    {
        private readonly IQueryEntities _entityQueries;
        private readonly ICommandObjects _objectCommander;
        private readonly IConsumeGeoPlanet _geoPlanet;
        private readonly IConsumeGeoNames _geoNames;
        private readonly IConsumePlaceFinder _placeFinder;
        private readonly IManageConfigurations _config;

        public HealthController(IQueryEntities entityQueries, ICommandObjects objectCommander, 
            IConsumeGeoNames geoNames, IConsumeGeoPlanet geoPlanet, IConsumePlaceFinder placeFinder, 
            IManageConfigurations config)
        {
            _entityQueries = entityQueries;
            _objectCommander = objectCommander;
            _geoNames = geoNames;
            _geoPlanet = geoPlanet;
            _placeFinder = placeFinder;
            _config = config;
        }

        [ActionName("sample-cached-page")]
        //[OutputCache(Duration = 300, VaryByCustom = "User", Location = OutputCacheLocation.Server)]
        [Authorize(Users = "ludwigd1@uc.edu,sodhiha1@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult SampleCachedPage()
        {
            return View();
        }

        [ActionName("run-establishment-hierarchy")]
        [Authorize(Users = "ludwigd1@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult RunEstablishmentHierarchy()
        {
            var establishmentChanger = new EstablishmentChanger(_objectCommander, _entityQueries);
            establishmentChanger.DeriveNodes();
            _objectCommander.SaveChanges();

            return View();
        }

        [ActionName("run-institutional-agreement-hierarchy")]
        [Authorize(Users = "ludwigd1@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult RunInstitutionalAgreementHierarchy()
        {
            var agreementChanger = new InstitutionalAgreementChanger(_objectCommander, _entityQueries);
            agreementChanger.DeriveNodes();
            _objectCommander.SaveChanges();

            return View();
        }

        [ActionName("run-establishment-import")]
        //[Authorize(Users = "ludwigd1@uc.edu,sodhiha1@uc.edu")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult RunEstablishmentImport()
        {
            var placeMarks = new KmlPlaceMarks(Server.MapPath(string.Format("~{0}",
                Links.content.kml.establishment_import_kml)));
            var establishmentRows = new EstablishmentRows(Server.MapPath(string.Format("~{0}",
                Links.content.kml.establishment_import_tsv)));
            var placeFactory = new PlaceFactory(_entityQueries, _objectCommander, _geoPlanet, _geoNames, _config);
            var en = new LanguageFinder(_entityQueries).FindOne(LanguageBy.IsoCode("en"));
            var university = new EstablishmentTypeFinder(_entityQueries).FindOne(EstablishmentTypeBy.EnglishName("University"));

            foreach (var placeMark in placeMarks)
            {
                ConsoleLog(); // write a new line

                // skip placemarks with no name / title
                if (string.IsNullOrWhiteSpace(placeMark.Name))
                {
                    ConsoleLog("Skipping Placemark with no name", false, true);
                    continue;
                }

                // begin processing the placemark
                ConsoleLog(string.Format("Processing Placemark '{0}'", placeMark.Name));

                // look in spreadsheet for row where official name or english name matches placemark name / title
                var placeMarkClosure = placeMark;
                var establishmentMatches = establishmentRows.Where(e => placeMarkClosure.Name.Equals(e.OfficialName)
                    || placeMarkClosure.Name.Equals(e.EnglishName)).ToList();
                if (establishmentMatches.Count > 1)
                {
                    // skip when there are multiple matching rows in the spreadsheet
                    ConsoleLog(string.Format("Skipping, found multiple establishments with name '{0}'.", placeMark.Name), false, true);
                    continue;
                }
                if (establishmentMatches.Count < 1)
                {
                    // skip when there are no matching rows in the spreadsheet
                    ConsoleLog(string.Format("Skipping, found no establishments with name '{0}'.", placeMark.Name), false, true);
                    continue;
                }

                // settle on the row found in the spreadsheet
                ConsoleLog(string.Format("Found 1 establishment with name '{0}'.", placeMark.Name), true);
                var establishmentRow = establishmentMatches.Single();

                // skip if the spreadsheet row has no website URL
                if (string.IsNullOrWhiteSpace(establishmentRow.WebsiteUrl))
                {
                    ConsoleLog(string.Format("Skipping, establishment with name '{0}' has no website URL.", placeMark.Name), false, true);
                    continue;
                }

                // make sure the spreadsheet does not contain any duplicates for this row
                var duplicateEstablishmentRows = establishmentRows.Where(r =>
                    establishmentRow.WebsiteUrl.Equals(r.WebsiteUrl)
                    || r.OtherUrls.Contains(establishmentRow.WebsiteUrl)
                    || establishmentRow.OfficialName.Equals(r.OfficialName, StringComparison.OrdinalIgnoreCase)
                    || establishmentRow.OfficialName.Equals(r.EnglishName, StringComparison.OrdinalIgnoreCase)
                    || establishmentRow.OfficialName.Equals(r.UCosmic1Name, StringComparison.OrdinalIgnoreCase)
                    || establishmentRow.EnglishName.Equals(r.OfficialName, StringComparison.OrdinalIgnoreCase)
                    || establishmentRow.EnglishName.Equals(r.EnglishName, StringComparison.OrdinalIgnoreCase)
                    || establishmentRow.EnglishName.Equals(r.UCosmic1Name, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                if (duplicateEstablishmentRows.Count > 1)
                {
                    ConsoleLog(string.Format("Skipping, spreadsheet has duplicate rows for '{0}' / '{1}'.", establishmentRow.OfficialName, establishmentRow.WebsiteUrl), false, true);
                    continue;
                }

                // lookup the establishment in the database by URL
                //var establishments = context.Establishments.Where(e => establishmentRow.WebsiteUrl
                //    .Equals(e.WebsiteUrl, StringComparison.OrdinalIgnoreCase)
                //    || e.Urls.Any(u => u.Value.Equals(establishmentRow.WebsiteUrl, StringComparison.OrdinalIgnoreCase))).ToList();
                var establishmentFinder = new EstablishmentFinder(_entityQueries);
                var establishments = establishmentFinder.FindMany(EstablishmentBy.WebsiteUrl(establishmentRow.WebsiteUrl)
                    .ForInsertOrUpdate());
                Establishment establishment;

                // skip when the database contains more than 1 matching institution (needs correction)
                if (establishments.Count > 1)
                {
                    ConsoleLog(string.Format("Skipping, found multiple seeded establishments with website URL '{0}' -- NEEDS CORRECTED.", establishmentRow.WebsiteUrl), false, true);
                    continue;
                }

                // when exactly 1 establishment exists in the db, check its geography
                if (establishments.Count == 1)
                {
                    ConsoleLog(string.Format("Establishment with website URL '{0}' is already seeded", establishmentRow.WebsiteUrl));
                    establishment = establishments.Single();

                    // check geography
                    if (!establishment.Location.Center.HasValue)
                    {
                        var result = _placeFinder.Find(new PlaceByCoordinates(placeMark.Latitude, placeMark.Longitude)).Single();
                        var places = new List<Place>();
                        if (!result.WoeId.HasValue)
                        {
                            ConsoleLog(string.Format("Unable to determine WOE ID for establishment with website URL '{0}'.", establishmentRow.WebsiteUrl), null, true);
                        }
                        else
                        {
                            var place = placeFactory.FromWoeId(result.WoeId.Value);
                            places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                            places.Add(place);
                        }

                        establishment.Location.Center = new Coordinates { Latitude = placeMark.Latitude, Longitude = placeMark.Longitude };
                        establishment.Location.Places = places;
                        establishment.Location.BoundingBox = (places.Count > 0) ? places.Last().BoundingBox : new BoundingBox();
                        //context.SaveChanges();
                        _objectCommander.SaveChanges();
                        ConsoleLog(string.Format("Updated location of seeded establishment with website URL '{0}'.", establishmentRow.WebsiteUrl), true, true);
                        continue;
                    }
                    ConsoleLog(string.Format("Bypassing seeded establishment with website URL '{0}' as it already has location information", establishmentRow.WebsiteUrl), true, true);
                }
                else
                {
                    ConsoleLog(string.Format("Seeding establishment with website URL '{0}'...", establishmentRow.WebsiteUrl));
                    var result = _placeFinder.Find(new PlaceByCoordinates(placeMark.Latitude, placeMark.Longitude)).Single();
                    var places = new List<Place>();
                    if (!result.WoeId.HasValue)
                    {
                        ConsoleLog(string.Format("Unable to determine WOE ID for establishment with website URL '{0}'.", establishmentRow.WebsiteUrl), null, true);
                    }
                    else
                    {
                        var place = placeFactory.FromWoeId(result.WoeId.Value);
                        places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                        places.Add(place);
                    }
                    establishment = new Establishment
                    {
                        Type = university,
                        OfficialName = establishmentRow.OfficialName,
                        WebsiteUrl = establishmentRow.WebsiteUrl,
                        Location = new EstablishmentLocation
                        {
                            Center = new Coordinates
                            {
                                Latitude = placeMark.Latitude,
                                Longitude = placeMark.Longitude,
                            },
                            Places = places,
                            BoundingBox = (places.Count > 0) ? places.Last().BoundingBox : new BoundingBox(),
                        },
                        InstitutionInfo = new InstitutionInfo
                        {
                            UCosmicCode = establishmentRow.CeebCode,
                        },
                        Names = new Collection<EstablishmentName>
                        {
                            new EstablishmentName
                            {
                                Text = establishmentRow.OfficialName,
                                IsOfficialName = true,
                            },
                        },
                        Urls = new Collection<EstablishmentUrl>
                        {
                            new EstablishmentUrl
                            {
                                Value = establishmentRow.WebsiteUrl,
                                IsOfficialUrl = true,
                            },
                        },
                    };
                    if (!establishmentRow.OfficialName.Equals(establishmentRow.EnglishName, StringComparison.OrdinalIgnoreCase))
                    {
                        establishment.Names.Add(new EstablishmentName
                        {
                            Text = establishmentRow.EnglishName,
                            TranslationToLanguage = en,
                        });
                    }
                    if (establishmentRow.OtherUrls != null && establishmentRow.OtherUrls.Length > 0)
                    {
                        foreach (var otherUrl in establishmentRow.OtherUrls)
                        {
                            establishment.Urls.Add(new EstablishmentUrl
                            {
                                Value = otherUrl,
                            });
                        }
                    }

                    //context.Establishments.Add(establishment);
                    //context.SaveChanges();
                    _objectCommander.Insert(establishment, true);
                    ConsoleLog(string.Format("Establishment with website URL '{0}' has been seeded.", establishmentRow.WebsiteUrl), true, true);
                }
            }

            ViewBag.Console = _consoleLog.ToString();
            return View();
        }

        [NonAction]
        private void ConsoleLog(string line = null, bool? good = null, bool? bold = null)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                var color = string.Empty;
                switch (good)
                {
                    case true:
                        color = "green"; break;
                    case false:
                        color = "red"; break;
                }
                var weight = (bold.HasValue && bold.Value) ? "bold" : "normal";
                if (good.HasValue && !good.Value)
                {
                    _consoleLog.Append("<br />");
                    _consoleLog.Append("<br />");
                    _consoleLog.Append("<br />");
                    _consoleLog.Append("<br />");
                    _consoleLog.Append("<br />");
                }
                _consoleLog.Append(string.Format("<span style='color: {0}; font-weight: {1}'>", color, weight));
                _consoleLog.Append(line);
                _consoleLog.Append("</span>");
            }
            if (good.HasValue && !good.Value)
            {
                _consoleLog.Append("<br />");
                _consoleLog.Append("<br />");
                _consoleLog.Append("<br />");
                _consoleLog.Append("<br />");
                _consoleLog.Append("<br />");
            }
            _consoleLog.Append("<br />");
        }
        private readonly StringBuilder _consoleLog = new StringBuilder();

        private class KmlPlaceMarks : Collection<KmlPlaceMark>
        {
            public KmlPlaceMarks(string path)
            {
                var kmlDoc = new XmlDocument();
                using (var reader = new StreamReader(path))
                    kmlDoc.LoadXml(reader.ReadToEnd());
                var placeMarkNodes = kmlDoc.GetElementsByTagName("Placemark");
                foreach (XmlNode placeMarkNode in placeMarkNodes)
                    Add(new KmlPlaceMark(placeMarkNode));
            }
        }
        private class KmlPlaceMark
        {
            private readonly XmlNode _xmlNode;
            public KmlPlaceMark(XmlNode xmlNode)
            {
                _xmlNode = xmlNode;
            }

            public string Name
            {
                get
                {
                    foreach (XmlNode childNode in _xmlNode.ChildNodes)
                    {
                        if (childNode.Name == "name") return childNode.InnerText.Trim();
                    }
                    throw new NotSupportedException("KML Placemark did not have a name.");
                }
            }
            public double Latitude
            {
                get
                {
                    foreach (XmlNode childNode in _xmlNode.ChildNodes)
                    {
                        if (childNode.Name == "Point")
                        {
                            foreach (XmlNode grandChildNode in childNode.ChildNodes)
                            {
                                if (grandChildNode.Name == "coordinates")
                                {
                                    return double.Parse(grandChildNode.InnerText.Split(',')[1]);
                                }
                            }
                        }
                    }
                    throw new NotSupportedException("KML Placemark did not have a latitude.");
                }
            }
            public double Longitude
            {
                get
                {
                    foreach (XmlNode childNode in _xmlNode.ChildNodes)
                    {
                        if (childNode.Name == "Point")
                        {
                            foreach (XmlNode grandChildNode in childNode.ChildNodes)
                            {
                                if (grandChildNode.Name == "coordinates")
                                {
                                    return double.Parse(grandChildNode.InnerText.Split(',')[0]);
                                }
                            }
                        }
                    }
                    throw new NotSupportedException("KML Placemark did not have a longitude.");
                }
            }
        }
        private class EstablishmentRows : Collection<EstablishmentRow>
        {
            public EstablishmentRows(string path)
            {
                string lines;
                using (var reader = new StreamReader(path))
                    lines = reader.ReadToEnd();
                foreach (var line in lines.Split('\n').Skip(1).Select(x => x.Trim()))
                    Add(new EstablishmentRow(line));
            }
        }
        private class EstablishmentRow
        {
            private readonly string[] _columns;
            public EstablishmentRow(string line)
            {
                _columns = line.Split('\t');
            }

            public string CeebCode
            {
                get
                {
                    var raw = _columns[0].Trim();
                    if (!string.IsNullOrWhiteSpace(raw) && raw.Length == 6)
                    {
                        int code;
                        if (int.TryParse(raw.Substring(2), out code))
                        {
                            return raw;
                        }
                    }
                    return null;
                }
            }

            public string UCosmic1Name
            {
                get
                {
                    if (_columns.Length > 1)
                    {
                        var raw = _columns[1].Trim();
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            return raw;
                        }
                    }
                    return null;
                }
            }

            public string WebsiteUrl
            {
                get
                {
                    if (_columns.Length > 1)
                    {
                        var raw = _columns[2].Trim();
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            return raw;
                        }
                    }
                    return null;
                }
            }

            public string[] OtherUrls
            {
                get
                {
                    var otherUrls = new List<string>();
                    if (_columns.Length > 1)
                    {
                        var raw = _columns[3];
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            // ReSharper disable LoopCanBeConvertedToQuery
                            foreach (var otherUrl in raw.Split(',').Select(s => s.Trim()))
                            {
                                if (string.IsNullOrWhiteSpace(otherUrl)) continue;
                                otherUrls.Add(otherUrl);
                            }
                            // ReSharper restore LoopCanBeConvertedToQuery
                        }
                    }
                    return otherUrls.ToArray();
                }
            }

            //public bool IsSeeded
            //{
            //    get
            //    {
            //        if (!string.IsNullOrWhiteSpace(_columns[6]) && _columns[6].Length > 1)
            //        {
            //            var raw = _columns[6].Trim().Substring(0, 2);
            //            if (!string.IsNullOrWhiteSpace(raw)
            //                && raw.Equals("ye", StringComparison.OrdinalIgnoreCase))
            //            {
            //                return true;
            //            }
            //        }
            //        return false;
            //    }
            //}

            public string OfficialName
            {
                get
                {
                    if (_columns.Length > 1)
                    {
                        var raw = _columns[7].Trim();
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            return raw;
                        }
                    }
                    return null;
                }
            }

            public string EnglishName
            {
                get
                {
                    if (_columns.Length > 1)
                    {
                        var raw = _columns[8].Trim();
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            return raw;
                        }
                    }
                    return null;
                }
            }

            public override string ToString()
            {
                return string.Format("{0} | {1} | {2} | {3} | {4} | {5}",
                    CeebCode, UCosmic1Name, WebsiteUrl, _columns[3], OfficialName, EnglishName);
            }
        }

        //[ActionName("run-imager")]
        //[Authorize(Users = "ludwigd1@uc.edu")]
        ////[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        //[UnitOfWork]
        //public virtual ActionResult RunImager()
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        var image = Image.FromFile(@"C:\Users\Dan\Desktop\progress.gif");
        //        image.Save(ms, ImageFormat.Gif);
        //        var bytes = ms.ToArray();
        //        var base64 = Convert.ToBase64String(bytes);
        //        ViewBag.ImageBytes = base64;
        //    }

        //    return View();
        //}

    }
}
