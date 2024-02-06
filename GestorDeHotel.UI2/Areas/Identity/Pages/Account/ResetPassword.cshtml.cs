using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GestorDeHotel.UI2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Este campo es requerido")]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Este campo es requerido")]
            [StringLength(100, ErrorMessage = "La {0} debe ser de almenos {2} y maximo {1} caracteres de longitud.", MinimumLength = 6)]
            [Display(Name = "Clave Nueva")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Este campo es requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar la clave nueva")]
            [Compare("Password", ErrorMessage = "Las claves no coinciden")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }

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



        public IActionResult OnGet(string code = null)
        {                
                return Page();            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByNameAsync(Input.UserName);


            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario no existe");
                return Page();

            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            Input.Code = code;
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {


                string elCorreoElectronicoDelUsuario = user.Email;

                string elMensajeDeCambioDeClave = "Le informamos que el cambio de clave de la cuenta del usuario " + Input.UserName + " se ejecutó satisfactoriamente.";

                EnvioDeCorreo(elCorreoElectronicoDelUsuario, "Cambio de clave", elMensajeDeCambioDeClave);



                return RedirectToPage("./ResetPasswordConfirmation"); ;
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }






    }
}
