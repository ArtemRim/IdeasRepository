using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using IdeaRepository.Models;
using System.Text;
using IdeaRepository.Repository;
namespace IdeaRepository.Controllers
{
    [Authorize]
    public class IdeasController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public JsonResult AddIdea(string TextIdea)
        {
            if (TextIdea == null)
                return Json(Properties.Resources.RequestTextIdeaError);
            int id = User.Identity.GetUserId<int>();
            unitOfWork.Ideas.Create(new Idea { Text = TextIdea, Author = User.Identity.Name, UserId = id, Date = DateTime.Now, Confirm = false, DeletedByAdmin = false, DeletedByUser = false });
            unitOfWork.Save();
            var ideas = unitOfWork.Ideas.GetAllForUser(id);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);
        }

        [Authorize(Roles = "user,admin")]
        [System.Web.Http.HttpGet]
        public JsonResult GetAllIdeas()
        {
            int id = User.Identity.GetUserId<int>();
            var ideas = unitOfWork.Ideas.GetAllForUser(id);
            return Json(ideas, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public JsonResult RemoveIdea(int IdeaId)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
                int id = User.Identity.GetUserId<int>();
                Idea idea = unitOfWork.Ideas.Get(IdeaId);
                CheckOnAdmin(idea, IdeaId);
                unitOfWork.Save();
                var ideas = unitOfWork.Ideas.GetAllForUser(id);
                if (ideas == null)
                    return Json(Properties.Resources.IdeasNotFoundError);
                return Json(ideas, "application/json", Encoding.UTF8);
        }

        private void CheckOnAdmin(Idea idea,int IdeaId)
        {
            if (User.IsInRole("admin"))
                unitOfWork.Ideas.Delete(IdeaId);
            else
            {
                idea.DeletedByUser = true;
                unitOfWork.Ideas.Update(idea);
            }
        }
        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public JsonResult GetIdea(int IdeaId)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
            var idea = unitOfWork.Ideas.Get(IdeaId);
            if (idea == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(idea, "application/json", Encoding.UTF8);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public JsonResult SaveChanges(Idea idea)
        {
            if (idea == null)
                return Json(Properties.Resources.RequestIdeaError);
            SaveChangedIdea(idea);
            var ideas = unitOfWork.Ideas.GetAllForUser(idea.UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);

        }

        private void SaveChangedIdea(Idea idea)
        {
            idea.Date = DateTime.Now;
            unitOfWork.Ideas.Update(idea);
            unitOfWork.Save();
        }

        [Authorize(Roles = "admin")]
        [System.Web.Http.HttpGet]
        public JsonResult GetAllUsers()
        {
            var users = unitOfWork.Users.GetAll().ToArray();
            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult GetUserIdeas(int UserId)
        {
            if (UserId == null)
                return Json(Properties.Resources.RequestUserIdError);
            var ideas = unitOfWork.Ideas.GetAllForAdmin(UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);
        }


        [HttpPost]
        public JsonResult RestoreIdea(int IdeaId)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
            var idea = RestoreProperties(IdeaId);
            Update(idea);
            var ideas = unitOfWork.Ideas.GetAllForAdmin(idea.UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);
        }

        private Idea RestoreProperties(int id)
        {
            var idea = unitOfWork.Ideas.Get(id);
            idea.DeletedByAdmin = false;
            idea.DeletedByUser = false;
            idea.Date = DateTime.Now;
            return idea;
        }


        private void Update(Idea idea)
        {
            unitOfWork.Ideas.Update(idea);
            unitOfWork.Save();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult RemoveIdeaByAdmin(int IdeaId)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
            Idea idea = unitOfWork.Ideas.Get(IdeaId);
            idea.Confirm = true;
            Update(idea);
            var ideas = unitOfWork.Ideas.GetAllForAdmin(idea.UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public JsonResult RemoveConfirmed(int IdeaId, bool isConfirmed)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
            if (isConfirmed == null)
                return Json(Properties.Resources.RequestFlagConfirmedError);
            var idea = ChangeDeleteFlag(IdeaId, isConfirmed);
            Update(idea);
            var ideas = unitOfWork.Ideas.GetAllForUser(idea.UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
            return Json(ideas, "application/json", Encoding.UTF8);                
        }

        private Idea ChangeDeleteFlag(int IdeaId, bool isConfirmed)
        {
            var idea = unitOfWork.Ideas.Get(IdeaId);
            if (isConfirmed)
            {
                idea.Confirm = false;
                idea.DeletedByAdmin = true;
            }
            else
                idea.Confirm = false;
            return idea;
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult CancelDeletionByAdmin(int IdeaId)
        {
            if (IdeaId == null)
                return Json(Properties.Resources.RequestIdeaIdError);
            Idea idea = unitOfWork.Ideas.Get(IdeaId);
            idea.Confirm = false;
            Update(idea);
            var ideas = unitOfWork.Ideas.GetAllForAdmin(idea.UserId);
            if (ideas == null)
                return Json(Properties.Resources.IdeasNotFoundError);
           return Json(ideas, "application/json", Encoding.UTF8);
        }
    }
}