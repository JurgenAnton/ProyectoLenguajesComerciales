using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestorDeHotel.DA
{
    public class DbContexto:DbContext 
    {

       public DbSet<GestorDeHotel.Model.Habitacion> Habitacion { get; set; }

       public DbSet<GestorDeHotel.Model.Tarifa> Tarifa { get; set; }

        public DbSet<GestorDeHotel.Model.TipoHabitacion> TipoHabitacion { get; set; }

        public DbSet<GestorDeHotel.Model.Promociones> Promociones { get; set; }

        public DbSet<GestorDeHotel.Model.Reservaciones> Reservaciones { get; set; }

        public DbContexto(DbContextOptions<DbContexto> opciones) : base(opciones)
        {

        }



    }
}
