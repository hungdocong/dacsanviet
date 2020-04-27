
$(document).ready(function () {
    $('.btn-close').click(function () {
        $('.mini_cart_box').removeClass('openCart');
    });
});

var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {

        $('.btn-addCart').off('click').on('click', function () {

            $.ajax({
                type: 'GET',
                url: '/Cart/AddCart',
                data: {
                    product_ID: $(this).data('id'),
                    quantity: $(this).data('quantity')
                },
                dataType: 'Json',
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.status == true) {
                        var count = $('.cart_count').text();
                        var Soluong = parseInt(count) + 1;
                        $('.mini_cart_box').css('display', 'block');
                        $('.cart_count').text(Soluong);
                        $('.mini_cart_box').addClass('openCart');
                        $('.mini_cart_box').delay(4000).slideUp(500);

                        //PNotify.success({
                        //    title: 'THÔNG BÁO!!',
                        //    stack: { dir1: "up", dir2: "left", firstpos1: 25, firstpos2: 25 },
                        //    text: 'Thêm giỏ hàng thành công.'
                        //});
                    }
                    
                }
            });
        });


        //$('.btn-Edit').off('click').on('click', function () {
        //    var listbook = $('.txtQuantity');
        //    var cartList = [];
        //    $.each(listbook, function (i, item) {
        //        cartList.push({
        //            Quantity: $(item).val(),
        //            Book: {
        //                ID: $(item).data('id')
        //            }
        //        });
        //    });

        //    //Phương thức Ajax dùng để đẩy lên Controller
        //    $.ajax({
        //        url: '/Cart/Edit',
        //        data: { cartModel: JSON.stringify(cartList) },
        //        dataType: 'Json',
        //        type: 'POST',
        //        success: function (res) {
        //            if (res.status == true) {
        //                window.location.href = "/Cart";
        //            }
        //        }
        //    });
        //});

        //$('#btnDeleteAll').off('click').on('click', function () {
        //    var conf = confirm("Bạn có muốn xóa giỏ hàng??");
        //    if (conf == true) {
        //        $.ajax({
        //            url: '/Cart/DeleteAll',
        //            dataType: 'Json',
        //            type: 'POST',
        //            success: function (res) {
        //                if (res.status == true) {
        //                    window.location.href = "/Cart";
        //                }
        //            }
        //        });
        //    }

        //});

        //$('.btn-Delete').off('click').on('click', function () {
        //    $.ajax({
        //        data: { id: $(this).data('id') },
        //        url: '/Cart/Delete',
        //        dataType: 'Json',
        //        type: 'POST',
        //        success: function (res) {
        //            if (res.status == true) {
        //                window.location.href = "/Cart";
        //            }
        //        }
        //    });
        //});


    }

}

cart.init();

function formatNumber(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}


function showStackTopCenter(type) {
    if (typeof window.stackTopCenter === 'undefined') {
        window.stackTopCenter = new Stack({
            dir1: 'down',
            firstpos1: 25,
            modal: false,
            maxOpen: Infinity
        });
    }
    const opts = {
        title: 'Over Here',
        text: "Check me out. I'm in a different stack.",
        stack: window.stackTopCenter
    };
    switch (type) {
        case 'error':
            opts.title = 'Oh No';
            opts.text = 'Watch out for that water tower!';
            opts.type = 'error';
            break;
        case 'info':
            opts.title = 'Breaking News';
            opts.text = 'Have you met Ted?';
            opts.type = 'info';
            break;
        case 'success':
            opts.title = 'THÔNG BÁO!!';
            opts.text = "Thêm giỏ hàng thành công.";
            opts.type = 'success';
            break;
    }
    PNotify.alert(opts);
}