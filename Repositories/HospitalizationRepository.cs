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
        public async Task<Hospitalization> Add(Hospitalization hospitalization)
        {
                await _context.Hospitalizations.AddAsync(hospitalization);
                await _context.SaveChangesAsync();
                return hospitalization;
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

        public async Task<Hospitalization?> Remove(int id)
        {
            var hospitalization = await _context.Hospitalizations.FindAsync(id); ;
            if(hospitalization == null)
            {
                return null;
            }
            _context.Hospitalizations.Remove(hospitalization);
            await _context.SaveChangesAsync();
            return hospitalization;
        }


        public async Task<Hospitalization?> Update(int id,Hospitalization hospitalization)
        {
                var existhospitalization = await GetById(id);
                if (existhospitalization == null)
                {
                    return null;
                }
                existhospitalization.AdmissionDate = hospitalization.AdmissionDate;
                existhospitalization.PetId = hospitalization.PetId;
               existhospitalization.KennelId = hospitalization.KennelId;
               existhospitalization.VetId = hospitalization.VetId;
                existhospitalization.DischargeDate = hospitalization.DischargeDate;
                existhospitalization.TotalCost = hospitalization.TotalCost;
            await _context.SaveChangesAsync();
            return hospitalization;
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
        public async Task<List<Hospitalization>> GetAllByVetId(string id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .Where(a => a.VetId == id)
                .ToListAsync();
        }
        public async Task<List<Hospitalization>> GetAllByPetId(int id)
        {
            return await _context.Hospitalizations
                .Include(a => a.Pet)
                .Include(a => a.Kennel)
                .Include(a => a.Vet)
                .Where(a => a.PetId == id)
                .ToListAsync();
        }
        public async Task<bool> IsVetDateConflict(Hospitalization hospitalization)
        {
            return await _context.Hospitalizations
                .AnyAsync(a => a.VetId == hospitalization.VetId && hospitalization.AdmissionDate >= a.AdmissionDate && hospitalization.AdmissionDate <= a.DischargeDate);
        }
    }
}
