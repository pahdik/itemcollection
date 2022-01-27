using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public class ItemItemCollection
    {
        public IEnumerable<Item> items;
        public IEnumerable<ItemCollection> collections;
    }
}
