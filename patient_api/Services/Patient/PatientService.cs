﻿using AutoMapper;
using Bogus;
using Bogus.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using patient_api.Data;
using patient_api.Data.dto;
using patient_api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;
        private readonly IUriService _uriService;

        public PatientService(PatientContext context, IMapper mapper, ILogger<PatientService> logger, IUriService uriService)
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
        public async Task<PatientPagedResponse> GetPatients(PaginationQuery paging)
        {
            try
            {
                List<Patient> patients = new List<Patient>();
                PatientPagedResponse patientResponse = new PatientPagedResponse();
                patientResponse.TotalRecords = _context.Patients.Count();
                patientResponse.TotalPages = (int)Math.Ceiling((decimal)patientResponse.TotalRecords / (decimal)paging.PageSize);
                patientResponse.PageNumber = paging.PageNumber >= 1 ? paging.PageNumber : (int?)null;
                patientResponse.PageSize = paging.PageSize >= 1 ? paging.PageSize : (int?)null;
                
                if (patientResponse.TotalRecords > 0)
                {
                    var skip = (paging.PageNumber - 1) * paging.PageSize;
                    patients = await _context.Patients.Skip(skip).Take(paging.PageSize).ToListAsync();

                    patientResponse.Data = _mapper.Map<List<Patient_dto>>(patients);
                    patientResponse.PreviousPage = paging.PageNumber - 1 >= 1 ? _uriService.GetPatientsPagedUri(new PaginationQuery(paging.PageNumber - 1, paging.PageSize)).ToString() : null;
                    if (patientResponse.PageNumber < patientResponse.TotalPages)
                    {
                        patientResponse.NextPage = paging.PageNumber >= 1 ? _uriService.GetPatientsPagedUri(new PaginationQuery(paging.PageNumber + 1, paging.PageSize)).ToString() : null;
                    }
                }
                return patientResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<string> AddPatientAddress(Address_dto address)
        {
            try
            {
                address.Id = null;
                Address newPatientAddress = _mapper.Map<Address>(address);
                newPatientAddress.LastUpdate = DateTime.Now;
                _context.PatientAddresss.Add(newPatientAddress);
                await _context.SaveChangesAsync();
                return _uriService.GetPatientAddressUri(newPatientAddress.Id.ToString()).ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdatePatientAddress(Address_dto address_dto)
        {
            try
            {
                var Address = await _context.PatientAddresss.FindAsync(Guid.Parse(address_dto.Id));
                Address = _mapper.Map<Address>(address_dto);
                Address.LastUpdate = DateTime.Now;
                var dbresponse = await _context.SaveChangesAsync();
                return dbresponse == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletePatientAddress(string Id)
        {
            try
            {
                var Address = await _context.PatientAddresss.FindAsync(Guid.Parse(Id));
                _context.PatientAddresss.Remove(Address);
                var result = _context.SaveChanges();
                return result == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Address_dto> GetPatientAddress(string Id)
        {
            try
            {
                var Address = await _context.PatientAddresss.FindAsync(Guid.Parse(Id));
                if (Address != null)
                {
                    return _mapper.Map<Address_dto>(Address);
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

        public async Task<AddressPagedResponse> GetPatientAddresses(string PatientId, PaginationQuery paging = null)
        {
            try
            {
                List<Address> addressList = new List<Address>();
                AddressPagedResponse addressResponse = new AddressPagedResponse();
                addressResponse.TotalRecords = _context.Patients.Count();
                addressResponse.TotalPages = (int)Math.Ceiling((decimal)addressResponse.TotalRecords / (decimal)paging.PageSize);
                addressResponse.PageNumber = paging.PageNumber >= 1 ? paging.PageNumber : (int?)null;
                addressResponse.PageSize = paging.PageSize >= 1 ? paging.PageSize : (int?)null;

                if (addressResponse.TotalRecords > 0)
                {
                    var skip = (paging.PageNumber - 1) * paging.PageSize;
                    addressList = await _context.PatientAddresss.Skip(skip).Take(paging.PageSize).ToListAsync();

                    addressResponse.Data = _mapper.Map<List<Address_dto>>(addressList);
                    addressResponse.PreviousPage = paging.PageNumber - 1 >= 1 ? _uriService.GetPatientAddressPagedUri(PatientId,  new PaginationQuery(paging.PageNumber - 1, paging.PageSize)).ToString() : null;
                    if (addressResponse.PageNumber < addressResponse.TotalPages)
                    {
                        addressResponse.NextPage = paging.PageNumber >= 1 ? _uriService.GetPatientAddressPagedUri(PatientId, new PaginationQuery(paging.PageNumber + 1, paging.PageSize)).ToString() : null;
                    }
                }
                return addressResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    
        public async Task<bool> SeedPatientData()
        {
            Random rnd = new Random();
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

                  var testPatientRecords = testPatientData.GenerateBetween(1000, 1000);

                  foreach (var testPatient in testPatientRecords)
                  {
                    
                    
                    testPatient.LastUpdate = DateTime.Now;
                    _context.Patients.Add(testPatient);

                    var testAddressData = new Faker<Address>()
                        .RuleFor(r => r.Street1, r => r.Address.StreetAddress())
                        .RuleFor(r => r.Street2, r => r.Address.SecondaryAddress())
                        .RuleFor(r => r.City, r => r.Address.City())
                        .RuleFor(r => r.State, r => r.Address.State())
                        .RuleFor(r => r.Zip, r => r.Address.ZipCode());

                    var testAddressRecords = testAddressData.GenerateBetween(1, 5);
                    foreach (var address in testAddressRecords)
                    {
                        address.PatientId = testPatient.Id;
                        address.LastUpdate = DateTime.Now;
                        _context.PatientAddresss.Add(address);
                    }
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
