using Orion.Bussines.Diger;
using Orion.Bussines.İnterface;
using Orion.DataAccess;
using Orion.Domain;
using Orion.Dto;
using Orion.Summray;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.Service
{
    public class Users : Base<UserDto, UserSummary, User>, IUser
    {
 
        public Users(AppDbContext op): base (op)
        {
            
        }

        protected override Expression<Func<User, UserDto>> DtoMapper => entity => new UserDto()
        { 
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            FullName= entity.FullName,
            

        };

        protected override Expression<Func<User, UserSummary>> SummaryMapper => entity => new UserSummary()
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            FullName = entity.FullName,
          
        };

        public User MapEntitys(UserDto model)
        {
            return new User()
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
             
            };
        }

        protected override User CreatMapEntity(UserDto model)
        {
            throw new NotImplementedException();
        }

        protected override User MapEntity(UserDto model)
        {
            return new User()
            {
                Id = model.Id,
                UserName = model.UserName,            
                FullName = model.FullName,
                
            };
        }
    }
}
