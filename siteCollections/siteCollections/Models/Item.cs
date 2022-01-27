using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public class Item
    {
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tegs { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public int ItemCollectionId { get; set; }

        [ForeignKey("ItemCollectionId")]
        public virtual ItemCollection ItemCollection { get; set; }
        public virtual ICollection<BoolField> BoolField { get; set; }
        public virtual ICollection<IntField> IntField { get; set; }
        public virtual ICollection<StringField> StringField { get; set; }
        public virtual ICollection<TextField> TextField { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}