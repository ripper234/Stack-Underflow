#region

using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Web.Ui.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //
        // GET: /Authentication/

        public ActionResult Login()
        {
            return View();
        }

        //
        // Get: /Authentication/Login/openid
        public ActionResult Authenticate()
        {
            using (var relayingParty = new OpenIdRelyingParty())
            {
                var response = relayingParty.GetResponse();

                if (response == null)
                {
                    // Stage 2: user submitting Identifier
                    var openId = Request.Form["openId"];
                    relayingParty.CreateRequest(openId).RedirectToProvider();
                    
                    throw new Exception("Never gets here");
                }
                
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var claimedIdentifier = response.ClaimedIdentifier;
                        var user = _userRepository.FindByOpenId(claimedIdentifier);
                        if (user != null)
                        {
                            // login
                            FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), false);
                            return new EmptyResult();

                            // TODO - http://stackoverflow.com/questions/1991710/understanding-redirections-in-asp-net-mvc
                            // throw new Exception("Should never get here");
                        }
                        
                        // register
                        var username = response.FriendlyIdentifierForDisplay;
                        user = new User
                                       {
                                           Name = username,
                                           OpenId = claimedIdentifier,
                                           Reputation = 1,
                                           SignupDate = DateTime.Now
                                       };
                        _userRepository.Save(user);
                        FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), false);
                        throw new Exception("Should never get here");

                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("Login");

                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("Login");

                    default:
                        throw new Exception("Unknown status");
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl != null)
                return Redirect(returnUrl);
            
            return RedirectToAction("Index", "Home");
        }
    }
}