using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using N_EntidadesCompartidas;
using N_Persistencia;


namespace N_Logica
{
  public  class LogicaEntidades
    {
        //-eliminar (si tiene tramites generados no se podra eliminar la entidad, si se elimina deben eliminarse todos los tramites que tenga)
        //-agregar 

        #region BuscarEntidadPorNombre
        public static Entidades BuscarNombreEntidad(string pNombreEntidad)
        {
            return (PersistenciaEntidades.BuscarNombreEntidad(pNombreEntidad));
        }
        #endregion

        #region ModificarEntidades
        public static void Modificar(Entidades Nentidad )
        {

            PersistenciaEntidades.ModificarEntidades((Entidades)Nentidad);//aunque es un poco redundante lo dejo asi por si me llegase a equivocar al convertir el tipo de dato
        }
        #endregion

        #region AgregarEntidad
        public static void Agregar(Entidades Nentidad)
        {

            PersistenciaEntidades.AgregarEntidades((Entidades)Nentidad);
        }

        #endregion

        #region EliminarEntidades
        public static void Eliminar(Entidades pSolicitudes)
        {
            PersistenciaEntidades.EliminarEntidades(pSolicitudes); 
        }

        #endregion

        #region ListarEntidades
        public static List<Entidades> ListarEntidades()
        {
            List<Entidades> p = PersistenciaEntidades.ListarEntidaddes();          
            return p;
        }
        #endregion

    }
}
