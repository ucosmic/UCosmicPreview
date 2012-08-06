using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using NGeo.Yahoo.PlaceFinder;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Controllers;
using Place = UCosmic.Domain.Places.Place;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class HealthController : Controller
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsumePlaceFinder _placeFinder;
        private readonly IHandleCommands<UpdateEstablishmentHierarchiesCommand> _updateEstablishmentHierarchy;
        private readonly IHandleCommands<UpdateInstitutionalAgreementHierarchiesCommand> _updateInstitutionalAgreementHierarchy;
        private readonly IHandleCommands<CreateEstablishment> _createEstablishment;
        private readonly IHandleCommands<UpdateEstablishment> _updateEstablishment;

        public HealthController(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IUnitOfWork unitOfWork
            , IConsumePlaceFinder placeFinder
            , IHandleCommands<UpdateEstablishmentHierarchiesCommand> updateEstablishmentHierarchy
            , IHandleCommands<UpdateInstitutionalAgreementHierarchiesCommand> updateInstitutionalAgreementHierarchy
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IHandleCommands<UpdateEstablishment> updateEstablishment
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _unitOfWork = unitOfWork;
            _placeFinder = placeFinder;
            _updateEstablishmentHierarchy = updateEstablishmentHierarchy;
            _updateInstitutionalAgreementHierarchy = updateInstitutionalAgreementHierarchy;
            _createEstablishment = createEstablishment;
            _updateEstablishment = updateEstablishment;
        }

        [UnitOfWork]
        [Authorize(Users = "ludwigd1@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        [ActionName("run-establishment-hierarchy")]
        public virtual ActionResult RunEstablishmentHierarchy()
        {
            _updateEstablishmentHierarchy.Handle(
                new UpdateEstablishmentHierarchiesCommand()
            );
            return View();
        }

        [ActionName("run-institutional-agreement-hierarchy")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult RunInstitutionalAgreementHierarchy()
        {
            _updateInstitutionalAgreementHierarchy.Handle(
                new UpdateInstitutionalAgreementHierarchiesCommand()
            );
            return View();
        }

        [ActionName("run-establishment-import")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu,ludwigd@uc.edu")]
        public virtual ActionResult RunEstablishmentImport()
        {
            var placeMarks = new KmlPlaceMarks(Server.MapPath(string.Format("~{0}",
                Links.content.kml.establishment_import_kml)));
            var establishmentRows = new EstablishmentRows(Server.MapPath(string.Format("~{0}",
                Links.content.kml.establishment_import_tsv)));
            var en = _entities.Get<Language>().SingleOrDefault(x => x.TwoLetterIsoCode.Equals("en", StringComparison.OrdinalIgnoreCase));
            if (en == null) throw new InvalidOperationException("Could not find entity for 'English' language");
            var university = _entities.Get<EstablishmentType>().SingleOrDefault(x =>
                x.EnglishName.Equals("University", StringComparison.OrdinalIgnoreCase));
            if (university == null) throw new InvalidOperationException("Could not find entity for 'University' establishment type");

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
                var establishment = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(establishmentRow.WebsiteUrl));

                // when exactly 1 establishment exists in the db, check its geography
                if (establishment != null)
                {
                    ConsoleLog(string.Format("Establishment with website URL '{0}' is already seeded", establishmentRow.WebsiteUrl));
                    //establishment = establishments.Single();

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
                            //var place = placeFactory.FromWoeId(result.WoeId.Value);
                            var place = _queryProcessor.Execute(
                                new GetPlaceByWoeIdQuery
                                {
                                    WoeId = result.WoeId.Value,
                                });
                            places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                            places.Add(place);
                        }

                        var placesCount = places.Count();
                        var lastPlace = places.Last();
                        var command = new UpdateEstablishment
                        {
                            Id = establishment.RevisionId,
                            CenterLatitude = placeMark.Latitude,
                            CenterLongitude = placeMark.Longitude,
                            NorthLatitude = placesCount > 0 ? lastPlace.BoundingBox.Northeast.Latitude : null,
                            EastLongitude = placesCount > 0 ? lastPlace.BoundingBox.Northeast.Longitude : null,
                            SouthLatitude = placesCount > 0 ? lastPlace.BoundingBox.Southwest.Latitude : null,
                            WestLongitude = placesCount > 0 ? lastPlace.BoundingBox.Southwest.Longitude : null,
                            PlaceIds = places.Select(p => p.RevisionId),
                        };
                        _updateEstablishment.Handle(command);
                        _unitOfWork.SaveChanges();
                        //establishment.Location.Center = new Coordinates { Latitude = placeMark.Latitude, Longitude = placeMark.Longitude };
                        //establishment.Location.Places = places;
                        //establishment.Location.BoundingBox = (places.Count > 0) ? places.Last().BoundingBox : new BoundingBox();
                        //_unitOfWork.SaveChanges();
                        ConsoleLog(string.Format("Updated location of seeded establishment with website URL '{0}'.", establishmentRow.WebsiteUrl), true, true);
                        continue;
                    }
                    ConsoleLog(string.Format("Bypassing seeded establishment with website URL '{0}' as it already has location information", establishmentRow.WebsiteUrl), true, true);
                }
                else
                {
                    ConsoleLog(string.Format("Seeding establishment with website URL '{0}'...", establishmentRow.WebsiteUrl));
                    //var result = _placeFinder.Find(new PlaceByCoordinates(placeMark.Latitude, placeMark.Longitude)).Single();
                    //var places = new List<Place>();
                    //if (!result.WoeId.HasValue)
                    //{
                    //    ConsoleLog(string.Format("Unable to determine WOE ID for establishment with website URL '{0}'.", establishmentRow.WebsiteUrl), null, true);
                    //}
                    //else
                    //{
                    //    var place = _queryProcessor.Execute(
                    //        new GetPlaceByWoeIdQuery
                    //        {
                    //            WoeId = result.WoeId.Value,
                    //        });
                    //    places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                    //    places.Add(place);
                    //}

                    var command = new CreateEstablishment
                    {
                        TypeId = university.RevisionId,
                        OfficialName = establishmentRow.OfficialName,
                        OfficialWebsiteUrl = establishmentRow.WebsiteUrl,
                        FindPlacesByCoordinates = true,
                        CenterLatitude = placeMark.Latitude,
                        CenterLongitude = placeMark.Longitude,
                        UCosmicCode = establishmentRow.CeebCode,
                    };

                    var nonOfficialNames = new List<CreateEstablishment.NonOfficialName>();
                    if (!establishmentRow.OfficialName.Equals(establishmentRow.EnglishName, StringComparison.OrdinalIgnoreCase))
                    {
                        nonOfficialNames.Add(new CreateEstablishment.NonOfficialName
                        {
                            Text = establishmentRow.EnglishName,
                            TranslationToLanguageId = en.Id,
                        });
                    }
                    command.NonOfficialNames = nonOfficialNames.ToArray();

                    var nonOfficialUrls = new List<CreateEstablishment.NonOfficialUrl>();
                    if (establishmentRow.OtherUrls != null && establishmentRow.OtherUrls.Length > 0)
                    {
                        nonOfficialUrls.AddRange(establishmentRow.OtherUrls.Select(u =>
                            new CreateEstablishment.NonOfficialUrl
                            {
                                Value = u,
                            })
                        );
                    }
                    command.NonOfficialUrls = nonOfficialUrls.ToArray();
                    _createEstablishment.Handle(command);
                    establishment = command.CreatedEstablishment;

                    _entities.Create(establishment);
                    _unitOfWork.SaveChanges();
                    ConsoleLog(string.Format("Establishment with website URL '{0}' has been seeded.", establishmentRow.WebsiteUrl), true, true);
                }
            }

            //// set up new member(s)
            //var hpu = _entities.Get<Establishment>().SingleOrDefault(
            //    e => e.Urls.Any(u => u.Value.Equals("www.hpu.edu", StringComparison.OrdinalIgnoreCase)));
            //if (hpu != null)
            //{
            //    if (!hpu.IsMember)
            //    {
            //        hpu.IsMember = true;
            //        hpu.EmailDomains.Add(new EstablishmentEmailDomain
            //        {
            //            Value = "@hpu.edu",
            //        });

            //        var lindseyCreator = new CreatePersonHandler(_entities);
            //        var createLindsey = new CreatePersonCommand
            //        {
            //            DisplayName = "Lindsey Garbenis",
            //            UserName = "lgarbenis@hpu.edu",
            //            UserIsRegistered = false,
            //            FirstName = "Lindsey",
            //            LastName = "Garbenis",
            //        };
            //        lindseyCreator.Handle(createLindsey);
            //        createLindsey.CreatedPerson.Emails
            //            .Add(new EmailAddress { IsDefault = true, Value = createLindsey.UserName });

            //        _entities.Update(hpu);
            //        _unitOfWork.SaveChanges();
            //    }
            //}
            //var uwm = _entities.Get<Establishment>().SingleOrDefault(
            //    e => e.Urls.Any(u => u.Value.Equals("www.uwm.edu", StringComparison.OrdinalIgnoreCase)));
            //if (uwm != null)
            //{
            //    if (!uwm.IsMember)
            //    {
            //        uwm.IsMember = true;
            //        uwm.EmailDomains.Add(new EstablishmentEmailDomain
            //                                 {
            //                                     Value = "@uwm.edu",
            //                                 });
            //        _entities.Update(uwm);
            //        _unitOfWork.SaveChanges();
            //    }
            //}

            //// add former name(s)
            //const string upvName = "Universidad Privada del Valle";
            //var univalle = _entities.Get<Establishment>().SingleOrDefault(e => e.WebsiteUrl == "www.univalle.edu");
            //var upv = univalle.Names.SingleOrDefault(n => n.Text == upvName);
            //if (upv == null)
            //{
            //    univalle.Names.Add(new EstablishmentName { Text = upvName, IsFormerName = true });
            //    _entities.Update(univalle);
            //}

            //// fix name
            //var uniatlantico = _entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.uniatlantico.edu.co");
            //var engName = uniatlantico.Names.SingleOrDefault(n => n.Text == "Atlantic University, Atlantico");
            //if (engName != null)
            //{
            //    engName.Text = "Atlantic University";
            //    _unitOfWork.SaveChanges();
            //}

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

    public static class HealthRouter
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Health.Name;

        public class RunEstablishmentHierarchyRoute : MvcRoute
        {
            public RunEstablishmentHierarchyRoute()
            {
                Url = "health/run-establishment-hierarchy";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Health.ActionNames.RunEstablishmentHierarchy,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class RunInstitutionalAgreementHierarchyRoute : MvcRoute
        {
            public RunInstitutionalAgreementHierarchyRoute()
            {
                Url = "health/run-institutional-agreement-hierarchy";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Health.ActionNames.RunInstitutionalAgreementHierarchy,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class RunEstablishmentImportRoute : MvcRoute
        {
            public RunEstablishmentImportRoute()
            {
                Url = "health/run-establishment-import";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Health.ActionNames.RunEstablishmentImport,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
