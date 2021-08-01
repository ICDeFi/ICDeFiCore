var BaseController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $('body').on('click', '.add-to-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                data: {
                    productId: id,
                    quantity: 1,
                    color: 0,
                    size: 0
                },
                success: function (response) {
                    be.notify('The product was added to cart', 'success');
                    loadHeaderCart();
                }
            });
        });

        $('body').on('click', '.remove-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                    be.notify('The product was removed', 'success');
                    loadHeaderCart();
                }
            });
        });

        $('body').on('click', '.btn-view-fast-product', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            debugger;
            //$.ajax({
            //    url: '/Cart/RemoveFromCart',
            //    type: 'post',
            //    data: {
            //        productId: id
            //    },
            //    success: function (response) {
            //        be.notify('The product was removed', 'success');
            //        loadHeaderCart();
            //    }
            //});
        })
    }

    function loadHeaderCart() {
        $("#headerCart").load("/AjaxContent/HeaderCart");
    }
}