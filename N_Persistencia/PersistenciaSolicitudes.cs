using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;
using System.Threading.Tasks;

namespace N_Persistencia
{
   public class PersistenciaSolicitudes
    {
        #region BuscarPorIDSolicitudes
        //*******tengo que cargar todas las variables aunque la bd solo tenga una??????*******se cargan todos para mostrar??***************
        public static Solicitudes BuscarIDSolicitudes(int IDSolicitud)
        {
            //cargo variables vacias
            Solicitudes S = null;
            SqlDataReader _Reader;
            //conecto a bd
            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarSolicitud " + IDSolicitud, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@NEntidades", NombreE.NombreEntidad);


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();
                //cargo las variables
                if (_Reader.Read())
                    //**************tengo que cargar todas las variables aunque la bd solo tenga una??????**********************
                    S = new Solicitudes(Convert.ToInt32(_Reader["IDSolicitud"]), _Reader["NombreSolicitante"].ToString(), _Reader["Estado"].ToString(), Convert.ToDateTime(_Reader["Agenda"]));

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

        #region AgregarSolicitud
        public static void AgragarSolicitudes(Solicitudes solicitudes)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("RegistrarunaSolicitud", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@IDSolicitud", solicitudes.IDSolicitud);
            _Comando.Parameters.AddWithValue("@NombreSolicitante", solicitudes.NombreSolicitante);
            _Comando.Parameters.AddWithValue("@Estado", solicitudes.Estado);
            _Comando.Parameters.AddWithValue("@Fecha", solicitudes.Agenda);
            _Comando.Parameters.AddWithValue("@Hora", solicitudes.Agenda);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(_Retorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                int _ValorBD = (int)_Comando.Parameters["@Retorno"].Value;
                if (_ValorBD == -2)
                    throw new Exception("Error inAgregar in BD");
                else if (_ValorBD == -1)
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

        #region ListarSolicitudes
        //HACER!!!
        //LISTAR POR FECHA - POR ESTADO DE SOLCITUD
        #endregion
    }
}
