using Orion.Bussines.Diger;
using Orion.Domain;
using Orion.Dto;
using Orion.Summray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.İnterface
{
    public interface IUser:IBase<UserDto,UserSummary>
    {
        public abstract User MapEntitys(UserDto model);
    }
}
