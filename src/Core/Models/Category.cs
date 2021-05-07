#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public partial class Category
    {

        public long Id { get; set; }


        [StringLength(50)]
        public string? Color { get; set; }

        [Required] [StringLength(50)]
        public string? Title { get; set; }


        public bool is_deleted { get; set; }

        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }



        //public int? OwnerId { get; set; }


        /*[ForeignKey("OwnerId")]
        [InverseProperty("Categories")]
        public virtual Owner? Owner { get; set; }*/
    }
}
