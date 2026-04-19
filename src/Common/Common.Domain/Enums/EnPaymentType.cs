namespace Common.Domain.Enums;

using Common.Domain.Resources;
using System.ComponentModel.DataAnnotations;

public enum EnPaymentType
{
    [Display(Name = "PaymentType_Deposit", ResourceType = typeof(Resource))]
    Deposit = 0,

    [Display(Name = "PaymentType_Withdrawal", ResourceType = typeof(Resource))]
    Withdrawal = 1,

    [Display(Name = "PaymentType_Payment", ResourceType = typeof(Resource))]
    Payment = 2,

    [Display(Name = "PaymentType_Refund", ResourceType = typeof(Resource))]
    Refund = 3
}
