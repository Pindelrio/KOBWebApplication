// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });
    $('.moveUp').on('click', function (e) {
        moveUp(e.target);
    });
    $('.moveDown').on('click', function (e) {
        moveDown(e.target);
    });
    $('.removeItem').on('click', function (e) {
        removeItem(e.target);
    });
    $('.removeAllItems').on('click', function (e) {
        removeAllItems(e.target);
    });
});

function markCompleted(checkbox) {
    checkbox.enabled = true;
    var row = checkbox.closest('tr');
    $(row).addClass('done');
    var form = checkbox.closest('form');
    form.submit();
}

function moveUp(button) {
    var form = button.closest('form');
    form.submit();
}
function moveDown(button) {
    var form = button.closest('form');
    form.submit();
}
function removeItem(button) {
    var form = button.closest('form');
    form.submit();
}
function removeAllItems(button) {
    
    var r = confirm("SEGUR QUE VOLS BORRAR TOOOOOTA LA LLISTA???!");
    if (r == true) {
        var form = button.closest('form');
        form.submit();
    } 
    
}