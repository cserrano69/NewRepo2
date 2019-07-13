using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PreFinalOp.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        public int MFID { get; set; }
    }
}