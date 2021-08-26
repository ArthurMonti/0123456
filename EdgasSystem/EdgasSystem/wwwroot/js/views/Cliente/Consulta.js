//Cliente
var inicializador = {
    init: function() {
        obterClientes()
    }
}

function obterClientes() {
    $.ajax({
        type: 'GET',
        url: '/Cliente/ObterClientes',
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

function alterar(cod_cliente) {
    window.location.href = '/Cliente/Alterar/' + cod_cliente;
}

function excluir(cod_cliente) {

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
                url: '/Cliente/Excluir',
                data: {
                    cod_cliente
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
        
        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + res[i].id + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + res[i].id + ')"><i class="la la-trash"></i></button>';
        data.push([
            res[i].name,
            res[i].cpf,
            res[i].address + ' - ' + res[i].city,
            res[i].fone,
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