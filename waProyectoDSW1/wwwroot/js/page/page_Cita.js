$(document).ready(function () {

    $('#btnNuevaCita').click(function () {
        $.ajax({
            url: '/Cita/CitaMant',
            type: 'GET',
            data: { pk_cita: 0 },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#citaModal').modal('show');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'No se pudo cargar el formulario de la cita.',
                });
            }
        });
    });

    $('.btn-editar').click(function () {
        var pk_cita = $(this).data('id');

        $.ajax({
            url: '/Cita/CitaMant',
            type: 'GET',
            data: { pk_cita: pk_cita },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#citaModal').modal('show');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'No se pudo cargar el formulario del doctor.',
                });
            }
        });
    });

    $('.btn-borrar').click(function () {
        var pk_cita = $(this).data('id');

        Swal.fire({
            title: '¿Estás seguro?',
            text: "¡Esta acción no se puede deshacer!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminarlo!',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Cita/Mantenimiento',
                    type: 'POST',
                    data: {
                        pk_cita: pk_cita,
                        op: 3
                    },
                    success: function () {
                        Swal.fire({
                            title: '¡Eliminada!',
                            text: 'La cita ha sido eliminada.',
                            icon: 'success'
                        }).then(() => {
                            location.reload(); // Se recarga después de cerrar el Swal
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo eliminar la cita.',
                        });
                    }
                });
            }
        });
    });


    // Botón Limpiar
    $('#limpiarFiltro').click(function () {
        $('#fechaDesde').val('');
        $('#fechaHasta').val('');
        $('#especialidad').val('');
    });
});