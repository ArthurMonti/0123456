var index = {
    init: function () {
        document.getElementById("btnLogin").onclick = index.RealizarLogin;
    },
    RealizarLogin: function () {

        var logon = document.getElementById("login").value;
        var senha = document.getElementById("password").value;

        $.ajax({
            type: 'POST',
            url: '/Login/Logar',
            data: {
                logon, senha
            },
            success: function (res) {
                if (res == true)
                    window.location.href = "/Home";
                else {
                    $('#erro_usuario').removeAttr('hidden');
                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
}
index.init();