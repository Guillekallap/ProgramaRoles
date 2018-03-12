

$(document).ready(function () {

    //Oculta una columna
    //$('td:nth-child(1)').hide();
    //Oculta columna si tiene (th)
    //$('td:nth-child(5),th:nth-child(5)').hide();
    HoverTable();
    ChkedFila();

    $('[data-toggle="popover"]').popover();
});

function HoverTable() {
    var tablaHover = document.getElementById('redColorHover');

    $('tbody#redColorHover tr').mouseenter(function (e) {


        e.target.parentNode.style.backgroundColor = "#ffdddd"

        var colOcultaValue;
        var colPopover;
        var chkedRolesUsu;

        colOcultaValue = e.target.parentNode.lastElementChild.value;
        colPopover = e.target.parentNode;

        colPopover.dataset.content = "";
        chkedRolesUsu = colOcultaValue.split(',');

        for (i = 0; i < chkedRolesUsu.length; i++) {
            colPopover.dataset.content += chkedRolesUsu[i] + "<br> ";
            colPopover.value
        }

    });

    tablaHover.onmouseout = function (e) {
        e.target.parentNode.style.backgroundColor = ""
    };
}

function ChkedFila() {
    var tablaHover = document.getElementById('redColorHover');

    var nColumnas = $("#redColorHover tr:last td").length;

    /*// Evento que se ejecuta al pulsar en un checkbox
    $("input[type=checkbox]").change(function () {

      // Cogemos el elemento actual
      var elemento = this;

      $("input[type=checkbox]").prop('checked', false);
      //$(elemento).prop('checked', true);

    });*/

    var rows = tablaHover.rows;
    for (var i = 0; i < rows.length; i++) {

        rows[i].onclick = function (e) {

            if (nColumnas === 2)
                $("input[type=checkbox]").prop('checked', false);
            e.target.parentNode.firstElementChild.firstElementChild.checked = !e.target.parentNode.firstElementChild.firstElementChild.checked;

        };
    }
}