using IdeaRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaRepository.Repository
{
    public class UnitOfWork
    {
        private IdeasContext db = new IdeasContext();
        private UserRepository userRepository;
        private IdeaRepository ideaRepository;
        private RoleRepository roleRepository;
        private bool disposed = false;


        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IdeaRepository Ideas
        {
            get
            {
                if (ideaRepository == null)
                    ideaRepository = new IdeaRepository(db);
                return ideaRepository;
            }
        }

        public RoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}