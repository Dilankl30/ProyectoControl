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
        public string Nombre { get; set; }
        public string NombreCien { get; set; }
        public string Descripcion { get; set; }

        // Constructor vacío
        public Plant() { }

        // Constructor con parámetros
        public Plant(int idPlanta, string nombre, string nombreCien, string descripcion)
        {
            this.IdPlanta = idPlanta;
            this.Nombre = nombre;
            this.NombreCien = nombreCien;
            this.Descripcion = descripcion;
        }

        // Mostrar el nombre común en listas o ComboBoxes
        public override string ToString()
        {
            return Nombre;
        }
    }
}
