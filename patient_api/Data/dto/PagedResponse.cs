﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Data.dto
{
    public class PatientPagedResponse
    {
        public PatientPagedResponse() { }
 
        public IEnumerable<Patient_dto> Data { get; set; }

        public int? TotalRecords { get; set; } = 0;

        public int? TotalPages { get; set; } = 0;

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

    }
}
