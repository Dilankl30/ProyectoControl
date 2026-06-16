using Entities;
using Npgsql;
using System.Data;

namespace BusinessLogic
{
    public class SensorBus
    {
        public static int insertar(Sensor sensor)
        {
            string sql = "INSERT INTO SENSOR (tipo, idInvernadero) VALUES (@tipo, @idInvernadero)";
            var parametros = new List<NpgsqlParameter>();
            parametros.Add(new NpgsqlParameter("@tipo", sensor.Tipo));
            parametros.Add(new NpgsqlParameter("@idInvernadero", sensor.IdInvernadero));

            return DataAccess.DataAccess.execQuery(sql, parametros);
        }

        public static Sensor getSensor(int idSensor)
        {
            Sensor sensor = null;
            string sql = "SELECT * FROM SENSOR WHERE idSensor=@id";
            var lstPar = new List<NpgsqlParameter>();
            lstPar.Add(new NpgsqlParameter("@id", idSensor));

            DataTable tabla = DataAccess.DataAccess.getQuery(sql, lstPar);

            foreach (DataRow row in tabla.Rows)
            {
                sensor = new Sensor();
                sensor.IdSensor = Convert.ToInt32(row[0]);
                sensor.Tipo = row[1].ToString();
                sensor.IdInvernadero = Convert.ToInt32(row[2]);
            }
            return sensor;
        }

        public static List<Sensor> getSensores()
        {
            var lista = new List<Sensor>();
            string sql = "SELECT * FROM SENSOR";
            DataTable tabla = DataAccess.DataAccess.getQuery(sql);

            foreach (DataRow row in tabla.Rows)
            {
                var sensor = new Sensor();
                sensor.IdSensor = Convert.ToInt32(row[0]);
                sensor.Tipo = row[1].ToString();
                sensor.IdInvernadero = Convert.ToInt32(row[2]);
                lista.Add(sensor);
            }
            return lista;
        }
    }
}
