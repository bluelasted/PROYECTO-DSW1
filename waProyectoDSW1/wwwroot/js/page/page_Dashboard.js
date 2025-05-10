$(document).ready(function () {
    var chart1 = echarts.init(document.getElementById('chart1'));
    var chart2 = echarts.init(document.getElementById('chart2'));

    // Obtener los datos de Citas por Estado
    $.getJSON('/Home/GetEstadosCitas', function (response) {
        console.log('Datos de Citas por Estado:', response.data);  // Verifica los datos

        // Verificar si data es un array
        if (Array.isArray(response.data)) {
            var estados = [];
            var citasPorEstado = [];

            response.data.forEach(function (item) {
                estados.push(item.estadoCita);  // Nombre del estado de la cita
                citasPorEstado.push(item.numeroCitas);  // Número de citas por estado
            });

            // Configuración del gráfico circular - Citas por Estado
            var option1 = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: estados  // Usar los estados dinámicamente
                },
                series: [{
                    name: 'Citas',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: estados.map(function (estado, index) {
                        return { value: citasPorEstado[index], name: estado };
                    })
                }]
            };

            chart1.setOption(option1);
        } else {
            console.error("La respuesta no es un array", response.data);
        }
    });

    $.getJSON('/Home/GetServiciosDentales', function (response) {
        console.log('Datos de Citas por Servicio Dental:', response.data);  // Verifica los datos

        // Verificar si data es un array
        if (Array.isArray(response.data)) {
            var servicios = [];
            var citasPorServicio = [];

            response.data.forEach(function (item) {
                servicios.push(item.servicioDental);
                citasPorServicio.push(item.numeroCitas);
            });

            var option2 = {
                tooltip: {},
                legend: {
                    data: ['Citas']
                },
                xAxis: {
                    data: servicios  // Usar los servicios dinámicamente
                },
                yAxis: {},
                series: [{
                    name: 'Citas',
                    type: 'bar',
                    data: citasPorServicio  // Usar los datos de citas por servicio
                }]
            };

            chart2.setOption(option2);
        } else {
            console.error("La respuesta no es un array", response.data);
        }
    });
});
