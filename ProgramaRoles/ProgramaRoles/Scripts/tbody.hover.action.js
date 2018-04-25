function HoverTable() {
    var tablaHover = document.getElementsByClassName('redColorHover');

    $('tbody.redColorHover tr').mouseenter(function (e)
    {
        e.target.parentNode.style.backgroundColor = "#ffdddd"
    });

    $('tbody.redColorHover tr').mouseleave(function (e)
    {
        e.target.parentNode.style.backgroundColor = ""
    });
}

function LoadPopover()
{
    var tablaHover = document.getElementsByClassName('redColorHover');

    $('tbody.redColorHover tr').mouseenter(function (e) {

        var colDeValues;
        var colPopover;
        var chkedRolesUsu;

        colDeValues = e.target.parentNode.lastElementChild.value;
        colPopover = e.target.parentNode;

        colPopover.dataset.content = "";
        chkedRolesUsu = colDeValues.split(',');

        for (i = 0; i < chkedRolesUsu.length; i++)
        {
            colPopover.dataset.content += chkedRolesUsu[i] + "<br> ";
            colPopover.value
        }

    });
}

function ChkedFila() {
    var tablaHover = document.getElementsByClassName('redColorHover');

    var myFunction = function () {
        var rows = this.getElementsByTagName('tr');
        for (var i = 0; i < rows.length; i++)
        {
            rows[i].onclick = function (e) {
                if (nColumnas === 2)
                    $("input[type=checkbox]").prop('checked', false);

                e.target.parentNode.firstElementChild.firstElementChild.checked = !e.target.parentNode.firstElementChild.firstElementChild.checked;
            };
        }
    };

    for (var i = 0; i < tablaHover.length; i++)
    {
        tablaHover[i].addEventListener('click', myFunction, true);
    }

    var nColumnas = $(".redColorHover tr:last td").length;
    debugger;
    /*// Evento que se ejecuta al pulsar en un checkbox*/
    $("input[type=checkbox]").change(function () {

        //Cogemos el elemento actual
        var elemento = this;

        if (nColumnas === 2)
        {
            $("input[type=checkbox]").prop('checked', false);
            $(elemento).prop('checked', true);
        }

    });

}

function MarcarDesmarcarchk()
{
    $('#chkPrincipal').change(function ()
    {
        if ($('#chkPrincipal').is(':checked'))
            $("input[type=checkbox]").prop('checked', true);
        else
            $("input[type=checkbox]").prop('checked', false);
    });
}