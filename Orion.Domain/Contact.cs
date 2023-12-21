using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Domain
{
    public class Contact:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public int DeleteUserId { get; set; }
        public int UpdateUserId { get; set; }

        public DateTime UserCreatDate { get; set; }
        public DateTime UserDeleteDate { get; set; }
        public DateTime UserUpdateDate { get; set; }
        public User Users { get; set; }
    }
}
