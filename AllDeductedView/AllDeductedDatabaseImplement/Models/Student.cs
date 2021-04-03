using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("student")]
    public class Student
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        public string LastName { get; set; }
        [Required]
        [Column("patronymic")]
        public string Patronymic { get; set; }
        [Column("thread_id")]
        public int? ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        [Column("provider_id")]
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual StudyingStatus StudyingStatus { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<OrderStudent> OrderStudents { get; set; }
    }
}
