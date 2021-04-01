using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("customer")]
    public class Customer
    {
        [ForeignKey("UserId")]
        [Column("user_id")]
        [Key]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Group> Groups { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Thread> Threads { get; set; }

        [ForeignKey("CustomerId")]
        public virtual List<Discipline> Disciplines { get; set; }
    }
}
