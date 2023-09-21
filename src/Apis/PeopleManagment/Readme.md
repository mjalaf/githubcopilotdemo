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
- Probar
 - API MS
    - Publicarla en AKS
        - Consumirla desde API MAnagement
    - Base de Datos - Azure SQL 
        - Para autenticar, usar Workload Indentity
    - Key Vault (revisar)
        - Para guardar la cadena de conexión    

- SPA MS
    - Probar publicar una SAP en AKS
        - Acceder la SAP desde un Application Gateway
        - Consumir la API que publicamos

- BFF  MS
    - Crear un BFF acceda a una API del API management y que le pasemos un Key, o un valor para atenticar

