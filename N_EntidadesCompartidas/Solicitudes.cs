using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace N_EntidadesCompartidas
{
    public class Solicitudes
    {
        //Atributos

        private int _IDSolicitud;
        private string _NombreSolicitante;
        private string _Estado;
        private DateTime _Agenda;



        //Propiedades
        public int IDSolicitud
        {
            set { _IDSolicitud = value; }
            get { return _IDSolicitud; }
        }

        public string NombreSolicitante
        {
            set
            {
                if (NombreSolicitante.ToString().Length <= 25)
                    _NombreSolicitante = value;
                else
                    throw new Exception("El campo no puede exceder los 25 caracteres");
            }
            get { return _NombreSolicitante; }
        }

        public string Estado
        {
            set
            {
                if (Estado == "Alta" || Estado == "Baja" || Estado == "Anulada")
                    _Estado = value;
                else
                    throw new Exception("El campo solo admite como estado, alta, baja o anulada");

            }

            get { return _Estado; }
        }

        public DateTime Agenda
        {
            set { _Agenda = value; }

            get { return _Agenda; }
        }


        //Constructores
        public Solicitudes(int pIDSolicitud, string pNombreSolicitante, string pEstado, DateTime pAgenda)
        {
            IDSolicitud = pIDSolicitud;
            NombreSolicitante = pNombreSolicitante;
            Estado = pEstado;
            Agenda = pAgenda;
            
        }



        //Operaciones
        public override string ToString()
        {
        string _completo = ToString() + " Datos de la solicitud realizada: " + "Id de solicitud: " + _IDSolicitud + "quin solicita " + _NombreSolicitante + "estado:  " + _Estado
                                      + " agrenda: " + _Agenda; //+ "tramite: "+ this._TramiteAsociacion.TramiteAsoEntidad.TipoTramite



            return (_completo);
        }
    }
}