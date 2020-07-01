using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace N_EntidadesCompartidas
{
    public class Usuarios
    {
        //Atributos

        private string _NombreEmpleado;
        private string _Pasword;
        private string _CIEmpleado;



        //Propiedades
        public string NombreEmpleado
        {
            set
            {
                if (value.Length <= 25)
                    _NombreEmpleado = value;
                else
                    throw new Exception("El campo no puede exceder los 25 caracteres");
            }
            get { return _NombreEmpleado; }
        }

        public string Pasword
        {
            set
            {
                if (value.Length <= 10)

                    _Pasword = value;
                else
                    throw new Exception("El pasword ingresado no puede ser superior a 10 digitos.");
            }
            get { return _Pasword; }
        }

        public string CIEmpleado
        {
            set
            {

                if (value.Length >= 7)

                    _CIEmpleado = value;
                else
                    throw new Exception("Los digitos ingresados no pueden ser mayor a 7 caracteres.");

            }
            get { return _CIEmpleado; }
        }



        //Constructores
        public Usuarios(string pNombre, string pPasword, string pCIEmpleado)

        {//propiedad=parametro
            NombreEmpleado = pNombre;
            Pasword = pPasword;
            CIEmpleado = pCIEmpleado;

        }



        //Operaciones
        public override string ToString()
        {
            string _completo = ToString() + " Datos del usuario: " + _NombreEmpleado;



            return (_completo);
        }
    }
}
