using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/medico")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;
        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }
        [HttpGet]
        public async Task<IEnumerable<MedicoDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _medicoService.CountRecordsAsync();
            HttpContext.Response.Headers.Append("cantidad-total-registros", totalRecords.ToString());
            return await _medicoService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetMedico")]
        public async Task<ActionResult<MedicoDTO>> Get(int id)
        {
            var result = await _medicoService.GetByIdAsync(id);
            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems
            );
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateMedicoDTO modelo)
        {
            var result = await _medicoService.CreateAsync(modelo);
            return result.Match(
                    onSuccess: () => CreatedAtRoute("GetMedico", new { id = result.Value.Id }, result.Value),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateMedicoDTO modelo)
        {
            var result = await _medicoService.UpdateAsync(id, modelo);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _medicoService.DeleteAsync(id);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
    }
}
