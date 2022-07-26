using System;
using ChatServer.Entities;
using System.Linq;
using System.Collections.Generic;

namespace ChatServer.Mocks
{
    public class UserMocks
    {

        public static List<User> users = new List<User>() {
        new User() { Email = "test@gmail.com", Username = "testOne", Password = "password" } ,
        new User() { Email = "test2@gmail.com", Username = "testTwo", Password = "password" } ,
        new User() { Email = "tes3t@gmail.com", Username = "testThree", Password = "password" } ,
        };
            
    }
}
