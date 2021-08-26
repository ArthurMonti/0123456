//Cliente

function gravar() {
    var nome = $("#nome").val();
    var cpf = $("#cpf").val();
    var fone = $("#fone").val();
    var endereco = $("#endereco").val();
    endereco += ", " + $("#numero_endereco").val();
    var bairro = $("#bairro").val();
    var cidade = $("#cidade").val();
    var cep = $("#cep").val();
    var limite_fiado = $("#fiado").val();
    if ($("#form_cadastro").valid()) {

        var dados = {
            nome, cpf, fone, endereco, bairro, cidade, cep, limite_fiado
        }
        $.ajax({
            type: 'POST',
            url: '/Cliente/GravarCliente', // Chama no Controller
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
                        title: 'Erro ao Gravar!',
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
