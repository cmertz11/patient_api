using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Data.dto
{
    public class Patient_dto
    {
        public string Id { get; set; }

        public string MedicalRecordNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MI { get; set; }
        public DateTime? DOB { get; set; }

        public string email { get; set; }

        public int Sex { get; set; }

        public int Race { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
