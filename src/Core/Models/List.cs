#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public partial class List
    {

        public long Id { get; set; }

        [Required] [StringLength(50)]
        public string? Title { get; set; }


        public bool is_deleted { get; set; }

        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }



        public long CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        [InverseProperty("Lists")]
        public virtual Category Category { get; set; }



        [InverseProperty("List")]
        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
