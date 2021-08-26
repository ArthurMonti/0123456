//Produto
var inicializador = {
    init: function() {
        obterProdutos()
    }
}

function obterProdutos() {
    $.ajax({
        type: 'GET',
        url: '/Produto/ObterProdutos',
        dataType: 'json',
        success: function (res) {
            if (res != null)
                carregaTabela(res);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function alterar(cod_produto) {
    window.location.href = '/Produto/Alterar/' + cod_produto;
}

function excluir(cod_produto) {

    Swal.fire({
        title: 'Você tem certeza?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, eu quero deletar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/Produto/Excluir',
                data: {
                    cod_produto
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O registro foi excluido.',
                            'success'
                        )
                        limparTabela();
                        obterTodos();
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }
    });
}

function carregaTabela(res) {
    var data = [];

    var action;
    for (var i = 0; i < res.length; i++) {
        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + res[i].cod_produto + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + res[i].cod_produto + ')"><i class="la la-trash"></i></button>';
        data.push([
            res[i].nome,
            res[i].tipo_produto.nome,
            res[i].preco.toFixed(2),
            res[i].qtde_estoque,
            action

            
        ]);
    }
    $('#tableCliente').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}


inicializador.init();