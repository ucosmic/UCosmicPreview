using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using NGeo.Yahoo.PlaceFinder;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Impl;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;
using UCosmic.Www.Mvc.Controllers;
using BoundingBox = UCosmic.Domain.Places.BoundingBox;

namespace UCosmic.Www.Mvc.Areas.Establishments.Controllers
{
    public partial class SupplementalFormsController : BaseController
    {
        private readonly IConsumePlaceFinder _placeFinder;
        private readonly PlaceFactory _placeFactory;
        private readonly EstablishmentFinder _establishmentFinder;
        private readonly ICommandObjects _objectCommander;

        public SupplementalFormsController(IConsumePlaceFinder placeFinder, IConsumeGeoNames geoNames, 
            IConsumeGeoPlanet geoPlanet, IQueryEntities entityQueries, ICommandObjects objectCommander, 
            IManageConfigurations config)
        {
            _establishmentFinder = new EstablishmentFinder(entityQueries);
            _objectCommander = objectCommander;
            _placeFinder = placeFinder;
            _placeFactory = new PlaceFactory(entityQueries, objectCommander, geoPlanet, geoNames, config);
        }

        [ActionName("locate")]
        public virtual ActionResult Locate(Guid establishmentId, string returnUrl)
        {
            if (establishmentId != Guid.Empty)
            {
                var establishment = _establishmentFinder
                    .FindOne(By<Establishment>.EntityId(establishmentId)
                        .EagerLoad(e => e.Location)
                    );
                if (establishment != null)
                {
                    var model = Mapper.Map<EstablishmentForm>(establishment);
                    model.ReturnUrl = returnUrl;
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("locate")]
        public virtual ActionResult Locate(EstablishmentForm model)
        {
            if (model != null)
            {
                var establishment = _establishmentFinder.FindOne(By<Establishment>.EntityId(model.EntityId)
                    .EagerLoad(e => e.Location)
                    .ForInsertOrUpdate()
                );
                if (establishment != null)
                {
                    var oldCenter = establishment.Location.Center;
                    establishment.Location.GoogleMapZoomLevel = model.Location.GoogleMapZoomLevel;
                    establishment.Location.Center = new Coordinates
                    {
                        Latitude = model.Location.CenterLatitude,
                        Longitude = model.Location.CenterLongitude
                    };
                    establishment.Location.BoundingBox = new BoundingBox
                    {
                        Northeast = new Coordinates
                        {
                            Latitude = model.Location.BoundingBoxNortheastLatitude,
                            Longitude = model.Location.BoundingBoxNortheastLongitude,
                        },
                        Southwest = new Coordinates
                        {
                            Latitude = model.Location.BoundingBoxSouthwestLatitude,
                            Longitude = model.Location.BoundingBoxSouthwestLongitude,
                        },
                    };
                    //_establishments.UnitOfWork.SaveChanges();
                    _objectCommander.Update(establishment, true);
                    if (!oldCenter.HasValue)
                    {
                        var builder = new SupplementalLocationPlacesBuilder(
                            establishment, _placeFinder, _placeFactory, _objectCommander);
                        var thread = new Thread(builder.Build);
                        thread.Start();
                    }
                    SetFeedbackMessage(string.Format("Successfully located {0}", establishment.TranslatedName));
                    return Redirect(string.Format("/{0}", model.ReturnUrl));
                }
            }
            return HttpNotFound();
        }

        [HttpGet]
        [ActionName("find-places")]
        [Authorize(Users = "ludwigd1@uc.edu,ajith_i@uc.edu,sodhiha1@uc.edu,ganesh_c@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual PartialViewResult FindPlaces(double latitude, double longitude)
        {
            var results = _placeFinder.Find(new PlaceByCoordinates(latitude, longitude))
                .Where(y => y.WoeId != null).ToList();
            if (results.Count == 1)
            {
                var woeId = results.Single().WoeId;
                if (woeId != null)
                {
                    var place = _placeFactory.FromWoeId(woeId.Value);
                    var places = place.Ancestors.OrderByDescending(n => n.Separation)
                        .Select(n => n.Ancestor).ToList();
                    places.Add(place);
                    var models = Mapper.Map<Collection<EstablishmentForm.LocationForm.EstablishmentPlaceForm>>(places);
                    return PartialView(GetEditorTemplateViewName(Area, SharedName, 
                        MVC.Establishments.Shared.Views.EditorTemplates.EstablishmentPlacesForm), models);
                }
            }
            return null;
        }

    }

    public class SupplementalLocationPlacesBuilder
    {
        private readonly Establishment _establishment;
        private readonly ICommandObjects _objectCommander;
        private readonly IConsumePlaceFinder _placeFinder;
        private readonly PlaceFactory _placeFactory;

        public SupplementalLocationPlacesBuilder(Establishment establishment, IConsumePlaceFinder placeFinder,
            PlaceFactory placeFactory, ICommandObjects objectCommander)
        {
            _establishment = establishment;
            _objectCommander = objectCommander;
            _placeFinder = placeFinder;
            _placeFactory = placeFactory;
        }

        public void Build()
        {
            Build(0);
        }

        private void Build(int retryCount)
        {
            try
            {
                if (!_establishment.Location.Center.HasValue) return;

                var latitude = _establishment.Location.Center.Latitude;
                var longitude = _establishment.Location.Center.Longitude;
                if (!latitude.HasValue || !longitude.HasValue) return;

                var result = _placeFinder.Find(new PlaceByCoordinates(latitude.Value, longitude.Value)).SingleOrDefault();
                if (result == null) return;
                if (!result.WoeId.HasValue)
                {
                    throw new NotSupportedException(string.Format(
                        "Could not find WOE ID for coordinates {0},{1}", latitude, longitude));
                }
                var place = _placeFactory.FromWoeId(result.WoeId.Value);
                var places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                _establishment.Location.Places.Clear();
                _establishment.Location.Places = places;
                _objectCommander.Update(_establishment, true);
            }
            catch (Exception ex)
            {
                var exceptionLogger = DependencyInjector.Current.GetService<ILogExceptions>();
                exceptionLogger.LogException(ex);

                if (ex is NotSupportedException)
                {
                    retryCount = 3;
                }

                if (retryCount < 2)
                {
                    Build(++retryCount);
                }
            }
        }
    }
}
