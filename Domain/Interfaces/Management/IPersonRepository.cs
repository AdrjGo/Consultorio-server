using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonById(Guid id);
        Task<Person?> GetPersonByName(string name);
        Task<Person?> GetPersonByCi(string ci);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(Guid id);
    }
}