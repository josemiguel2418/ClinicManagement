using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class HorarioMedicoService : IHorarioMedicoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HorarioMedicoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.HorarioMedicoRepository.CountRecordsAsync();
        }

        public async Task<List<HorarioMedicoDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var horariosMedicosDB = await _unitOfWork.HorarioMedicoRepository.GetAllAsync(pagination.Pagina, pagination.RecordsPorPagina);
            var horariosDTO = _mapper.Map<List<HorarioMedicoDTO>>(horariosMedicosDB);
            return horariosDTO;
        }

        public async Task<Result<HorarioMedicoDTO>> GetByIdAsync(int id)
        {
            var horarioMedicoDB = await _unitOfWork.HorarioMedicoRepository.GetByIdAsync(id);
            if (horarioMedicoDB is null)
            {
                return Result<HorarioMedicoDTO>.Failure(HorarioMedicoErrors.NotFound(id));
            }
            var dto = _mapper.Map<HorarioMedicoDTO>(horarioMedicoDB);
            return Result<HorarioMedicoDTO>.Success(dto);
        }

        public async Task<Result<HorarioMedicoDTO>> CreateAsync(CreateHorarioMedicoDTO modeloDTO)
        {
            if (modeloDTO.HoraInicio >= modeloDTO.HoraFin)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.InvalidSchedule);
            }

            var medicoDB = await _unitOfWork.MedicoRepository.GetByIdAsync(modeloDTO.MedicoId);

            if (medicoDB is null)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.MedicoNotFound(modeloDTO.MedicoId));
            }

            if (!medicoDB.Activo)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.MedicoInactive);
            }

            var consultorioDB = await _unitOfWork.ConsultorioRepository.GetByIdAsync(modeloDTO.ConsultorioId);

            if (consultorioDB is null)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.ConsultorioNotFound(
                        modeloDTO.ConsultorioId));
            }

            if (!consultorioDB.Activo)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.ConsultorioInactive);
            }

            var hasConflict = await _unitOfWork.HorarioMedicoRepository.HasConflictAsync(
                modeloDTO.MedicoId,
                modeloDTO.ConsultorioId,
                modeloDTO.Dia,
                modeloDTO.HoraInicio,
                modeloDTO.HoraFin);

            if (hasConflict)
            {
                return Result<HorarioMedicoDTO>.Failure(
                    HorarioMedicoErrors.ScheduleConflict);
            }

            var horarioMedico = _mapper.Map<HorarioMedico>(modeloDTO);

            horarioMedico.Activo = true;

            _unitOfWork.HorarioMedicoRepository.Add(horarioMedico);

            await _unitOfWork.Save();

            horarioMedico.Medico = medicoDB;
            horarioMedico.Consultorio = consultorioDB;
            var dto = _mapper.Map<HorarioMedicoDTO>(horarioMedico);

            return Result<HorarioMedicoDTO>.Success(dto);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var horarioDB = await _unitOfWork.HorarioMedicoRepository.GetByIdAsync(id);
            if (horarioDB is null)
            {
                return Result.Failure(ConsultorioErrors.NotFound(id));

            }
            horarioDB.Activo = false;
            await _unitOfWork.Save();
            return Result.Success();
        }
        public async Task<Result> UpdateAsync(int id, UpdateHorarioMedicoDTO modeloDTO)
        {
            if (modeloDTO.HoraInicio >= modeloDTO.HoraFin)
            {
                return Result.Failure(
                    HorarioMedicoErrors.InvalidSchedule);
            }

            var horarioMedicoDB = await _unitOfWork.HorarioMedicoRepository.GetByIdAsync(id);

            if (horarioMedicoDB is null)
            {
                return Result.Failure(
                    HorarioMedicoErrors.NotFound(id));
            }

            if (!horarioMedicoDB.Activo)
            {
                return Result.Failure(
                    HorarioMedicoErrors.ScheduleInactive);
            }

            var medicoDB = await _unitOfWork.MedicoRepository.GetByIdAsync(modeloDTO.MedicoId);

            if (medicoDB is null)
            {
                return Result.Failure(
                    HorarioMedicoErrors.MedicoNotFound(modeloDTO.MedicoId));
            }

            if (!medicoDB.Activo)
            {
                return Result.Failure(
                    HorarioMedicoErrors.MedicoInactive);
            }

            var consultorioDB = await _unitOfWork.ConsultorioRepository.GetByIdAsync(modeloDTO.ConsultorioId);

            if (consultorioDB is null)
            {
                return Result.Failure(
                    HorarioMedicoErrors.ConsultorioNotFound(
                        modeloDTO.ConsultorioId));
            }

            if (!consultorioDB.Activo)
            {
                return Result.Failure(
                    HorarioMedicoErrors.ConsultorioInactive);
            }

            var hasConflict = await _unitOfWork.HorarioMedicoRepository.HasConflictAsync(
                modeloDTO.MedicoId,
                modeloDTO.ConsultorioId,
                modeloDTO.Dia,
                modeloDTO.HoraInicio,
                modeloDTO.HoraFin,
                id);

            if (hasConflict)
            {
                return Result.Failure(
                    HorarioMedicoErrors.ScheduleConflict);
            }

            _mapper.Map(modeloDTO, horarioMedicoDB);

            await _unitOfWork.Save();

            return Result.Success();
        }
    }
}
