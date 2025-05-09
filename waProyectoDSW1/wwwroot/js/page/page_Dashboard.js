$(document).ready(function () {
    var chart1 = echarts.init(document.getElementById('chart1'));
    var chart2 = echarts.init(document.getElementById('chart2'));

    // Gráfico de barras - Pacientes por especialidad
    var option1 = {
        tooltip: {},
        legend: {
            data: ['Pacientes']
        },
        xAxis: {
            data: ["Cardiología", "Odontología", "Pediatría", "Traumatología"]
        },
        yAxis: {},
        series: [{
            name: 'Pacientes',
            type: 'bar',
            data: [50, 80, 40, 60]
        }]
    };

    // Gráfico circular - Distribución de Citas
    var option2 = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: ['Confirmadas', 'Canceladas', 'Pendientes']
        },
        series: [{
            name: 'Citas',
            type: 'pie',
            radius: '55%',
            center: ['50%', '60%'],
            data: [
                { value: 1048, name: 'Confirmadas' },
                { value: 735, name: 'Canceladas' },
                { value: 580, name: 'Pendientes' }
            ]
        }]
    };

    chart1.setOption(option1);
    chart2.setOption(option2);
});