function btnTabImage(imgs) {
    var expandImg = document.getElementById("expandedImg");
    expandImg.src = imgs.src;
    expandImg.parentElement.style.display = "block";
}

$(document).ready(function () {

    $('#btn-comment').click(function () {
        $('.product-to').css("display", "block");
        $('.comment-to').css("display", "block");

        $('html,body').animate({
            scrollTop: $(".comment-to").offset().top - 150
        }, 1500);
    });

    $('.btn-add-close').click(function () {
        $('.product-to').css("display", "none");
        $('.comment-to').css("display", "none");

        $('html,body').animate({
            scrollTop: $("#btn-comment").offset().top - 150
        }, 1500);
    });


    //đánh giá sao
    $('.rateit_star').rateit({ min: 0, max: 5, step: 1 });

    $('.rateit_star').bind('rated', function (e) {
        var ri = $(this);
        var value = ri.rateit('value');
        $('#numberStart').val(value);
    });

});