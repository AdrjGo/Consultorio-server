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
        private readonly IContractRepository _contractRepository;

        public PaymentTreatmentService(IPaymentTreatmentRepository paymentTreatmentRepository, IContractRepository contractRepository)
        {
            _paymentTreatmentRepository = paymentTreatmentRepository;
            _contractRepository = contractRepository;
        }

        public async Task<PaymentTreatmentResponse> GetPaymentTreatmentById(Guid id)
        {
            var paymentTreatment = await _paymentTreatmentRepository.GetPaymentTreatmentById(id);
            if (paymentTreatment == null)
                throw new KeyNotFoundException($"No se encontró el rregistro de pago");

            return new PaymentTreatmentResponse
            {
                Id = paymentTreatment.Id,
                PatientId = paymentTreatment.PatientId,
                Payment = paymentTreatment.Amount,
                Method = paymentTreatment.Method,
                RecivedBy = paymentTreatment.RecivedBy,
                Observations = paymentTreatment.Observations,
                CreatedAt = paymentTreatment.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss")
            };
        }

        public async Task<PaymentTreatmentProgressSummaryResponse> GetAllPaymentTreatmentsByPatientId(Guid patientId)
        {
            var paymentTreatments = await _paymentTreatmentRepository.GetAllPaymentTreatmentsByPatientId(patientId);
            var contract = await _contractRepository.GetContractByPatientId(patientId);

            if (contract == null)
                throw new KeyNotFoundException("Contrato no encontrado para este paciente");

            var totalPaid = paymentTreatments.Sum(tp => tp.Amount);
            var totalCost = contract.TotalCost;

            var totalDebt = Math.Max(totalCost - totalPaid, 0);

            var paymentResponses = paymentTreatments.Select(tp => new PaymentTreatmentResponse
            {
                Id = tp.Id,
                PatientId = tp.PatientId,
                Payment = tp.Amount,
                Debt = totalDebt,
                Method = tp.Method,
                RecivedBy = tp.RecivedBy,
                Observations = tp.Observations,
                CreatedAt = tp.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return new PaymentTreatmentProgressSummaryResponse
            {
                TotalPayment = totalPaid,
                TotalDebt = totalDebt,
                TotalCost = totalCost,
                PaymentTreatmentProgresses = paymentResponses
            };
        }


        public async Task<PaymentTreatmentCreatedResponse> CreatePaymentTreatment(PaymentTreatmentDto paymentTreatmentDto, string creatorName)
        {
            var contract = await _contractRepository.GetContractByPatientId(paymentTreatmentDto.PatientId);
            if (contract == null)
                throw new KeyNotFoundException("Contrato no encontrado para este paciente");

            var existingPayments = await _paymentTreatmentRepository.GetAllPaymentTreatmentsByPatientId(paymentTreatmentDto.PatientId);
            var totalPaid = existingPayments.Sum(p => p.Amount);
            var newTotalPaid = totalPaid + paymentTreatmentDto.Amount;

            if (newTotalPaid > contract.TotalCost)
                throw new InvalidOperationException("El monto del pago excede la deuda pendiente");

            var paymentTreatment = new PaymentTreatment
            {
                Id = Guid.NewGuid(),
                PatientId = paymentTreatmentDto.PatientId,
                ContractId = contract.Id,
                Amount = paymentTreatmentDto.Amount,
                Method = paymentTreatmentDto.Method,
                RecivedBy = creatorName,
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

            var contract = await _contractRepository.GetContractByPatientId(paymentTreatment.PatientId);
            if (contract == null)
                throw new KeyNotFoundException("Contrato no encontrado para este paciente");

            var existingPayments = await _paymentTreatmentRepository.GetAllPaymentTreatmentsByPatientId(paymentTreatment.PatientId);
            var totalPaidWithoutCurrent = existingPayments
                .Where(p => p.Id != id)
                .Sum(p => p.Amount);
            var newTotalPaid = totalPaidWithoutCurrent + paymentTreatmentDto.Amount;

            if (newTotalPaid > contract.TotalCost)
                throw new InvalidOperationException("El monto del pago excede la deuda pendiente");

            paymentTreatment.Amount = paymentTreatmentDto.Amount;
            paymentTreatment.Method = paymentTreatmentDto.Method;
            paymentTreatment.RecivedBy = creatorName;
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