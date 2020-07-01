using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using N_EntidadesCompartidas;
using N_Persistencia;

namespace N_Logica
{
    public class LogicaUsuarios
    {
        #region BuscarEmpleadoPorCI
        public static Usuarios BuscarCIUsuario(int pCIEmpleado)
        {
            return (PersistenciaUsuarios.BuscarCIUsuario(pCIEmpleado));
        }
        #endregion

        #region ModificarEmpleados
        public static void ModificarEmpleados(Usuarios usu)
        {

            PersistenciaUsuarios.ModificarEmpleados((Usuarios)usu);//aunque es un poco redundante lo dejo asi por si me llegase a equivocar al convertir el tipo de dato
        }
        #endregion

        #region AgregarEmpleado
        public static void AgregarUsuario(Usuarios CIEmpleado)
        {

            PersistenciaUsuarios.UsuarioEmpleadoAgregar((Usuarios)CIEmpleado);
        }

        #endregion

    /*    #region EliminarEmpleado
        public static void UsuarioEmpleadoEliminar(Usuarios pSolicitudes)
        {
            PersistenciaUsuarios.UsuarioEmpleadoEliminar(pSolicitudes);
        }

        #endregion*/

        #region Logueo
        public static N_EntidadesCompartidas.Usuarios Logueo(string pCIEmpleado, string pPasword)
        {
            return (N_Persistencia.PersistenciaUsuarios.Logueo(pCIEmpleado, pPasword));
        }
        #endregion

    }
}
