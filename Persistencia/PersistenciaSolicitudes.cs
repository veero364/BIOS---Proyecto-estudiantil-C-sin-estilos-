using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;
using System.Threading.Tasks;

namespace Persistencia
{
   public class PersistenciaSolicitudes
    {
        #region BuscarPorIDSolicitudes
        public static Solicitudes BuscarIDSolicitudes(int IDSolicitud)
        {
            Solicitudes S = null;
            SqlDataReader _Reader;

            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarSolicitudes " + IDSolicitud, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@NEntidades", NombreE.NombreEntidad);


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();

                if (_Reader.Read())
                    S = new Solicitudes(Convert.ToInt32(_Reader["IDSolicitud"]), _Reader["NombreSolicitante"].ToString(), _Reader["Estado"].ToString(), Convert.ToDateTime(_Reader["Agenda"]), _Reader["TipoTramite"].ToString(), _Reader["DescripcionT"].ToString(), Convert.ToInt32(_Reader["CodigoT"]), _Reader["NEntidad"].ToString(), _Reader["Direccion"].ToString(), (List<string>)_Reader["Telefonos"]);

                _Reader.Close();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("algo salio mal con la base de datos" + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return S;
        }
        #endregion

        #region ModificarSolicitudes
        public static void ModificarSolicitudes(Solicitudes solicitudes)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarSolicitudes", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            //verificar esten correctos los nombres de los procedimientos con respecto la bd
            _Comando.Parameters.AddWithValue("@NombreSolicitante", solicitudes.NombreSolicitante);
            _Comando.Parameters.AddWithValue("@Agenda", solicitudes.Agenda);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();

                int _Valor = (int)_Comando.Parameters["@Retorno"].Value;
                //VERIFICAR LUEGO QUE ESTE CULMINADA LA BD PARA CHECAR EL IF QUE SE AGREGUE
                if (_Valor == -1)
                    throw new Exception("No Existe la entidad - No se Modifico");
                else if (_Valor == -2)
                    throw new Exception("Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }
        }
        #endregion
    }
}
