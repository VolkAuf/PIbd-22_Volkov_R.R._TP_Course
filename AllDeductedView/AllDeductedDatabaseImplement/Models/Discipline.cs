using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("discipline")]
    public class Discipline
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("hours_count")]
        [Required]
        public int HoursCount { get; set; }
        [Column("thread_id")]
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
