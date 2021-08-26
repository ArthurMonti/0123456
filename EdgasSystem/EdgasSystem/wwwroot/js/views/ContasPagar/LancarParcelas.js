var inicializador = {
    init: function () {
        loadInicial()
    }
}


function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/ContasPagar/LoadInicialLancarParcelasView',
        dataType: 'json',
        success: function (res) {
            if (res.tipocontas != null)
                carregaTipoContas(res.tipocontas);
            if (res.fornecedor != null)
                carregaFornecedor(res.fornecedor);
            

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function limparCampos() {
    $("#cod_parcelascontaspagar").val("");
    $("#documento").val("");
    $("#tipocontas").val(0).trigger('change');
    $("#fornecedor").val(0).trigger('change');
    $('#tableParcelas').DataTable().clear().destroy();
    $("#btnGeraParcelas").attr('disabled', false);
}

function carregaTipoContas(res) {
    var data = [];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipocontaspagar,
            text: res[i].descricao
        });
    }
    $('#tipocontas').select2({
        data: data,
        placeholder: "Selecione um tipo de contas"
    });
}

function carregaFornecedor(res) {
    var data = [];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_fornecedor,
            text: res[i].nome
        });
    }

    $('#fornecedor').select2({
        data: data,
        placeholder: "Selecione um fornecedor"
    });

}

function gravarParcela() {
    var documento = $("#documento").val();
    var cod_tipocontaspagar = $("#tipocontas").val();
    var cod_fornecedor = $("#fornecedor").val();
    var qtd_parcelas = $("#qtd_parcelas").val();
    var valor = $("#valor").val();
    var data_vencimento = $("#data_vencimento").val();

    dados = {
        documento, cod_tipocontaspagar, cod_fornecedor, valor, data_vencimento, qtd_parcelas
    }
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/GravarParcela',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dados),
        success: function (res) {
            if (res.codigo > 0) {
                $("#addRowModal").modal('hide');
                Swal.fire({
                    icon: 'success',
                    title: 'Gravado com sucesso!',
                    showConfirmButton: false,
                    timer: 1500
                });
                buscarDocumento(res.codigo);
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: res.sqlerrormessage,
                    showConfirmButton: true
                });
                //buscarParcelasDocumento(documento);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

//Gerenciar Modal
function editarParcela(cod_parcelascontaspagar, parcela) {
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/BuscarParcelaDocumento',
        data: {
            cod_parcelascontaspagar, parcela
        },
        success: function (res) {
            $("#editparcela").val(res.parcela);
            $("#editdata_vencimento").val(new Date(res.data_vencimento).toLocaleDateString());
            $("#editvalor").val(res.valor);
            $("#editRowModal").modal();
        }
        ,
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function atualizarParcela() {
    var cod_parcelascontaspagar = $("#cod_parcelascontaspagar").val();
    var parcela = $("#editparcela").val();
    var valor = $("#editvalor").val();
    var data_vencimento = $("#editdata_vencimento").val();


    dados = {
        cod_parcelascontaspagar, parcela, valor, data_vencimento
    }
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/AtualizarParcela',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dados),
        success: function (res) {
            if (res.ok == true) {
                $("#editRowModal").modal('hide');
                Swal.fire({
                    icon: 'success',
                    title: 'Gravado com sucesso!',
                    showConfirmButton: false,
                    timer: 1500
                });
                buscarDocumento(cod_parcelascontaspagar);
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: res.sqlerrormessage,
                    showConfirmButton: true
                });
                //buscarParcelasDocumento(documento);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function obterContas() {
   $.ajax({
        type: 'GET',
        url: '/ContasPagar/ObterContas',
        success: function (res) {
            if (res != null) {
                $('#tableBuscaConta').DataTable().clear().destroy();
                $("#buscaContas").modal();
                var data = [];
                var data_pagamento;
                for (var i = 0; i < res.length; i++) {
                    if (res[i].data_pagamento == null)
                        data_pagamento = '';
                    else
                        data_pagamento = new Date(res[i].data_pagamento).toLocaleDateString();
                    data.push([
                        res[i].cod_parcelascontaspagar,
                        res[i].documento,
                        res[i].tipocontaspagar.descricao,
                        res[i].fornecedor.nome,
                        res[i].parcela,
                        new Date(res[i].data_vencimento).toLocaleDateString(),
                        res[i].valor,
                        data_pagamento,
                        res[i].valor_pago
                    ]);
                }
                $('#tableBuscaConta').DataTable({
                    data: data,
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
                    }
                });
            }
            else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Nenhum registro encontrado!',
                    showConfirmButton: true
                });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });   
}

function buscarDocumento(cod_parcelascontaspagar) //Busca realizada no botão pesquisar
{
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/BuscarContaPagar',
        data: {
            cod_parcelascontaspagar
        },
        success: function (res) {
            if (res != null) {
                $("#cod_parcelascontaspagar").val(res[0].cod_parcelascontaspagar);
                $("#documento").val(res[0].documento).trigger('change');
                $("#tipocontas").val(res[0].tipocontaspagar.cod_tipocontaspagar).trigger('change');
                $("#fornecedor").val(res[0].fornecedor.cod_fornecedor).trigger('change');
                var data = [];
                $('#tableParcelas').DataTable().clear().destroy();
                var action;
                for (var i = 0; i < res.length; i++) {
                    action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Parcela" onclick="editarParcela(' + res[i].cod_parcelascontaspagar + ',' + res[i].parcela + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Parcela" onclick="excluirParcela(' + res[i].cod_parcelascontaspagar + ',' + res[i].parcela + ')"><i class="la la-trash"></i></button>';
                    if (res[i].data_pagamento == null)
                        data_pagamento = '';
                    else
                        data_pagamento = new Date(res[i].data_pagamento).toLocaleDateString();
                    data.push([
                        res[i].cod_parcelascontaspagar,
                        res[i].documento,
                        res[i].parcela,
                        new Date(res[i].data_vencimento).toLocaleDateString(),
                        res[i].valor,
                        data_pagamento,
                        res[i].valor_pago,
                        action
                        
                    ]);
                }
                $('#tableParcelas').DataTable({
                    searching: false, paging: false, info: false,
                    data: data,
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
                    }
                });
                $("#btnGeraParcelas").attr('disabled', true); 
            }
            else {
                $('#tableParcelas').DataTable().clear().destroy();
                Swal.fire({
                    icon: 'warning',
                    title: 'Nenhum registro encontrado!',
                    showConfirmButton: true
                });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function buscarDocumento2(cod_parcelascontaspagar) //Busca realizada apos edição ou exclusão
{
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/BuscarContaPagar',
        data: {
            cod_parcelascontaspagar
        },
        success: function (res) {
            if (res != null) {
                $("#cod_parcelascontaspagar").val(res[0].cod_parcelascontaspagar);
                $("#tipocontas").val(res[0].tipocontaspagar.cod_tipocontaspagar).trigger('change');
                $("#fornecedor").val(res[0].fornecedor.cod_fornecedor).trigger('change');
                var data = [];
                $('#tableParcelas').DataTable().clear().destroy();
                var action;
                for (var i = 0; i < res.length; i++) {
                    action = '<button class="btn btn-sm btn-clean btn-icon" title="Editar Parcela" onclick="editarParcela(' + res[i].cod_parcelascontaspagar + ',' + res[i].parcela + ')"><i class="la la-edit"></i></button><button class="btn btn-sm btn-clean btn-icon" title="Excluir Parcela" onclick="excluirParcela(' + res[i].cod_parcelascontaspagar + ',' + res[i].parcela + ')"><i class="la la-trash"></i></button>';
                    if (res[i].data_pagamento == null)
                        data_pagamento = '';
                    else
                        data_pagamento = new Date(res[i].data_pagamento).toLocaleDateString();
                    data.push([
                        res[i].cod_parcelascontaspagar,
                        res[i].documento,
                        res[i].parcela,
                        new Date(res[i].data_vencimento).toLocaleDateString(),
                        res[i].valor,
                        data_pagamento,
                        res[i].valor_pago,
                        action

                    ]);
                }
                $('#tableParcelas').DataTable({
                    searching: false, paging: false, info: false,
                    data: data,
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
                    }
                });
            }
            else {
                $('#tableParcelas').DataTable().clear().destroy();
                $("#cod_parcelascontaspagar").val("");
                $("#documento").val("");
                $("#tipocontas").val(0).trigger('change');
                $("#fornecedor").val(0).trigger('change');
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function getNextDocumento() {
    $.ajax({
        type: 'GET',
        url: '/ContasPagar/getSequence_ContasPDoc',
        success: function (res) {
            if (res != 0)
                $("#documento").val(res);
            else
                alert('error');
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


function excluirParcela(cod_parcelascontaspagar, parcela) {
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
                url: '/ContasPagar/ExcluirParcela',
                data: {
                    cod_parcelascontaspagar, parcela
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Excluido com sucesso!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        buscarDocumento2(cod_parcelascontaspagar);
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: res.sqlerrormessage
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


