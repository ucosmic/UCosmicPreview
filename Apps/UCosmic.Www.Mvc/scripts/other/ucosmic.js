$(function ($) {
    $(document).ajaxError(function (e, jqXhr, settings, exception) {
        if (jqXhr.readyState === 0 || jqXhr.status === 0) {
            return; // user clicked link or browser control
        }
        var config = $('body:first');
        if (jqXhr.status === 401) {
            // unauthorized, need to sign on
            window.location.href = config.data('ucosmic-sign-on-url');
            return;
        }
        var message = config.data('ucosmic-ajax-error-log-message');
        if (message)
            alert(message);
        var url = config.data('ucosmic-ajax-error-log-url');
        if (url) {
            var args = {
                ReadyState: (jqXhr) ? jqXhr.readyState : null,
                Status: (jqXhr) ? jqXhr.status : null,
                Url: settings.url,
                ErrorThrown: exception
            };
            if (args.ReadyState || args.Status || args.Url || args.ErrorThrown) {
                $.ajax({
                    url: url,
                    dataType: 'json',
                    data: args
                });
            }
        }
    });
} (jQuery));

(function ($) {
    $.ucosmic = {
        unobtrusive: {},
        obtruders: {
            ux: {}
        },
        maps: []
    };

    // apply obtrusive behaviors
    $.ucosmic.obtrude = function (selector, obtruders) {
        var obtruder;
        obtruders = obtruders || $.ucosmic.obtruders;
        for (obtruder in obtruders) { // execute every registered obtruder
            if (obtruders.hasOwnProperty(obtruder)) { // skip any inherited members

                // apply an unobtrusive behavior
                if (typeof obtruders[obtruder] === 'function') {
                    obtruders[obtruder].apply(this,
                        Array.prototype.slice.call(arguments, 0, 1) || document);
                }

                // apply all unobtrusive behaviors in set
                if (typeof obtruders[obtruder] === 'object') {
                    $.ucosmic.obtrude(selector, obtruders[obtruder]);
                }
            }
        }
    };

    // apply calendar to date elements
    $.extend($.ucosmic.obtruders.ux, {
        datepicker: function (selector) {
            $(selector).find('[data-ucosmic-cal=datepicker]').datepicker();
        }
    });

    // give element focus
    $.extend($.ucosmic.obtruders.ux, {
        focus: function (selector) {
            $(selector).find('[data-ucosmic-focus=true]').focus();
        }
    });

    // trim text input
    $.extend($.ucosmic.obtruders.ux, {
        trimTextInput: function (selector) {
            $(selector).find('[data-ucosmic-trim-input=true]').each(function () {
                var target = $(this).find('input[type=text]:first');
                target.on('blur', function () {
                    $(this).val($.trim($(this).val()));
                });
            });
        }
    });

    // pwd-meter
    $.extend($.ucosmic.obtruders.ux, {
        pwdMeter: function (selector) {
            $(selector).find('input[data-ucosmic-pwd-meter=true]:first').each(function () {
                $(this).pwdMeter({
                    prefix: 'Password strength: ',
                    minLength: 6
                });
            });
        }
    });

    // watermark text box
    $.extend($.ucosmic.obtruders.ux, {
        watermark: function (selector) {
            $(selector).find('[data-ucosmic-watermark=true]').each(function () {
                var text = $(this).data('ucosmic-watermark-text'),
                cssClass = $(this).data('ucosmic-watermark-class'),
                focusCssClass = $(this).data('ucosmic-watermark-focused-class'),
                watermarked = $(this).find(':input:first');
                var watermark = function () {
                    watermarker.hide();
                    if (!watermarked.val()) {
                        watermarker.show();
                        if (focusCssClass) {
                            watermarker.removeClass(focusCssClass);
                            if (watermarked.is(':focus')) {
                                watermarker.addClass(focusCssClass);
                            }
                        }
                    }
                };

                var watermarker = $('<span></span>').addClass(cssClass)
                    .text(text).insertBefore(watermarked).hide();

                //watermark();
                watermarked.on('keydown focus', function () {
                    watermarker.hide();
                    setTimeout(watermark, 0);
                });
                watermarked.on('keyup keypress change blur', function () {
                    watermark();
                });
                watermarker.on('click focus', function () {
                    watermarked.focus();
                    watermark();
                });
                $(document).ready(function () {
                    setTimeout(watermark, 100);
                });
            });
        }
    });

    // validation-css integration
    $.extend($.ucosmic.obtruders.ux, {
        formValidationCss: function (selector) {
            $(selector).find('form').each(function () {
                var validator = $.data(this, 'validator');
                var settings = validator.settings;
                function applyHighlight(element) {
                    var addClass = '';
                    if ($(element).hasClass(settings.validClass)) {
                        addClass = settings.validClass;
                    }
                    else if ($(element).hasClass(settings.errorClass)) {
                        addClass = settings.errorClass;
                    }
                    var container = $(element).parents('[data-ucosmic-val=css]:first');
                    container.removeClass(settings.errorClass).removeClass(settings.validClass);
                    if (addClass) {
                        container.addClass(addClass);
                    }
                    if (container.data('ucosmic-watermark') === true) {
                        var watermarker = $(container).find('input.' + container.data('ucosmic-watermark-class'));
                        watermarker.removeClass(settings.errorClass).removeClass(settings.validClass);
                        if (addClass) {
                            watermarker.addClass(addClass);
                        }
                    }
                    if (addClass === settings.errorClass) {
                        $(container).find('[data-ucosmic-val-hide=error]').hide();
                        $(container).find('[data-ucosmic-val-show=error]').show();
                    }
                    else if (addClass === settings.validClass) {
                        $(container).find('[data-ucosmic-val-hide=valid]').hide();
                        $(container).find('[data-ucosmic-val-show=valid]').show();
                    }
                }
                $(this).find(':input[data-val=true], :input.input-validation-error').each(function () {
                    applyHighlight(this);
                });
                settings.highlight = function (element, errorClass, validClass) {
                    if ($(element).data('val') === true) {
                        if (element.type === 'radio') {
                            validator.findByName(element.name).addClass(errorClass).removeClass(validClass);
                        } else {
                            $(element).addClass(errorClass).removeClass(validClass);
                        }
                        applyHighlight(element);
                    }
                };
                settings.unhighlight = function (element, errorClass, validClass) {
                    if ($(element).data('val') === true) {
                        if (element.type === 'radio') {
                            validator.findByName(element.name).removeClass(errorClass).addClass(validClass);
                        } else {
                            $(element).removeClass(errorClass).addClass(validClass);
                        }
                        applyHighlight(element);
                    }
                };
                var eachInput = function () {
                    var input = this,
                        container = $(this).parents('[data-ucosmic-val=css]:first');
                    container.find('[data-ucosmic-focused-class]').each(function () {
                        var focused = this,
                            cssClass = $(this).data('ucosmic-focused-class');
                        $(input).on('focus', function () {
                            $(focused).addClass(cssClass);
                        });
                        $(input).on('blur', function () {
                            $(focused).removeClass(cssClass);
                        });
                    });
                };
                $(this).find('input').each(eachInput);
                $(this).find('textarea').each(eachInput);
            });
        }
    });

    // submit form once
    $.extend($.ucosmic.unobtrusive, {
        startFormSubmit: function (selector) {
            selector.find('[data-ucosmic-form-submitting=show]').show();
            selector.find('[data-ucosmic-form-submitting=hide]').hide();
            var button = selector.find('input[type="submit"]');
            setTimeout(function () {
                button.attr('disabled', 'disabled');
            }, 0);
        }
    });
    $.extend($.ucosmic.unobtrusive, {
        endFormSubmit: function (selector) {
            selector.find('[data-ucosmic-form-submitting=show]').hide();
            selector.find('[data-ucosmic-form-submitting=hide]').show();
            var button = selector.find('input[type="submit"]');
            setTimeout(function () {
                button.removeAttr('disabled');
            }, 1);
        }
    });
    $.extend($.ucosmic.obtruders.ux, {
        oneFormSubmit: function (selector) {
            $(selector).on('invalid-form.validate', 'form', function () {
                $.ucosmic.unobtrusive.endFormSubmit($(this));
            });
            $(selector).on('submit', 'form', function () {
                $.ucosmic.unobtrusive.startFormSubmit($(this));
            });
        }
    });

    // toggle sub
    $.extend($.ucosmic.obtruders.ux, {
        toggleSub: function (selector) {
            $(selector).find('a[data-ucosmic-toggle=sub]').each(function () {
                var toggle = $(this), target;
                toggle.parents().each(function () {
                    target = $(this).find('[data-ucosmic-toggle=target]');
                    if (target.length) {
                        return;
                    }
                });
                if (target.length) {
                    var trigger = toggle.data('ucosmic-toggle-trigger'),
                    cssClass = toggle.data('ucosmic-toggle-class'),
                    cancel = false,
                    doToggle = function () {
                        if (!cancel) {
                            if (cssClass) {
                                toggle.toggleClass(cssClass);
                            }
                            target.toggle();
                            if (target.is(':visible')) {
                                var toggleWidth = toggle.outerWidth(true);
                                var targetWidth = target.outerWidth(true);
                                var right = parseInt(target.css('right'), 10);
                                if (right && !isNaN(right) && right < 0) {
                                    toggleWidth -= right;
                                }
                                if (toggleWidth > targetWidth) {
                                    target.css({ minWidth: toggleWidth });
                                }
                            }
                        }
                        cancel = false;
                        return false;
                    };
                    if (!trigger) {
                        trigger = 'click';
                    }
                    toggle.on(trigger, doToggle);
                    if (trigger === 'mouseover') {
                        target.on(trigger, function () {
                            cancel = true;
                        });
                        target.on('mouseleave', function () {
                            cancel = false;
                            doToggle();
                        });
                        toggle.on('mouseout', function () {
                            setTimeout(doToggle, 0);
                        });
                    }
                }
            });
        }
    });

    // unimplemented clicks
    $.extend($.ucosmic.obtruders.ux, {
        notImplemented: function (selector) {
            $(selector).find('[data-ucosmic-not-implemented]').each(function () {
                var feature = $(this).data('ucosmic-not-implemented');
                if (feature === true) {
                    feature = $(this).text();
                    if (!feature) {
                        feature = $(this).val();
                    }
                }
                var scope = $(this).data('ucosmic-scope');
                if (!scope) {
                    scope = 'feature';
                }
                $(this).on('click', function () {
                    var dialog = $('[data-ucosmic-dialog=not-implemented]');
                    var title = $(this).attr('title');
                    var titleFormat = dialog.find('[data-ucosmic-text-format=title]');
                    if (titleFormat.length) {
                        title = titleFormat.html().replace('{0}', feature).replace('{1}', scope);
                    }
                    dialog.dialog({
                        title: title,
                        modal: true,
                        dialogClass: 'ucosmic modal w620',
                        resizable: false,
                        draggable: false,
                        width: 'auto'
                    });
                    return false;
                });
            });
        }
    });

    // user feedback message
    $.extend($.ucosmic.obtruders.ux, {
        userActionFeedback: function (selector) {
            $(selector).find('[data-ucosmic-feedback=true]').each(function () {
                var content = $(this).find('[data-ucosmic-feedback=message]');
                if (content.text()) {
                    $(this).show();
                    var feedback = $(this);
                    setTimeout(function () {
                        feedback.fadeOut(2000);
                    }, 10000);
                }
                $(this).find('a[data-ucosmic-feedback=close]').click(function () {
                    $(this).parents('[data-ucosmic-feedback=true]').fadeOut();
                    return false;
                });
            });
        }
    });

    // fire up behaviors
    $(function () {
        // initialize obtrusive behaviors
        $.ucosmic.obtrude(document);

        // apply close clicks to jquery ui dialogs
        $(document).on('click', '.ucosmic .ui-dialog-content a[data-ucosmic-dialog=close]', function () {
            $(this).parents('.ui-dialog-content').dialog('close');
            if ($(this).attr('href') === '#') {
                return false;
            }
            return true;
        });

        // apply window resize centering to jquery ui dialogs
        $(window).resize(function () {
            $('.ucosmic .ui-dialog-content').dialog('option', 'position', 'center');
            var dialog = $('.ucosmic.ui-dialog');
            var offset = dialog.offset();
            if (offset) {
                var top = offset.top;
                if (top < 20) {
                    dialog.css({ top: 20 });
                }
            }
        });
    });

} (jQuery));

// google maps
(function (ucosmic, google, $, undefined) {

    var parseLatAndLng = function (latAndLng) {
        var lat = 0, lng = 0, latLng = [];
        if (typeof latAndLng === 'string') {
            // lat & lng may be separated by comma or space
            if (latAndLng.indexOf(',') > 0) {
                latLng = latAndLng.split(',');
            }
            else if (latAndLng.indexOf(' ') > 0) {
                latLng = latAndLng.split(' ');
            }

            // set lat and lng if they exist and are numbers
            if (latLng.length > 0 && !isNaN(parseFloat(latLng[0]))) {
                lat = parseFloat(latLng[0]);
            }
            if (latLng.length > 1 && !isNaN(parseFloat(latLng[1]))) {
                lng = parseFloat(latLng[1]);
            }
        }
        return {
            lat: lat,
            lng: lng
        };
    };
    $.ucosmic.unobtrusive = $.ucosmic.unobtrusive || {};

    $.ucosmic.unobtrusive.Map = function (jq) {
        this.jq = jq;
        this.selectors = $.ucosmic.unobtrusive.Map.selectors;
    };
    $.extend($.ucosmic.unobtrusive.Map, {
        selectors: {
            mapTypeId: 'ucosmic-map-type-id',
            center: 'ucosmic-map-center',
            northEast: 'ucosmic-map-bounds-north-east',
            southWest: 'ucosmic-map-bounds-south-west',
            zoom: 'ucosmic-map-zoom',
            scrollwheel: 'ucosmic-map-zoom-scroll-wheel',
            fitBounds: 'ucosmic-map-bounds-fit',
            position: 'ucosmic-map-position',
            control: 'ucosmic-map-control',
            href: 'ucosmic-map-href',
            imgSrc: 'ucosmic-img-src',
            sizeWidth: 'ucosmic-size-width',
            sizeHeight: 'ucosmic-size-height',
            originX: 'ucosmic-map-origin-x',
            originY: 'ucosmic-map-origin-y',
            anchorX: 'ucosmic-map-anchor-x',
            anchorY: 'ucosmic-map-anchor-y',
            offsetY: 'ucosmic-map-offset-y',
            confirm: 'ucosmic-confirm',
            title: 'ucosmic-map-title',
            inputId: {
                zoom: 'ucosmic-map-zoom-input',
                lat: 'ucosmic-map-lat-input',
                lng: 'ucosmic-map-lng-input',
                north: 'ucosmic-map-north-input',
                east: 'ucosmic-map-east-input',
                south: 'ucosmic-map-south-input',
                west: 'ucosmic-map-west-input'
            }
        },
        prototype: {
            getCanvas: function () {
                var jCanvas = this.jq.find('.u-map-canvas');
                if (jCanvas.length > 0) {
                    return jCanvas[0];
                }
                return undefined;
            },
            createCanvas: function () {
                if (!this.getCanvas()) {
                    $('<div data-ucosmic-map="canvas"></div>')
                        .prependTo($(this.jq))
                        .css({ width: 400, height: 300 });
                }
                return this.getCanvas();
            },
            getMapTypeId: function () {
                return this.jq.data(this.selectors.mapTypeId);
            },
            getCenter: function () {
                return parseLatAndLng(this.jq.data(this.selectors.center));
            },
            getNorthEast: function () {
                return parseLatAndLng(this.jq.data(this.selectors.northEast));
            },
            getSouthWest: function () {
                return parseLatAndLng(this.jq.data(this.selectors.southWest));
            },
            getZoom: function () {
                var zoom = this.jq.data(this.selectors.zoom);
                if (typeof zoom === 'number') {
                    return zoom;
                }
                return undefined;
            },
            getScrollWheelZoom: function () {
                var scrollWheel = this.jq.data(this.selectors.scrollwheel);
                if (scrollWheel === false || (scrollWheel && scrollWheel.toLowerCase() === 'false')) {
                    return false;
                }
                return true;
            },
            getFitBounds: function () {
                var northEast = this.getNorthEast();
                var southWest = this.getSouthWest();
                if (northEast.lat || northEast.lng || southWest.lat || southWest.lng) {
                    var fitBounds = this.jq.data(this.selectors.fitBounds);
                    var zoom = this.getZoom();
                    var doFit = (fitBounds === 'if-no-zoom' && !zoom) ||
                        (fitBounds === 'if-low-zoom' && !zoom) ||
                        (fitBounds === 'if-low-zoom' && typeof zoom === 'number' && zoom < 4);
                    if (doFit) {
                        return {
                            southWest: southWest,
                            northEast: northEast
                        };
                    }
                }
                return false;
            },
            getControls: function () {
                if (!this.controls) {
                    var jControls = this.jq.find('.u-map-controls');
                    if (jControls.length) {
                        this.controls = new $.ucosmic.unobtrusive.MapControls(jControls);
                    }
                }
                return this.controls;
            },
            getMarkers: function () {
                var jMarkers = this.jq.find('.u-map-markers');
                if (jMarkers.length) {
                    return jMarkers[0];
                }
                return undefined;
            },
            getZoomInputId: function () { return '#' + this.jq.data(this.selectors.inputId.zoom); },
            getLatInputId: function () { return '#' + this.jq.data(this.selectors.inputId.lat); },
            getLngInputId: function () { return '#' + this.jq.data(this.selectors.inputId.lng); },
            getNorthInputId: function () { return '#' + this.jq.data(this.selectors.inputId.north); },
            getEastInputId: function () { return '#' + this.jq.data(this.selectors.inputId.east); },
            getSouthInputId: function () { return '#' + this.jq.data(this.selectors.inputId.south); },
            getWestInputId: function () { return '#' + this.jq.data(this.selectors.inputId.west); }
        }
    });

    $.ucosmic.unobtrusive.MapControls = function (jq) {
        this.jq = jq;
        this.selectors = $.ucosmic.unobtrusive.Map.selectors;
        this.element = (jq.length) ? jq[0] : undefined;
    };
    $.extend($.ucosmic.unobtrusive.MapControls, {
        prototype: {
            getPosition: function () {
                return this.jq.data(this.selectors.position);
            },
            getMarker: function () {
                var marker = this.jq.find('[data-ucosmic-map-control=marker]');
                if (marker.length) {
                    return marker[0];
                }
                return undefined;
            },
            hasMarker: function () {
                return (this.getMarker()) ? true : false;
            },
            getMarkerPosition: function () {
                var latAndLng = $(this.getMarker()).data(this.selectors.center);
                return parseLatAndLng(latAndLng);
            },
            getMarkerInfoWindow: function () {
                var infoWindow = $(this.getMarker()).find('[data-ucosmic-map=info-window]');
                if (infoWindow.length) {
                    return infoWindow[0];
                }
                return undefined;
            },
            getMarkerInfoWindowMaxWidth: function () {
                var maxWidth = $(this.getMarkerInfoWindow()).data(this.selectors.sizeWidth);
                if (typeof maxWidth === 'number') {
                    return maxWidth;
                }
                return undefined;
            },
            getCreateMarker: function () {
                var createMarker = this.jq.find('[data-ucosmic-map-control=create-marker]');
                if (createMarker.length) {
                    return createMarker[0];
                }
                return undefined;
            },
            getCreateMarkerHref: function () {
                return $(this.getCreateMarker()).data(this.selectors.href);
            },
            getCreateMarkerSrc: function () {
                return $(this.getCreateMarker()).data(this.selectors.imgSrc);
            },
            getCreateMarkerSize: function () {
                return {
                    width: $(this.getCreateMarker()).data(this.selectors.sizeWidth),
                    height: $(this.getCreateMarker()).data(this.selectors.sizeHeight)
                };
            },
            getCreateMarkerOrigin: function () {
                return {
                    x: $(this.getCreateMarker()).data(this.selectors.originX),
                    y: $(this.getCreateMarker()).data(this.selectors.originY)
                };
            },
            getCreateMarkerAnchor: function () {
                return {
                    x: $(this.getCreateMarker()).data(this.selectors.anchorX),
                    y: $(this.getCreateMarker()).data(this.selectors.anchorY)
                };
            },
            getCreateMarkerOffsetY: function () {
                return $(this.getCreateMarker()).data(this.selectors.offsetY);
            },
            getDestroyMarker: function () {
                var destroyMarker = this.jq.find('[data-ucosmic-map-control=destroy-marker]');
                if (destroyMarker.length) {
                    return destroyMarker[0];
                }
                return undefined;
            },
            getDestroyMarkerConfirm: function () {
                return $(this.getDestroyMarker()).data(this.selectors.confirm);
            }
        }
    });

    $.ucosmic.unobtrusive.MapMarker = function (jq) {
        this.jq = jq;
        this.selectors = $.ucosmic.unobtrusive.Map.selectors;
    };
    $.extend($.ucosmic.unobtrusive.MapMarker, {
        prototype: {
            getPosition: function () {
                return parseLatAndLng(this.jq.data(this.selectors.center));
            },
            getTitle: function () {
                return this.jq.data(this.selectors.title) || '';
            },
            getInfoWindow: function () {
                var jInfoWindow = this.jq.find('[data-ucosmic-map=info-window]');
                if (jInfoWindow.length) {
                    return jInfoWindow[0];
                }
                return undefined;
            },
            getHref: function () {
                return this.jq.data(this.selectors.href) || undefined;
            },
            getInfoWindowMaxWidth: function () {
                var maxWidth = $(this.getInfoWindow()).data(this.selectors.sizeWidth);
                if (typeof maxWidth === 'number') {
                    return maxWidth;
                }
                return undefined;
            }
        }
    });

    $.ucosmic.GoogleMarker = function (jq, map) {
        this.map = map;
        this.unobtrusive = new $.ucosmic.unobtrusive.MapMarker(jq);
        this.init();
    };
    $.extend($.ucosmic.GoogleMarker, {
        prototype: {
            init: function () {
                var googleMarker = this;
                var position = this.unobtrusive.getPosition();
                if (position.lat || position.lng) {
                    var options = {
                        map: this.map.map, // TODO this smells bad
                        position: new google.maps.LatLng(position.lat, position.lng),
                        title: this.unobtrusive.getTitle()
                    };
                    this.marker = new google.maps.Marker(options);

                    var infoWindow = this.unobtrusive.getInfoWindow();
                    var href = this.unobtrusive.getHref();
                    if (infoWindow) {
                        this.infoWindow = new google.maps.InfoWindow({
                            content: infoWindow,
                            maxWidth: this.unobtrusive.getInfoWindowMaxWidth()
                        });
                        google.maps.event.addListener(this.marker, 'click', function () {
                            for (var i = 0; i < googleMarker.map.markers.length; i++) {
                                if (googleMarker.map.markers[i].infoWindow) {
                                    googleMarker.map.markers[i].infoWindow.close();
                                }
                            }
                            googleMarker.infoWindow.open(googleMarker.map.map, googleMarker.marker); // TODO this smells bad
                        });
                    }
                    else if (href) {
                        var url = 'http://' + href;
                        if (url.indexOf('http://http') == 0)
                            url = href;
                        google.maps.event.addListener(this.marker, 'click', function () {
                            window.location.href = url;
                        });
                    }
                }
            }
        }
    });

    $.ucosmic.GoogleMap = function (jq) {
        this.unobtrusive = new $.ucosmic.unobtrusive.Map(jq);
        this.init();
    };
    $.extend($.ucosmic.GoogleMap, {
        prototype: {
            init: function () {
                var options = {
                    mapTypeId: this.getMapTypeId(),
                    center: this.getCenter(),
                    zoom: this.getZoom()
                };
                if (!this.unobtrusive.getScrollWheelZoom()) {
                    options.scrollwheel = false;
                }
                this.map = new google.maps.Map(this.getCanvas(), options);
                this.initZoom();
                this.initControls();
                this.initMarkers();
            },
            initControls: function () {
                if (this.unobtrusive.getControls()) {
                    var googleMap = this;
                    google.maps.event.addListenerOnce(this.map, 'idle', function () {
                        var mapControls = function (owner) {
                            this.owner = owner;
                            this.map = owner.map;
                            this.unobtrusive = owner.unobtrusive.controls;
                            this.element = this.unobtrusive.element;
                            this.marker = undefined;
                            this.setMap(this.map);
                            this.init();
                        };
                        mapControls.prototype = new google.maps.OverlayView();
                        mapControls.prototype.draw = function () { };
                        mapControls.prototype.init = function () {
                            if (this.unobtrusive.hasMarker()) {
                                var center = this.unobtrusive.getMarkerPosition();
                                if (center.lat && center.lng) {
                                    $(this.unobtrusive.getCreateMarker()).hide();
                                    this.putMarker(new google.maps.LatLng(center.lat, center.lng));
                                }
                                else {
                                    $(this.unobtrusive.getDestroyMarker()).hide();
                                }
                                $(this.unobtrusive.getCreateMarker()).on('click', this, this.createMarker);
                                $(this.unobtrusive.getDestroyMarker()).on('click', this, this.destroyMarker);
                            }

                            this.map.controls[this.getPosition()].push(this.element);
                            $(this.element).show();
                        };
                        mapControls.prototype.getPosition = function () {
                            var position = this.unobtrusive.getPosition();
                            if (position) { // google capitalizes control positions
                                position = position.toUpperCase();
                            }
                            if (!google.maps.ControlPosition[position]) {
                                position = 'TOP_LEFT'; // fallback to default
                            }
                            return google.maps.ControlPosition[position];
                        };
                        mapControls.prototype.getAddMarkerLatLng = function () {
                            var pixel = {
                                x: $(this.element).position().left + ($(this.element).outerWidth() / 2),
                                y: $(this.element).outerHeight()
                            };
                            var projection = this.getProjection();
                            return projection.fromContainerPixelToLatLng(pixel);
                        };
                        mapControls.prototype.putMarker = function (latLng) {
                            var controls = this;
                            this.marker = new google.maps.Marker({
                                map: this.map,
                                position: latLng,
                                draggable: true
                            });
                            googleMap.updateLatLngInputs(latLng);

                            var infoWindowContent = this.unobtrusive.getMarkerInfoWindow();
                            if (infoWindowContent) {
                                this.markerInfoWindow = new google.maps.InfoWindow({
                                    content: infoWindowContent,
                                    maxWidth: this.unobtrusive.getMarkerInfoWindowMaxWidth()
                                });
                                $(infoWindowContent).show();
                                this.markerInfoWindow.open(this.map, this.marker);
                                google.maps.event.addListener(this.marker, 'click', function () {
                                    controls.markerInfoWindow.open(controls.map, controls.marker);
                                });
                            }

                            google.maps.event.addListener(this.marker, 'dragstart', function (e) {
                                if (controls.markerInfoWindow) {
                                    controls.markerInfoWindow.close();
                                }
                                googleMap.updateLatLngInputs(e.latLng);
                                googleMap.unobtrusive.jq.trigger('marker_dragstart', googleMap);
                            });
                            google.maps.event.addListener(this.marker, 'drag', function (e) {
                                googleMap.updateLatLngInputs(e.latLng);
                                googleMap.unobtrusive.jq.trigger('marker_drag', googleMap);
                            });
                            google.maps.event.addListener(this.marker, 'dragend', function (e) {
                                if (controls.markerInfoWindow) {
                                    controls.markerInfoWindow.open(controls.map, controls.marker);
                                }
                                googleMap.updateLatLngInputs(e.latLng);
                                googleMap.unobtrusive.jq.trigger('marker_dragend', googleMap);
                            });
                        };
                        mapControls.prototype.createMarker = function (createClick) {
                            var size = createClick.data.unobtrusive.getCreateMarkerSize(),
                                origin = createClick.data.unobtrusive.getCreateMarkerOrigin(),
                                anchor = createClick.data.unobtrusive.getCreateMarkerAnchor();
                            $(createClick.data.unobtrusive.getDestroyMarker()).toggle();
                            $(createClick.data.unobtrusive.getCreateMarker()).toggle();
                            createClick.data.map.setOptions({ draggableCursor: 'pointer' });
                            createClick.data.marker = new google.maps.Marker({
                                map: createClick.data.map,
                                position: createClick.data.getAddMarkerLatLng(),
                                cursor: 'pointer',
                                clickable: false,
                                icon: new google.maps.MarkerImage(
                                    createClick.data.unobtrusive.getCreateMarkerSrc(),
                                    new google.maps.Size(size.width, size.height),
                                    new google.maps.Point(origin.x, origin.y),
                                    new google.maps.Point(anchor.x, anchor.y)
                                )
                            });
                            createClick.data.mouseMoveListener = google.maps.event.addListener(createClick.data.map, 'mousemove', function (mouseMove) {
                                createClick.data.marker.setPosition(mouseMove.latLng);
                            });
                            createClick.data.putClickListener = google.maps.event.addListenerOnce(createClick.data.map, 'click', function (putClick) {
                                google.maps.event.removeListener(createClick.data.mouseMoveListener);
                                createClick.data.map.setOptions({ draggableCursor: undefined });
                                createClick.data.marker.setMap(undefined);
                                var overlayView = new google.maps.OverlayView();
                                overlayView.draw = function () { };
                                overlayView.setMap(createClick.data.map);
                                var pixels = overlayView.getProjection().fromLatLngToContainerPixel(putClick.latLng);
                                pixels.y = pixels.y + createClick.data.unobtrusive.getCreateMarkerOffsetY();
                                putClick.latLng = overlayView.getProjection().fromContainerPixelToLatLng(pixels);
                                createClick.data.putMarker(putClick.latLng);
                                googleMap.unobtrusive.jq.trigger('marker_created', googleMap);
                            });
                        };
                        mapControls.prototype.destroyMarker = function (destroyClick) {
                            var message = destroyClick.data.unobtrusive.getDestroyMarkerConfirm();
                            if (message && !confirm(message)) {
                                return;
                            }
                            $(destroyClick.data.unobtrusive.getDestroyMarker()).toggle();
                            $(destroyClick.data.unobtrusive.getCreateMarker()).toggle();
                            if (destroyClick.data.marker) {
                                destroyClick.data.map.setOptions({ draggableCursor: undefined });
                                if (destroyClick.data.mouseMoveListener) {
                                    google.maps.event.removeListener(destroyClick.data.mouseMoveListener);
                                    destroyClick.data.mouseMoveListener = undefined;
                                }
                                if (destroyClick.data.putClickListener) {
                                    google.maps.event.removeListener(destroyClick.data.putClickListener);
                                    destroyClick.data.putClickListener = undefined;
                                }
                                google.maps.event.clearListeners(destroyClick.data.marker);
                                destroyClick.data.marker.setMap(undefined);
                                destroyClick.data.marker = undefined;
                                googleMap.clearLatLngInputs();
                                googleMap.unobtrusive.jq.trigger('marker_destroyed', googleMap);
                            }
                        };
                        googleMap.controls = new mapControls(googleMap);
                    });
                }
            },
            initZoom: function () {
                var googleMap = this;
                var fitBounds = this.unobtrusive.getFitBounds();
                if (fitBounds) {
                    var northEast = new google.maps.LatLng(
                        fitBounds.northEast.lat, fitBounds.northEast.lng);
                    var southWest = new google.maps.LatLng(
                        fitBounds.southWest.lat, fitBounds.southWest.lng);
                    google.maps.event.addListenerOnce(googleMap.map, 'zoom_changed', function () {
                        googleMap.updateZoomInput();
                        googleMap.updateBoundsInputs();
                        googleMap.unobtrusive.jq.trigger('zoom_changed', googleMap);
                    });
                    this.map.fitBounds(new google.maps.LatLngBounds(southWest, northEast));
                    var center = this.unobtrusive.getCenter();
                    if (center.lat || center.lng) {
                        google.maps.event.addListenerOnce(this.map, 'tilesloaded', function () {
                            googleMap.map.setCenter(new google.maps.LatLng(center.lat, center.lng));
                        });
                    }
                }
                google.maps.event.addListenerOnce(googleMap.map, 'idle', function () {
                    google.maps.event.addListener(googleMap.map, 'bounds_changed', function () {
                        googleMap.updateZoomInput();
                        googleMap.updateBoundsInputs();
                        googleMap.unobtrusive.jq.trigger('bounds_changed', googleMap);
                    });
                    google.maps.event.addListener(googleMap.map, 'zoom_changed', function () {
                        googleMap.updateZoomInput();
                        googleMap.updateBoundsInputs();
                        googleMap.unobtrusive.jq.trigger('zoom_changed', googleMap);
                    });
                });
            },
            initMarkers: function () {
                var markers = this.unobtrusive.getMarkers();
                if (markers) {
                    this.markers = [];
                    var googleMap = this;
                    $(markers).find('[data-ucosmic-map=marker]').each(function () {
                        googleMap.markers.push(new $.ucosmic.GoogleMarker($(this), googleMap));
                    });
                }
            },
            getCanvas: function () {
                // return from google API if present
                if (this.map) {
                    return this.map.getDiv();
                }

                // create new canvas element if not found
                var canvas = this.unobtrusive.getCanvas();
                if (!canvas) {
                    canvas = this.unobtrusive.createCanvas();
                }
                return canvas;
            },
            getMapTypeId: function () {
                // return from google API if present
                if (this.map) {
                    return this.map.getMapTypeId();
                }

                // get from unobtrusive data
                var mapTypeId = this.unobtrusive.getMapTypeId();
                if (mapTypeId) { // google capitalizes map type id's
                    mapTypeId = mapTypeId.toUpperCase();
                }
                if (!google.maps.MapTypeId[mapTypeId]) {
                    mapTypeId = 'ROADMAP'; // fallback to default
                }
                return google.maps.MapTypeId[mapTypeId];
            },
            getCenter: function () {
                // return from google API if present
                if (this.map) {
                    return this.map.getCenter();
                }

                // get from unobtrusive data
                var center = this.unobtrusive.getCenter();
                return new google.maps.LatLng(center.lat, center.lng);
            },
            getNorthEast: function () {
                // return from google API if present
                if (this.map && this.map.getBounds()) {
                    return this.map.getBounds().getNorthEast();
                }

                // get from unobtrusive data
                var northEast = this.unobtrusive.getNorthEast();
                return new google.maps.LatLng(northEast.lat, northEast.lng);
            },
            getSouthWest: function () {
                // return from google API if present
                if (this.map && this.map.getBounds()) {
                    return this.map.getBounds().getSouthWest();
                }

                // get from unobtrusive data
                var southWest = this.unobtrusive.getSouthWest();
                return new google.maps.LatLng(southWest.lat, southWest.lng);
            },
            getBounds: function () {
                // return from google API if present
                if (this.map && this.map.getBounds()) {
                    return this.map.getBounds();
                }

                // get from unobtrusive data
                return new google.maps.LatLngBounds(this.getSouthWest(), this.getNorthEast());
            },
            getZoom: function () {
                // return from google API if present
                if (this.map) {
                    return this.map.getZoom();
                }

                // get from unobtrusive data
                var zoom = this.unobtrusive.getZoom();
                if (!zoom && zoom !== 0) {
                    zoom = 1;
                }
                return zoom;
            },
            updateLatLngInputs: function (latLng) {
                $(this.unobtrusive.getLatInputId()).val(latLng.lat());
                $(this.unobtrusive.getLngInputId()).val(latLng.lng());
            },
            updateBoundsInputs: function () {
                var bounds = this.getBounds();
                $(this.unobtrusive.getNorthInputId()).val(bounds.getNorthEast().lat());
                $(this.unobtrusive.getEastInputId()).val(bounds.getNorthEast().lng());
                $(this.unobtrusive.getSouthInputId()).val(bounds.getSouthWest().lat());
                $(this.unobtrusive.getWestInputId()).val(bounds.getSouthWest().lng());
            },
            updateZoomInput: function () {
                $(this.unobtrusive.getZoomInputId()).val(this.getZoom());
            },
            clearLatLngInputs: function () {
                //$(this.unobtrusive.getNorthInputId()).val('');
                //$(this.unobtrusive.getEastInputId()).val('');
                //$(this.unobtrusive.getSouthInputId()).val('');
                //$(this.unobtrusive.getWestInputId()).val('');
                //$(this.unobtrusive.getZoomInputId()).val('');
                $(this.unobtrusive.getLatInputId()).val('');
                $(this.unobtrusive.getLngInputId()).val('');
            }
        }
    });

    $.ucosmic.maps = $.ucosmic.maps || [];
    $.ucosmic.maps.find = function (jq) {
        var i;
        for (i = 0; i < $.ucosmic.maps.length; i++) {
            if (jq.length && $.ucosmic.maps[i].unobtrusive.jq[0] === jq[0]) {
                return $.ucosmic.maps[i];
            }
        }
        return undefined;
    };
    $.ucosmic.maps.add = function (jq) {
        var uMap = new $.ucosmic.GoogleMap(jq);
        $.ucosmic.maps.push(uMap);
    };
    $.ucosmic.maps.remove = function (jq) {
        var i;
        for (i = 0; i < $.ucosmic.maps.length; i++) {
            if (jq.length && $.ucosmic.maps[i].unobtrusive.jq[0] === jq[0]) {
                $.ucosmic.maps.splice(i, 1);
                break;
            }
        }
    };

    // set up google maps
    $.ucosmic.obtruders.google = $.ucosmic.obtruders.google || {};
    $.extend($.ucosmic.obtruders.google, {
        maps: function (selector) {
            $(selector).find('.u-map-google').each(function () {
                if ($.ucosmic.maps.find($(this))) {
                    $.ucosmic.maps.remove($(this));
                }
                $.ucosmic.maps.add($(this));
            });
        }
    });

} (window.ucosmic = window.ucosmic || {}, window.google = window.google || {}, jQuery));

// pwdMeter plugin
(function (jQuery) {

    jQuery.fn.pwdMeter = function (options) {


        options = jQuery.extend({
            prefix: '',
            minLength: 6,
            displayGeneratePassword: false,
            generatePassText: 'Password Generator',
            generatePassClass: 'GeneratePasswordLink',
            randomPassLength: 13,
            passwordBox: this
        }, options);


        return this.each(function () {

            $(this).keyup(function () {
                evaluateMeter();
            });
            evaluateMeter();

            function evaluateMeter() {

                var passwordStrength = 0;
                var password = $(options.passwordBox).val();

                if ((password.length > 0) && (password.length <= 5)) passwordStrength = 1;

                if (password.length >= options.minLength) passwordStrength++;

                if ((password.match(/[a-z]/)) && (password.match(/[A-Z]/))) passwordStrength++;

                if (password.match(/\d+/)) passwordStrength++;

                if (password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)) passwordStrength++;

                if (password.length > 12) passwordStrength++;

                $('#pwdMeter').removeClass();
                $('#pwdMeter').addClass('neutral');

                switch (passwordStrength) {
                    case 1:
                        $('#pwdMeter').addClass('veryweak');
                        $('#pwdMeter').text(options.prefix + 'Very Weak');
                        break;
                    case 2:
                        $('#pwdMeter').addClass('weak');
                        $('#pwdMeter').text(options.prefix + 'Weak');
                        break;
                    case 3:
                        $('#pwdMeter').addClass('medium');
                        $('#pwdMeter').text(options.prefix + 'Medium');
                        break;
                    case 4:
                        $('#pwdMeter').addClass('strong');
                        $('#pwdMeter').text(options.prefix + 'Strong');
                        break;
                    case 5:
                        $('#pwdMeter').addClass('verystrong');
                        $('#pwdMeter').text(options.prefix + 'Very Strong');
                        break;
                    case 0:
                    default:
                        $('#pwdMeter').addClass('neutral');
                        $('#pwdMeter').text(options.prefix + 'Very Weak');
                }

            }
        });

    };
})(jQuery);

// combobox unobtrusive initialization
$(function () {
    $('input[data-app-combobox="true"]').each(function () {
        var url = $(this).data('app-combobox-url');
        if (url) {
            $(this).attr('disabled', 'disabled');

            var typeOptions = {
                source: [],
                strict: true,
                error: false,
                buttonTitle: ''
            };
            $.ajax({
                url: url,
                dataType: 'json',
                async: false,
                success: function (data) {
                    typeOptions = data;
                },
                error: function () {
                    typeOptions.error = true;
                }
            });

            if (!typeOptions.error) {
                $(this).cosmobox({
                    source: typeOptions.source,
                    buttonTitle: typeOptions.buttonTitle,
                    readonly: typeOptions.strict,
                    appendTo: '.' + $(this).attr('id') + '-field .autocomplete-menu'
                });
                $(this).removeAttr('disabled');
            }
        }
    });
});

// cosmobox plugin
(function ($) {
    $.fn.cosmobox = function (options) {

        var settings = {
            source: [
                'Option #1',
                'Option #2',
                'Option #3',
                '[Set a custom source to display real data]'
            ],
            buttonTitle: 'Show all options',
            appendTo: null,
            useValue: true,
            focusing: null,
            focused: null,
            selecting: null,
            selected: null,
            readonly: false
        };
        return this.each(function () {
            var $this = $(this);
            if (options) {
                $.extend(settings, options);
            }

            $this.autocomplete('destroy');
            $this.removeAttr('readonly');

            $this.autocomplete({
                source: settings.source,
                minLength: 0,
                appendTo: settings.appendTo,
                open: function () { // (event, ui) {
                    $('ul.ui-autocomplete').outerWidth($(this).parents('.combobox:first').outerWidth(true));
                },
                focus: function (event, ui) {
                    if (settings.focusing) {
                        settings.focusing();
                    }
                    if (settings.useValue) {
                        $this.val(ui.item.value);
                    }
                    else {
                        $this.val(ui.item.label);
                    }
                    if (settings.focused) {
                        settings.focused();
                    }
                    return false;
                },
                select: function (event, ui) {
                    if (settings.selecting) {
                        settings.selecting();
                    }
                    if (settings.useValue) {
                        $this.val(ui.item.value);
                    }
                    else {
                        $this.val(ui.item.label);
                    }
                    $(this).change();
                    if (settings.selected) {
                        settings.selected();
                    }
                    return false;
                }
            });

            var onClick = function (e) {
                e.stopPropagation();
                if ($this.autocomplete('widget').is(':visible')) {
                    $this.autocomplete('close');
                    return;
                }
                if (e.currentTarget && e.currentTarget.tagName.toUpperCase() !== 'INPUT')
                    $(this).blur();
                $this.autocomplete('search', '');
            };

            if (settings.readonly) {
                $this.attr('readonly', 'readonly');
                $this.click(onClick);
                $this.keydown(function (e) {
                    if (e.keyCode === 8) { // prevent backspace key from browsing backward
                        return false;
                    }
                    return true;
                });
            }

            var button = $('<button type="button"></button>');

            button = button
                .attr('tabIndex', -1)
                .attr('title', settings.buttonTitle)
                .insertAfter($this)
                .click(onClick)
                .height($this.outerHeight(true));
                //.html('<span class="ui-button-icon-primary ui-icon ui-icon-triangle-1-s"></span>'); //caused weirdness in chrome

            $this.width($this.width() - button.outerWidth(true));
            if ($.browser.mozilla && parseInt($.browser.version) >= 2) {
                $this.width($this.width() - 12); // hack firefox 4 width
            }

            // close on document clicks
            $(document).click(function () {
                if ($this.autocomplete('widget').is(':visible')) {
                    $this.autocomplete('close');
                    return;
                }
            });
        });
    };
})(jQuery);

// TODO: share code between RangeIf and RequiredIf
// unobtrusive client validation for RequiredIf rule
$.validator.unobtrusive.adapters.add('requiredif', ['otherinputname', 'comparisontype', 'othercomparisonvalue'],
    function (options) {
        options.rules['requiredif'] = {
            otherinputname: options.params['otherinputname'],
            comparisontype: options.params['comparisontype'],
            othercomparisonvalue: options.params['othercomparisonvalue']
        };
        options.messages['requiredif'] = options.message;
    }
);
$.validator.addMethod('requiredif',
    function (thisValue, thisElement, parameters) {
        var comparisonType = parameters['comparisontype'];
        // get the target value (as a string, that's what actual value will be)
        var otherComparisonValue = parameters['othercomparisonvalue'];
        otherComparisonValue = otherComparisonValue ? otherComparisonValue.toString() : '';

        // get the current value of the target control
        var otherInput = $(':input[name="' + parameters['otherinputname'] + '"]');
        var otherType = otherInput.attr('type');
        var otherComparedValue = otherInput.val();
        if (otherType == 'radio') {
            otherComparedValue = otherInput.filter(':checked').val();
        }
        if (otherType == 'checkbox') {
            otherComparedValue = otherInput.attr('checked').toString();
        }

        // if the condition is true, reuse the existing required field validator functionality
        if ((otherComparisonValue === otherComparedValue && comparisonType === 'IsEqualTo') ||
            (otherComparisonValue != otherComparedValue && comparisonType === 'IsNotEqualTo'))
            return $.validator.methods.required.call(
              this, thisValue, thisElement, parameters);

        return true;
    }
);

// unobtrusive client validation for RangeIf rule
    $.validator.unobtrusive.adapters.add('rangeif', ['minimum', 'maximum', 'otherinputname', 'comparisontype', 'othercomparisonvalue'],
    function (options) {
        options.rules['rangeif'] = {
            0: parseFloat(options.params['minimum']),
            1: parseFloat(options.params['maximum']),
            otherinputname: options.params['otherinputname'],
            comparisontype: options.params['comparisontype'],
            othercomparisonvalue: options.params['othercomparisonvalue']
        };
        options.messages['rangeif'] = options.message;
    }
);
    $.validator.addMethod('rangeif',
    function (thisValue, thisElement, parameters) {
        var comparisonType = parameters['comparisontype'];
        // get the target value (as a string, that's what actual value will be)
        var otherComparisonValue = parameters['othercomparisonvalue'];
        otherComparisonValue = otherComparisonValue ? otherComparisonValue.toString() : '';

        // get the current value of the target control
        var otherInput = $(':input[name="' + parameters['otherinputname'] + '"]');
        var otherType = otherInput.attr('type');
        var otherComparedValue = otherInput.val();
        if (otherType == 'radio') {
            otherComparedValue = otherInput.filter(':checked').val();
        }
        if (otherType == 'checkbox') {
            otherComparedValue = otherInput.attr('checked').toString();
        }

        // if the condition is true, reuse the existing range field validator functionality
        if ((otherComparisonValue === otherComparedValue && comparisonType === 'IsEqualTo') ||
            (otherComparisonValue != otherComparedValue && comparisonType === 'IsNotEqualTo'))
            return $.validator.methods.range.call(
              this, parseFloat(thisValue), thisElement, parameters);

        return true;
    }
);
