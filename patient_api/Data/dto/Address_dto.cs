using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Data.dto
{
    public class Address_dto
    {
        public string Id { get; set; }
        public bool CurrentAddress { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; } 
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
        public String PatientId { get; set; }
    }
}
