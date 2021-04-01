var Ids = [];

$(function () {
    $('#Enviar').click(function (event) {
        event.preventDefault();
        var DetallesResguardo = {
            NumColaborador : $('#NumColaborador').val(),
            Empresa: $('#Empresa').val(),
            FolioResguardo: $('#FolioResguardo').val(),
            FechaResguardo: $('#FechaResguardo ').val(),
            FechaDevolucion: $('#FechaDevolucion').val(),
            TipoMovimiento: $('#TipoMovimiento').val(),
            TipoPrestamo: $('#TipoPrestamo').val(),
            Ubicacion: $('#Ubicacion').val(),
            ObservacionesResguardo: $('#ObservacionesResguardo').val(),
            VoBo: $('#VoBo').val(),
           
            DetallesResguardo: Ids,
            __RequestVerificationToken: $('__RequestVerificationToken').val()
        }

        $.ajax({
            type: 'POST',
            url: '/resguardo/create',
            data: JSON.stringify(DetallesResguardo),

            contentType: 'application/json',
            success: function (data) { //Respuesta afirmativa desde el controlador
                if (data != "") {//Comprobacion de que los datos fueron agregados
                    alert("¡¡Guardado Con Exito!!");
                }
                else {//Error al agregar los datos
                    alert('¡Error Al Guardar Los Datos!');
                }
            },
            error: function (error) { //En caso de error en el controlador
                console.log(error.responseText);
            }
        });
    });
    $('#btninfo').click(function () {
        addRow();
        var detalles = {
            IdArticulo: $('#ArticleID').val().trim()
        }
        Ids.push(detalles);
    });

    
    
    $('#btnSearch').click(function () {
  
        var idA = $('#ArticleID').val().trim();
     
        var id = { id: idA };
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


function addRow() {
    var ID = $('#ArticleID').val().trim();
    var Nam = $('#ArticleName').val();
    var info = $('#ArticleInfo').val();
    var nuevoTr = '<tr><th>' + Nam + '</th><th>' + info + '</th><th>' + ID + ' </th><th><input type="button" class="borrar" value="Eliminar" /></th></tr>';
    jQueryTabla.append(nuevoTr);
    
    $(document).on('click', '.borrar', function (event) {
        event.preventDefault();
        $(this).closest('tr').remove();
    });
    
   /*let DetallesResguardo = [{ Nombre: "ArticleName" }, { Descripción: "ArticleInfo" }]
    let resguardo = {
        Nombre: "6566",
        fechaDevol: "26/03/2021",
        DetallesResguardo: DetallesResguardo
    }*/
  
};

var jQueryTabla = $("<table class='table table-striped'><tr><th>Nombre</th><th>Descripción</th><th>ID</th><th>Acciones</th></tr></table>");
jQueryTabla.attr({
    id: "idtabla"
});

$("#Articletable").append(jQueryTabla);



