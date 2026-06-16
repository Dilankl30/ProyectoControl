using Npgsql;
using System.Data;

namespace DataAccess
{
    public class DataAccess
    {
        // Para conectar desde otra máquina: Username=postgres (sin .ezuqrhdtpwbxnatzlzrt)
        // IPv6-only: requiere Cloudflare WARP si tu ISP no tiene IPv6 nativo
        public static string cadenaConexion = "Host=db.ezuqrhdtpwbxnatzlzrt.supabase.co; Database=postgres; Username=postgres; Password=EAx26oUQhsregViV; SSL Mode=Require; Trust Server Certificate=true;";

        public static DataTable getQuery(string SQL, List<NpgsqlParameter> parametros)
        {
            using var conexion = new NpgsqlConnection(cadenaConexion);
            using var comando = new NpgsqlCommand(SQL, conexion);
            for (int i = 0; i < parametros.Count; i++)
                comando.Parameters.Add(parametros[i]);
            using var da = new NpgsqlDataAdapter(comando);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable getQuery(string SQL)
        {
            using var conexion = new NpgsqlConnection(cadenaConexion);
            using var comando = new NpgsqlCommand(SQL, conexion);
            using var da = new NpgsqlDataAdapter(comando);
            var ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public static int execQuery(string SQL)
        {
            using var conexion = new NpgsqlConnection(cadenaConexion);
            using var comando = new NpgsqlCommand(SQL, conexion);
            conexion.Open();
            return comando.ExecuteNonQuery();
        }

        public static int execQuery(string SQL, List<NpgsqlParameter> parametros)
        {
            using var conexion = new NpgsqlConnection(cadenaConexion);
            using var comando = new NpgsqlCommand(SQL, conexion);
            for (int i = 0; i < parametros.Count; i++)
                comando.Parameters.Add(parametros[i]);
            conexion.Open();
            return comando.ExecuteNonQuery();
        }
    }
}
