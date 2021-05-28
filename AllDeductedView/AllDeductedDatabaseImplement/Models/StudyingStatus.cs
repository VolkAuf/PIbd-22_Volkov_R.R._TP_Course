using AllDeductedBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("studying_status")]
    public class StudyingStatus
    {
        [Column("id")]
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        [Column("student_id")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        
        [Column("enrollment_date")]
        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Column("studying_form", TypeName = "varchar(24)")]
        [Required]
        public StudyingForm StudyingForm { get; set; }

        [Column("studying_base", TypeName = "varchar(24)")]
        [Required]
        public StudyingBase StudyingBase { get; set; }

        [Column("course")]
        [Required]
        public int Course { get; set; }

        [Column("provider_id")]
        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
