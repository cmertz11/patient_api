using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using patient_api.Data.dto;
using patient_api.Services;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace patient_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _pateintService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        public PatientController(IPatientService repository, ILogger<PatientController> logger, IWebHostEnvironment env)
        {
            _pateintService = repository; _logger = logger; _env = env;
        }

        [HttpPost]
        [ActionName("AddPatient")]
        public async Task<ActionResult<String>> AddPatient(Patient_dto Patient)
        {
            try
            {
                var Id = await _pateintService.AddPatient(Patient);
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
                var success = await _pateintService.UpdatePatient(Patient);
               
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
        public async Task<ActionResult<Patient_dto>> GetPatient(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return StatusCode(442);
            try
            {
                var Patient = await _pateintService.GetPatient(Id);
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
        public async Task<ActionResult<PatientPagedResponse>> GetPatients([FromQuery]PaginationQuery paging)
        { 
            try
            {
                var patients = await _pateintService.GetPatients(paging);

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
                var success = await _pateintService.DeletePatient(Id);

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
                await _pateintService.SeedPatientData();

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
