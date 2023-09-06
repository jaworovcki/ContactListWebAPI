using ContactListWebAPI.Models;

namespace ContactListWebAPI.Interfaces
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Person>> GetPeopleAsync();

        Task<Person?> GetPersonByIdAsync(int id);

        Task<Person?> GetPersonByNameAsync(string? nameFilter = null);

        Task<Person> AddPersonAsync(Person person);

        Task DeletePersonAsync(int id);
    }
}
