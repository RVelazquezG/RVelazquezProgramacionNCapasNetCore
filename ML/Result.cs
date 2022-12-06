﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; } //Aalidando, manejar el flujo de mi codigo, me dices si salio bien o no mi metodo
        public string ErrorMessage { get; set; } //Almacenar el mensaje de error
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public Exception Ex { get; set; } //Almacenar la excepción completa

    }
}
