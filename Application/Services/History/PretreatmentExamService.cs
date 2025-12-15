using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class PretreatmentExamService
    {
        private readonly IPretreatmentExamRepository _pretreatmentExamRepository;

        public PretreatmentExamService(IPretreatmentExamRepository pretreatmentExamRepository)
        {
            _pretreatmentExamRepository = pretreatmentExamRepository;
        }

        public async Task<PretreatmentExamResponse> GetPretreatmentExamById(Guid id)
        {
            var pretreatmentExam = await _pretreatmentExamRepository.GetPretreatmentExamById(id);
            if (pretreatmentExam == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");
            return new PretreatmentExamResponse
            {
                Id = pretreatmentExam.Id,
                Observations = pretreatmentExam.Observations,
                Interconsultation = pretreatmentExam.Interconsultation,
                Piece = pretreatmentExam.Piece,
                Caries = pretreatmentExam.Caries,
                Treatment = pretreatmentExam.Treatment,
                Cost = pretreatmentExam.Cost,
            };
        }

        public async Task<PretreatmentSummaryResponse> GetAllPretreatmentExamsByPatientId(Guid id)
        {
            var pretreatmentExams = await _pretreatmentExamRepository.GetAllPretreatmentExamsByPatientId(id);
            if (pretreatmentExams == null)
                throw new KeyNotFoundException($"No se encontró ningún examen de tratamiento");

            var totalCost = pretreatmentExams.Sum(e => e.Cost);

            var examResponses = pretreatmentExams.Select(pretreatmentExam => new PretreatmentExamResponse
            {
                Id = pretreatmentExam.Id,
                Observations = pretreatmentExam.Observations,
                Interconsultation = pretreatmentExam.Interconsultation,
                Piece = pretreatmentExam.Piece,
                Caries = pretreatmentExam.Caries,
                Treatment = pretreatmentExam.Treatment,
                Cost = pretreatmentExam.Cost,
                Date = pretreatmentExam.CreatedAt.ToString("dd-MM-yyyy"),
            }).ToList();

            return new PretreatmentSummaryResponse
            {
                TotalCost = totalCost,
                Exams = examResponses
            };
        }

        public async Task<PretreatmentExamMessageResponse> CreatePretreatmentExam(PretreatmentExamDto pretreatmentExamDto, string creatorName)
        {
            var pretreatmentExam = new PretreatmentExam
            {
                Id = Guid.NewGuid(),
                PatientId = pretreatmentExamDto.PatientId,
                Observations = pretreatmentExamDto.Observations,
                Interconsultation = pretreatmentExamDto.Interconsultation,
                Piece = pretreatmentExamDto.Piece,
                Caries = pretreatmentExamDto.Caries,
                Treatment = pretreatmentExamDto.Treatment,
                Cost = pretreatmentExamDto.Cost,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,

            };

            await _pretreatmentExamRepository.CreatePretreatmentExam(pretreatmentExam);

            return new PretreatmentExamMessageResponse
            {
                Message = "El examen pretratamiento se creó correctamente",
            };
        }

        public async Task<PretreatmentExamMessageResponse> UpdatePretreatmentExam(Guid id, PretreatmentExamDto pretreatmentExamDto, string creatorName)
        {
            var pretreatmentExam = await _pretreatmentExamRepository.GetPretreatmentExamById(id);
            if (pretreatmentExam == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            pretreatmentExam.Observations = pretreatmentExamDto.Observations;
            pretreatmentExam.Interconsultation = pretreatmentExamDto.Interconsultation;
            pretreatmentExam.Piece = pretreatmentExamDto.Piece;
            pretreatmentExam.Caries = pretreatmentExamDto.Caries;
            pretreatmentExam.Treatment = pretreatmentExamDto.Treatment;
            pretreatmentExam.Cost = pretreatmentExamDto.Cost;
            pretreatmentExam.UpdatedAt = DateTime.UtcNow;
            pretreatmentExam.UpdatedBy = creatorName;

            await _pretreatmentExamRepository.UpdatePretreatmentExam(pretreatmentExam);

            return new PretreatmentExamMessageResponse
            {
                Message = "El examen pretratamiento se actualizó correctamente",
            };
        }

        public async Task DeletePretreatmentExam(Guid id)
        {
            var pretreatment = await _pretreatmentExamRepository.GetPretreatmentExamById(id);
            if (pretreatment == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            await _pretreatmentExamRepository.DeletePretreatmentExam(id);
        }

    }
}