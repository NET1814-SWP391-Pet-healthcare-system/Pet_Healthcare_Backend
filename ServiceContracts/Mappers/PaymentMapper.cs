using Entities;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts.DTO.PaymentDTO;

namespace ServiceContracts.Mappers;

public static class PaymentMapper
{
    public static CashOutDto ToCashOutDto(this Entities.Transaction transaction)
    {
        return new CashOutDto()
        {
            Id = transaction.Id,
            CustomerId = transaction.CustomerId,
            TransactionId = transaction.TransactionId ?? "",
            AppointmentId = transaction.AppointmentId ?? null,
            HospitalizationId = transaction.HospitalizationId ?? null,
            Amount = transaction.Amount,
            Date = transaction.Date
        };
    }
}