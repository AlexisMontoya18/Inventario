using Rotativa;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Documents;
using SystemaVidanta.DAL;
using SystemaVidanta.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SystemaVidanta.Controllers
{
    public class HomeController : Controller
    {
        private SystemVidantaContext db = new SystemVidantaContext();

        [Authorize]
        public ActionResult Index(string nombre = "")
        {
            ViewBag.nombre = nombre;
            return View(nombre);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login usuario)
        {
            if (ModelState.IsValid)
            {

                var UserExists = db.Users.Where(u => u.Username == usuario.User).FirstOrDefault();
                if (UserExists != null)
                {
                    bool IsValidUser = BCrypt.Net.BCrypt.Verify(usuario.Password, UserExists?.Password);

                    if (IsValidUser)
                    {


                        FormsAuthentication.SetAuthCookie(UserExists.ID, false);

                        return RedirectToAction("Create", "Article");
                    }
                }
            }
            ModelState.AddModelError("LOGIN_ERROR", "Usuario o contraseña invalidos");
            return View();
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            //return View();
        }
    }
    
    internal class SystemaVidantaContext
    {
        public IEnumerable<object> Users { get; internal set; }
    }
    
}