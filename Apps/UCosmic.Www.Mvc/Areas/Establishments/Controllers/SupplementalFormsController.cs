using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using NGeo.Yahoo.PlaceFinder;
using ServiceLocatorPattern;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Establishments.Controllers
{
    public partial class SupplementalFormsController : BaseController
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IConsumePlaceFinder _placeFinder;
        private readonly IHandleCommands<UpdateEstablishment> _updateEstablishment;
        private readonly IUnitOfWork _unitOfWork;

        public SupplementalFormsController(IProcessQueries queryProcessor
            , IConsumePlaceFinder placeFinder
            , IHandleCommands<UpdateEstablishment> updateEstablishment
            , IUnitOfWork unitOfWork
        )
        {
            _queryProcessor = queryProcessor;
            _placeFinder = placeFinder;
            _updateEstablishment = updateEstablishment;
            _unitOfWork = unitOfWork;
        }

        [ActionName("locate")]
        public virtual ActionResult Locate(Guid establishmentId, string returnUrl)
        {
            if (establishmentId != Guid.Empty)
            {
                var establishment = _queryProcessor.Execute(new EstablishmentByGuid(establishmentId)
                {
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.Location,
                    }
                });
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
                var establishment = _queryProcessor.Execute(new EstablishmentByGuid(model.EntityId)
                {
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.Location,
                    }
                });
                if (establishment != null)
                {
                    var command = new UpdateEstablishment
                    {
                        Id = establishment.RevisionId,
                        GoogleMapZoomLevel = model.Location.GoogleMapZoomLevel,
                        CenterLatitude = model.Location.CenterLatitude,
                        CenterLongitude = model.Location.CenterLongitude,
                        NorthLatitude = model.Location.BoundingBoxNortheastLatitude,
                        EastLongitude = model.Location.BoundingBoxNortheastLongitude,
                        SouthLatitude = model.Location.BoundingBoxSouthwestLatitude,
                        WestLongitude = model.Location.BoundingBoxSouthwestLongitude,
                    };
                    var oldCenter = establishment.Location.Center;
                    //establishment.Location.GoogleMapZoomLevel = model.Location.GoogleMapZoomLevel;
                    //establishment.Location.Center = new Coordinates
                    //{
                    //    Latitude = model.Location.CenterLatitude,
                    //    Longitude = model.Location.CenterLongitude
                    //};
                    //establishment.Location.BoundingBox = new BoundingBox
                    //{
                    //    Northeast = new Coordinates
                    //    {
                    //        Latitude = model.Location.BoundingBoxNortheastLatitude,
                    //        Longitude = model.Location.BoundingBoxNortheastLongitude,
                    //    },
                    //    Southwest = new Coordinates
                    //    {
                    //        Latitude = model.Location.BoundingBoxSouthwestLatitude,
                    //        Longitude = model.Location.BoundingBoxSouthwestLongitude,
                    //    },
                    //};
                    //_establishments.UnitOfWork.SaveChanges();
                    //_objectCommander.Update(establishment, true);
                    //_entities.Update(establishment);
                    _updateEstablishment.Handle(command);
                    _unitOfWork.SaveChanges();
                    if (!oldCenter.HasValue)
                    {
                        var builder = new SupplementalLocationPlacesBuilder(establishment.RevisionId);
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
                    //var place = _placeFactory.FromWoeId(woeId.Value);
                    var place = _queryProcessor.Execute(
                        new GetPlaceByWoeIdQuery
                        {
                            WoeId = woeId.Value,
                        });
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
        private readonly int _establishmentId;
        private IProcessQueries _queryProcessor;
        private IHandleCommands<UpdateEstablishment> _updateEstablishment;
        private IUnitOfWork _unitOfWork;
        private IConsumePlaceFinder _placeFinder;

        public SupplementalLocationPlacesBuilder(int establishmentId)
        {
            _establishmentId = establishmentId;
        }

        public void Build()
        {
            Build(0);
        }

        private void Build(int retryCount)
        {
            _queryProcessor = ServiceProviderLocator.Current.GetService<IProcessQueries>();
            _updateEstablishment = ServiceProviderLocator.Current.GetService<IHandleCommands<UpdateEstablishment>>();
            _unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();
            _placeFinder = ServiceProviderLocator.Current.GetService<IConsumePlaceFinder>();
            try
            {
                var establishment = _queryProcessor.Execute(new GetEstablishmentByIdQuery(_establishmentId));
                if (!establishment.Location.Center.HasValue) return;

                var latitude = establishment.Location.Center.Latitude;
                var longitude = establishment.Location.Center.Longitude;
                if (!latitude.HasValue || !longitude.HasValue) return;

                var result = _placeFinder.Find(new PlaceByCoordinates(latitude.Value, longitude.Value)).SingleOrDefault();
                if (result == null) return;
                if (!result.WoeId.HasValue)
                {
                    throw new NotSupportedException(string.Format(
                        "Could not find WOE ID for coordinates {0},{1}", latitude, longitude));
                }
                //var place = _placeFactory.FromWoeId(result.WoeId.Value);
                var place = _queryProcessor.Execute(
                    new GetPlaceByWoeIdQuery
                    {
                        WoeId = result.WoeId.Value,
                    });
                var places = place.Ancestors.OrderByDescending(n => n.Separation).Select(a => a.Ancestor).ToList();
                places.Add(place);
                var command = new UpdateEstablishment
                {
                    Id = establishment.RevisionId,
                    PlaceIds = places.Select(p => p.RevisionId).ToArray(),
                };
                _updateEstablishment.Handle(command);
                //_establishment.Location.Places.Clear();
                //_establishment.Location.Places = places;
                //_objectCommander.Update(_establishment, true);
                //_entities.Update(_establishment);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var exceptionLogger = ServiceProviderLocator.Current.GetService<ILogExceptions>();
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

    public static class SupplementalFormsRouter
    {
        private static readonly string Area = MVC.Establishments.Name;
        private static readonly string Controller = MVC.Establishments.SupplementalForms.Name;

        public class FindPlacesRoute : MvcRoute
        {
            public FindPlacesRoute()
            {
                Url = "establishments/new/location/places";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.SupplementalForms.ActionNames.FindPlaces,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class LocateGetRoute : MvcRoute
        {
            public LocateGetRoute()
            {
                Url = "establishments/{establishmentId}/locate/then-return-to/{*returnUrl}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.SupplementalForms.ActionNames.Locate,
                });
                Constraints = new RouteValueDictionary(new
                {
                    establishmentId = new NonEmptyGuidRouteConstraint(),
                    returnUrl = new RequiredIfPresentRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class LocateGetWithoutReturnUrlRoute : LocateGetRoute
        {
            public LocateGetWithoutReturnUrlRoute()
            {
                Url = "establishments/{establishmentId}/locate";
                Constraints = new RouteValueDictionary(new
                {
                    establishmentId = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class LocatePostRoute : MvcRoute
        {
            public LocatePostRoute()
            {
                Url = "establishments/locate";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.SupplementalForms.ActionNames.Locate,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST")
                });
            }
        }
    }
}
