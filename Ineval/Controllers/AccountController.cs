using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ineval.Models;
using Ineval.DAL;
using Ineval.BO;

namespace Ineval.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);


            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Usuario o Contraseña inválidos.");
                    return View(model);
            }
        }

        //
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }


        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
        //    var user = await UserManager.FindAsync(model.UserName, model.Password);

        //    if (user != null)
        //    {
        //        var service = new InevalServices();
        //        var servicee = new EmpresaService();
        //        try
        //        {
        //            var usuario = service.UsuarioService.ObtenerPorApplicationUserId(user.Id);
        //            var configEmpresa = EmpresaService.ObtenerConfiguracion(usuario.EmpresaId);
        //            Context.UrlLogo = configEmpresa.UrlImage;
        //            Context.UserId = usuario.Id;
        //            Context.NumeroDecimales = configEmpresa.Decimales;

        //                Empresa empresa = await servicee.FirstOrDefaultAsync(a => a.Id == model.EmpresaId.Value);
        //                Office office = await _officeService.FirstOrDefaultAsync(o => o.Id == model.OfficeId.Value);
        //                await SignInAsync(user, model.RememberMe, empresa, office, model);

        //           // await SignInAsync(user, isPersistent: true);

        //            //await UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(CustomClaimTypes.UserAgencyId, usuario.EmpresaId.ToString()));

        //        }
        //        finally
        //        {
        //            service.Dispose();
        //            servicee.Dispose();
        //        }
        //    }


        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return Json(returnUrl ?? "", JsonRequestBehavior.AllowGet);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Usuario o Contraseña inválidos.");
        //            return View(model);
        //    }
        //}

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }                
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Url.Scheme);
                var mensaje = string.Format("Para recuperar su contraseña haga clic  <a href=\"{0}\">aqui</a>", callbackUrl);

                var service = new SwmServices();

                try
                {
                    var usuario = service.UsuarioService.ObtenerPorApplicationUserId(user.Id);

                    if (usuario == null)
                    {
                        return View("ForgotPasswordConfirmation");
                    }

                    var respuesta = "";

                    bool status = await EnvioCorreos.SendAccountAsync(user.Id, mensaje);

                }
                finally
                {
                    service.Dispose();
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        //private async Task SignInAsync(ApplicationUser user, bool isPersistent, Empresa empresa, Office office, LoginViewModel model)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    identity.AddClaim(new Claim(CustomClaimTypes.UserAgencyId, empresa.Id.ToString()));
        //    identity.AddClaim(new Claim(CustomClaimTypes.UserAgencyName, empresa.RazonSocial));
        //    identity.AddClaim(new Claim(CustomClaimTypes.OfficeId, office.Id.ToString()));
        //    identity.AddClaim(new Claim(CustomClaimTypes.OfficeName, office.Description.ToString()));
        //    identity.AddClaim(new Claim(CustomClaimTypes.UserId, user.Id));
        //    identity.AddClaim(new Claim(CustomClaimTypes.UserData, model.UserName));
        //    identity.AddClaim(new Claim(CustomClaimTypes.PasswordData, model.Password));
        //    AuthenticationManager.SignIn(new AuthenticationProperties() {
        //        IsPersistent = isPersistent,
        //        AllowRefresh = true,
        //        ExpiresUtc = DateTime.UtcNow.AddDays(1)
        //    }, identity);
        //}

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent, Empresa empresa, Office office, LoginViewModel model)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

        //    List<Claim> claims = new List<Claim>{
        //        new Claim(CustomClaimTypes.UserAgencyId, empresa.Id.ToString()),
        //        new Claim(CustomClaimTypes.UserAgencyName, empresa.RazonSocial),
        //        new Claim(CustomClaimTypes.OfficeId, office.Id.ToString()),
        //        new Claim(CustomClaimTypes.OfficeName, office.Description.ToString()),
        //        new Claim(CustomClaimTypes.UserId, user.Id),
        //        new Claim(CustomClaimTypes.UserData, model.UserName),
        //        new Claim(CustomClaimTypes.PasswordData, model.Password),
        //    };

        //    UserSession.EmpresaId = empresa.Id;
        //    UserSession.CurrentEmpresa = empresa.RazonSocial;
        //    UserSession.CurrentUser = model.UserName;
        //    UserSession.CurrentPassword = model.Password;
        //    UserSession.Office = Mapper.Map<OfficeViewModel>(office);
        //    UserSession.Empresa = Mapper.Map<EmpresaViewModel>(empresa);
        //    //UserSession.Office.Description = office.Description;

        //    identity.AddClaims(claims);

        //    // ClaimsIdentity identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
        //}

        //public async Task<JsonResult> AlterClaims(Guid empresaId, Guid id)
        //{
        //    var office = _officeService.GetById(id);
        //    var empresa = _empresaService.GetById(empresaId);

        //    UserSession.Empresa = Mapper.Map<EmpresaViewModel>(empresa);

        //    var Identity = HttpContext.User.Identity as ClaimsIdentity;

        //    if (UserSession.EmpresaId != Guid.Empty)
        //    {
        //        var usuario = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
        //        var empresacId = usuario.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserAgencyId);
        //        if (empresacId == null)
        //        {
        //            List<Claim> claims = new List<Claim>{
        //                new Claim(CustomClaimTypes.UserAgencyId, empresa.Id.ToString()),
        //                new Claim(CustomClaimTypes.UserAgencyName, empresa.RazonSocial),
        //                new Claim(CustomClaimTypes.OfficeId, office.Id.ToString()),
        //                new Claim(CustomClaimTypes.OfficeName, office.Description.ToString()),
        //                new Claim(CustomClaimTypes.UserId, User.Identity.GetUserId()),
        //                new Claim(CustomClaimTypes.UserData, UserSession.CurrentUser),
        //                new Claim(CustomClaimTypes.PasswordData, UserSession.CurrentPassword),
        //            };
        //            Identity.AddClaims(claims);
        //        }
        //    }

        //    Identity.RemoveClaim(Identity.FindFirst(CustomClaimTypes.OfficeId));
        //    Identity.AddClaim(new Claim(CustomClaimTypes.OfficeId, id.ToString()));

        //    Identity.RemoveClaim(Identity.FindFirst(CustomClaimTypes.OfficeName));
        //    Identity.AddClaim(new Claim(CustomClaimTypes.OfficeName, office.Description));

        //    Identity.RemoveClaim(Identity.FindFirst(CustomClaimTypes.UserAgencyId));
        //    Identity.AddClaim(new Claim(CustomClaimTypes.UserAgencyId, empresaId.ToString()));

        //    Identity.RemoveClaim(Identity.FindFirst(CustomClaimTypes.UserAgencyName));
        //    Identity.AddClaim(new Claim(CustomClaimTypes.UserAgencyName, empresa.RazonSocial));

        //    UserSession.Empresa = Mapper.Map<EmpresaViewModel>(empresa);
        //    UserSession.EmpresaId = empresa.Id;
        //    UserSession.CurrentEmpresa = empresa.RazonSocial;
        //    UserSession.Office = Mapper.Map<OfficeViewModel>(office);

        //    var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
        //    authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(Identity), new AuthenticationProperties() { /*IsPersistent = true,*/ ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddDays(30)) });
        //    return Json("ok");
        //}


        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(model.userId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            IdentitySignout();
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}