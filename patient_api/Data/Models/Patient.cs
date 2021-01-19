using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace patient_api.Data.Models
{
    public partial class Patient
    {        

        [Required]
        [StringLength(100, ErrorMessage = "First Name too long (100 character limit).")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First Name too long (100 character limit).")]
        public string LastName { get; set; }

        [MaxLength(1)]
        public string MI { get; set; }
        public DateTime? DOB { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        public int Sex { get; set; }

        public int Race { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();
    }

}
