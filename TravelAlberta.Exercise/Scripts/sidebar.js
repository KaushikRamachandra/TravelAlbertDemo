$(window).on('load', function () {
    $('[data-toggle="site-menu"]').on('click', function () {
        var $this = $(this);
        var target = $this.attr('href') || $this.data('target');
        $(target).toggleClass('show-menu');
    });
});