using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using siteCollections.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace siteCollections.Controllers
{
    public class CollectionController : Controller
    {
        private ICollectionRepository CollectionRepo;
        private IItemReposytory ItemRepo;
        private UserManager<User> userManager;

        public CollectionController(ICollectionRepository CRep, IItemReposytory IRep, UserManager<User> userManager)
        {
            CollectionRepo = CRep;
            ItemRepo = IRep;
            this.userManager = userManager;
        }
        public IActionResult AddCollection(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCollection(ItemCollection model)
        {

            if (model.UserId == null)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                model.UserId = user.Id;
            }
            CollectionRepo.AddCollection(model);
            return RedirectToRoutePermanent(new
            {
                controller = "Home",
                action = "PersonalPage",
                Id = model.UserId
            });

        }
        public IActionResult DeleteCollection(int id, string userId)
        {
            var model = CollectionRepo.GetCollection(id);
            CollectionRepo.DeleteCollection(model);
            return RedirectToAction("PersonalPage", "Home", new { id = userId });
        }
        public IActionResult ShowCollection(int Id) => View(CollectionRepo.GetCollection(Id));
        public IActionResult EditCollection(int Id) => View(CollectionRepo.GetCollection(Id));
        [HttpPost]
        public IActionResult EditCollection(ItemCollection model)
        {
            if (ModelState.IsValid)
            {
                CollectionRepo.UpdateCollection(model);

            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Filters(int id, int typeSort = 2, string TegFilter = "", string NameFilter = "")
        {
            List<Item> i;
            var model = CollectionRepo.GetCollection(id);
            if (TegFilter != null && TegFilter != "")
            {
                i= model.Items.AsEnumerable().Where(x => x.Tegs.Contains(TegFilter)).ToList();
                model.Items = (ICollection<Item>)i;
            }
            if (NameFilter != null && NameFilter != "")
            {
                i = model.Items.Where(x => x.Tegs.Contains(NameFilter)).ToList();
                model.Items = (ICollection<Item>)i;
            }
            
             switch (typeSort)
            {
                case 1: 
                    i = model.Items.AsEnumerable().OrderBy(x => x.Name).ToList();
                    model.Items = (ICollection<Item>)i;
                       break;
    
                case 3: 
                    i = model.Items.AsEnumerable().OrderByDescending(x => x.Name).ToList();
                    model.Items = (ICollection<Item>)i;
                    break;
                case 4:
                    i = model.Items.AsEnumerable().Reverse().ToList();
                    model.Items = (ICollection<Item>)i;
                    break;
                default:
                    break;
            }
            return View("EditCollection", model);

        }
    }
}
