using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace siteCollections.Models
{
    public class BoolField
    {

        public int ItemId { get; set; }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Data { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
}
}
