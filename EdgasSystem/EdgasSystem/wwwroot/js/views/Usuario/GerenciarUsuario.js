function return_select2value_funcionario() {
    var id = $('#selectfuncionario').val();
    return id;
}


var gerenciaUsuario = {
    init: function () {
        gerenciaUsuario.ObterTodosUsuarios();
        document.getElementById('addNovoUsuario').onclick = gerenciaUsuario.cadastrarUsuario;
    },
    ObterTodosUsuarios: function () {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            }
        }
        fetch("/Seguranca/ObterTodosUsuarios", config)
            .then(function (res) {
                return res.json();
            })
            .then(function (res) {
                gerenciaUsuario.DataTableUsuarios(res);
            })
            .catch(function () {
            });
    },

    cadastrarUsuario: function () {
        gerenciaUsuario.validaCampos();

        if (valida.ok == true) {
            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(valida.dados)
            }
            fetch("/Seguranca/GravarUsuario", config)
                .then(function (res) {
                    return res.json();
                })
                .then(function (res) {
                    if (res.ok == true) {
                        swal({
                            icon: 'success',
                            title: 'Cadastrado com sucesso'
                        });
                        $('#addRowModal').modal('hide');
                    }
                    else
                        swal({
                            icon: 'error',
                            title: res.msg
                        });
                })
                .catch(function () {
                    alert("Erro de aplicação");
                })
        } else {
            toastr.error('Informe todos os campos!');
        }
        
    },

    validaCampos: function () {
        var logon = document.getElementById('logon').value;
        var senha = document.getElementById('senha').value;
        var confirmar_senha = document.getElementById('confirmar_senha').value;
        var cod_funcionario = return_select2value_funcionario();
        var email = document.getElementById('email').value;
        var ok = true;

        if (senha != confirmar_senha) {
            document.getElementById("senha_controle").classList.add("has-error");
            document.getElementById("confirmar_senha_controle").classList.add("has-error");
            ok = false;
        }
        else {
            document.getElementById("senha_controle").classList.remove("has-error");
            document.getElementById("confirmar_senha_controle").classList.remove("has-error");
            ok = true;
        }
        valida = {
            ok,
            dados: {
                id: '0',
                logon: logon,
                senha: senha,
                ativo: 'S',
                cod_funcionario,
                email: 'mrcmonti@live.com'
            }
        }
        return valida;
    },
    obterListaFuncionario: function () {
        var data = [];

        data.push({
            id: 1,
            text: 'Wellington Febbo'
        });

        data.push({
            id: 2,
            text: 'Carol Gaiaz'
        });
        $('#selectfuncionario').select2({
            data: data_func,
            theme: "bootstrap"
        });

    },
    DataTableUsuarios: function (res) {
        var data = [];

        var action;
        for (var i = 0; i < res.length; i++) {
            var action = '<td> <div class="form-button-action"> <button type="button" data-toggle="tooltip" title="Editar Usuário" class="btn btn-link btn-primary btn-lg" data-original-title="Editar Email"> <i class="fa fa-edit"></i> </button>  <button type="button" data-toggle="tooltip" title="" class="btn btn-link btn-primary btn-lg" data-original-title="Editar Senha"> <i class="fa fa-key"></i> </button> <button type="button" data-toggle="tooltip" title="" class="btn btn-link btn-danger" data-original-title="Remove"> <i class="fa fa-times"></i> </button> </div> </td>';



            data.push([
                res[i].id,
                res[i].logon,
                res[i].cod_funcionario,
                res[i].email,
                res[i].data_inclusao,
                res[i].data_bloqueio,
                res[i].ativo,
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


}

gerenciaUsuario.init();