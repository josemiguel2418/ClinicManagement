using ClinicManagement.Backend.API.Results;
using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Backend.API.Controllers
{
    [Route("api/consultorio")]
    [ApiController]
    public class ConsultorioController : ControllerBase
    {
        private readonly IConsultorioService _consultorioService;

        public ConsultorioController(IConsultorioService consultorioService)
        {
            _consultorioService = consultorioService;
        }

        [HttpGet]
        public async Task<IEnumerable<ConsultorioDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var totalRecords = await _consultorioService.CountRecordsAsync();
            HttpContext.Response.Headers.Append("cantidad-total-registros", totalRecords.ToString());
            return await _consultorioService.GetAllAsync(pagination);
        }

        [HttpGet("{id:int}", Name = "GetConsultorio")]
        public async Task<ActionResult<ConsultorioDTO>> Get(int id)
        {
            var result = await _consultorioService.GetByIdAsync(id);
            return result.Match(
                onSuccess: value => Ok(value),
                onFailure: ApiResults.MapProblems
            );
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateConsultorioDTO modelo)
        {
            var result = await _consultorioService.CreateAsync(modelo);
            return result.Match(
                    onSuccess: () => CreatedAtRoute("GetConsultorio", new { id = result.Value.Id }, result.Value),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,UpdateConsultorioDTO modelo)
        {
            var result = await _consultorioService.UpdateAsync(id,modelo);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _consultorioService.DeleteAsync(id);
            return result.Match(
                    onSuccess: () => NoContent(),
                    onFailure: ApiResults.MapProblems
                );
        }
    }
}
