using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public interface ICollectionRepository
    {
        IQueryable<ItemCollection> ItemCollections { get; }
        void AddCollection(ItemCollection collection);
        void UpdateCollection(ItemCollection collection);
        void DeleteCollection(ItemCollection collection);
        IQueryable<ItemCollection> GetUserCollections(string Id);
        ItemCollection GetCollection(int key);
    }
}
