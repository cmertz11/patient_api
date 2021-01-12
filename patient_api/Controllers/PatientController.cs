using Bogus;
using Bogus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using patient_api.Data;
using patient_api.Data.dto;
using patient_api.Data.Models;
using patient_api.Repositories;
using patient_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger _logger;
        public PatientController(IPatientRepository repository, ILogger<PatientController> logger)
        {
            _repository = repository; _logger = logger;
        }

        [HttpPost]
        [ActionName("AddPatient")]
        public async Task<ActionResult<String>> AddPatient(Patient_dto Patient)
        {
            try
            {
                var Id = await _repository.AddPatient(Patient);
                return Ok(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut]
        [ActionName("UpdatePatient")]
        public async Task<ActionResult<String>> UpdatePatient(Patient_dto Patient)
        {
            try
            {
                var success = await _repository.UpdatePatient(Patient);
               
                if(success) return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [ActionName("GetPatient")]
        public async Task<ActionResult<String>> GetPatient(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return StatusCode(442);
            try
            {
                var Patient = await _repository.GetPatient(Id);
                if(Patient == null)
                {
                    return NotFound();
                }
                return Ok(Patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [ActionName("GetPatients")]
        public async Task<IActionResult> GetPatients([FromQuery]PaginationQuery paging)
        { 
            try
            {
                var patients = await _repository.GetPatients(paging);

                return patients != null ? Ok(patients) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [ActionName("DeletePatient")]
        public async Task<ActionResult<String>> DeletePatient(string Id)
        {
            try
            {
                var success = await _repository.DeletePatient(Id);

                if (success)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        } 
        
        [HttpGet]
        [ActionName("SeedTestPatients")]
        public async Task<IActionResult> SeedTestPatients()
        {
            try
            {
                await _repository.SeedPatientData();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }


    }
}
