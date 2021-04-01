using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("order_group")]
    public class OrderGroup
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Group Group { get; set; }
    }
}
