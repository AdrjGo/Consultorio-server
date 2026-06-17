using Application.Dto;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class MonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public MonitoringService(IMonitoringRepository monitoringRepository, IContractRepository contractRepository, IAppointmentRepository appointmentRepository)
        {
            _monitoringRepository = monitoringRepository;
            _contractRepository = contractRepository;
            _appointmentRepository = appointmentRepository;
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
            var appointmentInCourse = await _appointmentRepository.GetAppointmentInCourseByPatientId(dto.PatientId);
            if (appointmentInCourse == null)
                throw new InvalidOperationException($"El paciente no tiene cita en curso");

            var existingMonitoring = await _monitoringRepository.GetMonitoringByAppointmentId(appointmentInCourse.Id);
            if (existingMonitoring != null)
                throw new InvalidOperationException($"La cita ya tiene un seguimiento registrado");

            var monitoring = new Monitoring
            {
                Id = Guid.CreateVersion7(),
                AppointmentId = appointmentInCourse.Id,
                Nomenclature = dto.Nomenclature,
                Treatment = dto.Treatment,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"))
            };

            await _monitoringRepository.CreateMonitoring(monitoring);

            return new MonitoringCreatedUpdateResponse
            {
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
            monitoring.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
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