using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/horariomedico")]
    [ApiController]
    public class HorarioMedicoController : ControllerBase
    {
        private readonly IHorarioMedicoService _horarioMedicoService;
        public HorarioMedicoController(IHorarioMedicoService horarioMedicoService)
        {
            _horarioMedicoService = horarioMedicoService;
        }
        [HttpGet]
        public async Task<IEnumerable<HorarioMedicoDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _horarioMedicoService.CountRecordsAsync();
            HttpContext.Response.Headers.Append("cantidad-total-registros", totalRecords.ToString());
            return await _horarioMedicoService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetHorarioMedico")]
        public async Task<ActionResult<HorarioMedicoDTO>> Get(int id)
        {
            var result = await _horarioMedicoService.GetByIdAsync(id);
            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems
            );
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateHorarioMedicoDTO modelo)
        {
            var result = await _horarioMedicoService.CreateAsync(modelo);
            return result.Match(
                    onSuccess: () => CreatedAtRoute("GetHorarioMedico", new { id = result.Value.Id }, result.Value),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateHorarioMedicoDTO modelo)
        {
            var result = await _horarioMedicoService.UpdateAsync(id, modelo);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _horarioMedicoService.DeleteAsync(id);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
    }
}
