using IdeaRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdeaRepository.Repository
{
    public class UserRepository:IRepository<User>
    {
        private IdeasContext db;

        public UserRepository(IdeasContext context)
        {
            this.db = context;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }


        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
          public User Get(string login, string passwordhash)
        {
            return db.Users.Where(u => u.Login == login && u.Password == passwordhash).FirstOrDefault();
        }

          public User Get(string login)
          {
              return db.Users.FirstOrDefault(u => u.Login == login);
          }
     
    }
}