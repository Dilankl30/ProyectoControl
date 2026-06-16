using BusinessLogic;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgroControl
{
    public partial class readingLog : Form
    {
        public readingLog()
        {
            InitializeComponent();
            CargarContenidoSensores();
            CargarHistorialLecturas();
        }

        private void CargarHistorialLecturas()
        {
            try
            {
                // Se añade el filtro CASE WHEN para el tipo 'luz'
                string sql = @"
            SELECT 
                L.fechaHora AS ""Fecha y hora"",
                MAX(CASE WHEN S.tipo = 'humSuelo' THEN L.valor END) AS ""Humedad del suelo"",
                MAX(CASE WHEN S.tipo = 'tempAire' THEN L.valor END) AS ""Temperatura"",
                MAX(CASE WHEN S.tipo = 'humAire' THEN L.valor END) AS ""Humedad del aire"",
                MAX(CASE WHEN S.tipo = 'luz' THEN L.valor END) AS ""Intensidad de luz""
            FROM LECTURA L
            INNER JOIN SENSOR S ON L.idSensor = S.idSensor
            GROUP BY L.fechaHora
            ORDER BY L.fechaHora DESC";

                // Llamado a la base de datos usando tu método funcional
                DataTable dtLecturas = DataAccess.DataAccess.getQuery(sql);

                // Inyección de los datos al DataGridView
                dgvGrid.DataSource = dtLecturas;

                // Formatos de datos para cada columna
                if (dgvGrid.Columns.Contains("Fecha y hora"))
                    dgvGrid.Columns["Fecha y hora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                if (dgvGrid.Columns.Contains("Humedad del suelo"))
                    dgvGrid.Columns["Humedad del suelo"].DefaultCellStyle.Format = "0' %'";

                if (dgvGrid.Columns.Contains("Temperatura"))
                    dgvGrid.Columns["Temperatura"].DefaultCellStyle.Format = "0.0' °C'";

                if (dgvGrid.Columns.Contains("Humedad del aire"))
                    dgvGrid.Columns["Humedad del aire"].DefaultCellStyle.Format = "0' %'";

                if (dgvGrid.Columns.Contains("Intensidad de luz"))
                    dgvGrid.Columns["Intensidad de luz"].DefaultCellStyle.Format = "0.0' lx'"; // lx representa Luxes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos en el sistema: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarContenidoSensores()
        {
            List<Sensor> lista = SensorBus.getSensores();
            dgvDatosSensores.DataSource = lista;
        }

        // 3. EL BOTÓN EN CASO DE QUE EL USUARIO QUIERA REFRESCAR MANUALMENTE
        private void btnCargarSensores_Click(object sender, EventArgs e)
        {
            CargarContenidoSensores();
        }

        private void dgvDatosSensores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /*
        // 4. AL ELIMINAR O INSERTAR UN NUEVO SENSOR
        private void btnIngresarSensor_Click(object sender, EventArgs e)
        {
            Sensor sensor = new Sensor();
            sensor.Tipo = txtTipo.Text.Trim();
            sensor.IdInvernadero = Convert.ToInt32(txtIdInvernadero.Text);

            int rowAff = FSensor.insertar(sensor);

            if (rowAff > 0)
            {
                MessageBox.Show("¡Sensor ingresado correctamente!");

                // ¡Aquí también llamamos al método! 
                // Así la tabla se actualiza sola y muestra el nuevo sensor de inmediato.
                CargarContenidoSensores();
            }
        }
        */
    }
}
