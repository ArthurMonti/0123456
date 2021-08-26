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
    for (var i = 0; i < res.length; i++) {

        date = new Date(res[i].abertura);
        date = ((date.getDate() + " " + meses[(date.getMonth())] + " " + date.getFullYear()));
        data.push([
            "Caixa " + res[i].codigo,
            res[i].funcionario_abriu.nome,
            date,
            res[i].saldoAbertura.toFixed(2),


        ]);
    }
    $('#tableCaixaAberto').DataTable({
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
    for (var i = 0; i < res.length; i++) {

        date = new Date(res[i].abertura);
        date = ((date.getDate() + " " + meses[(date.getMonth())] + " " + date.getFullYear()));
        data.push([
            "Caixa " + res[i].codigo,
            res[i].funcionario_abriu.nome,
            date,
            res[i].saldo.toFixed(2),


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

function gravarAbertura() {


    if ($("#formAbertura").valid()) {

        var codigo = $("#numCaixa").val();
        var cod_fun = $("#funcionario").val();
        var saldo = $("#saldo").val();

        var dados = {
            codigo, cod_fun, saldo
        }
        $.ajax({
            type: 'POST',
            url: '/Caixa/Abrir', // Chama no Controller
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Caixa aberto com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    sessionStorage.setItem('caixa', JSON.stringify(dados));
                    location.href = "/";
                }
                else {
                    Swal.fire({
                        icon: 'warning',
                        title: res.errorMessage,
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