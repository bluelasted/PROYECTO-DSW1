$(document).ready(function () {

    $('#btnNuevoUsuario').click(function () {
        $.ajax({
            url: '/Usuario/UsuarioMant',
            type: 'GET',
            data: { pk_usuario: 0 },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#usuarioModal').modal('show');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'No se pudo cargar el formulario del usuario.',
                });
            }
        });
    });

    $('.btn-editar').click(function () {
        var pk_usuario = $(this).data('id');

        $.ajax({
            url: '/Usuario/UsuarioMant',
            type: 'GET',
            data: { pk_usuario: pk_usuario },
            success: function (html) {
                $('#modalContainer').html(html);
                $('#usuarioModal').modal('show');
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'No se pudo cargar el formulario del usuario.',
                });
            }
        });
    });

    $('.btn-borrar').click(function () {
        var pk_usuario = $(this).data('id');

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
                    url: '/Usuario/Mantenimiento',
                    type: 'POST',
                    data: {
                        pk_usuario: pk_usuario,
                        op: 3
                    },
                    success: function () {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: 'El usuario ha sido eliminado.',
                            icon: 'success'
                        }).then(() => {
                            location.reload();
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'No se pudo eliminar el usuario.',
                        });
                    }
                });
            }
        });
    });
});