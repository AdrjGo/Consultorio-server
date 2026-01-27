using Application.Dto;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentTreatmentService
    {
        private readonly IPaymentTreatmentRepository _paymentTreatmentRepository;

        public PaymentTreatmentService(IPaymentTreatmentRepository paymentTreatmentRepository)
        {
            _paymentTreatmentRepository = paymentTreatmentRepository;
        }

        public async Task<PaymentTreatmentResponse> GetPaymentTreatmentById(Guid id)
        {
            var paymentTreatment = await _paymentTreatmentRepository.GetPaymentTreatmentById(id);
            if (paymentTreatment == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            return new PaymentTreatmentResponse
            {
                Id = paymentTreatment.Id,
                PatientId = paymentTreatment.PatientId,
                Amount = paymentTreatment.Amount,
                Method = paymentTreatment.Method,
                RecivedBy = paymentTreatment.RecivedBy,
                Observations = paymentTreatment.Observations,
                CreatedAt = paymentTreatment.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss")
            };
        }

        public async Task<IEnumerable<PaymentTreatmentResponse>> GetAllPaymentTreatmentsByPatientId(Guid id)
        {
            var paymentTreatments = await _paymentTreatmentRepository.GetAllPaymentTreatmentsByPatientId(id);
            return paymentTreatments.Select(pt => new PaymentTreatmentResponse
            {
                Id = pt.Id,
                PatientId = pt.PatientId,
                Amount = pt.Amount,
                Method = pt.Method,
                RecivedBy = pt.RecivedBy,
                Observations = pt.Observations,
                CreatedAt = pt.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss")
            });
        }

        public async Task<PaymentTreatmentCreatedResponse> CreatePaymentTreatment(PaymentTreatmentDto paymentTreatmentDto, string creatorName)
        {
            var paymentTreatment = new PaymentTreatment
            {
                Id = Guid.NewGuid(),
                PatientId = paymentTreatmentDto.PatientId,
                Amount = paymentTreatmentDto.Amount,
                Method = paymentTreatmentDto.Method,
                RecivedBy = paymentTreatmentDto.RecivedBy,
                Observations = paymentTreatmentDto.Observations,
                State = States.ACTIVE,
                CreatedBy = creatorName,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"))
            };

            await _paymentTreatmentRepository.CreatePaymentTreatment(paymentTreatment);

            return new PaymentTreatmentCreatedResponse
            {
                Message = "Pago registrado correctamente"
            };
        }

        public async Task<PaymentTreatmentCreatedResponse> UpdatePaymentTreatment(Guid id, PaymentTreatmentDto paymentTreatmentDto, string creatorName)
        {
            var paymentTreatment = await _paymentTreatmentRepository.GetPaymentTreatmentById(id);
            if (paymentTreatment == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            paymentTreatment.Amount = paymentTreatmentDto.Amount;
            paymentTreatment.Method = paymentTreatmentDto.Method;
            paymentTreatment.RecivedBy = paymentTreatmentDto.RecivedBy;
            paymentTreatment.Observations = paymentTreatmentDto.Observations;
            paymentTreatment.UpdatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o"));
            paymentTreatment.UpdatedBy = creatorName;

            await _paymentTreatmentRepository.UpdatePaymentTreatment(paymentTreatment);

            return new PaymentTreatmentCreatedResponse
            {
                Message = "Pago editado correctamente"
            };
        }

        public async Task DeletePaymentTreatment(Guid id)
        {
            var paymentTreatment = await _paymentTreatmentRepository.GetPaymentTreatmentById(id);
            if (paymentTreatment == null)
                throw new KeyNotFoundException($"No se encontró el examen de tratamiento");

            await _paymentTreatmentRepository.DeletePaymentTreatment(id);
        }
    }
}