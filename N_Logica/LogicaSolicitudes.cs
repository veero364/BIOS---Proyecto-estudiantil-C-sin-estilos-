using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using N_EntidadesCompartidas;
using N_Persistencia;

namespace N_Logica
{
    class LogicaSolicitudes
    {
        #region BuscarPorIDSolicitudes
        public static Solicitudes BuscarIDSolicitud(int pIDSolicitud)
        {
            return (PersistenciaSolicitudes.BuscarIDSolicitudes(pIDSolicitud));
        }
        #endregion

        #region ModificarSolicitudes
        public static void Modificar(Solicitudes solicitudes)
        {

            PersistenciaSolicitudes.ModificarSolicitudes((Solicitudes)solicitudes); //aunque es un poco redundante lo dejo asi por si me llegase a equivocar al convertir el tipo de dato
        }
        #endregion

//****************como hacer el agregar solicitud -  cambio de estado de solicitud y los 
//****************2 listados de solicitudes (por fecha y por estado-alta ejecutada anulada)

    }

}
