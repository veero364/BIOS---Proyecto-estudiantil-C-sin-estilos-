using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using N_EntidadesCompartidas;
using N_Persistencia;

namespace N_Logica
{
   public class LogicaTramites
    {
        //eliminar (si se elimina el tramite deben eliminarse todas las solicitudes)
        //mostrar datos
        //agregar
        
        #region BuscarTramitePorCodigoT
        public static Tramites BuscarCodigoTramite(int pCodigoT)
        {
            return (PersistenciaTramites.BuscarCodigoTramite(pCodigoT));
        }
        #endregion

        #region ListarTramites por Nombre Entidad
        public static List<Tramites> ListarTramites(string NEntidad)
        {
            List<Tramites> tr = PersistenciaTramites.ListarTramites(NEntidad);
            return tr;
        }
        #endregion

        #region ModificarTramites
        public static void Modificar(Entidades tramites)
        {

            PersistenciaTramites.ModificarTramites((Entidades) tramites);//aunque es un poco redundante lo dejo asi por si me llegase a equivocar al convertir el tipo de dato
        }
        #endregion

        #region AgregarTramite
        public static void AgregarTramite(Entidades Nentidad)
        {

            PersistenciaTramites.AgregarTramites((Entidades)Nentidad);
        }
            #endregion
        
        #region EliminarTramite
        public static void EliminarTramites(Entidades pSolicitudes)
        {
            PersistenciaTramites.EliminarTramites(pSolicitudes);
        }

        #endregion

    }
}
