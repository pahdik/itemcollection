using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace siteCollections.Models
{
    public class ItemRepository:IItemReposytory
    {
        private DataContext context;
        public ItemRepository(DataContext ctx) => context = ctx;

        public IQueryable<Item> Items => context.Items;
        public IQueryable<Comment> Comments => context.Comments;
        public IQueryable<Teg> Tegs => context.Tegs;
        public void AddItem(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
        }
        public void UpdateItem(Item model)
        {
            var item = GetItem(model.Id);
            item.Name = model.Name;
            item.Tegs = model.Tegs;
            if(model.BoolField!=null)
            foreach (var field in model.BoolField)
            {
                var boolField = context.BoolFields.Find(field.Id);
                boolField.Name = field.Name;
                boolField.Data = field.Data;
                context.BoolFields.Update(boolField);
            }
            
            if(model.IntField!=null)
            foreach (var field in model.IntField)
            {
                var intField = context.IntFields.Find(field.Id);
                intField.Name = field.Name;
                intField.Data = field.Data;
                context.IntFields.Update(intField);
            }
            
            if(model.StringField!=null)
            foreach (var field in model.StringField)
            {
                var stringField = context.StringFields.Find(field.Id);  
                stringField.Name = field.Name;
                stringField.Data = field.Data;
                context.StringFields.Update(stringField);
            }
            
            if(model.TextField!=null)
            foreach (var field in model.TextField)
            {
                var textField = context.TextFields.Find(field.Id);
                textField.Name = field.Name;
                textField.Data = field.Data;
                context.TextFields.Update(textField);
            }
            
            context.Items.Update(item);
            context.SaveChanges();

        }
        public void DeleteItem(Item item)
        {
            context.Items.Remove(item);
            context.SaveChanges();
        }
        public Item GetItem(int key) => context.Items.Include(x => x.StringField).Include(x=>x.IntField).Include(x=>x.TextField).Include(x=>x.BoolField).Include(x=>x.Comment).First(x=>x.Id==key);

        public void AddComment(Comment comm)
        {
            context.Comments.Add(comm);
            context.SaveChanges();
        }
        
        public void AddTegs(IEnumerable<Teg> tegs)
        {
            foreach(var teg in tegs)
            {
                if (!context.Tegs.Any(x=> x.Text==teg.Text))
                {
                    context.Tegs.Add(teg);
                }
            }
            context.SaveChanges();
        }
    }
}
