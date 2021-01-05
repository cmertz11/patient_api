using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using patient_api.Data;
using patient_api.Data.dto;
using patient_api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private PatientContext _context;
        public PatientRepository(PatientContext context)
        {
            _context = context;
        }

        public async Task<string> AddPatient(Patient_dto Patient_dto)
        {
            try
            {
                Patient Patient = new Patient
                {         
                    PersonId = Guid.Parse(Patient_dto.PersonId),
                    MedicalRecordNumber = Patient_dto.MedicalRecordNumber
                };

                _context.Patients.Add(Patient);
                await _context.SaveChangesAsync();
                return Patient.Id.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> UpdatePatient(Patient_dto Patient_dto)
        {
            try
            {
                var Patient = await _context.Patients.FindAsync(Guid.Parse(Patient_dto.Id));

                Patient.PersonId = Guid.Parse(Patient_dto.PersonId);
                Patient.MedicalRecordNumber = Patient_dto.MedicalRecordNumber;

                var dbresponse = await _context.SaveChangesAsync();
                return dbresponse == 1;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<Patient_dto> GetPatient(string Id)
        {
            try
            {
                var Patient = await _context.Patients.FindAsync(Guid.Parse(Id));
                if (Patient != null)
                {
                    return new Patient_dto
                    {
                        Id = Patient.Id.ToString(),
                        PersonId = Patient.PersonId.ToString(),
                        MedicalRecordNumber = Patient.MedicalRecordNumber
                    };
                } 
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }
        public async Task<bool> DeletePatient(string Id)
        {
            try
            {
                var Patient = await _context.Patients.FindAsync(Guid.Parse(Id));
                _context.Patients.Remove(Patient);
                var result = _context.SaveChanges();
                return result == 1;
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
        }

    }
}
