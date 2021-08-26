//Tipo_Despesa
function buscarTipoProduto(cod_tipo_despesa) {
    $.ajax({
        type: 'GET',
        url: '/Tipo_Despesa/BuscarTipo_Despesa',
        data: {
            cod_tipo_despesa
        },
        success: function (res) {
            
            $("#cod_tipo_despesa").val(cod_tipo_despesa);
            $("#nome").val(res.nome);
            $("#descricao").val(res.descricao);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}



function gravar() {
    var cod_tipo_despesa = $("#cod_tipo_despesa").val();
    var nome = $("#nome").val();
    var descricao = $("#descricao").val();

    if ($("#form_cadastro").valid()) {

        var dados = {
            cod_tipo_despesa,nome, descricao
        }
        $.ajax({
            type: 'POST',
            url: '/Tipo_Despesa/AlterarTipo_Despesa', // Chama no Controller
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
    }
    else
        Swal.fire({
            icon: 'warning',
            title: 'Preencha todos os campos corretamente!',
            showConfirmButton: false,
            timer: 1500
        });
}

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