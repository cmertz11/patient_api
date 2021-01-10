using AutoMapper;
using Bogus;
using Bogus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using patient_api.Data;
using patient_api.Data.dto;
using patient_api.Data.Models;
using patient_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientRepository> _logger;
        private readonly IUriService _uriService;

        public PatientRepository(PatientContext context, IMapper mapper, ILogger<PatientRepository> logger, IUriService uriService)
        {
            _context = context; _mapper = mapper; _logger = logger; _uriService = uriService;
        }

        public async Task<string> AddPatient(Patient_dto Patient_dto)
        {
            try
            {
                Patient_dto.Id = null;
                Patient newPatient = _mapper.Map<Patient>(Patient_dto);
                _context.Patients.Add(newPatient);
                await _context.SaveChangesAsync();
                return _uriService.GetPatientUri(newPatient.Id.ToString()).ToString();
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
        public async Task<PagedResponse<Patient_dto>> GetPatients(PaginationQuery paging = null)
        {
            try
            {
                if (paging == null) 
                    return new PagedResponse<Patient_dto>(_mapper.Map<List<Patient_dto>>(await _context.Patients.ToListAsync()));
                

                var skip = (paging.PageNumber - 1) * paging.PageSize;
                var patientRespone = new PagedResponse<Patient_dto>(_mapper.Map<List<Patient_dto>>(await _context.Patients.Skip(skip).Take(paging.PageSize).ToListAsync()));

                patientRespone.PageNumber = paging.PageNumber >= 1 ? paging.PageNumber : (int?)null;
                patientRespone.PageSize = paging.PageSize >= 1 ? paging.PageSize : (int?)null;
                patientRespone.NextPage = paging.PageNumber >= 1 ? _uriService.GetPatientsUri(new PaginationQuery(paging.PageNumber + 1, paging.PageSize)).ToString() : null;
                patientRespone.PreviousPage = paging.PageNumber - 1 >= 1 ? _uriService.GetPatientsUri(new PaginationQuery(paging.PageNumber - 1, paging.PageSize)).ToString() : null;

                return patientRespone;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> SeedPatientData()
        {
            try
            {
                 var testPatientData = new Faker<Patient>() 
                    .RuleFor(c => c.MedicalRecordNumber, f => f.Finance.RoutingNumber().ToString())
                    .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                    .RuleFor(c => c.LastName, f => f.Name.LastName())
                    .RuleFor(c => c.MI, f => f.Name.FindName().Substring(0, 1))
                    .RuleFor(c => c.DOB, f => f.Person.DateOfBirth)
                    .RuleFor(c => c.email, f => f.Person.Email)
                    .RuleFor(c => c.Sex, f => f.Random.Number(1, 2))
                    .RuleFor(c => c.Race, f => f.Random.Number(1, 6));

                  var testRecords = testPatientData.GenerateBetween(1000, 1000);

                  foreach (var item in testRecords)
                  {
                    item.LastUpdate = DateTime.Now;
                    _context.Patients.Add(item);
                  }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

    }
}
