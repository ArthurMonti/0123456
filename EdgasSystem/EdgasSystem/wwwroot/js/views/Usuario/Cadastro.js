//Usuario
inicializador = {
    init: function () {
        LoadInicial();
    }
}

function LoadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Funcionario/ObterFuncionariosSemUsuario',
        success: function (res) {

            carregaFuncionarioSemCadastro(res);
        }
    });
}

function carregaFuncionarioSemCadastro(res) {
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

function gravar() {
    if ($("#form_cadastro").valid()) {

        var login = $("#name").val();
        var nivel = $("#level").val();
        var senha = $("#password").val();
        var funcionario = $("#funcionario").val();

        login = login.toString().toLowerCase();

        var dados = {
            login, nivel, senha, funcionario
        }

        $.ajax({
            type: 'POST',
            url: '/Usuario/GravarUsuario',
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
                        title: res.errorMessage,
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    } else
        Swal.fire({
            icon: 'warning',
            title: 'Preencha todos os campos corretamente!',
            showConfirmButton: false,
            timer: 1500
        });
}


jQuery.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Please enter exactly {0} characters."));

$(document).ready(function () {
    $("#form_cadastro").validate({
        rules: {

            login: "required",
            password: "required",
            funcionario: "required",
            level: "required"
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
});


inicializador.init();