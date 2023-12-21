using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Orion.Bussines.Diger;
using Orion.Bussines.İnterface;
using Orion.DataAccess;
using Orion.Dto;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Orion.Web.Controllers
{
    public class PhoneController : Controller
    {
        IContact _contact;
        UserMangerSing _user;
        AppDbContext _appDbContext;
        public PhoneController(IContact contact,
            UserMangerSing user,
            AppDbContext appDbContext)
        {
            _contact = contact;
            _user = user;
            _appDbContext = appDbContext;
        }
       
        public IActionResult Liste(int page = 1, int pageSize = 5)
        {


            var Page = _contact.GetPadding(page, pageSize);


            ViewBag.Rehper = Page;
            if (TempData["SuccessMessage"] != null)
            
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            
           
             
            return View();
        }
        public IActionResult Listes(int page = 1, int pageSize = 5)
        {


            var Page = _contact.GetPadding(page, pageSize);
            ViewBag.Rehper = Page;


            return View();
        }

     
        [HttpPost]
        public IActionResult Kaydet(ContactDto contact)
        {
            contact.UserId = _user.ActiveUser.Id;
            contact.UserCreatDate = DateTime.Now;
            contact.UserUpdateDate = DateTime.Now;
            contact.UserDeleteDate = DateTime.Now;
            contact.DeleteUserId = _user.ActiveUser.Id;
            contact.UpdateUserId = _user.ActiveUser.Id;
            var Person = _contact.Create(contact);
            if (Person.IsSuccess)
            {
                TempData["SuccessMessage"] = Person.Message;
                return RedirectToAction("Liste", "Phone");
            }
            ViewBag.ErrorMessage = Person.Message.Replace("\n", "<br>");
            return View();
        }
        [HttpGet]
        public IActionResult GetData(int Id)
        {

            var Person = _contact.GetById(Id);

           
            return Json(Person);
        }
        [HttpPost]
        public IActionResult Update(ContactDto contact)
        {
            var person = _contact.GetById(contact.Id);
            contact.UserCreatDate = person.UserCreatDate;
            contact.UserId += person.UserId;
            contact.DeleteUserId += person.UserId;
            contact.UserDeleteDate = person.UserDeleteDate;
            contact.UserUpdateDate = DateTime.Now;
            contact.UpdateUserId = _user.ActiveUser.Id;
            
            var Person = _contact.Update(contact);
            if (Person.IsSuccess)
            {
                TempData["SuccessMessage"] = Person.Message;
                return RedirectToAction("Liste", "Phone");
            }
            ViewBag.ErrorMessage = Person.Message.Replace("\n", "<br>");
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
           
            var Person = _contact.GetById(Id); 
            Person.IsDeleted = true;
            Person.UserDeleteDate = DateTime.Now;
            Person.DeleteUserId = _user.ActiveUser.Id;

            var deger = _contact.Delete(Person);

            if (deger.IsSuccess)
            {
                return Json(new { status = true });
            }

            return View();
        }
    }
}
