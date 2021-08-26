//Produto
Inicializador = {
    init: function () {
       VerificaCaixasAbertos();
        
    }
}

function modal(res) {
    carregaTabelaAberto(res)

    $("#ModalCaixas").modal();
}

function carregaTabelaAberto(res) {
    var data = [];
    const meses = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    var date;
    var button;
    for (var i = 0; i < res.length; i++) {
        button = button = '<button type="button" class="btn btn-primary btn-sm" id="' + i + '" codigo="' + res[i].codigo + '" abertura="' + res[i].abertura + '"onclick="javascript: selecionaCaixa(' + i + ')">Selecionar</button>';

        date = new Date(res[i].abertura);
        date = ((date.getDate() + " " + meses[(date.getMonth())] + " " + date.getFullYear()));
        data.push([
            "Caixa " + res[i].codigo,
            res[i].funcionario_abriu.nome,
            res[i].saldo.toFixed(2),
            button


        ]);
    }
    $('#tableCaixaAberto').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}


function VerificaCaixasAbertos() {
    if (sessionStorage.getItem('verificado') == null) { // verificou se possui varios caixas
        var abertura = new Date();
        abertura = (abertura.getFullYear() + "-" + ((adicionaZero(abertura.getMonth() + 1))) + "-" + (adicionaZero(abertura.getDate())));

        $.ajax({
            type: 'GET',
            url: '/Caixa/ObterCaixasAbertosporData',
            data: {
                abertura
            },
            success: function (res) {
                if (res != null)
                    modal(res);
                else
                    verificaCaixa();
                sessionStorage.setItem('verificado', 'true');

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
    else {
        verificaCaixa();
        sessionStorage.setItem('verificado', 'true');
    }
    
}


function verificaCaixa() {

    var caixahtml = '<span class="navi-text"><span class="label label-xl label-inline label-light-danger">Nenhum Caixa Aberto</span></span>';
    $('#caixa').html(caixahtml);
    var caixa = JSON.parse(sessionStorage.getItem('caixa'));
    console.log(caixa);
    if (caixa != null) {

        caixahtml = '<div class="d-flex align-items-center p-4 ">' +
            '<div class="d-flex flex-column flex-grow-1 mr-2">' +
            ' <span class="font-weight-bolder text-break ">Caixa ' + caixa.codigo + '</span>' +
            '<span class="font-weight-bolder text-success font-size-lg">Saldo: R$ ' + caixa.saldo + '</span></div ></div >'
        $('#caixa').html(caixahtml);
    }
    else {
        if (sessionStorage.getItem('verificado') == null) {
            Swal.fire({
                title: 'Nenhum caixa se encontra aberto, gostaria de abrir?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: '  Sim  ',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed)
                    window.location.href = "/Caixa/AbrirCaixa";
            });
        }
            
    }
    
    
}

function selecionaCaixa(id) {

    var btnCaixa = document.getElementById(id);
    var num_caixa = btnCaixa.getAttribute("codigo");
    var abertura = btnCaixa.getAttribute("abertura");

    $.ajax({
        type: 'GET',
        url: '/Caixa/BuscarCaixa',
        dataType: 'json',
        data: {
            num_caixa, abertura
        },
        success: function (res) {
            if (res != null) {
                sessionStorage.setItem('caixa', JSON.stringify(res));
                verificaCaixa();
                $("#ModalCaixas").modal('hide');
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });


}

function adicionaZero(numero) {
    if (numero <= 9)
        return "0" + numero;
    else
        return numero;
}


Inicializador.init();