// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
$(document).ready(function () {
    $('#confirm').keyup(function () {
        var password = $('#new').val();
        var confirmpassword = $('#confirm').val();
        if (confirmpassword != password) {
            $('#Error').html('**Password Not Matched**');
            $('#Error').css('color', 'red');
            return false;
        } else {
            $('#Error').html('**Password Matched**');
            $('#Error').css('color', 'green');
            return true;
        }


    });
});