using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_EntidadesCompartidas
{
    public class Tramites
    {
        //Atributos

        private string _TipoTramite;
        private string _DescripcionT;
        private int _CodigoT;
        //atributo de asociacion
        private Solicitudes _TramiteAsociacionSol; // ****ver si se inicializa en null .....si no es igual a null ya tiene un objeto al cual se esta referenciando


        //Propiedades

        public string TipoTramite
        {
            set
            {
                if (TipoTramite.ToString().Length <= 25)
                    _TipoTramite = value;

                else
                    throw new Exception("El tipo de tramite no puede contener un nombre mayor a los 25 caracteres");
            }
            get { return _TipoTramite; }
        }

        public string DescripcionT
        {
            set
            {
                if (DescripcionT.ToString().Length <= 100)
                    _DescripcionT = value;
                else
                    throw new Exception("La descripcion del tramite no puede ser superior a los 100 caracteres");
            }

            get { return _DescripcionT; }
        }

        public int CodigoT
        {
            set
            {
                if (CodigoT.ToString().Length <= 6)

                    _CodigoT = value;
                else
                    throw new Exception("El codigo del tramite puede tener un maximo de 6 caracteres.");

            }
            get { return _CodigoT; }
        }
        public  Solicitudes TramiteAsociacionSol
        {
            set
            {
                if (value == null)
                    throw new Exception("Debe saberse a que tramite asocia la solicitud");
                else
                    _TramiteAsociacionSol = value;
            }
            get { return _TramiteAsociacionSol; }
        }

        //Constructores
        public Tramites(string pTipoTramite, string pDescripcionT, int pCodigoT, Solicitudes pTramiteAsociacionSol)
            
        {
            TipoTramite = pTipoTramite;
            DescripcionT = pDescripcionT;
            CodigoT = pCodigoT;
            TramiteAsociacionSol = pTramiteAsociacionSol;

        }
        public Tramites(string pTipoTramite, string pDescripcionT, int pCodigoT)

        {
            TipoTramite = pTipoTramite;
            DescripcionT = pDescripcionT;
            CodigoT = pCodigoT;
           // TramiteAsociacionSol = pTramiteAsociacionSol;

        }




        //Operaciones
        public override string ToString()
        {
            string _completo = ToString() + " Datos del tramite: " + "Entidad a la que corresponde el tramite: " + _TipoTramite;



            return (_completo);
        }
    }
}
