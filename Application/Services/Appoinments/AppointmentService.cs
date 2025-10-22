using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();
            return appointments.Select(a => new AppointmentResponse
            {
                Id = a.Id,
                PatientId = a.PatientId,
                ProfessionalId = a.ProfessionalId,
                StartDate = a.StartDate.ToString("dd-MM-yyyy HH:mm:ss"),
                EndDate = a.EndDate.ToString("dd-MM-yyyy HH:mm:ss"),
                Type = a.Type,
                Status = a.Status,
                Reason = a.Reason,
                Observations = a.Observations,
                Patient = new PatientResponse
                {
                    Id = a.Patient.Id,
                    PatientPerson = new PersonResponse
                    {
                        Id = a.Patient.Person.Id,
                        Name = a.Patient.Person.Name,
                        LastName = a.Patient.Person.LastName,
                        BirthDate = a.Patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Sex = a.Patient.Person.Sex.ToString(),
                        Ci = a.Patient.Person.Ci,
                        Email = a.Patient.Person.Email?.Value,
                        Phone = a.Patient.Person.Phone?.Value,
                    },
                    Address = a.Patient.Address,
                    Zone = a.Patient.Zone,
                    City = a.Patient.City,
                    HomePhone = a.Patient.HomePhone?.Value,
                    Occupation = a.Patient.Occupation,
                    PlaceOccupation = a.Patient.PlaceOccupation,
                    Sender = a.Patient.Sender ?? "No hay remitente"
                }
            });
        }

        public async Task<AppointmentResponse> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            return new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                ProfessionalId = appointment.ProfessionalId,
                StartDate = appointment.StartDate.ToString("dd-MM-yyyy HH:mm:ss"),
                EndDate = appointment.EndDate.ToString("dd-MM-yyyy HH:mm:ss"),
                Type = appointment.Type,
                Status = appointment.Status,
                Reason = appointment.Reason,
                Observations = appointment.Observations,
                Patient = new PatientResponse
                {
                    Id = appointment.Patient.Id,
                    PatientPerson = new PersonResponse
                    {
                        Id = appointment.Patient.Person.Id,
                        Name = appointment.Patient.Person.Name,
                        LastName = appointment.Patient.Person.LastName,
                        BirthDate = appointment.Patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Sex = appointment.Patient.Person.Sex.ToString(),
                        Ci = appointment.Patient.Person.Ci,
                        Email = appointment.Patient.Person.Email?.Value,
                        Phone = appointment.Patient.Person.Phone?.Value,
                    },
                    Address = appointment.Patient.Address,
                    Zone = appointment.Patient.Zone,
                    City = appointment.Patient.City,
                    HomePhone = appointment.Patient.HomePhone?.Value,
                    Occupation = appointment.Patient.Occupation,
                    PlaceOccupation = appointment.Patient.PlaceOccupation,
                    Sender = appointment.Patient.Sender ?? "No hay remitente"
                }
            };
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAppointmentsByDate(DateTime? initialDate, DateTime? finalDate)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDate(initialDate, finalDate);
            return appointments.Select(a => new AppointmentResponse
            {
                Id = a.Id,
                PatientId = a.PatientId,
                ProfessionalId = a.ProfessionalId,
                StartDate = a.StartDate.ToString("dd-MM-yyyy HH:mm:ss"),
                EndDate = a.EndDate.ToString("dd-MM-yyyy HH:mm:ss"),
                Type = a.Type,
                Status = a.Status,
                Reason = a.Reason,
                Observations = a.Observations,
                Patient = new PatientResponse
                {
                    Id = a.Patient.Id,
                    PatientPerson = new PersonResponse
                    {
                        Id = a.Patient.Person.Id,
                        Name = a.Patient.Person.Name,
                        LastName = a.Patient.Person.LastName,
                        BirthDate = a.Patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Sex = a.Patient.Person.Sex.ToString(),
                        Ci = a.Patient.Person.Ci,
                        Email = a.Patient.Person.Email?.Value,
                        Phone = a.Patient.Person.Phone?.Value,
                    },
                    Address = a.Patient.Address,
                    Zone = a.Patient.Zone,
                    City = a.Patient.City,
                    HomePhone = a.Patient.HomePhone?.Value,
                    Occupation = a.Patient.Occupation,
                    PlaceOccupation = a.Patient.PlaceOccupation,
                    Sender = a.Patient.Sender ?? "No hay remitente"
                }
            });
        }

        public async Task<AppointmentCreatedResponse> CreateAppointment(AppointmentDto dto, string creatorName)
        {
            var appointment = new Appointment
            {
                Id = Guid.CreateVersion7(),
                PatientId = dto.PatientId,
                ProfessionalId = dto.ProfessionalId,
                StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),
                Type = dto.Type,
                Status = dto.Status,
                Reason = dto.Reason,
                Observations = dto.Observations,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName
            };

            await _appointmentRepository.CreateAppointment(appointment);


            return new AppointmentCreatedResponse
            {
                Id = appointment.Id,
                Message = "Cita creada correctamente"
            };
        }

        public async Task<AppointmentUpdatedResponse> UpdateAppointment(Guid Id, AppointmentUpdateDto dto, string creatorName)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(Id);
            if (appointment == null)
                throw new KeyNotFoundException($"No se encontró la cita");

            appointment.PatientId = appointment.PatientId;
            appointment.ProfessionalId = appointment.ProfessionalId;
            appointment.StartDate = dto.StartDate ?? appointment.StartDate;
            appointment.EndDate = dto.EndDate ?? appointment.EndDate;
            appointment.Type = dto.Type ?? appointment.Type;
            appointment.Status = dto.Status ?? appointment.Status;
            appointment.Reason = dto.Reason ?? appointment.Reason;
            appointment.Observations = dto.Observations ?? appointment.Observations;
            appointment.State = States.ACTIVE;
            appointment.UpdatedAt = DateTime.UtcNow;
            appointment.UpdatedBy = creatorName;

            await _appointmentRepository.UpdateAppointment(appointment);

            return new AppointmentUpdatedResponse
            {
                Id = appointment.Id,
                Message = "Cita editada correctamente"
            };
        }

        public async Task DeleteAppointment(Guid id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
                throw new KeyNotFoundException($"No se encontró la paciente");

            await _appointmentRepository.DeleteAppointment(id);
        }

    }
}