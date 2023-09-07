# This document will explain the methods and how they work and was written by Copilot
## GetPeople
### Request

Description: Get a person from the database and return a list of people, this method is async which means that it will not block the thread while it is running

### Response
```csharp
public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
{
    return await _context.People.ToListAsync();
}
```
## GetPerson
### Request
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<Person>> GetPerson(int id)
```
### Response
```csharp
public async Task<ActionResult<Person>> GetPerson(int id)
{
    var person = await _context.People.FindAsync(id);

    if (person == null)
    {
        return NotFound();
    }

    return person;
}
```
