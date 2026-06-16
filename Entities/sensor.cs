using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sensor
    {
        public int IdSensor { get; set; }
        public string Tipo { get; set; } = "";
        public int IdInvernadero { get; set; }

        public Sensor() { }

        public Sensor(int idSensor, string tipo, int idInvernadero)
        {
            IdSensor = idSensor;
            Tipo = tipo;
            IdInvernadero = idInvernadero;
        }

        public override string ToString() => Tipo;
    }
}
