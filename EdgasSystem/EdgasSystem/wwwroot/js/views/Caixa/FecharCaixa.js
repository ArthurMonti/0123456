//Caixa
inicializador = {
    init: function () {
        LoadInicial();
        
    }
}

function LoadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Funcionario/ObterFuncionarios',
        success: function (res) {
            //VerificaCaixaAberto();
            carregaFuncionarios(res);
            obterCaixasAbertos();
            
        }
    });
}



function carregaFuncionarios(res) {
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

function obterCaixasAbertos() {
    $.ajax({
        type: 'GET',
        url: '/Caixa/ObterCaixasAbertosDESC',
        dataType: 'json',
        success: function (res) {
            if (res != null)
                carregaTabelaAberto(res);
            obterCaixasFechados();
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function carregaTabelaAberto(res) {
    var data = [];
    const meses = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    var date;
    var button;
    for (var i = 0; i < res.length; i++) {
        button = '<button type="button" class="btn btn-primary btn-sm" id="'+i+'" codigo="' + res[i].codigo + '" abertura="' + res[i].abertura + '"onclick="javascript: selecionaCaixa('+i+')">Selecionar</button>';

        date = new Date(res[i].abertura);
        
        date = ((date.getDate() + " " + meses[(date.getMonth())] + " " + date.getFullYear()));
        data.push([
            "Caixa " + res[i].codigo,
            res[i].funcionario_abriu.nome,
            date,
            res[i].saldoAbertura.toFixed(2),
            button

        ]);
    }
    $('#tableAbertos').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}

function obterCaixasFechados() {
    $.ajax({
        type: 'GET',
        url: '/Caixa/ObterCaixasFechadosDESC',
        dataType: 'json',
        success: function (res) {
            if (res != null)
                carregaTabelaFechado(res);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}




function carregaTabelaFechado(res) {
    var data = [];
    const meses = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    var date;
    var button;
    for (var i = 0; i < res.length; i++) {
        
        date = new Date(res[i].abertura);
        date = ((date.getDate() + " " + meses[(date.getMonth())] + " " + date.getFullYear()));
        data.push([
            "Caixa " + res[i].codigo,
            res[i].funcionario_abriu.nome,
            date,
            res[i].saldo.toFixed(2),
            button

        ]);
    }
    $('#tableCaixaUltimos').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }
    });
}

function adicionaZero(numero) {
    if (numero <= 9)
        return "0" + numero;
    else
        return numero;
}

function gravarFechamento() {


    if ($("#formFechamento").valid()) {

        var cod_caixa = $("#numCaixa").val();
        var cod_fun = $("#funcionario").val();
        var saldo = $("#saldo").val();
        var data = $("#date").val();
       
        var dados = {
            cod_caixa, cod_fun, saldo, data
        }
        $.ajax({
            type: 'POST',
            url: '/Caixa/Fechar', // Chama no Controller
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Caixa fechado com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    var caixa = JSON.parse(sessionStorage.getItem('caixa'));
                    if(caixa.codigo == cod_caixa)
                        sessionStorage.removeItem('caixa');
                    location.reload();
                }
                else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Erro ao fechar o caixa!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                }

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
    else
        Swal.fire({
            icon: 'warning',
            title: 'Preencha todos os campos corretamente!',
            showConfirmButton: false,
            timer: 1500
        });
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
            num_caixa,abertura
        },
        success: function (res) {
            if (res != null) {
                $("#formFechamento").removeAttr("hidden");
                $("#btnfechaCaixa").removeAttr("disabled");
                $("#numCaixa").val(res.codigo);
                $("#funcionario").val(res.funcionario_abriu.cod_fun).trigger('change');
                $("#saldo").val(res.saldo);
                $("#date").val(res.abertura);
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });

    
}



/*Validações dos formularios*/

$(document).ready(function () {
    $("#formAbertura").validate({
        rules: {
            numCaixa: "required",
            funcionario: "required",
            saldo: "required"
        },
        messages: {
            cpf: {
                cpf: "CPF inválido."
            },
            cep: {
                exactlength: "CEP inválido."
            },
            fone: {
                exactlength: "Telefone inválido."
            }

        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
});

inicializador.init();