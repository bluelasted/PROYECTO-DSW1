document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        locale: 'es',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: '/Cita/CitasCalendario',
        eventColor: '#378006',

        // Asignamos colores según el estado
        eventClassNames: function (arg) {
            var description = arg.event.extendedProps.description || '';
            if (description.includes('Estado: Confirmada')) {
                return ['evento-confirmado'];
            } else if (description.includes('Estado: Cancelada')) {
                return ['evento-cancelado'];
            } else if (description.includes('Estado: Programada')) {
                return ['evento-programado'];
            }
            return [];
        },

        // Usamos eventDidMount en lugar de eventRender
        eventDidMount: function (info) {
            // Configuramos el título como tooltip simple
            info.el.setAttribute('title', info.event.extendedProps.description);

            // Formateamos mejor el título
            var titleParts = info.event.title.split('\n');
            if (titleParts.length >= 2) {
                var nombrePaciente = titleParts[0];
                var servicio = titleParts[1];

                var titleEl = info.el.querySelector('.fc-event-title');
                if (titleEl) {
                    titleEl.innerHTML = nombrePaciente + '<br><small>' + servicio + '</small>';
                }
            }
        }
    });
    calendar.render();
});