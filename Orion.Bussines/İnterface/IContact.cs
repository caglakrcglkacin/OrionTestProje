using Orion.Bussines.Diger;
using Orion.Domain;
using Orion.Dto;
using Orion.Summray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.İnterface
{
    public interface IContact:IBase<ContactDto, ContactSummary>
    {
        public abstract Contact MapEntitys(ContactDto model);
        IEnumerable<ContactDto> GetDelete();
        PagingModel GetPadding(int page , int pageSize );

    }
}
