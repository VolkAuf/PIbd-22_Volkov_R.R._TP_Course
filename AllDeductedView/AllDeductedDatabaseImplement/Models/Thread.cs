using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("thread")]
    public class Thread
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("faculty")]
        [Required]
        public string Faculty { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("ThreadId")]
        public virtual List<Discipline> Disciplines { get; set; }

        [ForeignKey("ThreadId")]
        public virtual List<Group> Groups { get; set; }

        [ForeignKey("ThreadId")]
        public virtual List<Student> Students { get; set; }
    }
}
