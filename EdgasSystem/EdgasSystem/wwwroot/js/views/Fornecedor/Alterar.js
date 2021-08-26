//Fornecedor

function buscarFornecedor(cod_fornecedor) {
    $("#cod_fornecedor").val(cod_fornecedor);
    $.ajax({
        type: 'POST',
        url: '/Fornecedor/BuscarFornecedor',
        data: {
            cod_fornecedor
        },
        success: function (res) {
            
            $("#nome").val(res.nome);
            $("#cnpj").val(res.cnpj);
            var separado = res.endereco.split(", ");
            $("#endereco").val(separado[0]);
            $("#numero_endereco").val(separado[1]);

            $("#fone").val(res.telefone);
            
            $("#cidade").val(res.cidade);
            $("#cep").val(res.cep);
            $("#contato").val(res.contato);
            $("#telefone_contato").val(res.telefone_contato);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function gravar() {

    if ($("#form_cadastro").valid()) {
        var cod_fornecedor = $("#cod_fornecedor").val();
        var cnpj = $("#cnpj").val();
        var nome = $("#nome").val();
        var endereco = $("#endereco").val();
        endereco += ", " + $("#numero_endereco").val();
        var cidade = $("#cidade").val();
        var cep = $("#cep").val();
        var telefone = $("#fone").val();
        var contato = $("#contato").val();
        var telefone_contato = $("#telefone_contato").val();


        var dados = {
            cod_fornecedor,cnpj, nome, endereco, cidade, cep, telefone, contato, telefone_contato
        }
        $.ajax({
            type: 'POST',
            url: '/Fornecedor/AlterarFornecedor',
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
                        title: 'res.controlerrormessage!',
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

/*Inicio - Validações de Formulario */
jQuery.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Please enter exactly {0} characters."));


$(document).ready(function () {
    $("#form_cadastro").validate({

        rules: {
            cnpj: {
                exactlength: 18,
                required: true
            },
            cep: {
                exactlength: 9,
                required: true
            },
            fone: {
                exactlength: 15,
                required: true
            },
            telefone_contato: {
                exactlength: 15,
                required: false
            },
            endereco: "required",
            nome: "required",
            cidade: "required"
        },
        messages: {
            cnpj: {
                exactlength: "CNPJ inválido."
            },
            cep: {
                exactlength: "CEP inválido."
            },
            fone: {
                exactlength: "Telefone inválido."
            },
            telefone_contato: {
                exactlength: "Telefone do contato inválido."
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
/*Fim - Validações de Formulario */