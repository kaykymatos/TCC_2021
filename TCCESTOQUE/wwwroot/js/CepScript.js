$('#cep').keyup(function GetCep() {
    $.ajax({
        url: 'https://viacep.com.br/ws/' + $(this).val() + '/json',
        dataType: 'json',
        success: function (resposta) {
            $("#logradouro").val(resposta.logradouro);
            $("#complemento").val(resposta.complemento);
            $("#bairro").val(resposta.bairro);
            $("#localidade").val(resposta.localidade);
            $("#uf").val(resposta.uf);
        }
    })
});