//Funcionario
var inicializador = {
    init: function () {
        obterFuncionarios()
    }
}

function obterFuncionarios() {
    $.ajax({
        type: 'GET',
        url: '/Funcionario/ObterFuncionarios',
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

function alterar(cod_fun) {
    window.location.href = '/Funcionario/Alterar/' + cod_fun;
}

function excluir(cod_fun) {
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
                url: '/Funcionario/Excluir',
                data: {
                    cod_fun
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O registro foi excluido.',
                            'success'
                        )
                        obterTodos();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            text: 'O Funcionário possui movimentação!',
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

function carregaTabela(res) {
    var data = [];

    var action;
    var status;
    for (var i = 0; i < res.length; i++) {
        action = '<td><div class="form-button-action"> <button type="button" data-toggle="tooltip" title="Editar Funcionário" class="btn btn-sm btn-clean btn-icon" data-original-title="Editar Funcionário" onclick="alterar(' + res[i].cod_fun + ')"><i class="la la-edit"></i></button><button type="button" data-toggle="tooltip" title="" class="btn btn-sm btn-clean btn-icon" data-original-title="Excluir" onclick="excluir(' + res[i].cod_fun + ')"> <i class="la la-trash"></i></button ></div ></td > ';
        if (res[i].status == 'A')
            status = "Ativo";
        else if (res[i].status == 'D')
            status = "Desligado";
        else
            status = "De Ferias";
        data.push([
            res[i].nome,
            res[i].cpf,
            res[i].email,
            res[i].telefone,
            status,
            action
        ]);
    }
    $('#tableFuncionario').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}


inicializador.init();