var root = $("#datos").attr("data-root");

function id() { return parseInt($("#datos").attr("data-userid")); }
function opendialog() { $("#dialogo").dialog("open"); }
function closedialog() { $("#dialogo").dialog("close"); }
function opendialog1() { $("#dialogo1").dialog("open"); }
function closedialog1() { $("#dialogo1").dialog("close"); }

//___________________Login methods_______________
function Errorlogin() { $("#error-login").css("display", "block"); }
//____________________UI MEthods __________________
function BloquearPantalla() { $.blockUI({ message: "" }); }
function DesbloquearPantalla() { $.unblockUI(); }

//____________________Ajaxs_________________________________________________
function run_Ajax(url, parameters, partial, callback) {
    BloquearPantalla();
    $.ajax({
        url: url,
        data: parameters,
        cache: false,
        traditional: true,
        type: "POST",
        success: function (data, textStatus, jQXHR) { }
    }).done(function (html) {
        if (partial.length > 0) $(partial).html(html);
        callback();
        DesbloquearPantalla();
    }).error(function () { DesbloquearPantalla(); });
}

//______________________________________Ajaxs Callers_____________________________________________

function iniciarsesion(user, pass, type) {
    run_Ajax(root + "home/iniciarsesion", { user: user, pass: pass }, "", function () { });
}

function entrar(userid) {
    $("#datos").attr("data-userid", userid);
    run_Ajax(root + "home/entrar", { userid: id() }, "#render", function () { clientelisteners(); });
}

function adm(action, callback) {
    run_Ajax(root + action, { userid: id() }, "#rendersection", callback );
}

function salir() {    
    run_Ajax(root + "home/salir", { userid: id() }, "#render", function() { loginlisteners(); $("#datos").attr("data-userid", ""); });
}

//vistas
function registrarcontacto(nombres, apellidos, correo) {
    run_Ajax(root + "contactos/registrarcontacto", { userid: id(), nombres: nombres, apellidos: apellidos, correo: correo }, "#rendersection", function () { closedialog(); contactoslisteners(); });
}

function actualizarcontacto(contactoid, nombres, apellidos, correo, baja) {
    run_Ajax(root + "contactos/actualizarcontacto", { userid: id(), contactoid: parseInt(contactoid), nombres: nombres, apellidos: apellidos, correo: correo, baja: baja=="1" }, "#rendersection", function () { closedialog(); contactoslisteners(); });
}

function registrarevento(nombre, direccion, fechainicio, fechafinal, add) {
    run_Ajax(root + "eventos/registrarevento", { userid: id(), nombre: nombre, direccion: direccion, fechainicio: fechainicio, fechafinal: fechafinal, add: add.split(',') }, "#rendersection", function () { closedialog1(); eventoslisteners(); });
}

function actualizarevento(eventoid, nombre, direccion, fechainicio, fechafinal, cancelado, add, rem) {
    run_Ajax(root + "eventos/actualizarevento", { userid: id(), eventoid: parseInt(eventoid), nombre: nombre, direccion: direccion, fechainicio: fechainicio, fechafinal: fechafinal, cancelado: cancelado == '1', add: add.split(','), rem: rem.split(',') }, "#rendersection", function () { closedialog1(); eventoslisteners(); });
}


//_________________________________________dialog methods________________________

function dialog_configurations() {
    return {
        autoOpen: false,
        modal: true,
        closeOnEscape: true,
        draggable: false,
        resizable: false,
        //width: 500,
        //height: 500,
        dialogClass: "dialogo"
    }
}

function dialog1_configurations() {
    return {
        autoOpen: false,
        modal: true,
        closeOnEscape: true,
        draggable: false,
        resizable: false,
        width: 330,
        //height: 500,
        dialogClass: "dialogo"
    }
}

//_______________________________escalar methods__________________

function getValues(l) {
    var v = [];
    for (var i = 0; i < l.length; i++) {
        var aux = $(l[i]).attr("data-values").toString();
        if (aux.length > 0) { v[i] = aux; }
    }
    return v;
}

function getValuesContent(l) {
    var v = [];
    for (var i = 0; i < l.length; i++) {
        var aux = $(l[i]).attr("data-values").toString();
        var aux1 = $(l[i]).val();
        if (aux.length > 0 || aux1.length > 0) {
            v[i] = aux + "," + aux1;
        }
    }
    return v;
}

function getaccionidcontent(l) {
    var v = [];
    for (var i = 0; i < l.length; i++) {
        var aux = $(l[i]).attr("id");
        var aux1 = $(l[i]).val();
        if (aux.length > 0 || aux1.length > 0) {
            v[i] = aux + "," + aux1;
        }
    }
    return v;
}

function refreshlisteners() {
    $(".quitarvalor").unbind("click");
    $(".quitarvalor").click(function () {
        var values = $(this).parent().prev().children("input").attr("data-values").split(",");
        if (values.length != 2 || values[0] == 1) {
            $(this).parent().parent().remove();
        } else {
            $(this).parent().prev().children("input").attr("data-values", "3," + values[1]);
            $(this).parent().parent().hide();
        }
    });
}

function addfiled() {
    $("#bodytabla").append('<tr><td class="inputtd"><input class="inputp valor" data-values="1,-1"/></td><td class="btntd"><button class="quitarvalor botoneliminar"></button></td>');
    $(".valor").last().focus();
    refreshlisteners();
}

/*_________________________productos methods________________*/

function removevalue (list, value, separator) {
    separator = separator || ",";
    var values = list.split(separator);
    for (var i = 0 ; i < values.length ; i++) {
        if (values[i] == value) {
            values.splice(i, 1);
            return values.join(separator);
        }
    }
    return list;
}

function addvalue (list, value, separator) {
    separator = separator || ",";
    if (list.length > 0) {
        return list + separator + value;
    }
    return value;
}

function updatelists(add, rem, id, current, last) {
    if ($(current).hasClass("checkboxcheked")) {
        if (add.indexOf(id) >= 0) {
            add = removevalue(add, id);
        } else {
            rem = addvalue(rem, id);
        }
        $(current).toggleClass("checkboxcheked checkboxuncheked");
    } else {
        if (last != null && last.length > 0) {
            var idaux = $(last[0]).attr("id");
            if (add.indexOf(idaux) >= 0) {
                add = removevalue(add, idaux);
            } else {
                rem = addvalue(rem, idaux);
            }
            $(last).toggleClass("checkboxcheked checkboxuncheked");
        }

        if (rem.indexOf(id) >= 0) {
            rem = removevalue(rem, id);
        } else {
            add = addvalue(add, id);
        }
        $(current).toggleClass("checkboxcheked checkboxuncheked");
    }
    $("#sclrslist").attr("data-add", add);
    $("#sclrslist").attr("data-remove", rem);
}

function updatedescuentoslists(add, rem, id, current) {
    if ($(current).hasClass("checkboxcheked")) {
        if (add.indexOf(id) >= 0) {
            add = removevalue(add, id);
        } else {
            rem = addvalue(rem, id);
        }
    } else {
        if (rem.indexOf(id) >= 0) {
            rem = removevalue(rem, id);
        } else {
            add = addvalue(add, id);
        }
    };

    $(current).toggleClass("checkboxcheked checkboxuncheked");
    $("#contactoslist").attr("data-add", add);
    $("#contactoslist").attr("data-remove", rem);
}

function refreshprodlisteners() {
    $(".eliminarpronumerica").unbind("click");
    $(".eliminarpronumerica").click(function () {
        var values = $(this).parent().prev().children("input").attr("data-values").split(",");
        if (values.length != 2 || values[0] == 1) {
            $(this).parent().parent().remove();
        } else {
            $(this).parent().prev().children("input").attr("data-values", "3," + values[1]);
            $(this).parent().parent().hide();
        }
    });
}

function addfieldnumerica(id,nombre) {
    $("#valoresnumericas").append('<tr><td class="lbltd"><label class="lblnegro">'+nombre+'</label><input id="'+id+'" class="inputp valornumerica" data-accion="1" value="" /></td><td class="btntd"><button class="botoneliminar eliminarpronumerica"></button></td></tr>');
    $(".valor").last().focus();
    refreshprodlisteners();
}