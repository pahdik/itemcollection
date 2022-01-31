using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace siteCollections.Models
{
    public class DataContext:IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCollection> ItemСollections { get; set; }
        public DbSet<BoolField> BoolFields { get; set; }
        public DbSet<IntField> IntFields { get; set; }
        public DbSet<StringField> StringFields { get; set; }
        public DbSet<TextField> TextFields { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Teg> Tegs { get; set; }
    }
}
