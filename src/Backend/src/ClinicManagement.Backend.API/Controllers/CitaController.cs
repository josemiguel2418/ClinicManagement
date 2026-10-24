using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/cita")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<IEnumerable<CitaDTO>> Get(
            [FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _citaService.CountRecordsAsync();

            HttpContext.Response.Headers.Append(
                "cantidad-total-registros",
                totalRecords.ToString());

            return await _citaService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetCita")]
        public async Task<ActionResult<CitaDTO>> Get(int id)
        {
            var result = await _citaService.GetByIdAsync(id);

            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateCitaDTO modelo)
        {
            var result = await _citaService.CreateAsync(modelo);

            return result.Match(
                onSuccess: () => CreatedAtRoute(
                    "GetCita",
                    new { id = result.Value.Id },
                    result.Value),
                onFailure: ApiResults.MapProblems);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,UpdateCitaDTO modelo)
        {
            var result = await _citaService.UpdateAsync(id, modelo);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: ApiResults.MapProblems);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _citaService.DeleteAsync(id);

            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: ApiResults.MapProblems);
        }
    }
}
