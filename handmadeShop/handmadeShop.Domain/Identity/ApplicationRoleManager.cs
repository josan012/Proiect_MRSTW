using handmadeShop.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Identity
{
     public class ApplicationRoleManager : RoleManager<ApplicationRole>
     {
          public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store) { }
     }
}
