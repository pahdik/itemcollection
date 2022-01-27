using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using siteCollections.Models;
using siteCollections.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace siteCollections.Controllers
{
    
    public class UsersController : Controller
    {
        ICollectionRepository CollectionRepo;
        UserManager<User> userManager;
        public UsersController(UserManager<User> userManager,ICollectionRepository collectionRepository)
        {
            this.CollectionRepo = collectionRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> UserList = new List<UserViewModel>();
            foreach (var model in userManager.Users)
            {
                UserList.Add(new UserViewModel { user = model, Roles = await userManager.GetRolesAsync(model),IsBlock= await userManager.IsLockedOutAsync(model) });
            }
            return View(UserList);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleList = await userManager.GetRolesAsync(user);
            string role= roleList.Contains("admin") ?"admin" : "user";
 
             EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Role =  role};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                var UserRoles = await userManager.GetRolesAsync(user);
                bool IsAdmin = UserRoles.Contains("admin");
                if (user != null)
                {
                    if (model.Role == "user")
                    {
                        if (IsAdmin)
                        {
                            await userManager.RemoveFromRoleAsync(user, "admin");
                        }
                    }
                    else
                    {
                        if (!IsAdmin)
                        {
                            await userManager.AddToRoleAsync(user, "admin");
                        }
                    }

                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return View(model);
            }
            return View(model);
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Block(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if(await userManager.IsLockedOutAsync(user))
            {
                await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now);
                
            }
            else
            {
                await userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(3000, 10, 12, 21, 1, 1, new TimeSpan()));
            }
            return RedirectToAction("Index","Users");
        }
        public IActionResult UserPage(string Id)
        {
            ViewBag.Id = Id;
            return RedirectToRoutePermanent(new
            {
                controller = "Home",
                action = "PersonalPage",
                id = Id
            });
            //могу добавить только  один элемент(после добавляются не пользователю а админу)
        }
        

    }
}
