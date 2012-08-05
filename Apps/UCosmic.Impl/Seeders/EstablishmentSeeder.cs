using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using NGeo.Yahoo.PlaceFinder;
using ServiceLocatorPattern;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Impl.Orm;
using UCosmic.Domain.Languages;

namespace UCosmic.Impl.Seeders
{
    public class EstablishmentSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new EstablishmentPreview1Seeder().Seed(context);
            new EstablishmentPreview3Seeder().Seed(context);
            new EstablishmentUsfSeeder().Seed(context);
            new EstablishmentPreview5Seeder().Seed(context);
            new EstablishmentDecember2011Preview2Seeder().Seed(context);
            new EstablishmentJanuary2011Preview1Seeder().Seed(context);
            //new EstablishmentUcSamlIntegrationSeeder().Seed(context);
        }

        public class EstablishmentUsfSeeder : BaseEstablishmentSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                const string usfUrl = "www.usf.edu";
                var usf = Context.Set<Establishment>().SingleOrDefault(e => e.WebsiteUrl == usfUrl);
                if (usf == null)
                {
                    var placeFinderClient = ServiceProviderLocator.Current.GetService<IConsumePlaceFinder>();
                    const string officialName = "University of South Florida";
                    usf = EnsureEstablishment(officialName, true, null, GetUniversity(), usfUrl, "@usf.edu;@iac.usf.edu;@mail.usf.edu");
                    const double latitude = 28.061680;
                    const double longitude = -82.414803;
                    var result = placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                    //var place = placeFactory.FromWoeId(result.WoeId.Value);
                    var place = ServiceProviderLocator.Current.GetService<IProcessQueries>().Execute(
                        new GetPlaceByWoeIdQuery
                        {
                            WoeId = result.WoeId.Value,
                        }
                    );
                    var places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                    places.Add(place);
                    usf.Location = new EstablishmentLocation
                    {
                        Center = new Coordinates { Latitude = latitude, Longitude = longitude, },
                        BoundingBox = place.BoundingBox,
                        Places = places,
                    };
                    context.SaveChanges();
                }
            }
        }

        public class EstablishmentUcSamlIntegrationSeeder : UCosmicDbSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                var uc = Context.Set<Establishment>().Single(e => e.WebsiteUrl == "www.uc.edu");
                var samlHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<UpdateSamlSignOnInfoCommand>>();
                samlHandler.Handle(
                    new UpdateSamlSignOnInfoCommand
                    {
                        Establishment = uc,
                        EntityId = "https://qalogin.uc.edu/idp/shibboleth",
                        MetadataUrl = "https://qalogin.uc.edu/idp/profile/Metadata/SAML",
                    }
                );
                context.SaveChanges();
            }
        }

        public class EstablishmentJanuary2011Preview1Seeder : BaseEstablishmentSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                var testshib = Context.Set<Establishment>().SingleOrDefault(e => e.WebsiteUrl == "www.testshib.org");
                if (testshib == null)
                {
                    testshib = new Establishment
                    {
                        OfficialName = "TestShib2",
                        IsMember = true,
                        WebsiteUrl = "www.testshib.org",
                        Location = new EstablishmentLocation(),
                        Type = GetGenericBusiness(),
                        Names = new[]
                        {
                            new EstablishmentName { IsOfficialName = true, Text = "TestShib2" }
                        },
                        EmailDomains = new Collection<EstablishmentEmailDomain>
                        {
                            new EstablishmentEmailDomain { Value = "@testshib.org", }
                        },
                    };
                    Context.Set<Establishment>().Add(testshib);
                    context.SaveChanges();

                    // this won't seed if the testshib metadata url cannot be reached
                    var samlHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<UpdateSamlSignOnInfoCommand>>();
                    samlHandler.Handle(
                        new UpdateSamlSignOnInfoCommand
                        {
                            Establishment = testshib,
                            EntityId = "https://idp.testshib.org/idp/shibboleth",
                            MetadataUrl = "https://idp.testshib.org/idp/shibboleth",
                        }
                    );
                    context.SaveChanges();
                }
            }
        }

        public class EstablishmentDecember2011Preview2Seeder : BaseEstablishmentSeeder
        {
            //private PlaceFactory _placeFactory;
            private IConsumePlaceFinder _placeFinderClient;

            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                //var config = new DotNetConfigurationManager();
                //var commander = new ObjectCommander(context);
                //var geoPlanet = ServiceProviderLocator.Current.GetService<IConsumeGeoPlanet>();
                //var geoNames = ServiceProviderLocator.Current.GetService<IConsumeGeoNames>();
                _placeFinderClient = ServiceProviderLocator.Current.GetService<IConsumePlaceFinder>();
                //_placeFactory = new PlaceFactory(context, commander, geoPlanet, geoNames, config);
                Seed("www.ufl.edu", 29.643528, -82.350685);
                Seed("www.ufrj.br", -22.862494, -43.223907);
                Seed("www.ufpr.br", -25.434137, -49.267353);
                Seed("www.uc.edu", 39.132084, -84.516479);
                Seed("www.fhnw.ch", 47.484417, 8.207265);
                Seed("www.ippuc.org.br", -25.414003, -49.252010);
                Seed("www.jnu.edu.cn", 23.128067, 113.347710);
                Seed("www.jku.at", 48.337395, 14.317374);
                Seed("www.swinburne.edu.au", -37.851940, 144.991974);
                Seed("www.udd.cl", -36.823036, -73.036003);
                Seed("www.up.com.br", -25.448196, -49.355865);
                Seed("www.uclouvain.be", 50.673618, 4.604945);
                Seed("www.usp.br", -23.559305, -46.715672);
            }

            private void Seed(string url, double latitude, double longitude)
            {
                var est = Context.Set<Establishment>().Single(e => e.WebsiteUrl == url);
                if (est.Location.Center.HasValue) return;

                est.Location.Center = new Coordinates { Latitude = latitude, Longitude = longitude };
                var result = _placeFinderClient.Find(new PlaceByCoordinates(latitude, longitude)).Single();
                if (result.WoeId.HasValue)
                {
                    //var place = _placeFactory.FromWoeId(result.WoeId.Value);
                    var place = ServiceProviderLocator.Current.GetService<IProcessQueries>().Execute(
                        new GetPlaceByWoeIdQuery
                        {
                            WoeId = result.WoeId.Value,
                        });
                    var places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                    places.Add(place);
                    est.Location.BoundingBox = place.BoundingBox;
                    if (est.Location.Places != null)
                        est.Location.Places.Clear();
                    est.Location.Places = places;
                }
                Context.SaveChanges();
            }
        }

        public class EstablishmentPreview5Seeder : BaseEstablishmentSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                // create locations for all establishments
                var establishmentsWithoutLocations = Context.Set<Establishment>().Where(e => e.Location == null).ToList();
                establishmentsWithoutLocations.ForEach(e => e.Location = e.Location ??
                    new EstablishmentLocation
                    {
                        Addresses = new List<EstablishmentAddress>()
                    }
                );
                Context.SaveChanges();

                // constitute official names
                var establishmentsWithoutOfficialNames = Context.Set<Establishment>().Where(e => !e.Names.Any(n => n.IsOfficialName)).ToList();
                establishmentsWithoutOfficialNames.ForEach(e => e.Names.Add(new EstablishmentName
                {
                    Text = e.OfficialName,
                    IsOfficialName = true,
                })
                );
                Context.SaveChanges();

                // delete x-ascii names
                var asciiEstablishmentNames = Context.Set<Establishment>().SelectMany(e => e.Names.Where(n => n.TranslationToHint == "x-ascii")).ToList();
                asciiEstablishmentNames.ForEach(n => Context.Entry(n).State = EntityState.Deleted);
                Context.SaveChanges();

                // constitute official URL's
                var establishmentsWithoutOfficialUrls = Context.Set<Establishment>().Where(e => !string.IsNullOrEmpty(e.WebsiteUrl)
                    && !e.Urls.Any(n => n.IsOfficialUrl)).ToList();
                establishmentsWithoutOfficialUrls.ForEach(e => e.Urls.Add(new EstablishmentUrl
                {
                    Value = e.WebsiteUrl,
                    IsOfficialUrl = true,
                })
                );
                Context.SaveChanges();

                // convert URL names
                var urlEstablishmentNames = Context.Set<Establishment>().SelectMany(e => e.Names.Where(n => n.TranslationToHint == "x-url")).ToList();
                urlEstablishmentNames.ForEach(n =>
                {
                    n.ForEstablishment.Urls = n.ForEstablishment.Urls ?? new List<EstablishmentUrl>();
                    n.ForEstablishment.Urls.Add(new EstablishmentUrl
                    {
                        Value = n.Text,
                        IsFormerUrl = n.IsFormerName,
                    });
                    Context.Entry(n).State = EntityState.Deleted;
                });
                Context.SaveChanges();

                // translate hinted names
                var hintedEstablishmentNames = Context.Set<Establishment>().SelectMany(e => e.Names.Where(n => n.TranslationToHint != null)).ToList();
                hintedEstablishmentNames.ForEach(n =>
                {
                    n.TranslationToLanguage = Context.Set<Language>().SingleOrDefault(l => l.TwoLetterIsoCode == n.TranslationToHint);
                    n.TranslationToHint = null;
                });
                Context.SaveChanges();

                // correct www.udd.cl
                var universidadDeDesarrollo = Context.Set<Establishment>().SingleOrDefault(e => e.WebsiteUrl == "www.udd.cl");
                if (universidadDeDesarrollo != null)
                {
                    universidadDeDesarrollo.OfficialName = "Universidad del Desarrollo";
                    Context.SaveChanges();
                }

                // correct audencia
                var audencia = Context.Set<Establishment>().SingleOrDefault(e => e.WebsiteUrl == "www.audencua.com");
                if (audencia != null)
                {
                    audencia.WebsiteUrl = "www.audencia.com";
                    Context.SaveChanges();
                }
            }
        }

        public class EstablishmentPreview3Seeder : BaseEstablishmentSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                #region UMN Colleges

                var umn = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.OfficialName.Equals("University of Minnesota", StringComparison.OrdinalIgnoreCase));
                if (umn == null)
                    throw new InvalidOperationException("University of Minnesota does not exist.");

                EnsureEstablishment("Center for Allied Health Programs, University of Minnesota", true, umn, GetCollege(), "www.cahp.umn.edu", null);
                EnsureEstablishment("College of Biological Sciences, University of Minnesota", true, umn, GetCollege(), "www.cbs.umn.edu", null);
                EnsureEstablishment("College of Continuing Education, University of Minnesota", true, umn, GetCollege(), "www.cce.umn.edu", null);
                EnsureEstablishment("School of Dentistry, University of Minnesota", true, umn, GetCollege(), "www.dentistry.umn.edu", null);
                EnsureEstablishment("College of Design, University of Minnesota", true, umn, GetCollege(), "www.design.umn.edu", null);
                EnsureEstablishment("College of Education & Human Development, University of Minnesota", true, umn, GetCollege(), "www.cehd.umn.edu", null);
                EnsureEstablishment("University of Minnesota Extension", true, umn, GetCollege(), "www.extension.umn.edu", null);
                EnsureEstablishment("College of Food, Agricultural and Natural Resource Sciences, University of Minnesota", true, umn, GetCollege(), "www.cfans.umn.edu", null);
                EnsureEstablishment("The Graduate School, University of Minnesota", true, umn, GetCollege(), "www.grad.umn.edu", null);

                EnsureEstablishment("University of Minnesota Law School", true, umn, GetCollege(), "www.law.umn.edu", null);
                EnsureEstablishment("College of Liberal Arts, University of Minnesota", true, umn, GetCollege(), "www.cla.umn.edu", null);
                EnsureEstablishment("Carlson School of Management, University of Minnesota", true, umn, GetCollege(), "www.csom.umn.edu", null);
                EnsureEstablishment("University of Minnesota Medical School", true, umn, GetCollege(), "www.med.umn.edu", null);
                EnsureEstablishment("School of Nursing, University of Minnesota", true, umn, GetCollege(), "www.nursing.umn.edu", null);
                EnsureEstablishment("College of Pharmacy, University of Minnesota", true, umn, GetCollege(), "www.pharmacy.umn.edu", null);
                EnsureEstablishment("Hubert H. Humphrey School of Public Affairs, University of Minnesota", true, umn, GetCollege(), "www.hhh.umn.edu", null);
                EnsureEstablishment("School of Public Health, University of Minnesota", true, umn, GetCollege(), "www.sph.umn.edu", null);
                EnsureEstablishment("College of Science & Engineering, University of Minnesota", true, umn, GetCollege(), "www.cse.umn.edu", null);
                EnsureEstablishment("College of Veterinary Medicine, University of Minnesota", true, umn, GetCollege(), "www.cvm.umn.edu", null);

                #endregion
                #region Mary Watkins Institutions

                var officialName = "Nanyang Technological University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ntu.edu.sg", null);

                officialName = "Chungnam National University";
                var cnu = EnsureEstablishment(officialName, false, null, GetUniversity(), "www.cnu.ac.kr", null);
                if (cnu.Names.Count < 2)
                {
                    cnu.Names.Add(new EstablishmentName
                    {
                        Text = "plus.cnu.ac.kr",
                        TranslationToHint = "x-url",
                    });
                    cnu.Names.Add(new EstablishmentName
                    {
                        Text = "ipsi.cnu.ac.kr",
                        TranslationToHint = "x-url",
                    });
                }

                officialName = "Beijing University of Technology";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.bjut.edu.cn", null);

                #endregion
            }
        }

        public class EstablishmentPreview1Seeder : BaseEstablishmentSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                #region SUNY

                var officialName = "State University of New York (SUNY)";
                EnsureEstablishment(officialName, true, null, GetUniversitySystem(), "www.suny.edu", "@suny.edu");
                var suny = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.OfficialName.Equals(officialName, StringComparison.OrdinalIgnoreCase));

                EnsureEstablishment("SUNY Adirondack", true, suny, GetCommunityCollege(), "www.sunyacc.edu", "@sunyacc.edu");
                EnsureEstablishment("University at Albany (SUNY)", true, suny, GetUniversity(), "www.albany.edu", "@albany.edu");
                EnsureEstablishment("Alfred State College (SUNY)", true, suny, GetCollege(), "www.alfredstate.edu", "@alfredstate.edu");
                EnsureEstablishment("Alfred University (SUNY)", true, suny, GetUniversity(), "www.alfred.edu", "@alfred.edu");
                EnsureEstablishment("Binghamtom University (SUNY)", true, suny, GetUniversity(), "www.binghamton.edu", "@binghamton.edu");
                EnsureEstablishment("The College at Brockport (SUNY)", true, suny, GetCollege(), "www.brockport.edu", "@brockport.edu");
                EnsureEstablishment("Broome Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunybroome.edu", "@sunybroome.edu");
                EnsureEstablishment("University at Buffalo (SUNY)", true, suny, GetUniversity(), "www.buffalo.edu", "@buffalo.edu");
                EnsureEstablishment("Buffalo State College (SUNY)", true, suny, GetCollege(), "www.buffalostate.edu", "@buffalostate.edu");
                EnsureEstablishment("SUNY Canton", true, suny, GetCollege(), "www.canton.edu", "@canton.edu");
                EnsureEstablishment("Cayuga Community College (SUNY)", true, suny, GetCommunityCollege(), "www.cayuga-cc.edu", "@cayuga-cc.edu");
                EnsureEstablishment("Clinton Community College (SUNY)", true, suny, GetCommunityCollege(), "www.clinton.edu", "@clinton.edu");
                EnsureEstablishment("SUNY Cobleskill", true, suny, GetCollege(), "www.cobleskill.edu", "@cobleskill.edu");
                EnsureEstablishment("Columbia-Greene Community College", true, suny, GetCommunityCollege(), "www.sunycgcc.edu", "@sunycgcc.edu");
                EnsureEstablishment("Cornell University College of Agriculture and Life Sciences", true, suny, GetCollege(), "www.cals.cornell.edu", "@cals.cornell.edu");
                EnsureEstablishment("Cornell University College of Human Ecology", true, suny, GetCollege(), "www.human.cornell.edu", "@human.cornell.edu");
                EnsureEstablishment("Cornell University College of Veterinary Medicine", true, suny, GetCollege(), "www.vet.cornell.edu", "@vet.cornell.edu");
                EnsureEstablishment("Cornell University ILR School", true, suny, GetCollege(), "www.ilr.cornell.edu", "@ilr.cornell.edu");
                EnsureEstablishment("Corning Community College (SUNY)", true, suny, GetCommunityCollege(), "www.corning-cc.edu", "@corning-cc.edu");
                EnsureEstablishment("SUNY Cortland", true, suny, GetCollege(), "www.cortland.edu", "@cortland.edu");
                EnsureEstablishment("SUNY Delhi", true, suny, GetCollege(), "www.delhi.edu", "@delhi.edu");
                EnsureEstablishment("SUNY Downstate Medical Center", true, suny, GetCollege(), "www.downstate.edu", "@downstate.edu");
                EnsureEstablishment("Dutchess Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunydutchess.edu", "@sunydutchess.edu");
                EnsureEstablishment("SUNY Empire State College", true, suny, GetCollege(), "www.esc.edu", "@esc.edu");
                EnsureEstablishment("SUNY College of Environmental Science and Forestry", true, suny, GetCollege(), "www.esf.edu", "@esf.edu");
                EnsureEstablishment("Erie Community College (SUNY)", true, suny, GetCommunityCollege(), "www.ecc.edu", "@ecc.edu");
                EnsureEstablishment("Farmingdale State College (SUNY)", true, suny, GetCollege(), "www.farmingdale.edu", "@farmingdale.edu");
                EnsureEstablishment("Fashion Institute of Technology (SUNY)", true, suny, GetCommunityCollege(), "www.fitnyc.edu", "@fitnyc.edu");
                EnsureEstablishment("Finger Lakes Community College (SUNY)", true, suny, GetCommunityCollege(), "www.flcc.edu", "@flcc.edu");
                EnsureEstablishment("SUNY Fredonia", true, suny, GetCollege(), "www.fredonia.edu", "@fredonia.edu");
                EnsureEstablishment("Fulton-Montgomery Community College (SUNY)", true, suny, GetCommunityCollege(), "fmcc.suny.edu", "@fmcc.suny.edu");
                EnsureEstablishment("Genesse Community College (SUNY)", true, suny, GetCommunityCollege(), "www.genesse.edu", "@genesse.edu");
                EnsureEstablishment("SUNY Geneseo", true, suny, GetCollege(), "www.geneseo.edu", "@geneseo.edu");
                EnsureEstablishment("Herkimer County Community College (SUNY)", true, suny, GetCommunityCollege(), "www.herkimer.edu", "@herkimer.edu");
                EnsureEstablishment("Hudson Valley Community College (SUNY)", true, suny, GetCommunityCollege(), "www.hvcc.edu", "@hvcc.edu");
                EnsureEstablishment("Jamestown Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunyjcc.edu", "@sunyjcc.edu");
                EnsureEstablishment("Jefferson Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunyjefferson.edu", "@sunyjefferson.edu");
                EnsureEstablishment("Maritime College (SUNY)", true, suny, GetCollege(), "www.sunymaritime.edu", "@sunymaritime.edu");
                EnsureEstablishment("Mohawk Valley Community College (SUNY)", true, suny, GetCommunityCollege(), "www.mvcc.edu", "@mvcc.edu");
                EnsureEstablishment("Monroe Community College (SUNY)", true, suny, GetCommunityCollege(), "www.monroecc.edu", "@monroecc.edu");
                EnsureEstablishment("Morrisville State College (SUNY)", true, suny, GetCollege(), "www.morrisville.edu", "@morrisville.edu");
                EnsureEstablishment("Nassau Community College (SUNY)", true, suny, GetCommunityCollege(), "www.ncc.edu", "@ncc.edu");
                EnsureEstablishment("New Paltz (SUNY)", true, suny, GetCollege(), "www.newpaltz.edu", "@newpaltz.edu");
                EnsureEstablishment("Niagra County Community College (SUNY)", true, suny, GetCommunityCollege(), "www.niagaracc.suny.edu", "@niagaracc.suny.edu");
                EnsureEstablishment("North Country Community College (SUNY)", true, suny, GetCommunityCollege(), "www.nccc.edu", "@nccc.edu");
                EnsureEstablishment("The College at Old Westbury (SUNY)", true, suny, GetCollege(), "www.oldwestbury.edu", "@oldwestbury.edu");
                EnsureEstablishment("SUNY College at Oneonta", true, suny, GetCollege(), "www.oneonta.edu", "@oneonta.edu");
                EnsureEstablishment("Onondaga Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunyocc.edu", "@sunyocc.edu");
                EnsureEstablishment("SUNY College of Optometry", true, suny, GetCollege(), "www.sunyopt.edu", "@sunyopt.edu");
                EnsureEstablishment("SUNY Orange", true, suny, GetCommunityCollege(), "www.sunyorange.edu", "@sunyorange.edu");
                EnsureEstablishment("Oswego (SUNY)", true, suny, GetCollege(), "www.oswego.edu", "@oswego.edu");
                EnsureEstablishment("SUNY Plattsburgh", true, suny, GetCollege(), "www.plattsburgh.edu", "@plattsburgh.edu");
                EnsureEstablishment("SUNY Potsdam", true, suny, GetCollege(), "www.potsdam.edu", "@potsdam.edu");
                EnsureEstablishment("Purchase College (SUNY)", true, suny, GetCollege(), "www.purchase.edu", "@purchase.edu");
                EnsureEstablishment("SUNY Rockland Community College", true, suny, GetCommunityCollege(), "www.sunyrockland.edu", "@sunyrockland.edu");
                EnsureEstablishment("Schenectady County Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunysccc.edu", "@sunysccc.edu");
                EnsureEstablishment("Stony Brook University (SUNY)", true, suny, GetUniversity(), "www.stonybrook.edu", "@stonybrook.edu");
                EnsureEstablishment("Suffolk County Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunysuffolk.edu", "@sunysuffolk.edu");
                EnsureEstablishment("Sullivan Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunysullivan.edu", "@sunysullivan.edu");
                EnsureEstablishment("SUNYIT", true, suny, GetCollege(), "www.sunyit.edu", "@sunyit.edu");
                EnsureEstablishment("Tompkins Cortland Community College (SUNY)", true, suny, GetCommunityCollege(), "www.tc3.edu", "@tc3.edu");
                EnsureEstablishment("SUNY Ulster", true, suny, GetCommunityCollege(), "www.sunyulster.edu", "@sunyulster.edu");
                EnsureEstablishment("Upstate Medical University (SUNY)", true, suny, GetUniversity(), "www.upstate.edu", "@upstate.edu");
                EnsureEstablishment("Westchester Community College (SUNY)", true, suny, GetCommunityCollege(), "www.sunywcc.edu", "@sunywcc.edu");

                #endregion
                #region UC

                officialName = "University of Cincinnati";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.uc.edu", "@uc.edu;@ucmail.uc.edu");
                var uc = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.OfficialName.Equals(officialName, StringComparison.OrdinalIgnoreCase));
                EnsureEstablishment("College of Allied Health Sciences, University of Cincinnati", true, uc, GetCollege(), "www.cahs.uc.edu", null);
                EnsureEstablishment("McMicken College of Arts & Sciences, University of Cincinnati", true, uc, GetCollege(), "www.artsci.uc.edu", null);
                EnsureEstablishment("College of Business, University of Cincinnati", true, uc, GetCollege(), "www.business.uc.edu", null);
                EnsureEstablishment("College Conservatory of Music, University of Cincinnati", true, uc, GetCollege(), "ccm.uc.edu", null);
                EnsureEstablishment("College of Design, Architecture, Art, and Planning, University of Cincinnati", true, uc, GetCollege(), "www.daap.uc.edu", null);
                EnsureEstablishment("College of Education, Criminal Justice, and Human Services, University of Cincinnati", true, uc, GetCollege(), "www.cech.uc.edu", null);
                EnsureEstablishment("College of Engineering & Applied Sciences, University of Cincinnati", true, uc, GetCollege(), "www.ceas.uc.edu", null);
                EnsureEstablishment("College of Law, University of Cincinnati", true, uc, GetCollege(), "www.law.uc.edu", null);
                EnsureEstablishment("College of Medicine, University of Cincinnati", true, uc, GetCollege(), "www.med.uc.edu", null);
                EnsureEstablishment("College of Nursing, University of Cincinnati", true, uc, GetCollege(), "nursing.uc.edu", null);
                EnsureEstablishment("James L. Winkle College of Pharmacy, University of Cincinnati", true, uc, GetCollege(), "pharmacy.uc.edu", null);
                EnsureEstablishment("School of Social Work, University of Cincinnati", true, uc, GetCollege(), "www.uc.edu/socialwork", null);
                EnsureEstablishment("Raymond Walters College, University of Cincinnati", true, uc, GetCollege(), "www.rwc.uc.edu", null);
                EnsureEstablishment("Clermont College, University of Cincinnati", true, uc, GetCollege(), "www.ucclermont.edu", null);
                EnsureEstablishment("University of Cincinnati Graduate School", true, uc, GetCollege(), "www.grad.uc.edu", null);
                EnsureEstablishment("University of Cincinnati Honors Program", true, uc, GetAcademicProgram(), "www.uc.edu/honors.html", null);

                #endregion
                #region Lehigh

                officialName = "Lehigh University";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.lehigh.edu", "@lehigh.edu");
                var lehigh = Context.Set<Establishment>().SingleOrDefault(e => e.OfficialName.Equals(officialName, StringComparison.OrdinalIgnoreCase));
                EnsureEstablishment("Lehigh University College of Arts and Sciences", true, lehigh, GetCollege(), "cas.lehigh.edu", null);
                EnsureEstablishment("Lehigh University College of Business and Economics", true, lehigh, GetCollege(), "www.lehigh.edu/business", null);
                EnsureEstablishment("Lehigh University College of Education", true, lehigh, GetCollege(), "www.lehigh.edu/education", null);
                EnsureEstablishment("Lehigh University P.C. Rossin College of Engineering and Applied Science", true, lehigh, GetCollege(), "www.lehigh.edu/engineering", null);

                #endregion
                #region Manipal

                officialName = "Manipal Education";
                EnsureEstablishment(officialName, true, null, GetUniversitySystem(), "www.manipalglobal.com", null);
                var manipalGlobal = Context.Set<Establishment>().SingleOrDefault(e => e.OfficialName.Equals(officialName, StringComparison.OrdinalIgnoreCase));

                var manipalEdu = EnsureEstablishment("Manipal University", true, manipalGlobal, GetUniversity(), "www.manipal.edu", "@manipal.edu");
                var melaka = EnsureEstablishment("Melaka Manipal Medical College", true, manipalEdu, GetCollege(), "www.manipal.edu/Institutions/Medicine/MMMCMelaka", null);

                // dummy Melaka children
                var melakaA1 = EnsureEstablishment("Melaka Campus A1", true, melaka, GetCollege(), null, null);
                var melakaB1 = EnsureEstablishment("Melaka College B1", true, melakaA1, GetCommunityCollege(), null, null);
                var melakaC1 = EnsureEstablishment("Melaka Department C1", true, melakaB1, GetAcademicProgram(), null, null);
                EnsureEstablishment("Melaka Program D1", true, melakaC1, GetAcademicProgram(), null, null);

                EnsureEstablishment("ICICI Manipal Academy", true, manipalEdu, GetCollege(), "www.ima.manipal.edu", "@ima.manipal.edu");

                EnsureEstablishment("American University of Antigua", true, manipalGlobal, GetUniversity(), "www.auamed.org", "@auamed.org");
                EnsureEstablishment("Manipal University Dubai Campus", true, manipalGlobal, GetUniversity(), "www.manipaldubai.com", "@manipaldubai.com");
                EnsureEstablishment("Manipal College of Medical Sciences, Nepal", true, manipalGlobal, GetCollege(), "www.manipal.edu.np", "@manipal.edu.np");
                EnsureEstablishment("Sikkim Manipal University", true, manipalGlobal, GetUniversity(), "www.smude.edu.in", "@smude.edu.in");
                EnsureEstablishment("Manipal International University", true, manipalGlobal, GetUniversity(), "www.miu.edu.my", "@miu.edu.my");

                #endregion
                #region Other Founding Members

                officialName = "Universidad San Ignacio de Loyola";
                var usil = EnsureEstablishment(officialName, true, null, GetUniversity(), "www.usil.edu.pe", "@usil.edu.pe");
                EnsureEstablishment("Universidad San Ignacio de Loyola Facultad de Ciencias Empresariales", true, usil, GetCollege(), null, null);
                EnsureEstablishment("Universidad San Ignacio de Loyola Facultad de Educación", true, usil, GetCollege(), null, null);
                EnsureEstablishment("Universidad San Ignacio de Loyola Facultad de Administración Hotelera, Turismo y Gastronomía", true, usil, GetCollege(), null, null);
                EnsureEstablishment("Universidad San Ignacio de Loyola Facultad de  Humanidades", true, usil, GetCollege(), null, null);
                EnsureEstablishment("Universidad San Ignacio de Loyola Ingeniería y Arquitectura", true, usil, GetCollege(), null, null);

                officialName = "Griffith University";
                var griffith = EnsureEstablishment(officialName, true, null, GetUniversity(), "www.griffith.edu.au", "@griffith.edu.au");
                EnsureEstablishment("Griffith University Gold Coast Campus", true, griffith, GetUniversityCampus(), null, null);
                EnsureEstablishment("Griffith University Logan Campus", true, griffith, GetUniversityCampus(), null, null);
                EnsureEstablishment("Griffith University Mt Gravatt Campus", true, griffith, GetUniversityCampus(), null, null);
                EnsureEstablishment("Griffith University Nathan Campus", true, griffith, GetUniversityCampus(), null, null);
                EnsureEstablishment("Griffith University South Bank Campus", true, griffith, GetUniversityCampus(), null, null);

                officialName = "Beijing Jiaotong University";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.bjtu.edu.cn", "@bjtu.edu.cn;@njtu.edu.cn",
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Northern Jiaotong University",
                        TranslationToHint = "en",
                        IsFormerName = true,
                    },
                    new EstablishmentName
                    {
                        Text = "www.njtu.edu.cn",
                        TranslationToHint = "x-url",
                        IsFormerName = true,
                    },
                }
                );

                officialName = "Edinburgh Napier University";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.napier.ac.uk", "@napier.ac.uk");

                officialName = "Future University in Egypt";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.fue.edu.eg", "@fue.edu.eg");

                officialName = "Universidade Presbiteriana Mackenzie";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.mackenzie.br", "@mackenzie.br");

                officialName = "The University of New South Wales";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.unsw.edu.au", "@unsw.edu.au");

                officialName = "University of Minnesota";
                EnsureEstablishment(officialName, true, null, GetUniversity(), "www.umn.edu", "@umn.edu");

                officialName = "The College Board";
                EnsureEstablishment(officialName, true, null, GetGenericBusiness(), "www.collegeboard.org", "@collegeboard.org");

                officialName = "Institute of International Education (IIE)";
                EnsureEstablishment(officialName, true, null, GetAssociation(), "www.iie.org", "@iie.org");

                officialName = "Terra Dotta, LLC";
                EnsureEstablishment(officialName, true, null, GetGenericBusiness(), "www.terradotta.com", "@terradotta.com");

                #endregion
                #region Agreement Institutions

                officialName = "Jinan University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.jnu.edu.cn", null);

                officialName = "Swinburne University of Technology";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.swinburne.edu.au", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "www.swin.edu.au",
                        TranslationToHint = "x-url",
                    }
                }
                );

                officialName = "Fachhochschule Nordwestschweiz";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.fhnw.ch", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "University of Applied Sciences Northwestern Switzerland",
                        TranslationToHint = "en",
                    },
                    new EstablishmentName
                    {
                        Text = "Fachhochschule Beider Basel",
                        TranslationToHint = "en",
                        IsFormerName = true,
                    },
                    new EstablishmentName
                    {
                        Text = "www.fhbb.ch",
                        TranslationToHint = "x-url",
                        IsFormerName = true,
                    },
                }
                );

                officialName = "Johannes Kepler Universität Linz";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.jku.at", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Johannes Kepler Universitat Linz",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "Johannes Kepler University Linz",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Université catholique de Louvain";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uclouvain.be", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universite catholique de Louvain",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "Catholic University of Louvain",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade Federal do Paraná";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ufpr.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universidade Federal do Parana",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "Federal University of Parana",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade Federal do Rio de Janeiro";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ufrj.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Federal University of Rio de Janeiro",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "University of Florida";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ufl.edu", null);

                officialName = "Instituto de Pesquisa e Planejamento Urbano de Curitiba";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ippuc.org.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Institute for Research and Urban Planning Curtiba",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade Positivo";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.up.com.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Positive University",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade de São Paulo";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.usp.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universidade de Sao Paulo",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "University of Sao Paulo",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidad del Desarrollo";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.udd.cl", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "University of Development",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidad Nacional del Nordeste";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.unne.edu.ar", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Northeast National University",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidad de Flores";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uflo.edu.ar", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "University of Flores",
                        TranslationToHint = "en",
                    },
                    new EstablishmentName
                    {
                        Text = "universidad.uflo.edu.ar",
                        TranslationToHint = "x-url",
                    },
                }
                );

                officialName = "Universidade Federal de Goiás";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ufg.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universidade Federal de Goias",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "Federal University of Goias",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Fundação Getúlio Vargas";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.fgv.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Fundacao Getulio Vargas",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "portal.fgv.br",
                        TranslationToHint = "x-url",
                    },
                    new EstablishmentName
                    {
                        Text = "Getulio Vargas Foundation",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade Estadual do Ceara";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uece.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Ceara State University",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidade Estadual Paulista";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.unesp.br", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Paulista State University",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Université du Québec à Montréal";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uqam.ca", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universite du Quebec a Montreal",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "University of Quebec at Montreal",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidad de Artes, Ciencias y Comunicación";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uniacc.cl", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "Universidad de Artes, Ciencias y Comunicacion",
                        TranslationToHint = "x-ascii",
                    },
                    new EstablishmentName
                    {
                        Text = "University of Arts, Sciences, and Communication",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Universidad de Santiago de Chile";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.usach.cl", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "University of Santiago Chile",
                        TranslationToHint = "en",
                    },
                }
                );

                officialName = "Capital Normal University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.cnu.edu.cn", null);

                officialName = "Chang'an University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.xahu.edu.cn", null);

                officialName = "The China Conservatory";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ccmusic.edu.cn", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "China Conservatory of Music",
                        TranslationToHint = "en",
                        IsFormerName = true,
                    },
                }
                );

                officialName = "Dalian Jiaotong University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.djtu.edu.cn", null);

                officialName = "East China Jiaotong University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.ecjtu.jx.cn", null);

                officialName = "Environmental Management College of China";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.emcc.cn", null);

                officialName = "Guangxi University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.gxu.edu.cn", null);

                officialName = "Guangxi University of Technology";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.gxut.edu.cn", null);

                officialName = "Guilin University of Technology";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.glut.edu.cn", null,
                    new List<EstablishmentName>
                {
                    new EstablishmentName
                    {
                        Text = "www.glite.edu.cn",
                        TranslationToHint = "x-url",
                        IsFormerName = true,
                    },
                }
                );

                officialName = "Hebei University of Technology";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.hebut.edu.cn", null);

                officialName = "Chinese Academy of Sciences";
                var cas = EnsureEstablishment(officialName, false, null, GetUniversity(), "www.cas.cn", null);

                officialName = "Institute of Psychology, Chinese Academy of Sciences";
                EnsureEstablishment(officialName, false, cas, GetUniversity(), "www.psych.cas.cn", null);

                officialName = "Liaoning Normal University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.lnnu.edu.cn", null);

                officialName = "Nankai University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.nankai.edu.cn", null);

                officialName = "Shandong Province";
                EnsureEstablishment(officialName, false, null, GetGovernmentAdministration(), "www.sd.gov.cn", null);

                officialName = "Shandong University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.sdu.edu.cn", null);

                officialName = "Shanghai Academy of Environmental Sciences";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.saes.sh.cn", null);

                officialName = "Shanghai Jiao Tong University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.sjtu.edu.cn", null);

                officialName = "Soochow University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.suda.edu.cn", null);

                officialName = "South China Normal University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.scnu.edu.cn", null);

                officialName = "Southwestern University of Finance and Economics";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.swufe.edu.cn", null);

                officialName = "Sun Yat-Sen University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.sysu.edu.cn", null);

                officialName = "Tongji University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.tongji.edu.cn", null);

                officialName = "Tsinghua University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.tsinghua.edu.cn", null);

                officialName = "Xian International Studies University";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.xisu.edu.cn", null);

                officialName = "Universidad Tecnológica de Bolívar";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.unitecnologica.edu.co", null,
                    new List<EstablishmentName>
                    {
                        new EstablishmentName
                        {
                            Text = "Universidad Tecnologica de Bolivar",
                            TranslationToHint = "x-ascii",
                        },
                        new EstablishmentName
                        {
                            Text = "Technology University of Bolivar",
                            TranslationToHint = "en",
                        },
                    }
                );

                officialName = "Universidad del Atlántico";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.uniatlantico.edu.co", null,
                    new List<EstablishmentName>
                    {
                        new EstablishmentName
                        {
                            Text = "Universidad del Atlantico",
                            TranslationToHint = "x-ascii",
                        },
                        new EstablishmentName
                        {
                            Text = "Atlantic University",
                            TranslationToHint = "en",
                        },
                    }
                );

                officialName = "Universidad del Valle";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.univalle.edu.co", null,
                    new List<EstablishmentName>
                    {
                        new EstablishmentName
                        {
                            Text = "Valle University",
                            TranslationToHint = "en",
                        },
                    }
                );

                officialName = "Arcada";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.arcada.fi", null);

                officialName = "Tekniska Läroverkets Kamratförbund r.f";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.tlk.fi", null,
                    new List<EstablishmentName>
                    {
                        new EstablishmentName
                        {
                            Text = "Tekniska Laroverkets Kamratforbund r.f",
                            TranslationToHint = "x-ascii",
                        },
                        new EstablishmentName
                        {
                            Text = "Swedish Institute of Technology",
                            TranslationToHint = "en",
                            IsFormerName = true,
                        },
                    }
                );

                officialName = "Audencia Nantes School of Management";
                EnsureEstablishment(officialName, false, null, GetUniversity(), "www.audencia.com", null,
                    new List<EstablishmentName>
                    {
                        new EstablishmentName
                        {
                            Text = "Ecole Supérieure de Commerce de Nantes",
                            TranslationToHint = "fr",
                            IsFormerName = true,
                        },
                        new EstablishmentName
                        {
                            Text = "Ecole Superieure de Commerce de Nantes",
                            TranslationToHint = "x-ascii",
                            IsFormerName = true,
                        },
                    }
                );
                #endregion
            }
        }
    }
}