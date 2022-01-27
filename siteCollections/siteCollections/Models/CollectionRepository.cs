using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public class CollectionRepository:ICollectionRepository
    {
        private DataContext context;
        public CollectionRepository(DataContext ctx) => context = ctx;
        public IQueryable<ItemCollection> ItemCollections => context.ItemСollections;
        public void AddCollection(ItemCollection collection)
        {
            context.Add(collection);
            context.SaveChanges();
        }
        public void UpdateCollection(ItemCollection collection)
        {
            context.ItemСollections.Update(collection);
            context.SaveChanges();
        }
        public void DeleteCollection(ItemCollection collection)
        {
            context.ItemСollections.Remove(collection);
            context.SaveChanges();
        }
        public IQueryable<ItemCollection> GetUserCollections(string userId) => context.ItemСollections.Where(x => x.UserId == userId);

        public ItemCollection GetCollection(int key) => context.ItemСollections.Include(x => x.Items).First(x => x.Id == key);
    }
}
