//Veiculo
Inicializador = {
    init: function () {
        LoadInicial();
    }
}



function LoadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Veiculo/LoadInicialView',
        success: function (res) {
            carregaFuncionario(res.funcionario);
        }
    });
}

function gravar() {
    var placa = $("#placa").val();
    var descricao = $("#descricao").val();
    var cod_fun = $("#funcionario").val();
    var quilometragem = $("#quilometragem").val();
    
    var status = $("#status").val();
   

    if ($("#form_cadastro").valid()) {

        var dados = {
            placa, descricao, cod_fun, status, quilometragem
        }
        $.ajax({
            type: 'POST',
            url: '/Veiculo/GravarVeiculo', // Chama no Controller
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Gravado com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
                else {
                    Swal.fire({
                        icon: 'warning',
                        title: res.controlerrormessage,
                        showConfirmButton: false,
                        timer: 1500
                    });
                }

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
    else
        Swal.fire({
            icon: 'warning',
            title: 'Preencha todos os campos corretamente!',
            showConfirmButton: false,
            timer: 1500
        });
}


function carregaFuncionario(res) {
    var data = [];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_fun,
            text: res[i].nome
        });
    }

    $("#funcionario").select2({
        data: data
    });
}


/*Validações dos formularios*/
jQuery.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Please enter exactly {0} characters."));

$(document).ready(function () {
    $("#form_cadastro").validate({
        rules: {
            placa: {
                exactlength: 7,
                required: true
            },
            descricao: "required",
            quilometragem: "required",
            status: "required",
            funcionario: "required"
        },
        messages: {
            placa: {
                exactlength: "Placa inválido."
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
});

Inicializador.init();