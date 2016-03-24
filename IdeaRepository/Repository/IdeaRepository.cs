using IdeaRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdeaRepository.Repository
{
    public class IdeaRepository:IRepository<Idea>
    {
        private IdeasContext db;

        public IdeaRepository(IdeasContext context)
        {
            this.db = context;
        }
        public void Create(Idea idea)
        {
            db.Ideas.Add(idea);
        }

        public IEnumerable<Idea> GetAll()
        {
            return db.Ideas;
        }
        public IEnumerable<Idea> GetAllForUser(int UserId)
        {
            return db.Ideas.Where(i => i.UserId == UserId && i.DeletedByUser == false && i.DeletedByAdmin == false).ToArray();
        }

        public IEnumerable<Idea> GetAllForAdmin(int UserId)
        {
            return db.Ideas.Where(u=>u.UserId == UserId).ToArray();
        }

        public void Update(Idea idea)
        {
            db.Entry(idea).State = EntityState.Modified;
        }

        public Idea Get(int id)
        {
            return db.Ideas.Find(id);
        }

        public void Delete(int id)
        {
            Idea idea = db.Ideas.Find(id);
            if (idea != null)
                db.Ideas.Remove(idea);
        }
    }
}