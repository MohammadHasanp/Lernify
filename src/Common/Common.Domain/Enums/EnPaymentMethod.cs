namespace Common.Domain.Enums;

using Common.Domain.Resources;
using System.ComponentModel.DataAnnotations;

public enum EnPaymentMethod
{

    [Display(Name = "PaymentMethod_Cashed", ResourceType = typeof(Resource))]
    Cashed,

    [Display(Name = "PaymentMethod_Installment", ResourceType = typeof(Resource))]
    Installment,

    [Display(Name = "PaymentMethod_Combined", ResourceType = typeof(Resource))]
    Combined
}
