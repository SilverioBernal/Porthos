using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Orkidea.Porthos.Business;
using Orkidea.Porthos.Entities;
using Orkidea.Porthos.Security;
using Orkidea.Porthos.Utilities;

namespace Orkidea.Porthos.FrontEnd.Controllers
{
    public class SecurityController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(People user, string returnUrl)
        {
            Cryptography oCrypto = new Cryptography();
            if (!String.IsNullOrEmpty(user.usuario) && !String.IsNullOrEmpty(user.contraseña))
            {
                user.contraseña = oCrypto.Encrypt(user.contraseña);

                PeopleBiz peopleBiz = new PeopleBiz();

                People usuarioAutenticado = peopleBiz.PeopleAuthentication(user);

                if (usuarioAutenticado.id != null)
                {
                    FormsAuthentication.SetAuthCookie(user.usuario, false);

                    string userData = usuarioAutenticado.id.ToString().Trim() + "|" + StringHelper.NombreUsuario(usuarioAutenticado) + "|" + (usuarioAutenticado.usuarioOrkidea ? "true" : "false");

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.usuario, DateTime.Now, DateTime.Now.AddMinutes(150), false, userData);

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    HttpContext.Response.Cookies.Add(faCookie);

                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (authCookie != null)
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        CustomIdentity identity = new CustomIdentity(authTicket.Name, userData);
                        GenericPrincipal newUser = new GenericPrincipal(identity, new string[] { });
                        HttpContext.User = newUser;
                    }

                    return RedirectToLocal(returnUrl);
                }
            }

            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction
                ("Login");
            }
        }

    }
}
