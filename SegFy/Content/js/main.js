
function blockUI() {
    if ($(".blockUI").length == 0)
        $("body").append('<div class="blockUI"><div><div class="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></div></div>');
    
    $(".blockUI").fadeIn('fast');
}
function unblockUI() {
    $(".blockUI").fadeOut('slow');
}

function ValidarLogin() {
    if ($("#frmLogin").valid()) {
        blockUI();
        callAjaxPostAsync("/Login/ValidarLogin", $("#frmLogin").readForm(), function (ret) {
            if (ret && !ret.Error) {
                window.location.href = ret.URL;
            }
            else {
                unblockUI();
                $.notify({ message: ret.Message }, { type: 'danger' });
            }
        });
    }
    else
        $.notify({ message: "Todos os campos são obrigatórios!" }, { type: 'danger' });

    return false;
}

function ConfirmarLogoff() {
    ninjaModal({
        title: "Atenção!",
        css: 'fade',
        size: "modal-sm",
        html: "Deseja realmente <strong>sair</strong> da aplicação?",
        buttons: [
            {
                text: 'Não',
                css: 'btn btn-secondary',
            },
            {
                text: 'Sim',
                css: 'btn btn-primary',
                callback: function (modal) {
                    blockUI({ speed: 'fast' });
                    setTimeout(function(){
                        window.location.href = "/Login/Logout";
                    }, 50);
                }
            }
        ]
    });
    return false;
}