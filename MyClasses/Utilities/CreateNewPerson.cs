using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClasses.Entities;

namespace MyClasses.Utilities
{
    public class CreateNewPerson
    {

        // Óljóst hvort þetta sé nauðsynlegt, kanski þarf að newa upp áður en það er hægt að
        // færa gögnin úr textboxunum og búa til nýtt instance af person
        public static Person CreatePerson()
        {
            Person person = new Person
            {
                FirstName = "",
                LastName = "",
                Kennitala = "",
                Email = "",
                PhoneNumber = "",
                Street = "",
                City = "",
                Zip = 0,
                Preferences = "",
            };
            return person;
        }
    }
}
