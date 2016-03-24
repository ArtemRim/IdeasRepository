using IdeaRepository.Models;
using IdeaRepository.Repository;
using IdeaRepository.ViewModel;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IdeaRepository.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {           
            if (ModelState.IsValid)
            {
                User user = unitOfWork.Users.Get(model.Login, GetHashString(model.Password));     
                if (user != null)
                {
                    SetClaims(user);
                    if (Properties.Resources.Admin.Equals(unitOfWork.Roles.Get(user.RoleId).Name))
                        return RedirectToAction("AdminPage");
                    else
                        return RedirectToAction("UserPage");
                }
                else
                    ModelState.AddModelError("", Properties.Resources.PasswordLoginError);
            }
            return View(model);
        }

        public static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            return hash;
        }

        public string GetRoleName(User user)
        {
            return unitOfWork.Roles.Get(user.RoleId).Name;
        }

        private  void SetClaims(User user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, GetRoleName(user), ClaimValueTypes.String));
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = unitOfWork.Users.Get(model.Login);
                if (user == null)
                {
                    string passwordHash = GetHashString(model.Password);
                    unitOfWork.Users.Create(new User { Email = model.Email, Password = passwordHash, Login = model.Login, RoleId = 2 });
                    unitOfWork.Save();
                    user = unitOfWork.Users.Get(model.Login, passwordHash);
                    if (user != null)
                        return RedirectToAction("Login");                                
                }
                else
                    ModelState.AddModelError("", Properties.Resources.LoginExistError);            
            }     
            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        public ActionResult AdminItems()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}