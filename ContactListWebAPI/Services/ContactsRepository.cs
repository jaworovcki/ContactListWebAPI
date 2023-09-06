using ContactListWebAPI.DataAccess;
using ContactListWebAPI.Interfaces;
using ContactListWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebAPI.Services
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ContactDataContext _context;

        public ContactsRepository(ContactDataContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetPeopleAsync() 
            => await _context.People
            .AsNoTracking()
            .ToListAsync();

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("Incorrect id", nameof(id));
            }
            else
            {
                return await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            }
        }

        public async Task<Person?> GetPersonByNameAsync(string? nameFilter = null)
        {
            if (nameFilter is null)
            {
                throw new ArgumentNullException(nameof(nameFilter));
            }
            else
            {
                return await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.FirstName.Contains(nameFilter) || p.LastName.Contains(nameFilter));
            }
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            else
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();
                return person;
            }
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await GetPersonByIdAsync(id);

            if (person is null)
            {
                throw new ArgumentException("Person not found", nameof(id));
            }
            else
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}
