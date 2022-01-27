using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace siteCollections.Models
{
    public class User: IdentityUser
    {
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ItemCollection> ItemCollection { get; set; }
    }
}