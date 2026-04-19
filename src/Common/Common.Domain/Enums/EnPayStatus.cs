namespace Common.Domain.Enums;

using Common.Domain.Resources;
using System.ComponentModel.DataAnnotations;

public enum EnPayStatus
{
    [Display(Name = "PayStatus_Pending", ResourceType = typeof(Resource))]
    Pending = 1,

    [Display(Name = "PayStatus_Paid", ResourceType = typeof(Resource))]
    Paid = 2,

    [Display(Name = "PayStatus_Canceled", ResourceType = typeof(Resource))]
    Canceled = 3,

    [Display(Name = "PayStatus_ConnectedToBank", ResourceType = typeof(Resource))]
    ConnectedToBank = 4,

    [Display(Name = "PayStatus_HonoraryReservation", ResourceType = typeof(Resource))]
    HonoraryReservation = 5,
}
