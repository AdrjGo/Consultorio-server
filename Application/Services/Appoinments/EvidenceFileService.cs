using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class EvidenceFileService
    {
        private readonly IEvidenceFileRepository _evidenceFileRepository;

        public EvidenceFileService(IEvidenceFileRepository evidenceFileRepository)
        {
            _evidenceFileRepository = evidenceFileRepository;
        }

        public async Task<EvidenceFileResponse> GetEvidenceFileById(Guid id)
        {
            var evidenceFile = await _evidenceFileRepository.GetEvidenceFileById(id);
            if (evidenceFile == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            return new EvidenceFileResponse
            {
                Id = evidenceFile.Id,
                MonitoringId = evidenceFile.MonitoringId,
                Format = evidenceFile.Format.ToString(),
                ExternalReference = evidenceFile.ExternalReference?.Value,
                Reference = evidenceFile.Reference?.Value,
                Description = evidenceFile.Description
            };
        }

        public async Task<IEnumerable<EvidenceFileResponse>> GetEvidenceFilesByAppointmentId(Guid id)
        {
            var evidenceFiles = await _evidenceFileRepository.GetEvidenceFilesByAppointmentId(id);
            return evidenceFiles.Select(e => new EvidenceFileResponse
            {
                Id = e.Id,
                MonitoringId = e.MonitoringId,
                Format = e.Format.ToString(),
                ExternalReference = e.ExternalReference?.Value,
                Reference = e.Reference?.Value,
                Description = e.Description
            });
        }

        public async Task<IEnumerable<EvidenceFileResponse>> GetEvidenceFileByPatient(Guid id)
        {
            var evidenceFiles = await _evidenceFileRepository.GetEvidenceFileByPatient(id);
            return evidenceFiles.Select(e => new EvidenceFileResponse
            {
                Id = e.Id,
                MonitoringId = e.MonitoringId,
                Format = e.Format.ToString(),
                ExternalReference = e.ExternalReference?.Value,
                Reference = e.Reference?.Value,
                Description = e.Description
            });
        }

        public async Task<EvidenceFileCreatedResponse> CreateEvidenceFile(EvidenceFileDto dto, string creatorName)
        {
            var evidenceFile = new EvidenceFile
            {
                Id = Guid.CreateVersion7(),
                MonitoringId = dto.MonitoringId,
                Format = Enum.Parse<EvidenceFileFormat>(dto.Format),
                ExternalReference = new Url(dto.ExternalReference),
                Reference = new FilePath(dto.Reference),
                Description = dto.Description,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = DateTime.UtcNow
            };

            await _evidenceFileRepository.CreateEvidenceFile(evidenceFile);

            return new EvidenceFileCreatedResponse
            {
                Id = evidenceFile.Id,
                Message = "Archivo cargado correctamente"
            };
        }

        public async Task<EvidenceFileUpdatedResponse> UpdateEvidenceFile(Guid Id, EvidenceFileUpdateDto dto, string creatorName)
        {
            var evidenceFile = await _evidenceFileRepository.GetEvidenceFileById(Id);
            if (evidenceFile == null)
                throw new KeyNotFoundException($"No se encontró el archivo");

            evidenceFile.MonitoringId = dto.MonitoringId ?? evidenceFile.MonitoringId;
            evidenceFile.Format = Enum.Parse<EvidenceFileFormat>(dto.Format ?? evidenceFile.Format.ToString());
            evidenceFile.ExternalReference = new Url(dto.ExternalReference ?? evidenceFile.ExternalReference?.Value);
            evidenceFile.Reference = new FilePath(dto.Reference ?? evidenceFile.Reference?.Value);
            evidenceFile.Description = dto.Description ?? evidenceFile.Description;
            evidenceFile.UpdatedAt = DateTime.UtcNow;
            evidenceFile.UpdatedBy = creatorName;

            await _evidenceFileRepository.UpdateEvidenceFile(evidenceFile);

            return new EvidenceFileUpdatedResponse
            {
                Id = evidenceFile.Id,
                Message = "Archivo editado correctamente"
            };
        }

        public async Task DeleteEvidenceFile(Guid id)
        {
            var evidenceFile = await _evidenceFileRepository.GetEvidenceFileById(id);
            if (evidenceFile == null)
                throw new KeyNotFoundException($"No se encontró el archivo");

            await _evidenceFileRepository.DeleteEvidenceFile(id);
        }
    }
}