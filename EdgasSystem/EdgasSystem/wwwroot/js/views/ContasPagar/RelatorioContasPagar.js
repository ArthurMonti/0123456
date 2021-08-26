var inicializador = {
    init: function () {
        loadInicial()
    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/ContasPagar/LoadInicialViewRelatorioContasPagar',
        dataType: 'json',
        success: function (res) {
            var data_fornecedor = [];
            for (var i = 0; i < res.fornecedor.length; i++) {
               data_fornecedor.push({
                   id: res.fornecedor[i].cod_fornecedor,
                   text: res.fornecedor[i].cod_fornecedor + ' - ' + res.fornecedor[i].nome
               });
            }
            $('#fornecedor').select2({
                data: data_fornecedor,
                placeholder: "Selecione um Fornecedor"
            });
            var data_tipocp = [];
            for (var i = 0; i < res.tipocontas.length; i++) {
                data_tipocp.push({
                    id: res.tipocontas[i].cod_tipocontaspagar,
                    text: res.tipocontas[i].cod_tipocontaspagar + ' - ' + res.tipocontas[i].descricao
                });
            }
            $('#tipo_cp').select2({
                data: data_tipocp,
                placeholder: "Selecione um Tipo de Contas Pagar"
            });

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function preview() {
    var cod_fornecedor = $("#fornecedor").val();
    var cod_tipo = $("#tipo_cp").val();
    var v_data_i = $("#v_data_i").val();
    var v_data_f = $("#v_data_f").val();
    var p_data_i = $("#p_data_i").val();
    var p_data_f = $("#p_data_f").val();

    if (v_data_i != "" && v_data_f == "" || v_data_i == "" && v_data_f != "") {
        Swal.fire({
            icon: 'warning',
            title: 'Periodo incorreto para vencimento!'
        });
    }
    else if (p_data_i != "" && p_data_f == "" || p_data_i == "" && p_data_f != "") {
        Swal.fire({
            icon: 'warning',
            title: 'Periodo incorreto para pagamento!'
        });
    }
    else if (moment(v_data_i).isAfter(v_data_f)) {
        Swal.fire({
            icon: 'warning',
            title: 'Data inicial de vencimento não pode ser maior que data termino!'
        });
    }
    else if (moment(p_data_i).isAfter(p_data_f)) {
        Swal.fire({
            icon: 'warning',
            title: 'Data inicial pagamento não pode ser maior que data termino!'
        });
    }
    else {


        window.open("Preview/?cod_fornecedor=" + cod_fornecedor + "&cod_cp=" + cod_tipo + "&v_data_i=" + v_data_i + "&v_data_f=" + v_data_f + "&p_data_i=" + p_data_i + "&p_data_f=" + p_data_f, '_blank');
    }
}

inicializador.init();