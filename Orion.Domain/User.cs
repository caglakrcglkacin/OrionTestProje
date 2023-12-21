using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Domain
{
    public class User:IdentityUser<int>,IEntity
    {
        public string FullName { get; set; }
    }
}
