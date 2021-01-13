using patient_api.Data.dto;
using patient_api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Services
{
    public interface IPatientService
    {
        Task<string> AddPatient(Patient_dto Patient);

        Task<bool> UpdatePatient(Patient_dto Patient);

        Task<bool> DeletePatient(string Id);

        Task<Patient_dto> GetPatient(string Id);
        Task<PatientPagedResponse> GetPatients(PaginationQuery paginationQuery = null);

        Task<bool> SeedPatientData();
    }
}
