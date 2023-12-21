using Azure;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.Service
{
    public class Contacts : Base<ContactDto, ContactSummary, Contact>, IContact
    
    {
        UserMangerSing _user;
        
        public Contacts(AppDbContext context,
            UserMangerSing user):base (context)
        {
            
            _user = user;
        }

        protected override Expression<Func<Contact, ContactDto>> DtoMapper => entity => new ContactDto()
        {
            Id = entity.Id,
            IsDeleted = entity.IsDeleted,
            FirstName = entity.FirstName,
            Surname = entity.Surname,   
            UserCreatDate = entity.UserCreatDate,
            PhoneNumber = entity.PhoneNumber,   
            UserId = entity.UserId, 
            UpdateUserId = entity.UpdateUserId,
            DeleteUserId = entity.DeleteUserId,
            UserDeleteDate = entity.UserDeleteDate,
            UserUpdateDate = entity.UserUpdateDate, 
        };

        protected override Expression<Func<Contact, ContactSummary>> SummaryMapper => entity => new ContactSummary() { 
               FirstName = entity.FirstName,
               IsDeleted = entity.IsDeleted,
               PhoneNumber = entity.PhoneNumber,
               Surname = entity.Surname,
               UserName = entity.Users.FullName,
               UserCreatDate = entity.UserCreatDate,
               Id = entity.Id,
               UpdateUserName = entity.Users.FullName,
               DeleteUserName = entity.Users.FullName,
               UserDeleteDate = entity.UserDeleteDate,
               UserUpdateDate = entity.UserUpdateDate,

        };

        public IEnumerable<ContactDto> GetDelete()
        {
            try
            {
               return _context.Contacts.Where(p=>p.IsDeleted == false).Select(p=> new ContactDto{
                   Id = p.Id,
                   FirstName = p.FirstName,
                   Surname = p.Surname,
                   PhoneNumber = p.PhoneNumber
               }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PagingModel GetPadding(int page , int pageSize )
        {

            var totalItems = _context.Contacts.Count(); // Toplam öğe sayısı
            var items = _context.Contacts.Where(p => p.IsDeleted == false)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagingModel
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = page,
                PageSize = pageSize
            };


        }

        public Contact MapEntitys(ContactDto model)
        {
            return new Contact()
            {
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                FirstName = model.FirstName,
                Surname = model.Surname,
                UserCreatDate = model.UserCreatDate,
                UserUpdateDate = model.UserUpdateDate,
                UserDeleteDate = model.UserDeleteDate,
                PhoneNumber = model.PhoneNumber,
                UserId = model.UserId,
                UpdateUserId = model.UpdateUserId,
                DeleteUserId = model.DeleteUserId

            };
        }

        protected override Contact CreatMapEntity(ContactDto model)
        {
            return new Contact()
            {
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                FirstName = model.FirstName,
                Surname = model.Surname,
                UserCreatDate = DateTime.Now,
                UserUpdateDate = DateTime.Now,
                UserDeleteDate = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
                UserId = _user.ActiveUser.Id,
                UpdateUserId = _user.ActiveUser.Id,
                DeleteUserId = _user.ActiveUser.Id

            };
        }

        protected override Contact MapEntity(ContactDto model)
        {
            return new Contact()
            {
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                FirstName = model.FirstName,
                Surname = model.Surname,
                UserCreatDate = model.UserCreatDate,
                UserUpdateDate = model.UserUpdateDate,
                UserDeleteDate = model.UserDeleteDate,
                PhoneNumber = model.PhoneNumber,
                UserId = model.UserId,
                UpdateUserId = model.UpdateUserId,
                DeleteUserId = model.DeleteUserId

            };
        }
    }
}
