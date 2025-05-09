$(document).ready(function () {

    $('#btnNuevaEspecialidad').click(function () {
        $.ajax({
            url: '/Especialidad/EspecialidadMant',
            type: 'GET',
            data: { pk_especialidad: 0 },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#especialidadModal').modal('show');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'No se pudo cargar el formulario de especialidad.',
                });
            }
        });
    });

    $('.btn-editar').click(function () {
        var pk_especialidad = $(this).data('id');

        $.ajax({
            url: '/Especialidad/EspecialidadMant',
            type: 'GET',
            data: { pk_especialidad: pk_especialidad },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#especialidadModal').modal('show');
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
        var pk_especialidad = $(this).data('id');

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
                    url: '/Especialidad/Mantenimiento',
                    type: 'POST',
                    data: {
                        pk_especialidad: pk_especialidad,
                        op: 3
                    },
                    success: function () {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: 'El doctor ha sido eliminado.',
                            icon: 'success'
                        }).then(() => {
                            location.reload(); // Se recarga después de cerrar el Swal
                        });
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