using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deudores.Models
{
    public class Deudor
    {
        [PrimaryKey][AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double ValorDeuda { get; set; }

        public DateTime FechaEntrega { get; set; }

        public double Abono { get; set; }

        public bool Activo { get; set; }

        public double TotalDeudores { get; set; }
    }
}
