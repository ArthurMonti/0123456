
function buscarFuncionario(cod_fun) {
    $.ajax({
        type: 'POST',
        url: '/Funcionario/BuscarFuncionario',
        data: {
            cod_fun
        },
        success: function (res) {
            $("#cod_fun").val(cod_fun);
            $("#nome").val(res.nome);
            $("#cpf").val(res.cpf);         
            $("#telefone").val(res.telefone);
            $("#salario").val(res.salario.toFixed(2))
            $("#email").val(res.email);
            var data = new Date(res.data_admissao);
            var day = ("0" + data.getDate()).slice(-2);
            var month = ("0" + (data.getMonth() + 1)).slice(-2);
            var today = data.getFullYear() + "-" + (month) + "-" + (day);

            $("#data_admissao").val(today);
            $("#status").val(res.status).trigger('change');

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function gravar() {

    if ($("#form_cadastro").valid()) {
        var cod_fun = $("#cod_fun").val();
        var nome = $("#nome").val();
        var cpf = $("#cpf").val();
        var data_admissao = $("#data_admissao").val();
        var telefone = $("#telefone").val();
        var salario = $("#salario").val();
        var email = $("#email").val();

        var status = $("#status").val();

        var dados = {
            cod_fun,nome, cpf, telefone, salario, email, data_admissao, status
        }

        $.ajax({
            type: 'POST',
            url: '/Funcionario/AlterarFuncionario',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Alterado com sucesso!',
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
    } else
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
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());      // radio/checkbox?
            } else if (element.hasClass('select2-hidden-accessible')) {
                error.insertAfter(element.next('span'));  // select2
                element.next('span').addClass('error').removeClass('valid');
            } else {
                error.insertAfter(element);               // default
            }
        },
        rules: {
            cpf: "cpf",
            cep: {
                exactlength: 9,
                required: true
            },
            telefone: {
                exactlength: 15,
                required: true
            },
            email: "required",
            nome: "required",
            data_admissao: "required",
            status: "required",
            salario: "required"
        },
        messages: {
            cpf: {
                cpf: "CPF inválido."
            },
            cep: {
                exactlength: "CEP inválido."
            },
            telefone: {
                exactlength: "Telefone inválido."
            }
        },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
        }
    });
});
