using Application.Dto;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services
{
    public class ClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IUserRepository _userRepository;
        public ClinicService(IClinicRepository clinicRepository, IUserRepository userRepository)
        {
            _clinicRepository = clinicRepository;
            _userRepository = userRepository;
        }

        public async Task<ClinicResponse> GetClinic()
        {
            var clinic = await _clinicRepository.GetClinic();

            return new ClinicResponse
            {
                Id = clinic.Id,
                Name = clinic.ClinicName,
                Address = clinic.ClinicAddress,
                Phone = new PhoneNumber(clinic.ClinicPhone.Value).ToString(),
                CellPhone = new PhoneNumber(clinic.ClinicCellPhone.Value).ToString(),
                Email = new EmailAddress(clinic.ClinicEmail.Value).ToString(),
                LogoRef = clinic.LogoRef,
                LogoUrl = clinic.LogoUrl,
                ManagerId = clinic.ManagerId,
            };
        }

        public async Task<ClinicMessageResponse> CraeteClinic(ClinicDto dto, string creatorName)
        {

            var manager = await _userRepository.GetUserById(dto.ManagerId);
            if (manager == null)
                throw new KeyNotFoundException($"No se encontró ninguna persona con el id {dto.ManagerId}");

            var clinic = new Clinic
            {
                Id = Guid.CreateVersion7(),
                ClinicName = dto.Name,
                ClinicAddress = dto.Address,
                ClinicPhone = new PhoneNumber(dto.Phone),
                ClinicCellPhone = new PhoneNumber(dto.CellPhone),
                ClinicEmail = new EmailAddress(dto.Email),
                LogoRef = dto.LogoRef,
                LogoUrl = dto.LogoUrl,
                ManagerId = dto.ManagerId,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                Manager = manager
            };

            await _clinicRepository.CreateClinic(clinic);

            return new ClinicMessageResponse
            {
                Id = clinic.Id,
                Message = "Datos guardados correctamente"
            };
        }

        public async Task<ClinicMessageResponse> UpdateClinic(Guid Id, ClinicDto dto, string creatorName)
        {
            var clinic = await _clinicRepository.GetClinicById(Id);
            if (clinic == null)
                throw new KeyNotFoundException($"No se encontró la clínica con id {Id}");

            var user = await _userRepository.GetUserById(dto.ManagerId)
                ?? throw new KeyNotFoundException($"No se encontró ningún usuario con id {dto.ManagerId}");

            clinic.ClinicName = dto.Name;
            clinic.ClinicAddress = dto.Address;
            clinic.ClinicPhone = new PhoneNumber(dto.Phone);
            clinic.ClinicCellPhone = new PhoneNumber(dto.CellPhone);
            clinic.ClinicEmail = new EmailAddress(dto.Email);
            clinic.LogoRef = dto.LogoRef;
            clinic.LogoUrl = dto.LogoUrl;
            clinic.ManagerId = user.Id;
            clinic.Manager = user;

            clinic.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            clinic.UpdatedBy = creatorName;

            await _clinicRepository.UpdateClinic(clinic);

            return new ClinicMessageResponse
            {
                Id = clinic.Id,
                Message = "Datos actualizados correctamente"
            };
        }

    }
}