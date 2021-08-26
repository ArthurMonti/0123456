//Produto
Inicializador = {
    init: function () {
        LoadInicial();
    }
}

function LoadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Produto/LoadInicialView',
        success: function (res) {
            carregaTipo_Produto(res.tipo_produto);
        }
    });
}

function gravar() {
    var cod_tipo_produto = $("#tipo_produto").val();
    var nome = $("#nome").val();
    var descricao = $("#descricao").val();
    var preco = $("#preco").val();
    var qtde_estoque = $("#qtde_estoque").val();
    
    if ($("#form_cadastro").valid()) {

        var dados = {
            cod_tipo_produto,nome, descricao, preco, qtde_estoque
        }
        $.ajax({
            type: 'POST',
            url: '/Produto/GravarProduto', // Chama no Controller
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


function carregaTipo_Produto(res) {
    var data = [];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo_produto,
            text: res[i].nome
        });
    }

    $("#tipo_produto").select2({
        data: data
    });
};


/*Validações dos formularios*/

$(document).ready(function () {
    $("#form_cadastro").validate({
        rules: {
            
            descricao: "required",
            preco: "required",
            qtde_unitaria: "required",
            tipo_produto: "required"
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