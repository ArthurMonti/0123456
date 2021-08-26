//Tipo_Despesa

function gravar() {
    var nome = $("#nome").val();
    var descricao = $("#descricao").val();
    if ($("#form_cadastro").valid()) {

        var dados = {
            nome, descricao
        }
        $.ajax({
            type: 'POST',
            url: '/Tipo_Despesa/GravarTipo_Despesa', // Chama no Controller
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
$(document).ready(function () {
    $("#form_cadastro").validate({
        rules: {
            nome: "required",
            descricao: "required"

        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
});