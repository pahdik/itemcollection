using siteCollections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsBlock { get; set; }
    }
}
