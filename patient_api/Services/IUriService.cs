
using patient_api.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Services
{
    public interface IUriService
    {
        Uri GetPatientUri(string PatientId);
        Uri GetPatientsUri(PaginationQuery filter);
    }
}
