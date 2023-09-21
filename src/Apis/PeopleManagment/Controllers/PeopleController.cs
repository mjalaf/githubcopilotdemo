//Create a PeopleController class that inherits from the ControllerBase class.
//Add a constructor that accepts an IPeopleProvider as a parameter and assigns it to a private field.
//Add a Get method that returns an IActionResult.
//In the Get method, call the GetPeople method on the IPeopleProvider field and return the result.
//Add a Get method that accepts an id parameter and returns an IActionResult.
//In the Get method, call the GetPerson method on the IPeopleProvider field and return the result.
//Add a Post method that accepts a Person parameter and returns an IActionResult.
//In the Post method, call the AddPerson method on the IPeopleProvider field and return the result.
//Add a Put method that accepts an id parameter and a Person parameter and returns an IActionResult.
//In the Put method, call the UpdatePerson method on the IPeopleProvider field and return the result.
//Add a Delete method that accepts an id parameter and returns an IActionResult.
//In the Delete method, call the DeletePerson method on the IPeopleProvider field and return the result.
//Add a Get method that accepts an id parameter and a string parameter and returns an IActionResult.
//In the Get method, call the GetPerson method on the IPeopleProvider field and return the result.
//Add a Get method that accepts a string parameter and returns an IActionResult.
//In the Get method, call the GetPeople method on the IPeopleProvider field and return the result.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apis.PeopleManagment.Interfaces;
using Apis.PeopleManagment.Domain;

namespace Apis.PeopleManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleProvider _peopleProvider;

        public PeopleController(IPeopleProvider peopleProvider)
        {
            _peopleProvider = peopleProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _peopleProvider.GetPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _peopleProvider.GetPerson(id);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            var newPerson = await _peopleProvider.AddPerson(person);
            return Ok(newPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            var updatedPerson = await _peopleProvider.UpdatePerson(id, person);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedPerson = await _peopleProvider.DeletePerson(id);
            return Ok(deletedPerson);
        }

        [HttpGet("{id}/{city}")]
        public async Task<IActionResult> Get(int id, string city)
        {
            var person = await _peopleProvider.GetPersonByCity(city, id);
            return Ok(person);
        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var people = await _peopleProvider.GetPeopleByCity(city);
            return Ok(people);
        }
    }
}
