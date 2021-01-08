using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(PatientContext context, IMapper mapper, ILogger<PatientRepository> logger)
        {
            _context = context; _mapper = mapper; _logger = logger;
        }

        public async Task<string> AddPatient(Patient_dto Patient_dto)
        {
            try
            {
                Patient_dto.Id = null;
                Patient newPatient = _mapper.Map<Patient>(Patient_dto);
                _context.Patients.Add(newPatient);
                await _context.SaveChangesAsync();
                return newPatient.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdatePatient(Patient_dto Patient_dto)
        {
            try
            {
                var Patient = await _context.Patients.FindAsync(Guid.Parse(Patient_dto.Id));

                Patient = _mapper.Map<Patient>(Patient_dto);
                var dbresponse = await _context.SaveChangesAsync();
                return dbresponse == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                    return _mapper.Map<Patient_dto>(Patient);
                } 
                else
                { 
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PagedResponse<Patient_dto>> GetPatients(PaginationQuery paginationQuery = null)
        {
            try
            {
                if (paginationQuery == null) 
                    return new PagedResponse<Patient_dto>(_mapper.Map<List<Patient_dto>>(await _context.Patients.ToListAsync()));
                

                var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;
                return new PagedResponse<Patient_dto>(_mapper.Map<List<Patient_dto>>(await _context.Patients.Skip(skip).Take(paginationQuery.PageSize).ToListAsync()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
