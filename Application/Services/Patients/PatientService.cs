using Application.Common;
using Application.Dto;
using Application.Response;
using Application.Responses;
using Application.Utils;
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
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            var personResponsible = patient.ResponsibleId != null ? new ResponsibleResponse
            {
                Id = patient.PatientResponsible.Id,
                Parentage = patient.PatientResponsible.Parentage.ToString(),
                Person = new PersonResponse
                {
                    Id = patient.PatientResponsible.Person.Id,
                    Name = patient.PatientResponsible.Person.Name,
                    LastName = patient.PatientResponsible.Person.LastName,
                    BirthDate = patient.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                    Sex = patient.PatientResponsible.Person.Sex.ToString(),
                    Ci = patient.PatientResponsible.Person.Ci,
                    Email = patient.PatientResponsible.Person.Email?.Value,
                    Phone = patient.PatientResponsible.Person.Phone?.Value,
                    Profession = patient.PatientResponsible.Person.Profession,
                }
            } : null;

            var personResponse = patient.Person != null ? new PersonResponse
            {
                Id = patient.Person.Id,
                Name = patient.Person.Name,
                LastName = patient.Person.LastName,
                BirthDate = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                Sex = patient.Person.Sex.ToString(),
                Ci = patient.Person.Ci,
                Email = patient.Person.Email?.Value,
                Phone = patient.Person.Phone?.Value,
                Profession = patient.Person.Profession,
            } : null;

            return new PatientResponse
            {
                Id = patient.Id,
                PatientPerson = personResponse,
                Address = patient.Address,
                Zone = patient.Zone,
                City = patient.City,
                HomePhone = patient.HomePhone?.Value,
                Occupation = patient.Occupation,
                PlaceOccupation = patient.PlaceOccupation,
                Nit = patient.Nit ?? "Sin NIT",
                Sender = patient.Sender ?? "No hay remitente",
                State = patient.State.ToString(),
                CreatedAt = patient.CreatedAt.ToString(),
                UpdatedAt = patient?.UpdatedAt.ToString(),
                CreatedBy = patient.CreatedBy,
                UpdatedBy = patient.UpdatedBy,
                Responsible = age > 18 ? null : personResponsible,

            };
        }

        public async Task<IEnumerable<PatientResponse>> GetPatientByName(string name)
        {
            var patients = await _patientRepository.GetPatientsByName(name);
            if (patients == null)
                throw new KeyNotFoundException($"No se encontró al usuario: {name}");

            return patients.Select(patient =>
            {
                var birthDate = patient.Person?.BirthDate ?? DateOnly.MinValue;
                var today = DateOnly.FromDateTime(DateTime.UtcNow);
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;

                var personResponsible = patient.ResponsibleId != null ? new ResponsibleResponse
                {
                    Id = patient.PatientResponsible.Id,
                    Parentage = patient.PatientResponsible.Parentage.ToString(),
                    Person = new PersonResponse
                    {
                        Id = patient.PatientResponsible.Person.Id,
                        Name = patient.PatientResponsible.Person.Name,
                        LastName = patient.PatientResponsible.Person.LastName,
                        BirthDate = patient.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                        Sex = patient.PatientResponsible.Person.Sex.ToString(),
                        Ci = patient.PatientResponsible.Person.Ci,
                        Email = patient.PatientResponsible.Person.Email?.Value,
                        Phone = patient.PatientResponsible.Person.Phone?.Value,
                        Profession = patient.PatientResponsible.Person.Profession,
                    }
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
                    Profession = patient.Person.Profession,
                } : null;

                return new PatientResponse
                {
                    Id = patient.Id,
                    PatientPerson = personResponse,
                    Address = patient.Address,
                    Zone = patient.Zone,
                    City = patient.City,
                    HomePhone = patient.HomePhone?.Value,
                    Occupation = patient.Occupation,
                    PlaceOccupation = patient.PlaceOccupation,
                    Nit = patient.Nit ?? "Sin NIT",
                    Sender = patient.Sender ?? "No hay remitente",
                    State = patient.State.ToString(),
                    Responsible = age > 18 ? null : personResponsible,
                };
            });
        }

        public async Task<PagedResult<PatientResponse>> GetPagedPatients(int pageNumber, int pageSize, string? search = null, string? state = null)
        {
            var (patients, totalCount) = await _patientRepository.GetPatientsPagedAsync(pageNumber, pageSize, search, state);
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var patientResponses = patients.Select(p =>
            {
                var birthDate = p.Person.BirthDate;
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;

                return new PatientResponse
                {
                    Id = p.Id,
                    PatientPerson = new PersonResponse
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
                    Nit = p.Nit ?? "Sin NIT",
                    Sender = p.Sender ?? "No hay remitente",
                    State = p.State.ToString(),
                    Responsible = (age < 18 && p.PatientResponsible != null) ? new ResponsibleResponse
                    {
                        Id = p.PatientResponsible.Id,
                        Parentage = p.PatientResponsible.Parentage.ToString(),
                        Person = new PersonResponse
                        {
                            Id = p.PatientResponsible.Person.Id,
                            Name = p.PatientResponsible.Person.Name,
                            LastName = p.PatientResponsible.Person.LastName,
                            BirthDate = p.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                            Sex = p.PatientResponsible.Person.Sex.ToString(),
                            Ci = p.PatientResponsible.Person.Ci,
                            Email = p.PatientResponsible.Person.Email?.Value,
                            Phone = p.PatientResponsible.Person.Phone?.Value,
                            Profession = p.PatientResponsible.Person.Profession,
                        }
                    } : null
                };
            });

            return new PagedResult<PatientResponse>
            {
                Items = patientResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<PatientResponse>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients();
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            return patients.Select(p =>
            {
                var birthDate = p.Person.BirthDate;
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age)) age--;

                return new PatientResponse
                {
                    Id = p.Id,
                    PatientPerson = new PersonResponse
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
                    Responsible = (age < 18 && p.PatientResponsible != null) ? new ResponsibleResponse
                    {
                        Id = p.PatientResponsible.Id,
                        Parentage = p.PatientResponsible.Parentage.ToString(),
                        Person = new PersonResponse
                        {
                            Id = p.PatientResponsible.Person.Id,
                            Name = p.PatientResponsible.Person.Name,
                            LastName = p.PatientResponsible.Person.LastName,
                            BirthDate = p.PatientResponsible.Person.BirthDate.ToString("dd/MM/yyyy"),
                            Sex = p.PatientResponsible.Person.Sex.ToString(),
                            Ci = p.PatientResponsible.Person.Ci,
                            Email = p.PatientResponsible.Person.Email?.Value,
                            Phone = p.PatientResponsible.Person.Phone?.Value,
                            Profession = p.PatientResponsible.Person.Profession,
                        }
                    } : null

                };
            });
        }

        public async Task<PatientMessageResponse> CreatePatient(PatientDto dto, string creatorName)
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
                BirthDate = DateOnly.Parse(dto.Person.BirthDate),
                Sex = Enum.Parse<Gender>(dto.Person.Sex),
                Ci = dto.Person.Ci,
                Email = new EmailAddress(dto.Person.Email),
                Phone = new PhoneNumber(dto.Person.Phone),
                Profession = dto.Person.Profession,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
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
                Nit = dto.Nit,
                Sender = dto.Sender,
                Person = patientPerson,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
            };

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var birthDate = patient.Person.BirthDate;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;


            Person? responsible = null;
            PatientResponsible? patientResponsible = null;
            // Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++Edad calculada: {age} - Fecha de nacimiento: {birthDate}");


            if (age < 18 && dto.Responsible != null)
            {
                var responsibleDto = dto.Responsible;

                responsible = new Person
                {
                    Id = Guid.CreateVersion7(),
                    State = States.ACTIVE,
                    Name = responsibleDto.Person.Name,
                    LastName = responsibleDto.Person.LastName,
                    BirthDate = DateOnly.Parse(dto.Responsible.Person.BirthDate),
                    Sex = Enum.Parse<Gender>(responsibleDto.Person.Sex),
                    Ci = responsibleDto.Person.Ci,
                    Email = new EmailAddress(responsibleDto.Person.Email),
                    Phone = new PhoneNumber(responsibleDto.Person.Phone),
                    Profession = responsibleDto.Person.Profession,
                    CreatedBy = creatorName,
                    CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                };

                patientResponsible = new PatientResponsible
                {
                    Id = Guid.CreateVersion7(),
                    PersonId = responsible.Id,
                    Parentage = Enum.Parse<PatientParentage>(responsibleDto.Parentage),
                    Person = responsible,
                    Patient = patient,
                    State = States.ACTIVE,
                    CreatedBy = creatorName,
                    CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                };

                patient.PatientResponsible = patientResponsible;
                patient.ResponsibleId = responsible.Id;
            }

            await _patientRepository.CreatePatient(patient);

            return new PatientMessageResponse
            {
                Id = patient.Id,
                Message = "Paciente creado correctamente"
            };
        }

        public async Task<PatientMessageResponse> UpdatePatient(Guid id, PatientDto dto, string creatorName)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            // Actualizar datos del paciente
            patient.Person.Name = dto.Person.Name;
            patient.Person.LastName = dto.Person.LastName;
            patient.Person.BirthDate = DateOnly.Parse(dto.Person.BirthDate);
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
            patient.Nit = dto.Nit;
            if (dto.State != null)
                patient.State = Enum.Parse<States>(dto.State);
            patient.Person.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            patient.Person.UpdatedBy = creatorName;

            // Calcular edad
            var birthDate = patient.Person.BirthDate;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            if (age < 18 && dto.Responsible != null)
            {
                // Actualizar responsable
                if (patient.PatientResponsible != null)
                {
                    patient.PatientResponsible.Person.Name = dto.Responsible.Person.Name;
                    patient.PatientResponsible.Person.LastName = dto.Responsible.Person.LastName;
                    patient.PatientResponsible.Person.BirthDate = DateOnly.Parse(dto.Responsible.Person.BirthDate);
                    patient.PatientResponsible.Person.Sex = Enum.Parse<Gender>(dto.Responsible.Person.Sex);
                    patient.PatientResponsible.Person.Ci = dto.Responsible.Person.Ci;
                    patient.PatientResponsible.Person.Email = new EmailAddress(dto.Responsible.Person.Email);
                    patient.PatientResponsible.Person.Phone = new PhoneNumber(dto.Responsible.Person.Phone);
                    patient.PatientResponsible.Person.Profession = dto.Responsible.Person.Profession;
                    patient.PatientResponsible.Parentage = Enum.Parse<PatientParentage>(dto.Responsible.Parentage);
                    patient.PatientResponsible.Person.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
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
                        BirthDate = DateOnly.Parse(dto.Responsible.Person.BirthDate),
                        Sex = Enum.Parse<Gender>(dto.Responsible.Person.Sex),
                        Ci = dto.Responsible.Person.Ci,
                        Email = new EmailAddress(dto.Responsible.Person.Email),
                        Phone = new PhoneNumber(dto.Responsible.Person.Phone),
                        Profession = dto.Responsible.Person.Profession,
                        CreatedBy = creatorName,
                        CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"))
                    };

                    patient.PatientResponsible = new PatientResponsible
                    {
                        Id = Guid.CreateVersion7(),
                        PersonId = responsible.Id,
                        Person = responsible,
                        Patient = patient,
                        Parentage = Enum.Parse<PatientParentage>(dto.Responsible.Parentage),
                        State = States.ACTIVE,
                        CreatedBy = creatorName,
                        CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"))
                    };
                }
            }
            else
            {
                // Si ya no es menor o no se envía responsable, eliminar responsable existente
                if (patient.PatientResponsible != null)
                {
                    var oldResponsible = patient.PatientResponsible;
                    patient.PatientResponsible = null;
                    patient.ResponsibleId = null;
                    await _patientRepository.RemovePatientResponsible(oldResponsible);
                }
            }

            await _patientRepository.UpdatePatient(patient);

            return new PatientMessageResponse
            {
                Id = patient.Id,
                Message = "Paciente actualizado correctamente"
            };
        }

        public async Task DeletePatient(Guid id, string creatorName)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                throw new KeyNotFoundException($"No se encontró al paciente");

            patient.State = States.INACTIVE;
            patient.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            patient.UpdatedBy = creatorName;

            await _patientRepository.UpdatePatient(patient);
        }

    }
}