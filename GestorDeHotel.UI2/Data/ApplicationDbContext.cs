using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GestorDeHotel.Model;

namespace GestorDeHotel.UI2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GestorDeHotel.Model.TipoHabitacion> TipoHabitacion { get; set; }
        public DbSet<GestorDeHotel.Model.Tarifa> Tarifa { get; set; }
    }
}
