$(document).ready(function () {
    $('.btn-editar').click(function () {
        var pk_doctor = $(this).data('id');

        $.ajax({
            url: '/Doctor/DoctorMant',
            type: 'GET',
            data: { pk_doctor: pk_doctor },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#doctorModal').modal('show');
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
        var pk_doctor = $(this).data('id');

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
                // Aquí realizas la acción de borrar con AJAX
                $.ajax({
                    url: '/Doctor/EliminarDoctor',
                    type: 'POST',
                    data: { pk_doctor: pk_doctor },
                    success: function () {
                        Swal.fire(
                            '¡Eliminado!',
                            'El doctor ha sido eliminado.',
                            'success'
                        );
                        location.reload(); // Recarga la página para reflejar los cambios
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo eliminar el doctor.',
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