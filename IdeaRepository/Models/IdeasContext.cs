using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdeaRepository.Models
{
    public class IdeasContext : DbContext
    {
        public IdeasContext() : base("DBConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Idea> Ideas { get; set; }
    }
}