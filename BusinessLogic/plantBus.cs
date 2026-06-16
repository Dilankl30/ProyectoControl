using Entities;
using Npgsql;
using System.Data;

#nullable disable
namespace BusinessLogic
{
    public class PlantBus
    {
        public int IdPlanta { get; set; }
        public string Nombre { get; set; }
        public string NombreCien { get; set; }
        public string Descripcion { get; set; }

        public static int insertar(Plant planta)
        {
            string sql = "INSERT INTO PLANTA (nombre, nombreCien, descripcion) VALUES (@nombre, @nombreCien, @descripcion)";

            var parametros = new List<NpgsqlParameter>();
            parametros.Add(new NpgsqlParameter("@nombre", planta.Nombre));
            parametros.Add(new NpgsqlParameter("@nombreCien", planta.NombreCien));
            parametros.Add(new NpgsqlParameter("@descripcion", string.IsNullOrEmpty(planta.Descripcion) ? (object)DBNull.Value : planta.Descripcion));

            return DataAccess.DataAccess.execQuery(sql, parametros);
        }

        public static PlantBus getPlanta(int idPlanta)
        {
            PlantBus planta = null;
            string sql = "SELECT * FROM PLANTA WHERE idPlanta = @id";

            var parametros = new List<NpgsqlParameter>();
            parametros.Add(new NpgsqlParameter("@id", idPlanta));

            DataTable tabla = DataAccess.DataAccess.getQuery(sql, parametros);

            if (tabla.Rows.Count > 0)
            {
                DataRow row = tabla.Rows[0];
                planta = new PlantBus();
                planta.IdPlanta = Convert.ToInt32(row["idPlanta"]);
                planta.Nombre = row["nombre"].ToString();
                planta.NombreCien = row["nombreCien"].ToString();
                planta.Descripcion = row["descripcion"].ToString();
            }
            return planta;
        }

        public static List<PlantBus> getPlantas()
        {
            var lista = new List<PlantBus>();
            string sql = "SELECT * FROM PLANTA";

            DataTable tabla = DataAccess.DataAccess.getQuery(sql);

            foreach (DataRow row in tabla.Rows)
            {
                var planta = new PlantBus();
                planta.IdPlanta = Convert.ToInt32(row["idPlanta"]);
                planta.Nombre = row["nombre"].ToString();
                planta.NombreCien = row["nombreCien"].ToString();
                planta.Descripcion = row["descripcion"].ToString();
                lista.Add(planta);
            }
            return lista;
        }
    }
}
