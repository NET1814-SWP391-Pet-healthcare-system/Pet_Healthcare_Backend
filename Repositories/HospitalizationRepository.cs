using RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Repositories
{
    public class HospitalizationRepository : IHospitalizationRepository
    {
        private readonly ApplicationDbContext _context;
        public HospitalizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Hospitalization hospitalization)
        {
                await _context.Hospitalizations.AddAsync(hospitalization);
                return await SaveChangesAsync();

        }

        public async Task<IEnumerable<Hospitalization>> GetAll()
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .ToListAsync();
        }

        public async Task<Hospitalization?> GetById(int id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.HospitalizationId == id);
        }

        public async Task<bool> Remove(Hospitalization hospitalization)
        {
            _context.Hospitalizations.Remove(hospitalization);
            return await SaveChangesAsync();
        }


        public async Task<bool> Update(Hospitalization existhospitalization)
        {
            _context.Entry(existhospitalization).State = EntityState.Modified;
            await SaveChangesAsync();
            return true;
        }
        public async Task<Hospitalization?> GetByPetId(int id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.PetId == id);
        }
        public async Task<Hospitalization?> GetByVetId(string id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.VetId == id);
        }
        public async Task<IEnumerable<Hospitalization>> GetAllByVetId(string id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .Where(a => a.VetId == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Hospitalization>> GetAllByPetId(int id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .Where(a => a.PetId == id)
                .ToListAsync();
        }
        public async Task<bool> IsVetDateConflict(string id,DateOnly AdmissionDate, DateOnly DischargeDate)
        {
            return await _context.Hospitalizations
            .AnyAsync(a => a.VetId == id
        && ((AdmissionDate < a.DischargeDate && AdmissionDate >= a.AdmissionDate) 
            || (DischargeDate > a.AdmissionDate && DischargeDate <= a.DischargeDate)
            || (AdmissionDate <= a.AdmissionDate && DischargeDate >= a.DischargeDate)));
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}
