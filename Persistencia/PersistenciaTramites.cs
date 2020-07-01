using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;

namespace Persistencia
{
    public class PersistenciaTramites
    {
        #region BuscarTramitePorCodigo
        public static Tramites BuscarCodigoTramite(int CodigoT)
        {
            Tramites CT = null;
            SqlDataReader _Reader;

            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarTramites " + CodigoT, cnn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();

                if (_Reader.Read())
     //***************************COMO HICE QUE HEREDE TUVE QUE AGREGAR LAS ULTIMAS 3 VARIABLES PERO NO QUIERO QUE SE MUESTREN, IGUAL QUEDA ASI?? LO MISMO SUCEDE CON PERSISTENCIA SOLICITUDES QUE HEREDA DE TRAMITES!!
                    CT = new Tramites(_Reader["TipoTramite"].ToString(), _Reader["DescripcionT"].ToString(), Convert.ToInt32(_Reader["CodigoT"]), _Reader["NEntidad"].ToString(), _Reader["Direccion"].ToString(), (List<string>)_Reader["Telefonos"]);

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
            return CT;
        }
        #endregion

        #region ModificarTramites
        public static void ModificarTramites(Tramites tramites)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarEntidades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@NEntidades", tramites.NombreEntidad); //si la el procedimiento en la BD esta bn entonces tengo algo mal en las entidades compartidas
            _Comando.Parameters.AddWithValue("@CodigoT", tramites.CodigoT);
            //no tendria que ser modificable el tipo de tramite y descripcion del mismo tambien? en BD no esta

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
                    throw new Exception("No Existe  - No se Modifico");
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

        #region AgregarTramite
        public static void AgregarTramites(Tramites Nentidad)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("AgregarTramites", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@CodigoT", Nentidad.CodigoT);
            _Comando.Parameters.AddWithValue("@NEntidades", Nentidad.NombreEntidad);
            _Comando.Parameters.AddWithValue("@TipoT", Nentidad.TipoTramite);
            _Comando.Parameters.AddWithValue("@Descripcion", Nentidad.DescripcionT);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(oRetorno);

            try
            {
                //checar que significa cada return de la bd para poner el control
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                int _Valor = (int)_Comando.Parameters["@Retorno"].Value;
                if (_Valor == 0)
                    throw new Exception("Error inAggregar in BD");
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

        #region EliminarEntidades
        public static void EliminarTramites(Tramites pSolicitudes)
        {
            //Comando a ejecutar
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("EliminarTramites", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlParameter NEntidad = new SqlParameter("@NEntidad", pSolicitudes.NombreEntidad);
            SqlParameter codigoT = new SqlParameter("@codigoT", pSolicitudes.CodigoT);
            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;

            _Comando.Parameters.Add(NEntidad);
            _Comando.Parameters.Add(codigoT);
            _Comando.Parameters.Add(oRetorno);

            try
            {
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                //_Valor TIENE EL VALOR DE RETORNO DE LA BD
                int _Valorr = (int)_Comando.Parameters["@Retorno"].Value;
                if (_Valorr == -1)
                    throw new Exception("Hay solicitudes Asociados - No se Elimina");
                else if (_Valorr == 0)
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
