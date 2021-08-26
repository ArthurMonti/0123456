//Fornecedor

var inicializador = {
    init: function () {
        obterTodos()
    }
}

function obterTodos() {
    $.ajax({
        type: 'GET',
        url: '/Fornecedor/ObterFornecedores',
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

function carregaTabela(res) {
    var data = [];
    
    var action;
    for (var i = 0; i < res.length; i++) {
        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + res[i].cod_fornecedor + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + res[i].cod_fornecedor + ')"><i class="la la-trash"></i></button>';
        data.push([
            res[i].nome,
            res[i].cnpj,
            res[i].endereco + ' - ' + res[i].cidade,
            res[i].telefone,
            action
        ]);
        
    }
    $('#tableFornecedor').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}

function alterar(cod_fornecedor)
{
    window.location.href = '/Fornecedor/Alterar/' + cod_fornecedor;
}

function excluir(cod_fornecedor) {
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
            url: '/Fornecedor/Excluir',
            data: {
                cod_fornecedor
            },
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire(
                        'Excluido!',
                        'O registro foi excluido.',
                        'success'
                    )
                    obterTodos();
                }
                else if (res.sqlerrornumber = "547") {
                    Swal.fire({
                        icon: 'error',
                        text: 'O Fornecedor possui movimentação!',
                        showConfirmButton: true
                    });
                }

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
        }
    });
}


inicializador.init();
