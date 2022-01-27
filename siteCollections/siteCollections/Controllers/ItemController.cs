using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using siteCollections.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
namespace siteCollections.Controllers
{
    public class ItemController:Controller
    {
        private ICollectionRepository CollectionRepo;
        private IItemReposytory ItemRepo;
        private UserManager<User> userManager;
        const int pageSize=8;

        public ItemController(ICollectionRepository CRep, IItemReposytory IRep, UserManager<User> userManager)
        {
            CollectionRepo = CRep;
            ItemRepo = IRep;
            this.userManager = userManager;
        }
        public IActionResult AddItem(int Id)
        {
            string autocompleteTeg = "";
            foreach (var teg in ItemRepo.Tegs)
            {
                autocompleteTeg += teg.Text + " ";
            }
            ViewBag.Tegs = autocompleteTeg;
            return View(CollectionRepo.GetCollection(Id));
        }
        [HttpPost]
        public IActionResult AddItem(Item model)
        {
            
            model.Id = 0;
            ItemRepo.AddItem(model);
            var tegArray = model.Tegs.Split(' ', ',');
            var tegs = new List<Teg>();
            foreach (var teg in tegArray)
            {
                tegs.Add(new Teg { Text = teg ,Id=0});
            }
            ItemRepo.AddTegs(tegs);
            return RedirectToAction("EditCollection", "Collection",new {Id=model.ItemCollectionId });
        }
        public IActionResult ShowItem(int Id) => View(ItemRepo.GetItem(Id));
        

        public IActionResult DeleteItem(int Id)
        {
            var model = ItemRepo.GetItem(Id);
            ItemRepo.DeleteItem(model);
            return RedirectToAction("EditCollection", "Collection",new { Id = model.ItemCollectionId });
        }

        public IActionResult EditItem(int Id)
        {
            return View(ItemRepo.GetItem(Id));
        }
        [HttpPost]
        public IActionResult EditItem(Item model)
        {
            var tegArray = model.Tegs.Split(' ', ',');
            var tegs = new List<Teg>();
            foreach (var teg in tegArray)
            {
                tegs.Add(new Teg { Text = teg, Id=0});
            }
            ItemRepo.AddTegs(tegs);
            
            ItemRepo.UpdateItem(model);
            return RedirectToAction("EditCollection", "Collection", new { Id = model.ItemCollectionId });
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment model)
        {

            if (model.Text != null && model.Text != "")
            {
                model.Id = 0;
                model.UserId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
                ItemRepo.AddComment(model);
            }
            return View("ShowItem", ItemRepo.GetItem(model.ItemId));
        }
    }
}
