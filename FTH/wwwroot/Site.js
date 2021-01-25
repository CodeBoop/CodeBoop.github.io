
window.blazor = {};

window.background = () => {
    var t = new Trianglify({
        x_gradient: ["#E24015", "#87370C", "#730000", "#391100", "#000000"],
        noiseIntensity: 0,
        cellsize: 100
    });
    var pattern = t.generate(window.innerWidth, window.innerHeight);
    document.body.setAttribute('style', 'background-image: ' + pattern.dataUrl);
};

window.background();

window.blazor.createEventBriteFrame = (id) => {

    if (window.EBWidgets != null) {
        window.EBWidgets.createWidget({
            // Required
            widgetType: 'checkout',
            eventId: '137912330493',
            iframeContainerId: id,

            // Optional
            iframeContainerHeight: 1455, // Widget height in pixels. Defaults to a minimum of 425px if not provided
        });
    }
};

/*global jQuery */
/*!
* FitText.js 1.2
*
* Copyright 2011, Dave Rupert http://daverupert.com
* Released under the WTFPL license
* http://sam.zoy.org/wtfpl/
*
* Date: Thu May 05 14:23:00 2011 -0600
*/

(function ($) {

    $.fn.fitText = function (kompressor, options) {

        // Setup options
        var compressor = kompressor || 1,
            settings = $.extend({
                'minFontSize': Number.NEGATIVE_INFINITY,
                'maxFontSize': Number.POSITIVE_INFINITY
            }, options);

        return this.each(function () {

            // Store the object
            var $this = $(this);

            // Resizer() resizes items based on the object width divided by the compressor * 10
            var resizer = function () {
                $this.css('font-size', Math.max(Math.min($this.width() / (compressor * 10), parseFloat(settings.maxFontSize)), parseFloat(settings.minFontSize)));
            };

            // Call once to set.
            resizer();

            // Call on resize. Opera debounces their resize by default.
            $(window).on('resize.fittext orientationchange.fittext', resizer);

        });

    };

})(jQuery);


window.blazor.fitText = (selector) => {
    $(selector).fitText(1.2, { minFontSize: '20px', maxFontSize: '60px' });
};

