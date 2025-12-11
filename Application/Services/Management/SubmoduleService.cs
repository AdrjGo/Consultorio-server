using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class SubmoduleService
    {
        private readonly ISubmoduleRepository _submoduleRepository;

        public SubmoduleService(ISubmoduleRepository submoduleRepository)
        {
            _submoduleRepository = submoduleRepository;
        }

        public async Task<SubmoduleResponse> GetSubmoduleById(int id)
        {
            var submodule = await _submoduleRepository.GetSubmoduleById(id);
            if (submodule == null)
                throw new KeyNotFoundException($"No se encontró el submódulo con id {id}");
            return new SubmoduleResponse
            {
                Id = submodule.Id,
                SubmoduleName = submodule.Name,
                State = submodule.State.ToString(),
                CreatedAt = submodule.CreatedAt,
                CreatedBy = submodule.CreatedBy,
                UpdatedAt = submodule.UpdatedAt,
                UpdatedBy = submodule.UpdatedBy,
            };
        }

        public async Task<IEnumerable<SubmoduleResponse>> GetAllSubmodules()
        {
            var submodules = await _submoduleRepository.GetAllSubmodules();
            if (submodules == null)
                throw new KeyNotFoundException($"No se encontró ningún submódulo");
            return submodules.Select(s => new SubmoduleResponse
            {
                Id = s.Id,
                SubmoduleName = s.Name,
                State = s.State.ToString(),
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy,
            });
        }

        public async Task<SubmoduleMessageResponse> CreateSubmodule(SubmoduleDto dto, string creatorName)
        {
            var submoduleExist = await _submoduleRepository.GetSubmoduleById(dto.Id);
            if (submoduleExist != null)
                throw new KeyNotFoundException($"Ya existe un submódulo con id {dto.Id}");

            var submodule = new Submodule
            {
                Id = dto.Id,
                Name = dto.Name,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            await _submoduleRepository.CreateSubmodule(submodule);

            return new SubmoduleMessageResponse
            {
                Message = "Submódulo creado correctamente",
            };
        }

        public async Task<SubmoduleMessageResponse> UpdateSubmodule(int id, SubmoduleDto dto, string creatorName)
        {
            var submodule = await _submoduleRepository.GetSubmoduleById(id);
            if (submodule == null)
                throw new KeyNotFoundException($"No se encontró el submódulo con id {id}");

            submodule.Name = dto.Name;
            submodule.UpdatedAt = DateTime.UtcNow;
            submodule.UpdatedBy = creatorName;

            await _submoduleRepository.UpdateSubmodule(submodule);

            return new SubmoduleMessageResponse
            {
                Message = "Submódulo actualizado correctamente",
            };
        }

        public async Task DeleteSubmodule(int id)
        {
            var submodule = await _submoduleRepository.GetSubmoduleById(id);
            if (submodule == null)
                throw new KeyNotFoundException($"No se encontró el submódulo con id {id}");

            await _submoduleRepository.DeleteSubmodule(id);
        }
    }
}