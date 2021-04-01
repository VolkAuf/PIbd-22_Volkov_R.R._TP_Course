using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllDeductedDatabaseImplement.Models
{
    [Table("user")]
    public class User
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
        [Column("login")]
        public string Login { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Column("mail")]
        public string Mail { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
