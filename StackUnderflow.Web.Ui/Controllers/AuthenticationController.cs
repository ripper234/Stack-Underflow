#region

using System;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.OpenId.RelyingParty;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Web.Ui.Controllers
{
    public class AuthenticationController : UserAwareController
    {
        public AuthenticationController(IUserRepository userRepository) : base(userRepository)
        {
        }

        //
        // GET: /Authentication/

        public ActionResult Login()
        {
            return UserView(CreateModel());
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
                        var user = Users.FindByOpenId(claimedIdentifier);
                        if (user != null)
                        {
                            // login
                            return RedirectFromLoginPage(user);
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
                        Users.Save(user);
                        return RedirectFromLoginPage(user);

                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        // todo
                        return View("Login");

                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        // todo
                        return View("Login");

                    default:
                        throw new Exception("Unknown status");
                }
            }
        }

        private ActionResult RedirectFromLoginPage(User user)
        {
            FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), false);
            return new EmptyResult();
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