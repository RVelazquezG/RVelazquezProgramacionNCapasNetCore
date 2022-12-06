using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        [DisplayName("Numero interior")]
        public string NumeroInterior { get; set; }
        [DisplayName("Numero exterior")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public ML.Usuario Usuario { get; set; }
    }
}

