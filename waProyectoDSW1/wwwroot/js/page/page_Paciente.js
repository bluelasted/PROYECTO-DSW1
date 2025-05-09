$(document).ready(function () {

    $('#btnNuevoPaciente').click(function () {
        $.ajax({
            url: '/Paciente/PacienteMant',
            type: 'GET',
            data: { pk_paciente: 0 },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#pacienteModal').modal('show');
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

    $('.btn-editar').click(function () {
        var pk_paciente = $(this).data('id');

        $.ajax({
            url: '/Paciente/PacienteMant',
            type: 'GET',
            data: { pk_paciente: pk_paciente },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#pacienteModal').modal('show');
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
        var pk_paciente = $(this).data('id');

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
                    url: '/Doctor/Mantenimiento',
                    type: 'POST',
                    data: {
                        pk_paciente: pk_paciente,
                        op: 3
                    },
                    success: function () {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: 'El paciente ha sido eliminado.',
                            icon: 'success'
                        }).then(() => {
                            location.reload(); 
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo eliminar el paciente.',
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