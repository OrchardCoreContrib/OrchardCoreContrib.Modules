$(function () {
    $('[data-bs-toggle="tooltip"]').tooltip();

    $('#btnDesktopPreview').change(function () {
        if (this.checked) {
            $('iframe').removeClass();
        }
    });
    $('#btnTabletPreview').change(function () {
        if (this.checked) {
            $('iframe').removeClass();
            $('iframe').addClass('tablet-preview');
        }
    });
    $('#btnMobilePreview').change(function () {
        if (this.checked) {
            $('iframe').removeClass();
            $('iframe').addClass('mobile-preview');
        }
    });
});