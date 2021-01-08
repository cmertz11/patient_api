using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace patient_api.Data.Models
{
    public class Patient
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid Id { get; set; }
 
        public string MedicalRecordNumber { get; set; }

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

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
