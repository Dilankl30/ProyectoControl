using Entities;
using Npgsql;
using System.Data;

#nullable disable
namespace BusinessLogic
{
    public class UsuarioBus
    {
        public static Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            string sql = "SELECT idUsuario, nombre, tipoUsuario FROM USUARIOS WHERE nombre = @nombre AND contrasena = @contrasena";

            var parametros = new List<NpgsqlParameter>();
            parametros.Add(new NpgsqlParameter("@nombre", nombreUsuario));
            parametros.Add(new NpgsqlParameter("@contrasena", contrasena));

            DataTable dt = DataAccess.DataAccess.getQuery(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                var usuarioLogueado = new Usuario();
                usuarioLogueado.IdUsuario = Convert.ToInt32(dt.Rows[0]["idUsuario"]);
                usuarioLogueado.Nombre = dt.Rows[0]["nombre"].ToString();
                usuarioLogueado.TipoUsuario = dt.Rows[0]["tipoUsuario"].ToString();
                return usuarioLogueado;
            }
            else
            {
                return null;
            }
        }
    }
}
