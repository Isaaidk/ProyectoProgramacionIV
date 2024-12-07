using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoProgramacionIV.Models
{
    public class Reservacion
    {
        public int IdReservacion { get; set; }
        public int IdCarro { get; set; } // Auto reservado
        public int IdCliente { get; set; } // Cliente que realiza la reserva
        public DateTime FechaReserva { get; set; }
    }
}
