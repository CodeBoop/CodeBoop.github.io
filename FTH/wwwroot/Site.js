
window.blazor = {};



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

                var donation = DotNet.invokeMethod("FTH", "GetDonation").result;

                alert('Transaction completed by ' + details.payer.name.given_name);
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