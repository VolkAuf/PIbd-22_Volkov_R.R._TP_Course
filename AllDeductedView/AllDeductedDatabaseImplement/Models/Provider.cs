using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("provider")]
    public class Provider
    {
        [ForeignKey("UserId")]
        [Column("user_id")]
        [Key]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<Student> Students { get; set; }

        [ForeignKey("ProviderId")]
        public virtual List<StudyingStatus> StudentStatuses { get; set; }
    }
}
