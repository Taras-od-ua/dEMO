#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public partial class Item
    {
        public long Id { get; set; }

        [Required] [StringLength(50)]
        public string? Title { get; set; }

        public bool Done { get; set; }
        public bool is_deleted { get; set; }

        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public long ListId { get; set; }


        [ForeignKey("ListId")]
        [InverseProperty("Items")]
        public virtual List List { get; set; }
    }
}
