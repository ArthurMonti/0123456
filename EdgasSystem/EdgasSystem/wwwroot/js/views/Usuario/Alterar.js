//Usuario
Inicializador = {
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


function buscaUsuario(login) {
    $.ajax({
        type: 'POST',
        url: '/Usuario/BuscarUsuario',
        data: {
            login
        },
        success: function (res) {


            var login = $("#name").val();
            var nivel = $("#level").val();
            var senha = $("#password").val();
            var funcionario = $("#funcionario").val();

            $("#name").val(res.login);
            $("#level").val(res.nivel).trigger('change');
            $("#password").val(res.senha);
            $("#funcionario").val(res.funcionario.cod_fun).trigger('change');
            //document.write($("#fiado").val())
            
                

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


function gravar() {
    var cod_produto = $("#cod_produto").val();
    var cod_tipo_produto = $("#tipo_produto").val();
    var nome = $("#nome").val();
    var descricao = $("#descricao").val();
    var preco = $("#preco").val();
    var qtde_estoque = $("#qtde_estoque").val();

    if ($("#form_cadastro").valid()) {

        var dados = {
            cod_produto,cod_tipo_produto, nome, descricao, preco, qtde_estoque
        }
        $.ajax({
            type: 'POST',
            url: '/Produto/AlterarProduto', // Chama no Controller
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
                        title: 'Erro ao Alterar!',
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

/*Validações dos formularios*/
jQuery.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Please enter exactly {0} characters."));

$(document).ready(function () {
    $("#form_cadastro").validate({
        rules: {
            cpf: "cpf",
            cep: {
                exactlength: 9,
                required: true
            },
            fone: {
                exactlength: 15,
                required: true
            },
            bairro: "required",
            numero_endereco: "required",
            endereco: "required",
            cidade: "required"
        },
        messages: {
            cpf: {
                cpf: "CPF inválido."
            },
            cep: {
                exactlength: "CEP inválido."
            },
            fone: {
                exactlength: "Telefone inválido."
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