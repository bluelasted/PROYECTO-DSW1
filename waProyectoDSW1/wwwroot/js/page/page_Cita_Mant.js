$(document).ready(function () {

    $('#citaForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: formData,
            success: function (result) {
                if (result.success) {
                    toastr.success(result.Message, 'Éxito');
                    $('#citaModal').modal('hide');
                    setTimeout(() => location.reload(), 1500);
                } else {
                    toastr.error(result.Message, 'Error');
                }
            },
            error: function () {
                toastr.error('Error inesperado al guardar la cita.', 'Error');
            }
        });
    });
});