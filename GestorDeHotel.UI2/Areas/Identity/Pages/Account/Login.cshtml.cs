using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace GestorDeHotel.UI2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        


        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public void EnvioDeCorreo(string destino, string asunto, string cuerpo)
        {

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("teampicateclasucr@gmail.com", "lenguajes2021");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;


            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("teampicateclasucr@gmail.com", "TeamPicaTeclas_Support");
            correo.To.Add(new MailAddress(destino));
            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = cuerpo;
            smtp.Send(correo);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Este campo es requerido")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "Este campo es requerido")]
            [Display(Name = "Clave")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Este campo es requerido")]
            [Display(Name = "Recordar")]
            public bool RememberMe { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _signInManager.UserManager.FindByNameAsync(Input.Nombre);

                

                int ? intentosFallidos = ((user != null) ? user.AccessFailedCount : null);

                var result = await _signInManager.PasswordSignInAsync(Input.Nombre, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                
                string asuntoIntentoDeInicioDeSesion = "Intento de inicio de sesión del usuario " + Input.Nombre + " bloqueado.";

                string mensajeDeInicioDeSesion = "Usted inicio sesión día " + DateTime.Now.ToString("dd/MM/yyyy") + " a las " + DateTime.Now.ToString("hh:mm")

;
              



                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    EnvioDeCorreo(user.Email, "Inicio de sesión usuario " + Input.Nombre, mensajeDeInicioDeSesion);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    

                    if (intentosFallidos == 2)
                    {
                        DateTime fechaFinalDelBloqueo = user.LockoutEnd.Value.DateTime.ToLocalTime();
                        string mensajeDeCuentaBloqueada = "Le informamos que la cuenta del usuario " + Input.Nombre +
                  " se encuentra bloqueada por 10 minutos. Por favor ingrese el día " + fechaFinalDelBloqueo.ToString("dd/MM/yyyy") + " a las " + fechaFinalDelBloqueo.ToString("hh:mm");
                        EnvioDeCorreo(user.Email, "Usuario Bloqueado.", mensajeDeCuentaBloqueada);

                    }
                   else
                    {

                        DateTime fechaFinalDelBloqueo = user.LockoutEnd.Value.DateTime.ToLocalTime();
                        string mensajeDeCuentaBloqueada = "Le informamos que la cuenta del usuario " + Input.Nombre +
                  " se encuentra bloqueada por 10 minutos. Por favor ingrese el día " + fechaFinalDelBloqueo.ToString("dd/MM/yyyy") + " a las " + fechaFinalDelBloqueo.ToString("hh:mm");
                        EnvioDeCorreo(user.Email, asuntoIntentoDeInicioDeSesion, mensajeDeCuentaBloqueada);
                    }
                                      
                        _logger.LogWarning("Cuenta bloqueada.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {

                    ModelState.AddModelError(string.Empty, "Inicio de sesión incorrecto");                                         
                    return Page();
                    }
                }

                // If we got this far, something failed, redisplay form
                return Page();
            }
        }
    }

