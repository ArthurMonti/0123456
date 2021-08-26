var inicializador = {
    init: function () {
        loadInicial()
    }
}


function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/ContasPagar/LoadInicialContasPagarPorDataView',
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


//Gerenciar Modal
function editarParcela(cod_parcelascontaspagar, parcela) {
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/BuscarParcelaDocumento',
        data: {
            cod_parcelascontaspagar, parcela
        },
        success: function (res) {
            $("#hidden_editcod_parcelascontaspagar").val(res.cod_parcelascontaspagar);
            $("#editparcela").val(res.parcela);
            $("#editdata_pagamento").val("");
            $("#editvalor").val(res.valor);
            $("#editRowModal").modal();
        }
        ,
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


function buscarContas() {
    var cod_tipocontaspagar = $("#tipocontas").val();
    var cod_fornecedor = $("#fornecedor").val();
    var data_vencimento_inicio = $("#data_vencimento_inicio").val();
    var data_vencimento_termino = $("#data_vencimento_termino").val();
    var data_pagamento_inicio = $("#data_pagamento_inicio").val();
    var data_pagamento_termino = $("#data_pagamento_termino").val();

    if (data_vencimento_inicio == "")
        data_vencimento_inicio = null;
    if (data_vencimento_termino == "")
        data_vencimento_termino = null;
    if (data_pagamento_inicio == "")
        data_pagamento_inicio = null;
    if (data_pagamento_termino == "")
        data_pagamento_termino = null;

    dados = {
        cod_tipocontaspagar, cod_fornecedor, data_vencimento_inicio, data_vencimento_termino, data_pagamento_inicio, data_pagamento_termino
    }
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/BuscarContasFiltroContasPagarPorData',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dados),
        success: function (res) {
            if (res.parcelas != null) {
                var data = [];
                var data_pagamento;
                var data_entrada;
                var action;
                $('#tableParcelas').DataTable().clear().destroy();
                for (var i = 0; i < res.parcelas.length; i++) {
                    action = '<button class="btn btn-sm btn-clean btn-icon" title="Pagar Parcela" onclick="editarParcela(' + res.parcelas[i].cod_parcelascontaspagar + ',' + res.parcelas[i].parcela + ')"><i class="la la-edit"></i></button>';
                    if (res.parcelas[i].valor_pago == 0)
                        res.parcelas[i].valor_pago = null;
                    if (res.parcelas[i].data_pagamento == null)
                        data_pagamento = '';
                    else
                        data_pagamento = new Date(res.parcelas[i].data_pagamento).toLocaleDateString();
                    if (res.parcelas[i].data_entrada == null)
                        data_entrada = '';
                    else
                        data_entrada = new Date(res.parcelas[i].data_entrada).toLocaleDateString();
                    data.push([
                        res.parcelas[i].cod_parcelascontaspagar,
                        res.parcelas[i].documento,
                        res.parcelas[i].tipocontaspagar.descricao,
                        res.parcelas[i].fornecedor.nome,
                        data_entrada,
                        res.parcelas[i].parcela,
                        new Date(res.parcelas[i].data_vencimento).toLocaleDateString(),
                        res.parcelas[i].valor,
                        data_pagamento,
                        res.parcelas[i].valor_pago,
                        action
                    ]);
                }
                $('#tableParcelas').DataTable({
                    data: data,
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
                    }
                });
            }
            else {
                $('#tableParcelas').DataTable().clear().destroy();
                Swal.fire({
                    icon: 'warning',
                    title: 'Nenhum registro encontrado',
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

function quitarParcela() {
    var cod_parcelascontaspagar = $("#hidden_editcod_parcelascontaspagar").val();
    var parcela = $("#editparcela").val();
    var data_pagamento = $("#editdata_pagamento").val();
    var valor = $("#editvalor").val();

    dados = {
        cod_parcelascontaspagar, parcela, data_pagamento, valor
    }
    $.ajax({
        type: 'POST',
        url: '/ContasPagar/QuitarParcela',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dados),
        success: function (res) {
            if (res.ok == true) {
                Swal.fire({
                    icon: 'success',
                    showConfirmButton: true
                });
                buscarContas();
                $("#editRowModal").modal('hide');
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: res.sqlerrormessage,
                    showConfirmButton: true
                });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


inicializador.init();


