// log ajax errors (global)
//$(function () {
//    $(document).ajaxError(function (e, jqXhr, settings, exception) {
//        if (jqXhr.readyState === 0 || jqXhr.status === 0) {
//            return; // user clicked link or browser control
//        }
//        var message = $('body:first').data('app-ajax-error-log-message');
//        if (message)
//            alert(message);
//        var url = $('body:first').data('app-ajax-error-log-url');
//        if (url) {
//            var args = {
//                ReadyState: (jqXhr) ? jqXhr.readyState : null,
//                Status: (jqXhr) ? jqXhr.status : null,
//                Url: settings.url,
//                ErrorThrown: exception
//            };
//            if (args.ReadyState || args.Status || args.Url || args.ErrorThrown) {
//                $.ajax({
//                    url: url,
//                    dataType: 'json',
//                    data: args
//                });
//            }
//        }
//    });
//});

//// file input change event in IE < 9
//$(function () {
//    if ($.browser.msie == false || parseInt($.browser.version) > 8)
//        return;
//    $('input[type=file]').live('click', function () {
//        var self = this;
//        var blur = function () {
//            $(self).blur();
//        };
//        setTimeout(blur, 0);
//    });
//});

//// focus input elements
//$(function () {
//    $('input[data-app-focus="true"]').each(function () {
//        $(this).focus();
//    });
//});

// disable form submit buttons during postback
//$(function () {
//    $('form').on('invalid-form.validate', function () {
//        //$('form').each(function () {
//            $(this).find('[data-app-form-submitting-icon="true"]').hide();
//            var button = $(this).find('input[type="submit"][data-app-form-submit-button="true"]');
//            setTimeout(function () {
//                button.removeAttr('disabled');
//            }, 1);
//        //});
//    });
//    $(document).on('submit', 'form', function () {
//        $(this).find('[data-app-form-submitting-icon="true"]').show();
//        //var button = $(this).find('input[type="submit"][data-app-form-submit-button="true"]');
//        var button = $(this).find('input[type="submit"]');
//        setTimeout(function () {
//            button.attr('disabled', 'disabled');
//        }, 0);
//    });
//});

// sign up eligibility button
//$(function () {
//    $('[data-app-sign-up-eligibility-url] input[type="button"]').click(function () {
//        var button = $(this);
//        var container = $(this).parents('.check-eligibility');
//        var form = $(this).parents('form');
//        var wait = container.find('img.wait');
//        var eligible = container.find('img.eligible');
//        var url = container.data('app-sign-up-eligibility-url');
//        var email = form.find('input[data-val-remote-url="' + url + '"]');
//        var message = form.find('p.eligible');
//        eligible.hide();
//        wait.show();
//        var args = {
//            emailAddress: email.val()
//        };
//        $.ajax({
//            url: url,
//            type: 'POST',
//            dataType: 'json',
//            data: args,
//            complete: function () {
//                form.valid();
//                wait.hide();
//            },
//            success: function (data) {
//                if (data === true && args.emailAddress) {
//                    eligible.show();
//                    message.slideDown(200);
//                    var changedEmail = function () {
//                        message.slideUp(200);
//                        eligible.hide();
//                    };
//                    email.one('keydown', changedEmail);
//                    email.one('change', changedEmail);
//                    button.one('click', changedEmail);
//                } else {
//                    email.focus();
//                    email.select();
//                }
//            }
//        });
//        return false;
//    });
//});

//// google maps unobtrusive initialization
//$(function () {
//    var google = window.google;
//    $('[data-app-map="google"]').each(function () {
//        // initialize map
//        var jMap = $(this),
//            mapId = $(this).attr('id'),
//            mapOptions = { zoom: getZoom() || 1 };
//        mapOptions.center = new google.maps.LatLng(getCenterLat() || 0, getCenterLng() || 0);
//        mapOptions.mapTypeId = google.maps.MapTypeId[getMapType() || 'ROADMAP'];
//        if (hasNoScrollWheel()) mapOptions.scrollwheel = false;
//        var gMap = new google.maps.Map(getCanvas()[0], mapOptions);
//        window[mapId] = gMap;
//        window[mapId].markers = [];
//
//        // fit bounds when zoom is unknown
//        if (!hasZoom() && hasBoundingBox()) {
//            var boundingBox = getBoundingBox();
//            var northEast = new google.maps.LatLng(boundingBox.north, boundingBox.east);
//            var southWest = new google.maps.LatLng(boundingBox.south, boundingBox.west);
//            gMap.fitBounds(new google.maps.LatLngBounds(southWest, northEast));
//        }
//
//        // add first tilesloaded listener
//        var tilesFirstLoaded = function () {
//            setZoom(gMap.getZoom());
//            // re-center map if zoom was derived from bounds
//            if (!hasZoom() && hasBoundingBox() && hasCenter())
//                gMap.setCenter(new google.maps.LatLng(getCenterLat(), getCenterLng()));
//
//            // set up tools
//            if (hasTools()) {
//                function toolsControl(map, div) {
//                    this.setMap(map);
//                    this.node_ = div;
//                    map.controls[google.maps.ControlPosition.TOP_LEFT].push(div);
//                }
//                toolsControl.prototype = new google.maps.OverlayView();
//                toolsControl.prototype.draw = function () { };
//                toolsControl.prototype.getAddMarkerLatLng = function () {
//                    var point = {
//                        x: $(this.node_).position().left + ($(this.node_).outerWidth() / 2),
//                        y: $(this.node_).outerHeight()
//                    };
//                    var projection = this.getProjection();
//                    return projection.fromContainerPixelToLatLng(point);
//                };
//                var tools = new toolsControl(gMap, getTools()[0]);
//                if (gMap.markers.length == 0) {
//                    getAddMarkerTool().show();
//                    getRemoveMarkerTool().hide();
//                }
//                else {
//                    getAddMarkerTool().hide();
//                    getRemoveMarkerTool().show();
//                }
//                getTools().show();
//
//                // add new marker
//                var marker, mapMouseMoveListener, mapClickListener;
//                getAddMarkerTool().on('click', function () {
//                    getAddMarkerTool().toggle();
//                    getRemoveMarkerTool().toggle();
//
//                    var addLatLng = tools.getAddMarkerLatLng();
//                    var markerOptions = {
//                        map: gMap,
//                        position: addLatLng,
//                        cursor: 'pointer'
//                    };
//                    markerOptions.clickable = false;
//                    markerOptions.icon = new google.maps.MarkerImage(
//                        jMap.find('.new-marker').data('app-img-src'),
//                            new google.maps.Size(52, 61),
//                            new google.maps.Point(0, 0),
//                            new google.maps.Point(10, 10)
//                        );
//                    marker = new google.maps.Marker(markerOptions);
//                    gMap.setOptions({ draggableCursor: 'pointer' });
//                    mapMouseMoveListener = google.maps.event.addListener(gMap, 'mousemove', function (e) {
//                        marker.setPosition(e.latLng);
//                    });
//                    mapClickListener = google.maps.event.addListener(gMap, 'click', function (e) {
//                        google.maps.event.removeListener(mapMouseMoveListener);
//                        mapMouseMoveListener = undefined;
//                        google.maps.event.removeListener(mapClickListener);
//                        mapClickListener = undefined;
//                        gMap.setOptions({ draggableCursor: null });
//                        marker.setMap(null);
//                        marker = undefined;
//
//                        var overlayView = new google.maps.OverlayView();
//                        overlayView.draw = function () { };
//                        overlayView.setMap(gMap);
//                        var pixels = overlayView.getProjection().fromLatLngToContainerPixel(e.latLng);
//                        pixels.y = pixels.y + 43;
//                        e.latLng = overlayView.getProjection().fromContainerPixelToLatLng(pixels);
//
//                        jMap.find('.markers .marker:first .lat').data('app-value', e.latLng.lat());
//                        jMap.find('.markers .marker:first .lng').data('app-value', e.latLng.lng());
//                        jMap.find('.markers .marker').each(parseMarker);
//                        google.maps.event.trigger(gMap, 'marker_created', { marker: gMap.markers[0] });
//                    });
//                });
//
//                // remove existing marker
//                getRemoveMarkerTool().on('click', function () {
//                    getAddMarkerTool().toggle();
//                    getRemoveMarkerTool().toggle();
//
//                    if (gMap.markers.length == 1) {
//                        marker = gMap.markers[0];
//                        google.maps.event.removeListener(marker.dragListener);
//                        google.maps.event.removeListener(marker.dragEndListener);
//                        marker.dragListener = undefined;
//                        marker.dragEndListener = undefined;
//                        gMap.markers.splice(0, 1);
//                    }
//                    else if (marker) {
//                        google.maps.event.removeListener(mapMouseMoveListener);
//                        mapMouseMoveListener = undefined;
//                        google.maps.event.removeListener(mapClickListener);
//                        mapClickListener = undefined;
//                        var markerOptions = {};
//                        markerOptions.draggableCursor = null;
//                        gMap.setOptions(markerOptions);
//                    }
//                    marker.setMap(null);
//                    marker = undefined;
//                    google.maps.event.trigger(gMap, 'marker_destroyed');
//                });
//            }
//        };
//        google.maps.event.addListenerOnce(gMap, 'tilesloaded', tilesFirstLoaded);
//
//        // add zoom_changed function & listener
//        var zoomChanged = function () {
//            setZoom(gMap.getZoom());
//        };
//        google.maps.event.addListener(gMap, 'zoom_changed', zoomChanged);
//
//        // add bounds_changed function & listener
//        var boundsChanged = function () {
//            setBoundingBox(gMap.getBounds());
//        };
//        google.maps.event.addListener(gMap, 'bounds_changed', boundsChanged);
//
//        // add markers
//        var parseMarker = function () {
//            var jMarker = $(this);
//            if (hasLatLng()) {
//                var markerOptions = {
//                    map: gMap,
//                    position: new google.maps.LatLng(getLatLng().lat, getLatLng().lng)
//                };
//
//                if (isDraggable()) markerOptions.draggable = true;
//                var marker = new google.maps.Marker(markerOptions);
//
//                if (markerOptions.draggable) {
//                    var markerDrag = function () {
//                        setLatLng(marker.getPosition());
//                        google.maps.event.trigger(gMap, 'marker_moving', { marker: marker });
//                    };
//                    var markerDragEnd = function () {
//                        setLatLng(marker.getPosition());
//                        google.maps.event.trigger(gMap, 'marker_moved', { marker: marker });
//                    };
//                    marker.dragListener = google.maps.event.addListener(marker, 'drag', markerDrag);
//                    marker.dragEndListener = google.maps.event.addListener(marker, 'dragend', markerDragEnd);
//                }
//
//                gMap.markers.push(marker);
//                setLatLng(marker.getPosition());
//            }
//
//            // get / is draggable
//            function getDraggable() {
//                return jMarker.data('app-draggable');
//            }
//            function isDraggable() {
//                var draggable = getDraggable();
//                return (draggable === true || (draggable && draggable.toLowerCase() === 'true'));
//            }
//
//            // get / set / has lat / lng
//            function getLatLng() {
//                return {
//                    lat: jMarker.find('.lat').data('app-value'),
//                    lng: jMarker.find('.lng').data('app-value')
//                };
//            }
//            function hasLatLng() {
//                var latLng = getLatLng();
//                return (typeof (latLng.lat) === 'number'
//                        && typeof (latLng.lng) == 'number');
//            }
//            function setLatLng(latLng) {
//                var latInput = jMarker.find('.lat').data('app-input-id');
//                if (latInput) $('#' + latInput).val(latLng.lat());
//                var lngInput = jMarker.find('.lng').data('app-input-id');
//                if (lngInput) $('#' + lngInput).val(latLng.lng());
//            }
//        };
//        $(this).find('.markers .marker').each(parseMarker);
//
//        // get canvas & map type
//        function getCanvas() {
//            return jMap.find('.canvas');
//        }
//        function getMapType() {
//            return jMap.data('app-map-type');
//        }
//
//        // get / has center
//        function getCenterLat() {
//            return jMap.find('.center .lat').data('app-value');
//        }
//        function getCenterLng() {
//            return jMap.find('.center .lng').data('app-value');
//        }
//        function hasCenter() {
//            return (typeof (getCenterLat()) === 'number'
//                    && typeof (getCenterLng()) == 'number');
//        }
//
//        // get / set / has zoom
//        function getZoom() {
//            return jMap.find('.zoom').data('app-value');
//        }
//        function setZoom(zoom) {
//            var zoomInput = jMap.find('.zoom').data('app-input-id');
//            if (zoomInput) $('#' + zoomInput).val(zoom);
//        }
//        function hasZoom() {
//            return typeof (getZoom()) === 'number';
//        }
//
//        // get / set / has bounding box
//        function getBoundingBox() {
//            return {
//                north: jMap.find('.box .north').data('app-value'),
//                south: jMap.find('.box .south').data('app-value'),
//                east: jMap.find('.box .east').data('app-value'),
//                west: jMap.find('.box .west').data('app-value')
//            };
//        }
//        function hasBoundingBox() {
//            var box = getBoundingBox();
//            return (typeof (box.north) === 'number'
//                    && typeof (box.south) === 'number'
//                    && typeof (box.east) === 'number'
//                    && typeof (box.west) === 'number');
//        }
//        function setBoundingBox(latLngBounds) {
//            var northInput = jMap.find('.box .north').data('app-input-id');
//            if (northInput) $('#' + northInput).val(latLngBounds.getNorthEast().lat());
//            var southInput = jMap.find('.box .south').data('app-input-id');
//            if (southInput) $('#' + southInput).val(latLngBounds.getSouthWest().lat());
//            var eastInput = jMap.find('.box .east').data('app-input-id');
//            if (eastInput) $('#' + eastInput).val(latLngBounds.getNorthEast().lng());
//            var westInput = jMap.find('.box .west').data('app-input-id');
//            if (westInput) $('#' + westInput).val(latLngBounds.getSouthWest().lng());
//        }
//
//        // get / has scroll wheel
//        function getScrollWheel() {
//            return jMap.find('.scroll-wheel').data('app-value');
//        }
//        function hasNoScrollWheel() {
//            var sw = getScrollWheel();
//            return (sw === false || (sw && sw.toLowerCase() === 'false'));
//        }
//
//        // get / has tools
//        function getTools() {
//            return jMap.find('.tools');
//        }
//        function hasTools() {
//            return getTools().length == 1;
//        }
//        function getAddMarkerTool() {
//            return jMap.find('.tools .add-marker');
//        }
//        function getRemoveMarkerTool() {
//            return jMap.find('.tools .remove-marker');
//        }
//    });
//});
//
//// password meter unobtrusive initialization
//$(function () {
//    $('input[data-app-pwd-meter="true"]:first').each(function () {
//        var url = $('#pwdMeter:first').data('app-pwd-meter-script-url');
//        var minLength = $(this).data('app-pwd-meter-min-length');
//        var displayGenerate = $(this).data('app-pwd-meter-display-generate');
//        if (url) {
//            $.ajaxSetup({ async: false });
//            $.getScript(url);
//            $.ajaxSetup({ async: true });
//            $(this).pwdMeter({
//                minLength: minLength ? parseInt(minLength) : 6,
//                displayGeneratePassword: displayGenerate
//            });
//        }
//    });
//});

