using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using siteCollections.Models;

namespace siteCollections.ViewComponents
{
    public class AdminNavViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly UserManager<User> userManager;
        public AdminNavViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (await userManager.IsInRoleAsync(await userManager.FindByNameAsync(User.Identity.Name), "admin"))
            {
                return new HtmlContentViewComponentResult(
                    new HtmlString($"<li class=\"nav-item\">" +
                    $" <a class=\"nav-link text-dark\" href=\"/Users\">List of users</a>" +
                    $"</li>"));
            }
            return new HtmlContentViewComponentResult(new HtmlString(""));
        }

    }
}
