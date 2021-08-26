//Usuario

inicializador = {
    init: function () {
        obterUsuarios();
        //LoadNewModal();
    }
}

function LoadNewModal() {
    $.ajax({
        type: 'GET',
        url: '/Funcionario/ObterFuncionariosSemUsuario',
        success: function (res) {
            
            carregaFuncionarioSemCadastro(res);
        }
    });
}

function carregaFuncionarioSemCadastro(res) {
    var data = [];

    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_fun,
            text: res[i].nome
        });
    }
    
    $("#funcionario").select2({
        data: data
    });
}





function obterUsuarios() {
    $.ajax({
        type: 'GET',
        url: '/Usuario/ObterUsuarios',
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

function alterar(login) {
    window.location.href = '/Usuario/Alterar/' + login;
}

function excluir(login) {
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
                url: '/Usuario/Excluir',
                data: {
                    login
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O Usuario foi excluido.',
                            'success'
                        )
                        obterTodos();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            text: 'Erro au excluir o Usuario!',
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
    var nivel;
    var status;
    var login;
    for (var i = 0; i < res.length; i++) {
        login = "'" + res[i].login + "'";

        action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Cliente" onclick="alterar(' + login + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Cliente" onclick="excluir(' + login + ')"><i class="la la-trash"></i></button>';
        if (res[i].nivel == 1)
            nivel = "Funcionario";
        else if (res[i].nivel == 2)
            nivel = "Vendedor";
        else
            nivel = "Gerente";


        if (res[i].funcionario.status == 'A')
            status = "Ativo";
        else if (res[i].funcionario.status == 'D')
            status = "Desligado";
        else
            status = "De Ferias";

        data.push([
            res[i].login,
            res[i].funcionario.nome,
            nivel,
            status,
            action
        ]);
    }
    $('#tableUsuario').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}


inicializador.init();