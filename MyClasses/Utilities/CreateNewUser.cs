using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClasses.Entities;

namespace MyClasses.Utilities
{
    public class CreateNewUser
    {
        // Óljóst hvort þetta sé nauðsynlegt, kanski þarf að newa upp áður en það er hægt að
        // færa gögnin úr textboxunum og búa til á nýtt instance af user
        public static User CreateUser()
        {
            User user = new User
            {
                UserName = "",
                Password = ""
            };
            return user;
        }
    }
}
