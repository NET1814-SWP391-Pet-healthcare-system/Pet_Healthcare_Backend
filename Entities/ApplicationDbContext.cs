using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<PetVaccination> PetVaccinations { get; set; }
        public DbSet<PetHealthTrack> PetHealthTracks { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Kennel> Kennels { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}