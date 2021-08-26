var index = {
    init: function () {
        document.getElementById("btnLogout").onclick = index.RealizarLogout;
    },
    RealizarLogout: function () {

        $.ajax({
            type: 'POST',
            url: '/Login/Logout',
            success: function (res) {
                    window.location.href = "/Login";
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
}
index.init();