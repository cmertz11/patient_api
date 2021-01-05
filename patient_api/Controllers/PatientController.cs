using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using patient_api.Data.dto;
using patient_api.Data.Models;
using patient_api.Repositories;
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

        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
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
                //log
                return StatusCode(500);
            }
        }

        [HttpPut]
        [ActionName("UpdatePatient")]
        public async Task<ActionResult<String>> UpdatePatient(Patient_dto Patient)
        {
            try
            {
                await _repository.UpdatePatient(Patient);
                return Ok();

            }
            catch (Exception ex)
            {
                //log
                return StatusCode(500);
            }
        }

        [HttpGet]
        [ActionName("GetPatient")]
        public async Task<ActionResult<String>> GetPatient(string Id)
        {
            try
            {
                var Patient = await _repository.GetPatient(Id);
                return Ok(Patient);

            }
            catch (Exception ex)
            {
                //log
                return StatusCode(500);
            }
        }


        [HttpDelete]
        [ActionName("DeletePatient")]
        public async Task<ActionResult<String>> DeletePatient(string Id)
        {
            try
            {
                await _repository.DeletePatient(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(500);
            }
        }
    }
}
