
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PersonRespository : IPersonRepository
    {
        private readonly DBContext _context;
        public PersonRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person?> GetPersonByName(string name)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Person?> GetPersonByCi(string ci)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Ci == ci);
        }

        public async Task<Person?> GetPersonById(Guid id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return;
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}