﻿using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using N_EntidadesCompartidas;

namespace N_Persistencia
{
   public class PersistenciaUsuarios
    {
        #region BuscarEmpleadoPorCI
        public static Usuarios BuscarCIUsuario(int CIEmpleado)
        {   //cargo variable vacia 
            Usuarios U = null;
            SqlDataReader _Reader;
            //conexion con bd
            SqlConnection cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand cmd = new SqlCommand("Exec BuscarUsu " + CIEmpleado, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@NEntidades", NombreE.NombreEntidad);


            try
            {
                cnn.Open();
                _Reader = cmd.ExecuteReader();

                if (_Reader.Read())
                    //CARGO LAS VARIABLES CON LA INFORMACION DE LA BD
                    U = new Usuarios(_Reader["NombreEmpleado"].ToString(), _Reader["Pass"].ToString(), _Reader["CIEmpleado"].ToString());

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
            return U;
        }
        #endregion

        #region ModificarEmpleados
        public static void ModificarEmpleados(Usuarios usu)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("UsuarioEmpleadoModificar", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@NombreEmpleado", usu.NombreEmpleado);
            _Comando.Parameters.AddWithValue("@Pasword", usu.Pasword);
            _Comando.Parameters.AddWithValue("@CI", usu.CIEmpleado);

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
                    throw new Exception("No Existe - No se Modifico");
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

        #region AgregarEmpleados
        public static void UsuarioEmpleadoAgregar(Usuarios CIEmpleado)
        {
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("UsuarioEmpleadoAgregar", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@NombreEmpleado", CIEmpleado.NombreEmpleado);
            _Comando.Parameters.AddWithValue("@Pasword", CIEmpleado.Pasword);
            _Comando.Parameters.AddWithValue("@CI", CIEmpleado.CIEmpleado);

            SqlParameter oRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            oRetorno.Direction = ParameterDirection.ReturnValue;
            _Comando.Parameters.Add(oRetorno);

            try
            {
                //checar que significa cada return de la bd para poner el control
                _Conexion.Open();
                _Comando.ExecuteNonQuery();
                int oAfectados = (int)_Comando.Parameters["@Retorno"].Value;
                if (oAfectados == 0)
                    throw new Exception("Error inAggregar in BD");
                else if (oAfectados == -2)
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
        //*********************PENDIENTE HACER EL ELIMINAR XQ MAURICIO AUN NO TIENE COMPLETO EL PROCEDIMIENTO*************
        #region EliminarUsuarios
        public static void UsuarioEmpleadoEliminar(Entidades pSolicitudes)
        {
            //Comando a ejecutar
            SqlConnection _Conexion = new SqlConnection(Conexion._Cnn);
            SqlCommand _Comando = new SqlCommand("UsuarioEmpleadoEliminar", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            //TramiteAsoEntidad
            SqlParameter NEntidad = new SqlParameter("@NEntidad", pSolicitudes.NombreEntidad);
            SqlParameter idSolicitud = new SqlParameter("@idSolicitud", pSolicitudes.TramiteAsoEntidad.TramiteAsociacionSol.IDSolicitud);
            SqlParameter telefonos = new SqlParameter("@telefonos", pSolicitudes.Telefonos);
            SqlParameter codigoT = new SqlParameter("@codigoT", pSolicitudes.TramiteAsoEntidad.CodigoT);
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
                if (_Valor == -2)
                    throw new Exception("Hay solicitudes Asociadas - No se Elimina");
                else if (_Valor == -1)
                    throw new Exception("El empleado no existe - No se Elimina");
                //else if (_Valor == 0)
                //    throw new Exception("No existe entidad- No se Elimina");

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

        #region Logueo
        public static N_EntidadesCompartidas.Usuarios Logueo(string pCIEmpleado, string pPasword)
        {
            //variables
            SqlConnection _cnn = new SqlConnection(Conexion._Cnn);
            SqlCommand _comando = new SqlCommand("Logueo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            Usuarios unUsu = null;

            //parametros
            _comando.Parameters.AddWithValue("@CI", pCIEmpleado);
            _comando.Parameters.AddWithValue("@Pasword", pPasword);

            try
            {
                _cnn.Open();

                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                   
                  string  nomEmp = (string)_lector["NombreEmpleado"];
                  string  pasUsu = (string)_lector["Pasword"];
                  string CIUsu = (string)_lector["CIEmpleado"];

                    unUsu = new N_EntidadesCompartidas.Usuarios(nomEmp, pasUsu, CIUsu);
                    //unUsu = new N_EntidadesCompartidas.Usuarios(_lector["NombreEmpleado"].ToString(), _lector["Password"].ToString(), Convert.ToInt32(_lector["CIEmpleado"]));
                }

                _lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }
            
            return unUsu;
        }
    }
    #endregion
}

