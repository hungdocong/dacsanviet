function btnTabs(id) {
    $(this).addClass("active");
    var dem = 0;//đếm số dữ liệu trả về, chỉ cho phép hiển thị 6 sản phẩm
    $.ajax({
        type: 'GET',
        url: '/Home/getProductByCategory',
        dataType: 'json',
        data: { productCategory_ID: id },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var rows = '<div class="dataTab_' + id + ' tab-pane fade" id="Category_' + id + '" role="tabpanel">'+
                            '<div class="row">';
            $.each(data, function (i, item) {
                if (dem == 6)
                    return;
                            rows += '<div class="single__product" style="width: 16%;">'+
                                            '<div class="single_product__inner">';
                                            if (item.promotionPrice != null) {
                                                var discount = parseInt((1 - (item.promotionPrice / item.price)) * 100);
                                                rows += '<span class="discount_price">-' + discount + '%</span >';
                                            }
                                            rows +='<div class="product_img">'+
                                                        '<a href="#">'+
                                                            '<img style="width: 202px;height: 202px" src="'+item.productImage+'" alt="">'+
                                                        '</a>'+
                                                    '</div>'+
                                                    '<div class="product__content text-center">'+
                                                        '<div class="produc_desc_info">'+
                                                            '<div class="product_title">'+
                                                                '<h4><a href="">'+item.productName+'</a></h4>'+
                                                            '</div>'+
                                                            '<div class="product_price">';
                                                            if(item.promotionPrice != null){
                                                                rows += '<p>' + formatNumber(item.promotionPrice, '.', ',') +'&nbsp ₫</p>';
                                                                rows += '<p class="origin_price">' + formatNumber(item.price, '.', ',') + '&nbsp ₫</p>';
                                                            } else {
                                                                rows += '<p>' + formatNumber(item.price, '.', ',') + '&nbsp ₫</p>';
                                                            }
                                                    rows +='</div>'+
                                                    '</div>'+
                                                    '<div class="product__hover">'+
                                                        '<ul>'+
                                                            '<li><a href="#"><i class="ion-android-cart" title="Thêm vào giỏ hàng"></i></a></li>'+
                                                            '<li><a href="product-details.html" title="Chi tiết sản phẩm"><i class="ion-clipboard"></i></a></li>'+
                                                        '</ul>'+
                                                    '</div>'+
                                            '</div>'+
                                        '</div>'+
                                    '</div>';
                dem++;
            });
            if (dem == 6)
                rows += '<div class="col-lg-4"></div>' +
                        '<div class="col-lg-3">'+
                            '<div class="shop_product_head d-flex justify-content-between mb-30">'+
                                '<a href="#" class="btnSee btn btn-outline-primary btn-lg" role="button">Xem thêm</a>'+
                            '</div>'+
                        '</div>'+
                        '<div class="col-lg-4"></div>';


            rows +=     '</div>' +
                    '</div>';
            if ($('.tab-content > .dataTab_' + id).hasClass('tab-pane') == false) {
                $('.tab-content').append(rows);
            }
            $('.dataTab_' + id).addClass('active show').siblings().removeClass('active show');

            //$('ul li.tabs').addClass('active').siblings().removeClass('active');
            $(this).addClass('active').siblings().removeClass('active');
        },
        error: function () {
            alert("Error while inserting data");
        }  
    });

}

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

//Có thể bạn quan tâm
function btnTabs_2(id) {
    $(this).addClass("active");
    var dem = 0;//đếm số dữ liệu trả về, chỉ cho phép hiển thị 6 sản phẩm
    $.ajax({
        type: 'GET',
        url: '/Home/getProductByCategory',
        dataType: 'json',
        data: { productCategory_ID: id },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var rows = '<div class="dataTab_' + id + ' tab-pane fade" id="_' + id + '" role="tabpanel">'+
                            '<div class="row">';
            $.each(data, function (i, item) {
                if (dem == 6)
                    return;
                            rows += '<div class="single__product" style="width: 16%;">'+
                                            '<div class="single_product__inner">';
                                            if (item.promotionPrice != null) {
                                                var discount = parseInt((1 - (item.promotionPrice / item.price)) * 100);
                                                rows += '<span class="discount_price">-' + discount + '%</span >';
                                            }
                                            rows +='<div class="product_img">'+
                                                        '<a href="#">'+
                                                            '<img style="width: 202px;height: 202px" src="'+item.productImage+'" alt="">'+
                                                        '</a>'+
                                                    '</div>'+
                                                    '<div class="product__content text-center">'+
                                                        '<div class="produc_desc_info">'+
                                                            '<div class="product_title">'+
                                                                '<h4><a href="">'+item.productName+'</a></h4>'+
                                                            '</div>'+
                                                            '<div class="product_price">';
                                                            if(item.promotionPrice != null){
                                                                rows += '<p>' + formatNumber(item.promotionPrice, '.', ',') +'&nbsp ₫</p>';
                                                                rows += '<p class="origin_price">' + formatNumber(item.price, '.', ',') + '&nbsp ₫</p>';
                                                            } else {
                                                                rows += '<p>' + formatNumber(item.price, '.', ',') + '&nbsp ₫</p>';
                                                            }
                                                    rows +='</div>'+
                                                    '</div>'+
                                                    '<div class="product__hover">'+
                                                        '<ul>'+
                                                            '<li><a href="#"><i class="ion-android-cart" title="Thêm vào giỏ hàng"></i></a></li>'+
                                                            '<li><a href="product-details.html" title="Chi tiết sản phẩm"><i class="ion-clipboard"></i></a></li>'+
                                                        '</ul>'+
                                                    '</div>'+
                                            '</div>'+
                                        '</div>'+
                                    '</div>';
                dem++;
            });
            if (dem == 6)
                rows += '<div class="col-lg-4"></div>' +
                        '<div class="col-lg-3">'+
                            '<div class="shop_product_head d-flex justify-content-between mb-30">'+
                                '<a href="#" class="btnSee btn btn-outline-primary btn-lg" role="button">Xem thêm</a>'+
                            '</div>'+
                        '</div>'+
                        '<div class="col-lg-4"></div>';


            rows +=     '</div>' +
                    '</div>';
            if ($('.tab2_content > .dataTab_' + id).hasClass('tab-pane') == false) {
                $('.tab2_content').append(rows);
            }
            $('.dataTab_' + id).addClass('active show').siblings().removeClass('active show');

            //$('ul li.tabs').addClass('active').siblings().removeClass('active');
            $(this).addClass('active').siblings().removeClass('active');
        },
        error: function () {
            alert("Error while inserting data");
        }  
    });

}

