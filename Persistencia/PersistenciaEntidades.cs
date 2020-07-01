using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaEntidades
    {
        #region BuscarEntidadPorNombre
        public static Entidades BuscarNombreEntidad(string NombreE)
        {
            Entidades E = null;
            SqlDataReader _Reader;

            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarEntidades " + NombreE, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@NEntidades", NombreE.NombreEntidad);


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();

                if (_Reader.Read())
                    E = new Entidades(_Reader["NEntidad"].ToString(), _Reader["Direccion"].ToString(), (List<string>)_Reader["Telefonos"]);

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
            return E;
        }
        #endregion

        #region ModificarEntidades
        public static void ModificarEntidades(Entidades Nentidad)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarEntidades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@NEntidades", Nentidad.NombreEntidad);
            _Comando.Parameters.AddWithValue("@direccion", Nentidad.Direccion);
            // _Comando.Parameters.AddWithValue("@telefono", Nentidad.Telefonos);

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

        #region AgregarEntidades
        public static void AgregarEntidades(Entidades Nentidad)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("AgregarEntidades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@NEntidades", Nentidad.NombreEntidad);
            _Comando.Parameters.AddWithValue("@Direccion", Nentidad.Direccion);
            //verificar haya inner join para la tabla telefonos en la BD y que la variable eeste bn escrita
            //oComando.Parameters.AddWithValue("@Telefono", Nentidad.Telefonos);
           

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(oRetorno);

            try
            {
                //checar que significa cada return de la bd para poner el control
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                int _ValorBD = (int)_Comando.Parameters["@Retorno"].Value;
                if (_ValorBD == 0)
                    throw new Exception("Error inAggregar in BD");
                else if (_ValorBD == -2)
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

        #region EliminarEntidades
        public static void EliminarEntidades(Solicitudes pSolicitudes)
        {
            //Comando a ejecutar
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("EliminarEntidades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlParameter NEntidad = new SqlParameter("@NEntidad", pSolicitudes.NombreEntidad);
            SqlParameter idSolicitud = new SqlParameter("@idSolicitud", pSolicitudes.IDSolicitud);
            SqlParameter telefonos = new SqlParameter("@telefonos", pSolicitudes.Telefonos);
            SqlParameter codigoT = new SqlParameter("@codigoT", pSolicitudes.CodigoT);
            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;

            _Comando.Parameters.Add(NEntidad);
            _Comando.Parameters.Add(idSolicitud);
            _Comando.Parameters.Add(telefonos);
            _Comando.Parameters.Add(codigoT);
            _Comando.Parameters.Add(oRetorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                //_Valor TIENE EL VALOR DE RETORNO DE LA BD
                int _Valor = (int)_Comando.Parameters["@Retorno"].Value;
                if (_Valor == -1)
                    throw new Exception("Hay solicitudes Asociados - No se Elimina");
                else if (_Valor == 0)
                    throw new Exception("No existe entidad- No se Elimina");
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
