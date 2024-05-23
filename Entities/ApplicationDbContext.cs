using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<PetVaccination> PetVaccinations { get; set; }
        public DbSet<PetHealthTrack> PetHealthTracks { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Kennel> Kennels { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Disable cascading deletes for specific relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vet)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hospitalization>()
                .HasOne(h => h.Vet)
                .WithMany(v => v.Hospitalizations)
                .HasForeignKey(h => h.VetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppointmentDetail>()
                .HasOne(ad => ad.Record)
                .WithMany(r => r.AppointmentDetails)
                .HasForeignKey(ad => ad.RecordId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define composite keys
            modelBuilder.Entity<PetVaccination>()
                .HasKey(pv => new { pv.PetId, pv.VaccineId });

            modelBuilder.Entity<PetVaccination>()
                .HasOne(pv => pv.Pet)
                .WithMany(p => p.PetVaccinations)
                .HasForeignKey(pv => pv.PetId);

            modelBuilder.Entity<PetVaccination>()
                .HasOne(pv => pv.Vaccine)
                .WithMany(v => v.PetVaccinations)
                .HasForeignKey(pv => pv.VaccineId);

            #region Seed Data
            #region kennel
            //Seed data to kennel
            string kennelsJson = System.IO.File.ReadAllText("Kennel.json");
            //deserialize from json string to List of countries
            List<Kennel> kennels = System.Text.Json.JsonSerializer.Deserialize<List<Kennel>>(kennelsJson);
            //now foreach and use HasData
            foreach (Kennel country in kennels)
            {
                modelBuilder.Entity<Kennel>().HasData(country);
            }
            #endregion kennel

            #region vaccine
            //Seed data to vaccine
            string vaccinesJson = System.IO.File.ReadAllText("Vaccine.json");
            //deserialize from json string to List of vaccines
            List<Vaccine> vaccines = System.Text.Json.JsonSerializer.Deserialize<List<Vaccine>>(vaccinesJson);
            //now foreach and use HasData
            foreach (Vaccine vaccine in vaccines)
            {
                modelBuilder.Entity<Vaccine>().HasData(vaccine);
            }
            #endregion vaccine  

            #region service
            //Seed data to service
            string servicesJson = System.IO.File.ReadAllText("Service.json");
            //deserialize from json string to List of services
            List<Service> services = System.Text.Json.JsonSerializer.Deserialize<List<Service>>(servicesJson);
            //now foreach and use HasData
            foreach (Service service in services)
            {
                modelBuilder.Entity<Service>().HasData(service);
            }
            #endregion service

            #region role
            //Seed data to role
            string rolesJson = System.IO.File.ReadAllText("Role.json");
            //deserialize from json string to List of roles
            List<Role> roles = System.Text.Json.JsonSerializer.Deserialize<List<Role>>(rolesJson);
            //now foreach and use HasData
            foreach (Role role in roles)
            {
                modelBuilder.Entity<Role>().HasData(role);
            }
            #endregion

            #region user
            //Seed data to user
            string usersJson = System.IO.File.ReadAllText("User.json");
            //deserialize from json string to List of users
            List<User> users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson);
            //now foreach and use HasData
            foreach (User user in users)
            {
                modelBuilder.Entity<User>().HasData(user);
            }
            #endregion user

            #region pet
            //Seed data to pet
            string petsJson = System.IO.File.ReadAllText("Pet.json");
            //deserialize from json string to List of pets
            List<Pet> pets = System.Text.Json.JsonSerializer.Deserialize<List<Pet>>(petsJson);
            //now foreach and use HasData
            foreach (Pet pet in pets)
            {
                modelBuilder.Entity<Pet>().HasData(pet);
            }
            #endregion pet

            #region slot
            //Seed data to slot
            string slotsJson = System.IO.File.ReadAllText("Slot.json");
            //deserialize from json string to List of slots
            List<Slot> slots = System.Text.Json.JsonSerializer.Deserialize<List<Slot>>(slotsJson);
            //now foreach and use HasData
            foreach (Slot slot in slots)
            {
                modelBuilder.Entity<Slot>().HasData(slot);
            }
            #endregion slot

            #region record
            //Seed data to record
            string recordsJson = System.IO.File.ReadAllText("Record.json");
            //deserialize from json string to List of records
            List<Record> records = System.Text.Json.JsonSerializer.Deserialize<List<Record>>(recordsJson);
            //now foreach and use HasData
            foreach (Record record in records)
            {
                modelBuilder.Entity<Record>().HasData(record);
            }
            #endregion record

            #region petvaccination
            //Seed data to petvaccination
            string petvaccinationsJson = System.IO.File.ReadAllText("PetVaccination.json");
            //deserialize from json string to List of petvaccinations
            List<PetVaccination> petvaccinations = System.Text.Json.JsonSerializer.Deserialize<List<PetVaccination>>(petvaccinationsJson);
            //now foreach and use HasData
            foreach (PetVaccination petvaccination in petvaccinations)
            {
                modelBuilder.Entity<PetVaccination>().HasData(petvaccination);
            }
            #endregion petvaccination

            #region pethealthtrack
            //Seed data to pethealthtrack
            string pethealthtracksJson = System.IO.File.ReadAllText("PetHealthTrack.json");
            //deserialize from json string to List of pethealthtracks
            List<PetHealthTrack> pethealthtracks = System.Text.Json.JsonSerializer.Deserialize<List<PetHealthTrack>>(pethealthtracksJson);
            //now foreach and use HasData
            foreach (PetHealthTrack pethealthtrack in pethealthtracks)
            {
                modelBuilder.Entity<PetHealthTrack>().HasData(pethealthtrack);
            }
            #endregion pethealthtrack

            #region payment
            ////Seed data to payment
            //string paymentsJson = System.IO.File.ReadAllText("payments.json");
            ////deserialize from json string to List of payments
            //List<Payment> payments = System.Text.Json.JsonSerializer.Deserialize<List<Payment>>(paymentsJson);
            ////now foreach and use HasData
            //foreach (Payment payment in payments)
            //{
            //    modelBuilder.Entity<Payment>().HasData(payment);
            //}
            #endregion payment

            #region invoice
            ////Seed data to invoice
            //string invoicesJson = System.IO.File.ReadAllText("invoices.json");
            ////deserialize from json string to List of invoices
            //List<Invoice> invoices = System.Text.Json.JsonSerializer.Deserialize<List<Invoice>>(invoicesJson);
            ////now foreach and use HasData
            //foreach (Invoice invoice in invoices)
            //{
            //    modelBuilder.Entity<Invoice>().HasData(invoice);
            //}
            #endregion invoice

            #region hospitalization
            //Seed data to hospitalization
            string hospitalizationsJson = System.IO.File.ReadAllText("Hospitalization.json");
            //deserialize from json string to List of hospitalizations
            List<Hospitalization> hospitalizations = System.Text.Json.JsonSerializer.Deserialize<List<Hospitalization>>(hospitalizationsJson);
            //now foreach and use HasData
            foreach (Hospitalization hospitalization in hospitalizations)
            {
                modelBuilder.Entity<Hospitalization>().HasData(hospitalization);
            }
            #endregion hospitalization

            #region appointment
            //Seed data to appointment
            string appointmentsJson = System.IO.File.ReadAllText("Appointmnet.json");
            //deserialize from json string to List of appointments
            List<Appointment> appointments = System.Text.Json.JsonSerializer.Deserialize<List<Appointment>>(appointmentsJson);
            //now foreach and use HasData
            foreach (Appointment appointment in appointments)
            {
                modelBuilder.Entity<Appointment>().HasData(appointment);
            }
            #endregion appointment

            #region appointmentdetail
            //Seed data to appointmentdetail
            string appointmentdetailsJson = System.IO.File.ReadAllText("appointmentdetails.json");
            //deserialize from json string to List of appointmentdetails
            List<AppointmentDetail> appointmentdetails = System.Text.Json.JsonSerializer.Deserialize<List<AppointmentDetail>>(appointmentdetailsJson);
            //now foreach and use HasData
            foreach (AppointmentDetail appointmentdetail in appointmentdetails)
            {
                modelBuilder.Entity<AppointmentDetail>().HasData(appointmentdetail);
            }
            #endregion appointmentdetail

            #endregion Seed Data

        }


    }
}