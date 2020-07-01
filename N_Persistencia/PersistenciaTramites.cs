using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;

namespace N_Persistencia
{
    public class PersistenciaTramites
    {
        #region BuscarTramitePorCodigo
        public static Tramites BuscarCodigoTramite(int CodigoT)
        {//CARGO VARIABLE VACIA
            Tramites CT = null;
            SqlDataReader _Reader;
            //CONEXION CON BD
            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarTramites " + CodigoT, cnn);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();

                if (_Reader.Read())
                    //CARGO LAS VARIABLES CON LA INFORMACION DE LA BD
                    //***************************COMO HICE QUE HEREDE TUVE QUE AGREGAR LAS ULTIMAS 3 VARIABLES PERO NO QUIERO QUE SE MUESTREN, IGUAL QUEDA ASI?? LO MISMO SUCEDE CON PERSISTENCIA SOLICITUDES QUE HEREDA DE TRAMITES!!
                    CT = new Tramites(_Reader["TipoTramite"].ToString(), _Reader["DescripcionT"].ToString(), Convert.ToInt32(_Reader["CodigoT"]), null);

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
            //VARIABLE CON VALORES CARGADOS
            return CT;
        }
        #endregion

        #region ModificarTramites
        public static void ModificarTramites(Entidades tramites)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("ModificarEntidades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@NEntidades", tramites.NombreEntidad); //si la el procedimiento en la BD esta bn entonces tengo algo mal en las entidades compartidas
            _Comando.Parameters.AddWithValue("@CodigoT", tramites.TramiteAsoEntidad.CodigoT);
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
        public static void AgregarTramites(Entidades Nentidad)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("AgregarTramites", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@CodigoT", Nentidad.TramiteAsoEntidad.CodigoT);
            _Comando.Parameters.AddWithValue("@NEntidades", Nentidad.NombreEntidad);
            _Comando.Parameters.AddWithValue("@TipoT", Nentidad.TramiteAsoEntidad.TipoTramite);
            _Comando.Parameters.AddWithValue("@Descripcion", Nentidad.TramiteAsoEntidad.DescripcionT);

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

        #region EliminarTramites
        public static void EliminarTramites(Entidades pSolicitudes)
        {
            //Comando a ejecutar
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("EliminarTramites", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlParameter NEntidad = new SqlParameter("@NEntidad", pSolicitudes.NombreEntidad);
            SqlParameter CodigoT = new SqlParameter("@CodigoT", pSolicitudes.TramiteAsoEntidad.CodigoT);
            // SqlParameter IDSolicitud = new SqlParameter("@idSolicitud", pSolicitudes.IDSolicitud);           

            _Comando.Parameters.Add(NEntidad);
            _Comando.Parameters.Add(CodigoT);
            //_Comando.Parameters.Add(IDSolicitud);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;

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


        #region ListarTramites
        //HACER!!!
        //LISTAR POR nombre de entidad
        public static List<Tramites> ListarTramites(string NEntidad)
        {
            int codigoT;
            string TipoTramite = null;
            string DescripcionT = null;
            //string NomEntidad = null;

            List<N_EntidadesCompartidas.Tramites> _ListarTramites = new List<N_EntidadesCompartidas.Tramites>();
            N_EntidadesCompartidas.Tramites CT = null;
            SqlDataReader _Reader;
            //CONEXION CON BD
            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("ListarTramites ", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NEntidad", NEntidad);


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();
                while (_Reader.Read())//El read siempre me devuelve true or false, pude leer algo o no
                {
                    codigoT = (int)_Reader["CodigoT"];
                    TipoTramite = (string)_Reader["TipoTramite"];
                    DescripcionT = (string)_Reader["DescripcionT"];
                    //NomEntidad = (string)_Reader["NEntidad"];

                    CT = new Tramites(TipoTramite, DescripcionT, codigoT);//****xqe no trae la info de la bd de telefonos???????????
                    _ListarTramites.Add(CT);
                    //_ListarEntidades.Add(new Entidades() { pNombreEntidad  = NEntidad });
                }



               // if (_Reader.Read())
                    //CARGO LAS VARIABLES CON LA INFORMACION DE LA BD
                    //***************************COMO HICE QUE HEREDE TUVE QUE AGREGAR LAS ULTIMAS 3 VARIABLES PERO NO QUIERO QUE SE MUESTREN, IGUAL QUEDA ASI?? LO MISMO SUCEDE CON PERSISTENCIA SOLICITUDES QUE HEREDA DE TRAMITES!!
                 ////   CT = new Tramites(_Reader["TipoTramite"].ToString(), _Reader["DescripcionT"].ToString(), Convert.ToInt32(_Reader["CodigoT"]), null);

              //  _Reader.Close();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("algo salio mal con la base de datos" + ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            //VARIABLE CON VALORES CARGADOS
            return _ListarTramites;
            #endregion
        }
    }
}
