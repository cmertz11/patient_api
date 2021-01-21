using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using patient_api.Data.dto;
using patient_api.Services;
using System;
using System.Threading.Tasks;

namespace patient_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AddressController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        public AddressController(IPatientService patientService, ILogger<AddressController> logger, IWebHostEnvironment env)
        {
            _patientService = patientService; _logger = logger; _env = env;
        }

        [HttpPost]
        [ActionName("AddPatientAddress")]
        public async Task<ActionResult<String>> AddPatientAddress(Address_dto address)
        {
            try
            {
                var Id = await _patientService.AddPatientAddress(address);
                return Ok(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut]
        [ActionName("UpdatePatientAddress")]
        public async Task<ActionResult<String>> UpdatePatientAddress(Address_dto address)
        {
            try
            {
                var success = await _patientService.UpdatePatientAddress(address);
               
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
        [ActionName("GetPatientAddress")]
        public async Task<ActionResult<Address_dto>> GetPatientAddress(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return StatusCode(442);
            try
            {
                var address = await _patientService.GetPatientAddress(Id);
                if(address == null)
                {
                    return NotFound();
                }
                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [ActionName("GetPatientAddresses")]
        public async Task<ActionResult<AddressPagedResponse>> GetPatientAddresses(string PatientId, [FromQuery]PaginationQuery paging)
        { 
            try
            {
                var addresses = await _patientService.GetPatientAddresses(PatientId, paging);

                return addresses != null ? Ok(addresses) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [ActionName("DeletePatientAddress")]
        public async Task<ActionResult<String>> DeletePatientAddress(string Id)
        {
            try
            {
                var success = await _patientService.DeletePatientAddress(Id);

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
                await _patientService.SeedPatientData();

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
