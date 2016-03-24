using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IdeaRepository.Controllers;
namespace IdeaRepository.Models
{
    public class UserDbInitializer : CreateDatabaseIfNotExists<IdeasContext>
    {

        protected override void Seed(IdeasContext dbcontex)
        {
            dbcontex.Roles.Add(new Role {Id =1, Name = "admin" });
            dbcontex.Roles.Add(new Role {Id =2, Name = "user" });         
            dbcontex.Users.Add(new User
            {
                Email = "admin@gmail.com",
                Password = AccountController.GetHashString("123456"),
                Login = "admin",
                RoleId = 1
            });
            base.Seed(dbcontex);
        }

    }
}