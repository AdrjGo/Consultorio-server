using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class MonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;

        public MonitoringService(IMonitoringRepository monitoringRepository)
        {
            _monitoringRepository = monitoringRepository;
        }

        public async Task<MonitoringResponse> GetMonitoringById(Guid id)
        {
            var monitoring = await _monitoringRepository.GetMonitoringByAppointmentId(id);
            if (monitoring == null)
                throw new KeyNotFoundException($"No se encontró la cita");

            return new MonitoringResponse
            {
                Id = monitoring.Id,
                Nomenclature = monitoring.Nomenclature,
                Treatment = monitoring.Treatment
            };
        }

        public async Task<IEnumerable<MonitoringResponse>> GetMonitoringsByPatientId(Guid id)
        {
            var monitorings = await _monitoringRepository.GetAllMonitoringsByPatientId(id);
            return monitorings.Select(m => new MonitoringResponse
            {
                Id = m.Id,
                Date = m.CreatedAt.ToString("dd/MM/yyyy"),
                Nomenclature = m.Nomenclature,
                Treatment = m.Treatment,
                Files = m.EvidenceFiles.Count.ToString()
            });
        }

        public async Task<MonitoringCreatedUpdateResponse> CreateMonitoring(MonitoringDto dto, string creatorName)
        {
            var monitoring = new Monitoring
            {
                Id = Guid.CreateVersion7(),
                AppointmentId = dto.AppointmentId,
                Nomenclature = dto.Nomenclature,
                Treatment = dto.Treatment,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = DateTime.UtcNow
            };

            await _monitoringRepository.CreateMonitoring(monitoring);

            return new MonitoringCreatedUpdateResponse
            {
                Id = monitoring.Id,
                Message = "Seguimiento creado correctamente"
            };
        }

        public async Task<MonitoringCreatedUpdateResponse> UpdateMonitoring(Guid Id, MonitoringDto dto, string creatorName)
        {
            var monitoring = await _monitoringRepository.GetMonitoringByAppointmentId(Id);
            if (monitoring == null)
                throw new KeyNotFoundException($"No se encontró el seguimiento");

            monitoring.Nomenclature = dto.Nomenclature ?? monitoring.Nomenclature;
            monitoring.Treatment = dto.Treatment ?? monitoring.Treatment;
            monitoring.UpdatedAt = DateTime.UtcNow;
            monitoring.UpdatedBy = creatorName;

            await _monitoringRepository.UpdateMonitoring(monitoring);

            return new MonitoringCreatedUpdateResponse
            {
                Id = monitoring.Id,
                Message = "Seguimiento editado correctamente"
            };
        }

        public async Task DeleteMonitoring(Guid id)
        {
            var monitoring = await _monitoringRepository.GetMonitoringByAppointmentId(id);
            if (monitoring == null)
                throw new KeyNotFoundException($"No se encontró el seguimiento");

            await _monitoringRepository.DeleteMonitoring(id);
        }

    }
}