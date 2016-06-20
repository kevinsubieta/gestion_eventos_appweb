//_______________________Login Listeners______________________________________
function loginlisteners() {
    $("#username,#password").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) { IniciarSesion($("#username").val(), $("#password").val(), $("#rol").attr("data-val")); }
    });

    $("#iniciarsesion").click(function () { iniciarsesion($("#username").val(), $("#password").val(),$("#rol").attr("data-val")); })
}
loginlisteners();

function clientelisteners() {
    $("#dialogo").dialog(dialog_configurations());
    $("#dialogo1").dialog(dialog1_configurations());

    $("#contactos").click(function () {
        run_Ajax(root + "contactos/index", { userid: id() }, "#rendersection", function () { contactoslisteners() });
    });

    $("#eventos").click(function () {
        run_Ajax(root + "eventos/index", { userid: id() }, "#rendersection", function () { eventoslisteners() });
    });

    $("#salir").click(function () {
        $("#dialogo1").dialog("destroy");
        $("#dialogo").dialog("destroy"); salir();
    });
}

//_______________________________contactos listeners__________________________

function contactoslisteners() {
    $("#nuevocontacto").click(function () { run_Ajax(root + "contactos/contactovacio", { userid: id() }, "#dialogo", function () { opendialog(); contactolisteners(); }); })

    $(".actualizarcontacto").click(function () { run_Ajax(root + "contactos/contactocompleto", { userid: id(), contactoid: $(this).attr("id") }, "#dialogo", function () { opendialog(); contactolisteners(); }); })
}

function contactolisteners() {
    $("#cerrarcontacto").click(function () {
        closedialog();
    })

    $("#registrarcontacto").click(function () {
        registrarcontacto($("#nombrescontacto").val(), $("#apellidoscontacto").val(), $("#correocontacto").val());
    })

    $("#actualizarcontacto").click(function () {
        actualizarcontacto($(this).attr("data-id"), $("#nombrescontacto").val(), $("#apellidoscontacto").val(), $("#correocontacto").val(), $("#bajacontacto").attr("data-value"));
    })

    $("#bajacontacto").click(function () {
        $(this).toggleClass("checkboxcheked checkboxuncheked");
        if ($(this).hasClass("checkboxcheked")) {
            $(this).attr("data-value", "1");
        } else {
            $(this).attr("data-value", "0");
        }
    });
}

//_______________________________eventos listeners__________________________

function eventoslisteners() {
    $("#nuevoevento").click(function () { run_Ajax(root + "eventos/eventovacio", { userid: id() }, "#dialogo1", function () { opendialog1(); eventolisteners(); }); });

    $(".actualizarevento").click(function () { run_Ajax(root + "eventos/eventocompleto", { userid: id(), eventoid: $(this).attr("id") }, "#dialogo1", function () { opendialog1(); eventolisteners(); }); });
}

function eventolisteners() {
    $("#cerrarevento").click(function () { closedialog1(); })

    $("#registrarevento").click(function () {
        registrarevento($("#nombreevento").val(), $("#direccionevento").val(), $("#fechainicioevento").val(), $("#fechafinalevento").val(), $("#contactoslist").attr("data-add"));
    })

    $("#actualizarevento").click(function () {
        actualizarevento($(this).attr("data-id"), $("#nombreevento").val(), $("#direccionevento").val(), $("#fechainicioevento").val(), $("#fechafinalevento").val(), $("#canceladoevento").attr("data-value"), $("#contactoslist").attr("data-add"), $("#contactoslist").attr("data-remove"));
    })

    $("#canceladoevento").click(function () {
        $(this).toggleClass("checkboxcheked checkboxuncheked");
        if ($(this).hasClass("checkboxcheked")) {
            $(this).attr("data-value", "1");
        } else {
            $(this).attr("data-value", "0");
        }
    });

    $(".checkcontacto").click(function () {
        updatedescuentoslists($("#contactoslist").attr("data-add"), $("#contactoslist").attr("data-remove"), $(this).attr("id"), this);
    })
}