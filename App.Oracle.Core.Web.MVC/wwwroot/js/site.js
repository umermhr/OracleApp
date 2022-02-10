// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $("a.detail-files").click(function (e) {
        detail_files_view(this);
    });

});


function detail_files_view(param) {
    $("#filename").text($(param).data('filename'));
    $("#creationtime").text($(param).data('creationtime'));
    $("#recordtime").text($(param).data('recordtime'));
    $("#recordby").text($(param).data('recordby'));
}
