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

 
        public Guid PersonId { get; set; }

        public string MedicalRecordNumber { get; set; }


    }
}
