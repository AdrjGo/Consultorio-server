using Domain.Interfaces;

namespace Application.Services
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }
}