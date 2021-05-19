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
        [Column("date_create")]
        [Required]
        public DateTime DateCreate { get; set; }
        [Column("studying_form")]
        [Required]
        public StudyingForm StudyingForm { get; set; }
        [Column("studying_base")]
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
