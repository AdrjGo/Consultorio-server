using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubmoduleRespository : ISubmoduleRepository
    {
        private readonly DBContext _context;
        public SubmoduleRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Submodule?> GetSubmoduleById(int id)
        {
            return await _context.Submodules.FindAsync(id);
        }

        public async Task<IEnumerable<Submodule>> GetAllSubmodules()
        {
            return await _context.Submodules.ToListAsync();
        }

        public async Task<Submodule> CreateSubmodule(Submodule submodule)
        {
            _context.Submodules.Add(submodule);
            await _context.SaveChangesAsync();
            return submodule;
        }

        public async Task<Submodule> UpdateSubmodule(Submodule submodule)
        {
            _context.Submodules.Update(submodule);
            await _context.SaveChangesAsync();
            return submodule;
        }

        public async Task DeleteSubmodule(int id)
        {
            var submodule = await _context.Submodules.FindAsync(id);
            if (submodule == null) return;
            _context.Submodules.Remove(submodule);
            await _context.SaveChangesAsync();
        }
    }
}