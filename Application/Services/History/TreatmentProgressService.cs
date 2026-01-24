using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class TreatmentProgressService
    {
        private readonly ITreatmentProgressRepository _treatmentProgressRepository;
        private readonly IPretreatmentExamRepository _pretreatmentExamRepository;

        public TreatmentProgressService(ITreatmentProgressRepository treatmentProgressRepository, IPretreatmentExamRepository pretreatmentExamRepository)
        {
            _treatmentProgressRepository = treatmentProgressRepository;
            _pretreatmentExamRepository = pretreatmentExamRepository;
        }

        public async Task<TreatmentProgressResponse> GetTreatmentProgressById(Guid id)
        {
            var treatmentProgress = await _treatmentProgressRepository.GetTreatmentProgressById(id);
            if (treatmentProgress == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");
            return new TreatmentProgressResponse
            {
                Id = treatmentProgress.Id,
                PatientId = treatmentProgress.PatientId,
                Payment = treatmentProgress.Payment,
                Debt = treatmentProgress.Debt,
            };
        }
        public async Task<TreatmentProgressSummaryResponse> GetAllTreatmentProgressByPatientId(Guid id)
        {
            var treatmentProgresses = await _treatmentProgressRepository.GetAllTreatmentProgressByPatientId(id);
            var exams = await _pretreatmentExamRepository.GetAllPretreatmentExamsByPatientId(id);

            var totalPaid = treatmentProgresses.Sum(tp => tp.Payment);
            var totalCost = exams.Sum(e => e.Cost);

            var totalDebt = Math.Max(totalCost - totalPaid, 0);

            var payments = treatmentProgresses.Select(tp => new TreatmentProgressResponse
            {
                Id = tp.Id,
                Payment = tp.Payment,
                Debt = tp.Debt,
                Date = tp.CreatedAt.ToString("dd-MM-yyyy")
            }).ToList();

            return new TreatmentProgressSummaryResponse
            {
                TotalPayment = totalPaid,
                TotalDebt = totalDebt,
                TreatmentProgresses = payments
            };
        }

        public async Task<TreatmentProgressMessageResponse> CreateTreatmentProgress(Guid patientId, int paymentAmount, string creatorName)
        {
            var exams = await _pretreatmentExamRepository.GetAllPretreatmentExamsByPatientId(patientId);
            var totalCost = exams.Sum(e => e.Cost);

            var previousPayments = await _treatmentProgressRepository.GetAllTreatmentProgressByPatientId(patientId);
            var totalPaid = previousPayments.Sum(p => p.Payment);

            var newTotalPaid = totalPaid + paymentAmount;
            var remainingDebt = Math.Max(totalCost - newTotalPaid, 0);

            var newProgress = new TreatmentProgress
            {
                Id = Guid.CreateVersion7(),
                PatientId = patientId,
                Payment = paymentAmount,
                Debt = remainingDebt,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            await _treatmentProgressRepository.CreateTreatmentProgress(newProgress);

            return new TreatmentProgressMessageResponse
            {
                Message = "El avance de tratamiento agregó correctamente"
            };
        }

        public async Task DeleteTreatmentProgress(Guid id)
        {
            var treatmentProgress = await _treatmentProgressRepository.GetTreatmentProgressById(id);
            if (treatmentProgress == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            await _treatmentProgressRepository.DeleteTreatmentProgress(id);
        }
    }
}