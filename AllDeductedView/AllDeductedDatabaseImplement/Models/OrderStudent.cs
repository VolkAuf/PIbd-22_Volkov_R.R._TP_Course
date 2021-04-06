using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("order_student")]
    public class OrderStudent
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Student Student { get; set; }
    }
}
