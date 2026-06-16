using Npgsql;
using System.Data;

namespace DataAccess
{
    public class DataAccess
    {
        // CAMBIA ESTA CADENA por los datos de tu proyecto en Supabase
        // Host=aws-0-us-west-1.pooler.supabase.com; Database=postgres; Username=project.user; Password=tu-contraseña; SSL Mode=Require;
        public static string cadenaConexion = "Host=db.ezuqrhdtpwbxnatzlzrt.supabase.co; Database=postgres; Username=postgres; Password=EAx26oUQhsregViV; SSL Mode=Require; Trust Server Certificate=true;";

        public static DataTable getQuery(string SQL, List<NpgsqlParameter> parametros)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable getQuery(string SQL)
        {
            try
            {
                using var conexion = new NpgsqlConnection(cadenaConexion);
                using var comando = new NpgsqlCommand(SQL, conexion);
                using var da = new NpgsqlDataAdapter(comando);
                var ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int execQuery(string SQL)
        {
            int filasAfectadas = -1;
            try
            {
                using var conexion = new NpgsqlConnection(cadenaConexion);
                using var comando = new NpgsqlCommand(SQL, conexion);
                conexion.Open();
                filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int execQuery(string SQL, List<NpgsqlParameter> parametros)
        {
            int filasAfectadas = -1;
            try
            {
                using var conexion = new NpgsqlConnection(cadenaConexion);
                using var comando = new NpgsqlCommand(SQL, conexion);
                for (int i = 0; i < parametros.Count; i++)
                    comando.Parameters.Add(parametros[i]);
                conexion.Open();
                filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
