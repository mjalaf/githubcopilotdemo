//Create a PeopleManager Interface that defines the following methods:
//GetPeople
//GetPerson
//AddPerson
//UpdatePerson
//DeletePerson
//GetPeopleByCity
//GetPersonByCity

using Apis.PeopleManagment.Domain;

namespace Apis.PeopleManagment.Interfaces
{
    public interface IPeopleProvider
    {
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPerson(int id);
        Task<Person> AddPerson(Person person);
        Task<Person> UpdatePerson(int id, Person person);
        Task<Person> DeletePerson(int id);
        Task<IEnumerable<Person>> GetPeopleByCity(string city);
        Task<Person> GetPersonByCity(string city, int id);
    }
}