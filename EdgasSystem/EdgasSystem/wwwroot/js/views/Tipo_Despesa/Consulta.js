//Tipo_Despesa

var inicializador = {
    init: function () {
        obterTipo_Despesa()
    }
}

function obterTipo_Despesa() {
    $.ajax({
        type: 'GET',
        url: '/Tipo_Despesa/ObterTipo_Despesas',
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

function alterar(cod_tipo_despesa) {
    window.location.href = '/Tipo_Despesa/Alterar/' + cod_tipo_despesa;
}

function excluir(cod_tipo_despesa) {

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
                url: '/Tipo_Despesa/Excluir',
                data: {
                    cod_tipo_despesa
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O registro foi excluido.',
                            'success'
                        )
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
        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + res[i].cod_tipo_despesa + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + res[i].cod_tipo_despesa + ')"><i class="la la-trash"></i></button>';
        data.push([
            res[i].nome,
            res[i].descricao,
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