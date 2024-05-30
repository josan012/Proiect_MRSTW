using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.BusinessModels
{
    public class CustomPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {
            throw new NotImplementedException();
        }
    }
}
