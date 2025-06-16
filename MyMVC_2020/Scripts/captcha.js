$(function () {
    $(".newCaptcha").click(function (e) {
        var cmd = $(this);
        var url = cmd.parent(".captchaContainer").find("img").first().attr("src");
        url = url.replace(/(.*?)\?r=\d+\.\d+$/, "$1");
        if (typeof console != "undefined") {
            console.log(url);
        }
        var img = new Image();
        $(img).load(function () {
            cmd.parent(".captchaContainer").find('img').replaceWith(img);
        });
        img.src = url + "?r=" + Math.random();
        e.preventDefault();
    });
});