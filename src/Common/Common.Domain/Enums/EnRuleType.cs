namespace Common.Domain.Enums;

using Common.Domain.Resources;
using System.ComponentModel.DataAnnotations;

public enum EnRuleType
{
    [Display(Name = "RuleType_Public", ResourceType = typeof(Resource))]
    Public = 0,

    [Display(Name = "RuleType_Cancellation", ResourceType = typeof(Resource))]
    Cancellation = 1,

    [Display(Name = "RuleType_Invalidation", ResourceType = typeof(Resource))]
    Invalidation = 2,

    [Display(Name = "RuleType_Reservation", ResourceType = typeof(Resource))]
    Reservation = 3,

    [Display(Name = "RuleType_Delivery", ResourceType = typeof(Resource))]
    Delivery = 4,

    [Display(Name = "RuleType_Animal", ResourceType = typeof(Resource))]
    Animal = 5,
}
