$('#data_nascimento').mask('00/00/0000');
$('#cpf').mask('000.000.000-00', { reverse: true });
$('#cnpj').mask('00.000.000/0000-00', { reverse: true });
$('#cep').mask('00000-000');
$('#fone').mask('(00) 0000-00009');
$('#fone').blur(function (event) {
    if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
        $('#fone').mask('(00) 00000-0009');
    } else {
        $('#fone').mask('(00) 0000-00009');
    }
});