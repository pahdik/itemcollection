using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public interface IItemReposytory
    {
        IQueryable<Item> Items { get; }
        IQueryable<Comment> Comments { get; }
        void AddItem(Item item);
        void UpdateItem(Item model);
        void DeleteItem(Item item);
        void AddComment(Comment comm);
        Item GetItem(int key);
        IQueryable<Teg> Tegs { get; }
        public void AddTegs(IEnumerable<Teg> tegs);
    }
}
