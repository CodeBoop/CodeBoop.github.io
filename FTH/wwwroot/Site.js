
window.blazor = {};

window.blazor.tempDonation=null;

window.blazor.bindPaypalButton = (selector) => {

    paypal.Buttons({
        style: {
            layout: 'horizontal'
        },
        createOrder: function(data, actions) {

            var donation = DotNet.invokeMethod("FTH", "GetDonation").result;
            if (donation.total < 1) {
                return null;
            }

            window.blazor.tempDonation = donation;

            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: donation.total
                    }
                }]
            });
        },
        onApprove: function(data, actions) {
            return actions.order.capture().then(function (details) {
                // This function shows a transaction success message to your buyer.
                var don = window.blazor.tempDonation;

                try {
                    DotNet.invokeMethod("FTH",
                        "CreateDonation",
                        details.id,
                        don.name,
                        don.anon,
                        don.comment,
                        don.email);
                } catch (e) {
                    //ignore
                }
                

            });
        }
    }).render(selector);;
}


window.background = () => {
    var t = new Trianglify({
        x_gradient: ["#E24015", "#87370C", "#730000", "#391100","#000000"],
        noiseIntensity: 0,
        cellsize: 100
    });
    var pattern = t.generate(window.innerWidth, window.innerHeight);
    document.body.setAttribute('style', 'background-image: ' + pattern.dataUrl);
}


window.background();

var exampleCallback = function () {
    console.log('Order complete!');
};


window.blazor.createEventBriteFrame = (id) => {
    window.EBWidgets.createWidget({
        // Required
        widgetType: 'checkout',
        eventId: '137912330493',
        iframeContainerId: id,

        // Optional
        iframeContainerHeight: window.innerHeight * 0.9,  // Widget height in pixels. Defaults to a minimum of 425px if not provided
        onOrderComplete: exampleCallback  // Method called when an order has successfully completed
    });
}

