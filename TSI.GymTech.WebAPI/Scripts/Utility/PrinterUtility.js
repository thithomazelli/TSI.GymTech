// Showing toastr success alert
$("#print").click(function () {
    var conteudo = document.getElementById('modal-print-content').innerHTML, tela_impressao = window.open('about:blank');
    tela_impressao.document.write(conteudo);
    tela_impressao.window.print();
    tela_impressao.window.close();
    $('#modal-print').modal('hide')
    $('#cancel').click(); 
});