using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/paciente")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }
        [HttpGet]
        public async Task<IEnumerable<PacienteDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _pacienteService.CountRecordsAsync();
            HttpContext.Response.Headers.Append("cantidad-total-registros", totalRecords.ToString());
            return await _pacienteService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetPaciente")]
        public async Task<ActionResult<PacienteDTO>> Get(int id)
        {
            var result = await _pacienteService.GetByIdAsync(id);
            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems
            );
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreatePacienteDTO modelo)
        {
            var result = await _pacienteService.CreateAsync(modelo);
            return result.Match(
                    onSuccess: () => CreatedAtRoute("GetPaciente", new { id = result.Value.Id }, result.Value),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdatePacienteDTO modelo)
        {
            var result = await _pacienteService.UpdateAsync(id, modelo);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _pacienteService.DeleteAsync(id);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
    }
}
