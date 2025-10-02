using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class PatientsService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientResponse> GetPatientById(Guid id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al usuario");

            var birthDate = patient.Person.BirthDate;
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            var personResponsible = patient.ResponsibleId != null ? new PersonResponse
            {
                Id = patient.PatientResponsible.Id,
                BirthDate = patient.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                Ci = patient.PatientResponsible.Person.Ci,
                Email = patient.PatientResponsible.Person.Email.Value,
                Name = patient.PatientResponsible.Person.Name,
                Phone = patient.PatientResponsible.Person.Phone.Value,
                Profession = patient.PatientResponsible.Person.Profession,
            } : null;

            var personResponse = patient.Person != null ? new PersonResponse
            {
                Name = patient.Person.Name,
                LastName = patient.Person.LastName,
                BirthDate = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                Sex = patient.Person.Sex.ToString(),
                Ci = patient.Person.Ci,
                Email = patient.Person.Email?.Value,
                Phone = patient.Person.Phone?.Value,
            } : null;

            return new PatientResponse
            {
                Id = patient.Id,
                Patient = personResponse,
                Address = patient.Address,
                Zone = patient.Zone,
                City = patient.City,
                HomePhone = patient.HomePhone?.Value,
                Occupation = patient.Occupation,
                PlaceOccupation = patient.PlaceOccupation,
                Sender = patient.Sender ?? "No hay remitente",
                Responsible = age > 18 ? null : personResponsible,
            };
        }

        public async Task<PatientResponse> GetPatientByName(string name)
        {
            var patient = await _patientRepository.GetPatientByName(name);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al usuario: {name}");

            var birthDate = patient.Person.BirthDate;
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            var personResponsible = patient.ResponsibleId != null ? new PersonResponse
            {
                Id = patient.PatientResponsible.Id,
                BirthDate = patient.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                Ci = patient.PatientResponsible.Person.Ci,
                Email = patient.PatientResponsible.Person.Email.Value,
                Name = patient.PatientResponsible.Person.Name,
                Phone = patient.PatientResponsible.Person.Phone.Value,
                Profession = patient.PatientResponsible.Person.Profession,
            } : null;

            var personResponse = patient.Person != null ? new PersonResponse
            {
                Name = patient.Person.Name,
                LastName = patient.Person.LastName,
                BirthDate = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                Sex = patient.Person.Sex.ToString(),
                Ci = patient.Person.Ci,
                Email = patient.Person.Email?.Value,
                Phone = patient.Person.Phone?.Value,
            } : null;

            return new PatientResponse
            {
                Id = patient.Id,
                Patient = personResponse,
                Address = patient.Address,
                Zone = patient.Zone,
                City = patient.City,
                HomePhone = patient.HomePhone?.Value,
                Occupation = patient.Occupation,
                PlaceOccupation = patient.PlaceOccupation,
                Sender = patient.Sender ?? "No hay remitente",
                Responsible = age > 18 ? null : personResponsible,
            };
        }

        public async Task<IEnumerable<PatientResponse>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients();
            var today = DateTime.UtcNow;

            return patients.Select(p =>
            {
                var birthDate = p.Person.BirthDate;
                var age = today.Year - birthDate.Year;
                if (birthDate.Date > today.AddYears(-age)) age--;

                return new PatientResponse
                {
                    Id = p.Id,
                    Patient = new PersonResponse
                    {
                        Id = p.Person.Id,
                        Name = p.Person.Name,
                        LastName = p.Person.LastName,
                        BirthDate = p.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Sex = p.Person.Sex.ToString(),
                        Ci = p.Person.Ci,
                        Email = p.Person.Email?.Value,
                        Phone = p.Person.Phone?.Value,
                        Profession = p.Person.Profession
                    },
                    Address = p.Address,
                    Zone = p.Zone,
                    City = p.City,
                    HomePhone = p.HomePhone?.Value,
                    Occupation = p.Occupation,
                    PlaceOccupation = p.PlaceOccupation,
                    Sender = p.Sender ?? "No hay remitente",
                    Responsible = (age < 18 && p.PatientResponsible != null) ? new PersonResponse
                    {
                        Id = p.PatientResponsible.Person.Id,
                        Name = p.PatientResponsible.Person.Name,
                        LastName = p.PatientResponsible.Person.LastName,
                        BirthDate = p.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Ci = p.PatientResponsible.Person.Ci,
                        Email = p.PatientResponsible.Person.Email?.Value,
                        Phone = p.PatientResponsible.Person.Phone?.Value,
                        Profession = p.PatientResponsible.Person.Profession
                    } : null
                };
            });
        }


        public async Task<PatientResponse> CreatePatient(PatientDto dto, string creatorName)
        {
            var existingPatient = await _patientRepository.GetPatientByCi(dto.Person.Ci);
            if (existingPatient != null)
                throw new KeyNotFoundException($"El paciente {dto.Person.Name} {dto.Person.LastName} ya existe");

            var patientPerson = new Person
            {
                Id = Guid.CreateVersion7(),
                State = States.ACTIVE,
                Name = dto.Person.Name,
                LastName = dto.Person.LastName,
                BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Person.BirthDate), DateTimeKind.Utc),
                Sex = Enum.Parse<Gender>(dto.Person.Sex),
                Ci = dto.Person.Ci,
                Email = new EmailAddress(dto.Person.Email),
                Phone = new PhoneNumber(dto.Person.Phone),
                Profession = dto.Person.Profession,
                CreatedBy = creatorName,
                CreatedAt = DateTime.UtcNow,
            };

            var patient = new Patient
            {
                Id = Guid.CreateVersion7(),
                PersonId = patientPerson.Id,
                State = States.ACTIVE,
                Address = dto.Address,
                Zone = dto.Zone,
                City = dto.City,
                HomePhone = new PhoneNumber(dto.HomePhone ?? ""),
                Occupation = dto.Occupation,
                PlaceOccupation = dto.PlaceOccupation,
                Sender = dto.Sender,
                Person = patientPerson,
                CreatedBy = creatorName,
                CreatedAt = DateTime.UtcNow,
            };

            var birthDate = patientPerson.BirthDate;
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            Person? responsible = null;
            PatientResponsible? patientResponsible = null;

            if (age < 18 && dto.Responsible != null)
            {
                var responsibleDto = dto.Responsible;

                responsible = new Person
                {
                    Id = Guid.CreateVersion7(),
                    State = States.ACTIVE,
                    Name = responsibleDto.Person.Name,
                    LastName = responsibleDto.Person.LastName,
                    BirthDate = DateTime.SpecifyKind(DateTime.Parse(responsibleDto.Person.BirthDate), DateTimeKind.Utc),
                    Sex = Enum.Parse<Gender>(responsibleDto.Person.Sex),
                    Ci = responsibleDto.Person.Ci,
                    Email = new EmailAddress(responsibleDto.Person.Email),
                    Phone = new PhoneNumber(responsibleDto.Person.Phone),
                    Profession = responsibleDto.Person.Profession,
                    CreatedBy = creatorName,
                    CreatedAt = DateTime.UtcNow,
                };

                patientResponsible = new PatientResponsible
                {
                    Id = Guid.CreateVersion7(),
                    PersonId = responsible.Id,
                    Parentage = responsibleDto.Parentage,
                    Person = responsible,
                    Patient = patient,
                    State = States.ACTIVE,
                    CreatedBy = creatorName,
                    CreatedAt = DateTime.UtcNow,
                };

                patient.PatientResponsible = patientResponsible;
                patient.ResponsibleId = responsible.Id;
            }

            await _patientRepository.CreatePatient(patient);

            return new PatientResponse
            {
                Id = patient.Id,
                Patient = new PersonResponse
                {
                    Id = patient.Person.Id,
                    Name = patient.Person.Name,
                    LastName = patient.Person.LastName,
                    BirthDate = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = patient.Person.Sex.ToString(),
                    Ci = patient.Person.Ci,
                    Email = patient.Person.Email?.Value,
                    Phone = patient.Person.Phone?.Value,
                },
                Address = patient.Address,
                Zone = patient.Zone,
                City = patient.City,
                HomePhone = patient.HomePhone?.Value,
                Occupation = patient.Occupation,
                PlaceOccupation = patient.PlaceOccupation,
                Sender = patient.Sender ?? "No hay remitente",
                Responsible = responsible != null ? new PersonResponse
                {
                    Id = responsible.Id,
                    Name = responsible.Name,
                    LastName = responsible.LastName,
                    BirthDate = responsible.BirthDate.ToString("dd/MM/yyyy"),
                    Ci = responsible.Ci,
                    Email = responsible.Email?.Value,
                    Phone = responsible.Phone?.Value,
                    Profession = responsible.Profession,
                } : null
            };
        }

        public async Task<PatientResponse> UpdatePatient(Guid id, PatientDto dto, string creatorName)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            // Actualizar datos del paciente
            patient.Person.Name = dto.Person.Name;
            patient.Person.LastName = dto.Person.LastName;
            patient.Person.BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Person.BirthDate), DateTimeKind.Utc);
            patient.Person.Sex = Enum.Parse<Gender>(dto.Person.Sex);
            patient.Person.Ci = dto.Person.Ci;
            patient.Person.Email = new EmailAddress(dto.Person.Email);
            patient.Person.Phone = new PhoneNumber(dto.Person.Phone);
            patient.Person.Profession = dto.Person.Profession;
            patient.Address = dto.Address;
            patient.Zone = dto.Zone;
            patient.City = dto.City;
            patient.HomePhone = new PhoneNumber(dto.HomePhone ?? "");
            patient.Occupation = dto.Occupation;
            patient.PlaceOccupation = dto.PlaceOccupation;
            patient.Sender = dto.Sender;
            patient.Person.UpdatedAt = DateTime.UtcNow;
            patient.Person.UpdatedBy = creatorName;

            // Calcular edad
            var birthDate = patient.Person.BirthDate;
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            if (age < 18 && dto.Responsible != null)
            {
                // Actualizar responsable
                if (patient.PatientResponsible != null)
                {
                    patient.PatientResponsible.Person.Name = dto.Responsible.Person.Name;
                    patient.PatientResponsible.Person.LastName = dto.Responsible.Person.LastName;
                    patient.PatientResponsible.Person.BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Responsible.Person.BirthDate), DateTimeKind.Utc);
                    patient.PatientResponsible.Person.Sex = Enum.Parse<Gender>(dto.Responsible.Person.Sex);
                    patient.PatientResponsible.Person.Ci = dto.Responsible.Person.Ci;
                    patient.PatientResponsible.Person.Email = new EmailAddress(dto.Responsible.Person.Email);
                    patient.PatientResponsible.Person.Phone = new PhoneNumber(dto.Responsible.Person.Phone);
                    patient.PatientResponsible.Person.Profession = dto.Responsible.Person.Profession;
                    patient.PatientResponsible.Parentage = dto.Responsible.Parentage;
                    patient.PatientResponsible.Person.UpdatedAt = DateTime.UtcNow;
                    patient.PatientResponsible.Person.UpdatedBy = creatorName;
                }
                else
                {
                    var responsible = new Person
                    {
                        Id = Guid.CreateVersion7(),
                        State = States.ACTIVE,
                        Name = dto.Responsible.Person.Name,
                        LastName = dto.Responsible.Person.LastName,
                        BirthDate = DateTime.SpecifyKind(DateTime.Parse(dto.Responsible.Person.BirthDate), DateTimeKind.Utc),
                        Sex = Enum.Parse<Gender>(dto.Responsible.Person.Sex),
                        Ci = dto.Responsible.Person.Ci,
                        Email = new EmailAddress(dto.Responsible.Person.Email),
                        Phone = new PhoneNumber(dto.Responsible.Person.Phone),
                        Profession = dto.Responsible.Person.Profession,
                        CreatedBy = creatorName,
                        CreatedAt = DateTime.UtcNow
                    };

                    patient.PatientResponsible = new PatientResponsible
                    {
                        Id = Guid.CreateVersion7(),
                        PersonId = responsible.Id,
                        Person = responsible,
                        Patient = patient,
                        Parentage = dto.Responsible.Parentage,
                        State = States.ACTIVE,
                        CreatedBy = creatorName,
                        CreatedAt = DateTime.UtcNow
                    };
                }
            }
            else
            {
                // Si ya no es menor, eliminar responsable
                patient.PatientResponsible = null;
            }

            await _patientRepository.UpdatePatient(patient);

            return new PatientResponse
            {
                Id = patient.Id,
                Patient = new PersonResponse
                {
                    Id = patient.Person.Id,
                    Name = patient.Person.Name,
                    LastName = patient.Person.LastName,
                    BirthDate = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = patient.Person.Sex.ToString(),
                    Ci = patient.Person.Ci,
                    Email = patient.Person.Email?.Value,
                    Phone = patient.Person.Phone?.Value,
                },
                Address = patient.Address,
                Zone = patient.Zone,
                City = patient.City,
                HomePhone = patient.HomePhone?.Value,
                Occupation = patient.Occupation,
                PlaceOccupation = patient.PlaceOccupation,
                Sender = patient.Sender ?? "No hay remitente",
                Responsible = patient.PatientResponsible != null ? new PersonResponse
                {
                    Id = patient.PatientResponsible.Person.Id,
                    Name = patient.PatientResponsible.Person.Name,
                    LastName = patient.PatientResponsible.Person.LastName,
                    BirthDate = patient.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Ci = patient.PatientResponsible.Person.Ci,
                    Email = patient.PatientResponsible.Person.Email?.Value,
                    Phone = patient.PatientResponsible.Person.Phone?.Value,
                    Profession = patient.PatientResponsible.Person.Profession
                } : null
            };
        }


        public async Task DeletePatient(Guid id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            await _patientRepository.DeletePatient(id);
        }

    }
}