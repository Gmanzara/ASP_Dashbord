using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Models
{
    public class InfrastructureStart
    {
        public int IdInfra {get;set;}
        public string Decrription { get; set; }
        public int idEtap { get; set; }

        public string Nom { get; set; }
        public int Nombre { get; set; }
    }
}