using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Plant
    {
        public int IdPlanta { get; set; }
        public string Nombre { get; set; } = "";
        public string NombreCien { get; set; } = "";
        public string Descripcion { get; set; } = "";

        public Plant() { }

        public Plant(int idPlanta, string nombre, string nombreCien, string descripcion)
        {
            IdPlanta = idPlanta;
            Nombre = nombre;
            NombreCien = nombreCien;
            Descripcion = descripcion;
        }

        public override string ToString() => Nombre;
    }
}
