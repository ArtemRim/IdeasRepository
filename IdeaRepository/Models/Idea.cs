using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaRepository.Models
{
    public class Idea
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool Confirm { get; set; }
        public bool DeletedByUser { get; set; }
        public bool DeletedByAdmin{get;set;}

    }


}