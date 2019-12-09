//Toda a manipulaçao de campos da tela vai ser feita com Jquery (biblioteca para manipulação da
//tela). Facilitadora
//$ >> Eh jQuery (js)
//MASK >> Desceu da biblioteca de mascaras
//# >> Busca por ID = SELECT * FROM TELA WHERE ID = TELEFONE

//subimos um tooltip(Dica mais bonita). O tooltipo do jquery ui opera sobre o title
$("#Telefone").mask("(99) 99999-9999").tooltip();
// como o jquery é recursivo não precisameo capturar
//Novamente o campo de data de nascimento, utilizamos o mesmo tanto para máscara como para data 

$("#DataNascimento").mask("99/99/9999").datepicker({
    dateFormat: "dd/mm/yy",
    changeYear: true,
    changeMonth: true,
    yearRange: "1940:*",
    dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
    monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov","Dez"]
});

//Caso o usuário clique no limpar as anotações não somem, elas ficam pra sempre visiveis
//temos que limpar as mensagens de validação (manual, braçal)
$("#limpar").click(function() {
    //resetamos as mensagens de validação
    //# == ID
    //. == CLASS="field-validation-error"
    $(".field-validation-error").empty();

    //após limpar as mensagens de validação, posicionamos o cursor dentro do campo nome
    $("#Nome").focus();
});