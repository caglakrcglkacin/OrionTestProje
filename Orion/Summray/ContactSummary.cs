using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Summray
{
    public class ContactSummary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public string DeleteUserName { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime UserCreatDate { get; set; }
        public DateTime UserDeleteDate { get; set; }
        public DateTime UserUpdateDate { get; set; }


    }
}
