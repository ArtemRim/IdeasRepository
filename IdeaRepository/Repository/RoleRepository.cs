using IdeaRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdeaRepository.Repository
{
    public class RoleRepository:IRepository<Role>
    {
        private IdeasContext db;
        public RoleRepository(IdeasContext context)
        {
            this.db = context;
        }
        public void Create(Role role)
        {
            db.Roles.Add(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }
    }
}