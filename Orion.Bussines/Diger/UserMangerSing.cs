using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orion.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.Diger
{
    public class UserMangerSing:UserManager<User>
    {
        IHttpContextAccessor _httpContext;
        public UserMangerSing(IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IHttpContextAccessor httpContext,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger) : base(store, optionsAccessor,
                passwordHasher, userValidators, passwordValidators,
               keyNormalizer, errors, services, logger)
        {
            _httpContext = httpContext;
        }
        public User ActiveUser
        {
            get { return GetUserAsync(_httpContext.HttpContext.User).Result; }
        }
    }
}
