// Create a Person class in the Domain folder that has the following properties:

// Id
// FirstName
// LastName
// Age
// Address
// City
// State
// ZipCode

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Apis.PeopleManagment.Domain
{
    //Add entitiy framework decorators to the Person class.


    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}