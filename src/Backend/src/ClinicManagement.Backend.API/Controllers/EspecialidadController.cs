using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/especialidad")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public async Task<IEnumerable<EspecialidadDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _especialidadService.CountRecordsAsync();
            HttpContext.Response.Headers.Append("cantidad-total-registros", totalRecords.ToString());
            return await _especialidadService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetEspecialidad")]
        public async Task<ActionResult<EspecialidadDTO>> Get(int id)
        {
            var result = await _especialidadService.GetByIdAsync(id);
            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems
            );
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateEspecialidadDTO modelo)
        {
            var result = await _especialidadService.CreateAsync(modelo);
            return result.Match(
                    onSuccess: () => CreatedAtRoute("GetEspecialidad", new { id = result.Value.Id }, result.Value),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UpdateEspecialidadDTO modelo)
        {
            var result = await _especialidadService.UpdateAsync(id, modelo);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _especialidadService.DeleteAsync(id);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
    }
}
