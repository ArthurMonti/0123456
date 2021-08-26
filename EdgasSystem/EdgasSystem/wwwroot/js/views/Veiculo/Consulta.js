//Veiculo
var inicializador = {
    init: function () {
        document.write(123);
        obterVeiculos()
    }
}

function obterVeiculos() {
    $.ajax({
        type: 'GET',
        url: '/Veiculo/ObterVeiculos',
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

function alterar(placa) {
    window.location.href = '/Veiculo/Alterar/' + placa;
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
                url: '/Veiculo/Excluir',
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
    var placa;
    var action;
    for (var i = 0; i < res.length; i++) {
        placa = "'" + res[i].placa + "'";
        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + placa + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + placa + ')"><i class="la la-trash"></i></button>';
        data.push([
            res[i].placa,
            res[i].funcionario.nome,
            res[i].quilometragem.toFixed(2),
            res[i].status,
            action


        ]);
    }
    $('#tableVeiculo').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}


inicializador.init();