using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public class ItemCollection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public int IntFields { get; set; }
        public int TextFields { get; set; }
        public int StringFields { get; set; }
        public int BoolFields { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
