using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_EntidadesCompartidas
{
    public class Entidades
    {
        //Atributos

        private string _NombreEntidad;
        private string _Direccion;
        private List<string> _Telefonos;
        
        //atributo 
        private  Tramites _TramiteAsoEntidad;



        //Propiedades
        public string NombreEntidad
        {
            set
            {
                if (value.Length <= 25)
                    _NombreEntidad = value;
                else
                    throw new Exception("El nombre no puede contener mas de 25 caracteres");
            }
            get { return _NombreEntidad; }
        }

        public string Direccion
        {
            set
            {
                if (value.Length <= 25)
                    _Direccion = value;
                else
                    throw new Exception("La direccion no puede contener mas de 25 caracteres");
            }
            get { return _Direccion; }
        }


       public List<string> Telefonos
           
        {
            set
            {
                //if (value.ToString().Length <= 9 || _Telefonos.ToString().Length == 8)
                //    throw new Exception("Los numero telefonicos no pueden ser menor a 8 caracteres ni superior a 9.");
                _Telefonos = value;

            }
            get { return _Telefonos; }
        }

        public Tramites TramiteAsoEntidad
        {
            set
            {
                if (value == null)
                    throw new Exception("Debe saberse que tramite asocia a entidad");
                else
                    _TramiteAsoEntidad = value;
            }
            get { return _TramiteAsoEntidad; }
        }


        //Constructores
        public Entidades(string pNombreEntidad, string pDireccion, List<string> ListaTelefonos, Tramites pTramiteAsoEntidad)
        

        {
            NombreEntidad = pNombreEntidad;
            Direccion = pDireccion;
            Telefonos = ListaTelefonos;
            TramiteAsoEntidad = pTramiteAsoEntidad;
        }

        public Entidades(string pNombreEntidad, string pDireccion, List<string> ListaTelefonos)


        {
            NombreEntidad = pNombreEntidad;
            Direccion = pDireccion;
            Telefonos = ListaTelefonos;
            //TramiteAsoEntidad = pTramiteAsoEntidad;
        }




        //Operaciones
        public override string ToString()
        {
            //string _completo = ToString() + " Datos de la entidad: " + _NombreEntidad + " Direccion : " + _Direccion;
            string _completo = " Datos de la entidad: " + _NombreEntidad + " Direccion : " + _Direccion;

            return (_completo);
        }

    }
}
