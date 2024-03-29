using handmadeShop.BusinessLogic;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.BusinessLogic.Core;
using handmadeShop.Domain.Entities.User;
using System;
using System.Web.Mvc;

namespace handmadeShop.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBl();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDataTime = DateTime.Now
                };

                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                }
            }

            return View();
        }
    }
}
