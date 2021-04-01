using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("order")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("date_create")]
        public DateTime DateCreate { get; set; }

        [Column("provider_id")]
        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderGroup> OrderGroups { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderStudent> OrderStudents { get; set; }
    }
}
