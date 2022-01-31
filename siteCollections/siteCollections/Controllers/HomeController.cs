using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using siteCollections.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Controllers
{
    public class HomeController : Controller
    {
        private ICollectionRepository CollectionRepo;
        private IItemReposytory ItemRepo;
        private UserManager<User> userManager;

        public HomeController(ICollectionRepository CRep, IItemReposytory IRep, UserManager<User> userManager)
        {
            CollectionRepo = CRep;
            ItemRepo = IRep;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            ItemItemCollection iit = new ItemItemCollection();
            iit.items = ItemRepo.Items.AsEnumerable().TakeLast(7).Reverse();
            iit.collections = CollectionRepo.ItemCollections.OrderByDescending(x=>x.Items.Count);
            ViewBag.Tegs = ItemRepo.Tegs;

            return View(iit);
        }
        
        
        public async Task<IActionResult> PersonalPage(string Id, int model=2,string TopicFilter="" , string NameFilter="")
        {
            if (Id == null)
            {
                Id = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
            }
            ViewBag.Id = Id;
            var collections = CollectionRepo.GetUserCollections(Id);
            if (TopicFilter != "" && TopicFilter != null)
            {
                collections = collections.Where(x => x.Topic.Contains(TopicFilter));
            }
            if (NameFilter != "" && NameFilter != null)
            {
                collections = collections.Where(x => x.Name.Contains(NameFilter));
            }
            switch (model)
            {
                case 1:return View(collections.OrderBy(x => x.Name));
    
                case 3:return View(collections.OrderByDescending(x => x.Name));
                case 4:return View(collections.Reverse().ToArray());
                default:
                    break;
            }
            return View(collections);
        }
        public IActionResult Search(string SearchLine)
        {
            ItemItemCollection model = new ItemItemCollection();
            model.collections = CollectionRepo.ItemCollections.Where(x => x.Description.Contains(SearchLine) || x.Name.Contains(SearchLine));
            model.items = ItemRepo.Items.Where(x => x.Name.Contains(SearchLine));

            return View("Search", model);
        }
        public IActionResult SearchByTeg(string teg)
        {
            var model = ItemRepo.Items.Where(x => x.Tegs.Contains(teg)).ToList();
            return View("Search", new ItemItemCollection() { items = model, collections=new List<ItemCollection>() }); ;
        }

    }
}