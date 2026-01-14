$(function () {

	loadData();

});


var state = {

	auxId: 0,
	editar: false

};

var error = "Ocurrió un error insesperado en el sitio, por favor intentelo mas tarde o pongase en contacto con su administrador.";
var success = "La accion se ralizó con exito";
var datosIncorrectos = "Datos incorrectos, vuelve a intentarlo.";

function loadData(){

	var filtro = $('#select_status').val();

	$.ajax({
		url: SITE_URL +'/Home/TablaPersonas',
		type:'POST',
		data: { Filtro: filtro},
		dataType:'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
			$("#tbody").empty();
		},
		error: function(error){
			//console.log(error);
			MsgAlerta("Error!", error, 3000, "error");
			LoadingOff();
		},
		success: function(data){
			//console.log(data);
			LoadingOff();

			if(data != ""){

				var TablaPersonas = "";

				for (var i = 0; i < data.length; i++) {
					//Console.log(data[i]);
					TablaPersonas += '<tr>';
					TablaPersonas += '<td>' + data[i].Id + '</td>';
					TablaPersonas += '<td>' + data[i].Nombre + '</td>';
					TablaPersonas += '<td>' + data[i].Direccion + '</td>';
					TablaPersonas += '<td>' + data[i].Telefono + '</td>';
					TablaPersonas += '<td>' + data[i].Estatus + '</td>';
					TablaPersonas += '<td>';
					if (data[i].Estatus == 1) {
						TablaPersonas += `
			   			<button class="btn btn-danger" onclick="eliminar(`+ data[i].Id + `)" title="Eliminar" type="">
							<i class="fa fa-trash" aria-hidden="true"></i>
			   			</button>
			   			<button class="btn btn-primary" onclick="detalles(`+ data[i].Id + `)"  title="Ver Detalles" type="">
							<i class="fa fa-eye" aria-hidden="true"></i>
               	    	</button></tr>`;
					}
					if (data[i].Estatus == 0) {
						TablaPersonas += `
			   			<button class="btn btn-success" onclick="reactivar(`+ data[i].Id + `)" title="Reactivar" type="">
							<i class="fa fa-check" aria-hidden="true"></i>
			   			</button></tr>`;
					}

				}

				$('#tbody').html(TablaPersonas);
			}
			else{
				MsgAlerta("Atencion!", "No hay personas para mostrar", 5000, "warning");
			}
		}
	});

}

function detalles(id){

	$.ajax({
		url: SITE_URL + '/Home/DetallesPersona',
		type: 'POST',
		data: { Id: id },
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
		},
		error: function (error) {
			//console.log(error);
			MsgAlerta("Error!", error, 3000, "error");
			LoadingOff();
		},
		success: function (data) {
			//console.log(data);
			LoadingOff();

			if (data != "") {

				$("#inputNombre").val(data[0].Nombre);
				$("#inputApellidoP").val(data[0].ApellidoP);
				$("#inputApillidoM").val(data[0].ApellidoM);
				$("#inputDireccion").val(data[0].Direccion);
				$("#inputTelefono").val(data[0].Telefono);

				state.editar = true;
				state.auxId = id;

				$('#ModalAgregarPersonas').modal('show');

			}
			else {
				MsgAlerta("Atencion!", "No hay personas para mostrar", 5000, "warning");
			}
		}
	});


}

function guardarPersonas() {

	validarFormulario('.vfper', function (json) {

		if (json.bool) {

			let info = {};

			info.Nombre = $("#inputNombre").val();
			info.ApellidoP = $("#inputApellidoP").val();
			info.ApellidoM = $("#inputApillidoM").val();
			info.Direccion = $("#inputDireccion").val();
			info.Telefono = $("#inputTelefono").val();
			//console.log(info)

			if (state.editar == true) {
				info.Id = state.auxId;

				sendPersonaEdit(info);

			}

			else {
				sendPersona(info);
			}

		}
		else {

			MsgAlerta("Atención!", "Llenar campos faltantes", 3000, "warning");
		}
	});
}

function sendPersona(info) {

	$.ajax({
		type: "POST",
		contentType: "application/x-www-form-urlencoded",
		url: SITE_URL + "/Home/Guardar",
		data: info,
		dataType: "JSON",
		beforeSend: function () {
			LoadingOn("Espere...");
		},
		success: function (data) {
			if (data) {

				LoadingOff();
				LimpiarPersonasForm();

				MsgAlerta("Realizado!", "Registro guardado", 3000, "success");
				$('#ModalAgregarPersonas').modal('hide');

				loadData();

			} else {
				ErrorLog("Error"," Error controlado");
				LoadingOff();
			}
		},
		error: function (error) {
			ErrorLog(error.responseText, "Error de comunicación, verifica tu conexión y vuelve a intentarlo.");
			LoadingOff();

		}
	});


}

function sendPersonaEdit(info) {

	$.ajax({
		type: "POST",
		contentType: "application/x-www-form-urlencoded",
		url: SITE_URL + "/Home/Editar",
		data: info,
		dataType: "JSON",
		beforeSend: function () {
			LoadingOn("Espere...");
		},
		success: function (data) {
			if (data) {

				LoadingOff();
				LimpiarPersonasForm();

				MsgAlerta("Realizado!", "Registro guardado", 3000, "success");
				$('#ModalAgregarPersonas').modal('hide');

				loadData();

			} else {
				ErrorLog("Error", " Error controlado");
				LoadingOff();
			}
		},
		error: function (error) {
			ErrorLog(error.responseText, "Error de comunicación, verifica tu conexión y vuelve a intentarlo.");
			LoadingOff();

		}
	});

}

function eliminar(id){

	$.ajax({
		type: "POST",
		contentType: "application/x-www-form-urlencoded",
		url: SITE_URL + "/Home/Eliminar",
		data: { Id: id },
		dataType: "JSON",
		beforeSend: function () {
			LoadingOn("Espere...");
		},
		success: function (data) {

			if (data) {

				LoadingOff();

				MsgAlerta("Realizado!", "Registro Eliminado", 3000, "success");

				loadData();

			} else {
				ErrorLog("Error", " Error controlado");
				LoadingOff();
			}
		},
		error: function (error) {
			ErrorLog(error.responseText, "Error de comunicación, verifica tu conexión y vuelve a intentarlo.");
			LoadingOff();

		}
	});

}

function reactivar(id) {

	$.ajax({
		type: "POST",
		contentType: "application/x-www-form-urlencoded",
		url: SITE_URL + "/Home/Reactivar",
		data: { Id: id },
		dataType: "JSON",
		beforeSend: function () {
			LoadingOn("Espere...");
		},
		success: function (data) {
			if (data) {

				LoadingOff();

				MsgAlerta("Realizado!", "Registro Activado", 3000, "success");


				loadData();

			} else {
				ErrorLog("Error", " Error controlado");
				LoadingOff();
			}
		},
		error: function (error) {
			ErrorLog(error.responseText, "Error de comunicación, verifica tu conexión y vuelve a intentarlo.");
			LoadingOff();

		}
	});

}


$(document).on('change', '#select_status', function(e){
	loadData();
});


$(document).on('keyup', '#txt_busqueda', function (e) {

	$.ajax({
		url: SITE_URL + '/Home/TablaPersonasbusqueda',
		type:'POST',
		async: false,
		data: { Busqueda: $(this).val()},
		dataType:'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
			$("#tbody").empty();

		},
		error: function(error){
			//console.log(error);
			MsgAlerta("Error!", error, 5000, "error");
			LoadingOff();
		},
		success: function(data){
			console.log(data);
			LoadingOff();

			if (data != "") {

				var TablaPersonas = "";

				for (var i = 0; i < data.length; i++) {
					//Console.log(data[i]);
					TablaPersonas += '<tr>';
					TablaPersonas += '<td>' + data[i].Id + '</td>';
					TablaPersonas += '<td>' + data[i].Nombre + '</td>';
					TablaPersonas += '<td>' + data[i].Direccion + '</td>';
					TablaPersonas += '<td>' + data[i].Telefono + '</td>';
					TablaPersonas += '<td>' + data[i].Estatus + '</td>';
					TablaPersonas += '<td>';
					if (data[i].Estatus == 1) {
						TablaPersonas += `
			   			<button class="btn btn-danger" onclick="eliminar(`+ data[i].Id + `)" title="Eliminar" type="">
							<i class="fa fa-trash" aria-hidden="true"></i>
			   			</button>
			   			<button class="btn btn-primary" onclick="detalles(`+ data[i].Id + `)"  title="Ver Detalles" type="">
							<i class="fa fa-eye" aria-hidden="true"></i>
               	    	</button></tr>`;
					}
					if (data[i].Estatus == 0) {
						TablaPersonas += `
			   			<button class="btn btn-success" onclick="reactivar(`+ data[i].Id + `)" title="Reactivar" type="">
							<i class="fa fa-check" aria-hidden="true"></i>
			   			</button></tr>`;
					}

				}

				$('#tbody').html(TablaPersonas);
			}
			else {
				MsgAlerta("Atencion!", "No hay personas para mostrar", 5000, "warning");
			}
		}
	});
});

// FUNCION QUE REESTABLE EL MODAL Disponibilidad
function LimpiarPersonasForm() {

	document.getElementById('lblAddPersonas').innerHTML = "";

	$(".vfper").val("");

	state.auxId = 0;
	state.editar = false;

	$(".is-invalid").removeClass('is-invalid');
}

//Abrir el modal para agragar Persona
$(document).on('click', '#btn_new', function (e) {

	e.preventDefault();
	LimpiarPersonasForm();
	document.getElementById('lblAddPersonas').innerHTML = "Nuevo Registro";
	$('#ModalAgregarPersonas').modal('show');

});

//Abrir el modal para agragar Persona
$(document).on('click', '#btncerrarPersonas', function (e) {
	e.preventDefault();

	LimpiarPersonasForm();
	$('#ModalAgregarPersonas').modal('hide');

});

// FUNCION QUE VALIDA EL FORMULARIO DE AGREGAR Persona
function validarFormulario(identif, callback) {
	var formularioArr = [];
	var correcto = true;
	$(identif).each(function () {
		var elem = null;
		if ($(this).attr("type") === "checkbox") {
			elem = $(this).prop("checked");
		} else {
			if (parseInt($(this).attr("validar")) > 0) {
				if ($(this).val() === '') {
					correcto = false;
					setError($(this));
				} else if ($(this).val() == -1) {
					correcto = false;
					setError($(this));
				}
				else {
					elem = $(this).val();
				}
			} else {
				if ($(this).val() !== "") {
					elem = $(this).val();
				}
			}
		}

		formularioArr.push(elem);
	});
	quitarInvalidClase(identif);
	var resultado = {
		bool: correcto,
		formulario: formularioArr
	};
	idClassAttrInput = identif;
	callback(resultado);
}

// RETIRAR EL ESTILO ROJO
function quitarInvalidClase(idClassAttr) {
	$(document).on('click focus focusin keyup', idClassAttr, function () {
		$(this).css({ "border": "#ccc 1px solid" });
	});
}

//set error for a input element
function setError(element) {
	element.css({ "border": "#FF0000 1px solid" });
};

function removeError(element) {
	console.log(element)
	element.css({ "border": "#ccc 1px solid" });
};
