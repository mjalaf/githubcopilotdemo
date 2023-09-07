using Apis.PeopleManagment.Interfaces;
using Apis.PeopleManagment.Domain;
using Apis.PeopleManagment.DataBaseContxt;
using Microsoft.EntityFrameworkCore;

namespace Apis.PeopleManagement.Repositories
{
    public class PeopleManagementRepository : IPeopleProvider
    {
        private readonly PeopleManagementContext _context;

        public PeopleManagementRepository(PeopleManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPerson(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<Person> AddPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdatePerson(int id, Person person)
        {
            var personToUpdate = await GetPerson(id);
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            return personToUpdate;
        }

        public async Task<Person> DeletePerson(int id)
        {
            var personToDelete = await GetPerson(id);
            _context.People.Remove(personToDelete);
            await _context.SaveChangesAsync();
            return personToDelete;
        }

        public async Task<IEnumerable<Person>> GetPeopleByCity(string city)
        {
            return await _context.People.Where(p => p.City == city).ToListAsync();
        }

        public async Task<Person> GetPersonByCity(string city, int id)
        {
            return await _context.People.FirstOrDefaultAsync(p => p.City == city && p.Id == id);
        }
    }
}