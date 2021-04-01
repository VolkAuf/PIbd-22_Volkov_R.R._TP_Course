using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("group")]
    public class Group
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("curator_name")]
        public string CuratorName { get; set; }
        [Column("thread_id")]
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("GroupId")]
        public virtual List<OrderGroup> OrderGroups { get; set; }
    }
}
