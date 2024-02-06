using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorDeHotel.UI2
{
    public class LanzadorDeErrores : IdentityErrorDescriber
    {

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "La clave debe tener al menos una letra en mayúscula"


            };
        }


        public override IdentityError PasswordRequiresLower()
        {

            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "La clave debe tener al menos una letra en minúscula"

            };



        }


        public override IdentityError PasswordRequiresNonAlphanumeric()
        {

            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "La clave debe ser alfanumérica"

            };

        }




    }
}
