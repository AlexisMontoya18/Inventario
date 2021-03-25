$(function () {
    $('#btninfo').click(function () {
        addRow();
    });
    $('#btnSearch').click(function () {
  
        var idAr = $('#ArticleID').val().trim();
     
        var id = { id: idAr };
        //Envio de datos POST mediante Ajax
        $.ajax({
            type: 'POST',
            url: '/article/BuscarArticulos',
            data: JSON.stringify(id),

            contentType: 'application/json',
            success: function (article) { //Respuesta afirmativa desde el controlador
                if (article != "") {//Comprobacion de que los datos fueron agregados
                    $('#ArticleName').val(article.NombreArtículo);
                    $('#ArticleInfo').val(article.Descripción);
                    
                }
                else {//Error al agregar los datos
                    alert('No Existe');
                }
                $('#saveData').val('Guardar Cambios');
            },
            error: function (error) { //En caso de error en el controlador
                console.log(error.responseText);
                $('#saveData').val('Guardar Cambios');
            }
        });
    });
});

var jQueryTabla = $("<table class='table table-striped'><tr><th>Nombre</th><th>Descripcion</th></tr></table>");
jQueryTabla.attr({
    id: "idtabla"
});

function addRow() {
    var Nam = $('#ArticleName').val();
    var info = $('#ArticleInfo').val();
    var nuevoTr = '<tr><th>' + Nam + '</th><th>' + info + ' <td><input type="button" class="borrar" value="Eliminar" /></td></tr>';
    jQueryTabla.append(nuevoTr);

    $(document).on('click', '.borrar', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });

}
$("#Articletable").append(jQueryTabla);

