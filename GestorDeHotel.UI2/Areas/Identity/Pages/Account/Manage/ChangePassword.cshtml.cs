using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace GestorDeHotel.UI2.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; } 

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar la clave nueva")]
            public string confirmPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Clave nueva")]
            public string NewPassword { get; set; }

            
            [Display(Name = "Nombre")]            
            public string Nombre { get; set; }
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







        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var usuario = await _signInManager.UserManager.FindByNameAsync(Input.Nombre);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.confirmPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("El usuario cambió su contraseña correctamente.");
            StatusMessage = "Clave actualizada.";
            EnvioDeCorreo(usuario.Email, "Cambio de clave.", 
                "Le informamos que el cambio de clave de la cuenta del usuario " + Input.Nombre + " se ejecutó satisfactoriamente." );
            return RedirectToPage();
        }
    }
}
