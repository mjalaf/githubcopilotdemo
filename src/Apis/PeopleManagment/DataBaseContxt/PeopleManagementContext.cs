using Apis.PeopleManagment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Apis.PeopleManagment.DataBaseContxt
{
    public class PeopleManagementContext : DbContext
    {
        public PeopleManagementContext(DbContextOptions<PeopleManagementContext> options) : base(options)
        {
            
        }

       public DbSet<Person> People { get; set; }

        public IEnumerable<Person> GetPeople()
        {
            return People.ToList();
        }

        public Person GetPerson(int id)
        {
            return People.Find(id);
        }

        // Adds a person to the database and returns the added person.

        public Person AddPerson(Person person)
        {
            // validate the person object
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            // validate if the person has a valid id
            if (person.Id != 0)
            {
                throw new ArgumentException("The person Id must be 0 since it's an identity field", nameof(person));
            }
            // validate if the person has at least a name and last name
            if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.LastName))
            {
                throw new ArgumentException("The person must have a name and last name", nameof(person));
            }

            //validate if the age is not negative
            if (person.Age < 0)
            {
                throw new ArgumentException("The person must have a positive age", nameof(person));
            }

            // validate if the zip code belongs to florida
            if (!person.ZipCode.StartsWith("33"))
            {
                throw new ArgumentException("The person must have a zip code that belongs to Florida", nameof(person));
            }
            

            return People.Add(person).Entity;
        }

        public Person UpdatePerson(int id, Person person)
        {
            var personToUpdate = GetPerson(id);
            People.Update(person);
            SaveChanges();
            return personToUpdate;
        }

        public Person DeletePerson(int id)
        {
            var personToDelete = GetPerson(id);
            People.Remove(personToDelete);
            SaveChanges();
            return personToDelete;
        }

        public IEnumerable<Person> GetPeopleByCity(string city)
        {
            return People.Where(p => p.City == city);
        }

        public Person GetPersonByCity(string city, int id)
        {
            return People.FirstOrDefault(p => p.City == city && p.Id == id);
        }
    }
}
