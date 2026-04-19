namespace Common.Domain.Enums;

using Common.Domain.Resources;
using System.ComponentModel.DataAnnotations;

[Flags]
public enum EnRoomStatus
{
    [Display(Name = "RoomStatus_None", ResourceType = typeof(Resource))]
    None = 0,

    [Display(Name = "RoomStatus_Available", ResourceType = typeof(Resource))]
    Available = 1 << 0,  // 1

    [Display(Name = "RoomStatus_Occupied", ResourceType = typeof(Resource))]
    Occupied = 1 << 1,  // 2

    [Display(Name = "RoomStatus_Reserved", ResourceType = typeof(Resource))]
    Reserved = 1 << 2,  // 4

    [Display(Name = "RoomStatus_OutOfService", ResourceType = typeof(Resource))]
    OutOfService = 1 << 3,  // 8

    [Display(Name = "RoomStatus_Cleaning", ResourceType = typeof(Resource))]
    Cleaning = 1 << 4,  // 16

    [Display(Name = "RoomStatus_DepartureToday", ResourceType = typeof(Resource))]
    DepartureToday = 1 << 5,  // 32

    [Display(Name = "RoomStatus_DepartureTomorrow", ResourceType = typeof(Resource))]
    DepartureTomorrow = 1 << 6,  // 64

    [Display(Name = "RoomStatus_CheckedOutToday", ResourceType = typeof(Resource))]
    CheckedOutToday = 1 << 7,  // 128

    [Display(Name = "RoomStatus_CheckedOutTomorrow", ResourceType = typeof(Resource))]
    CheckedOutTomorrow = 1 << 8,  // 256

    [Display(Name = "RoomStatus_Blocked", ResourceType = typeof(Resource))]
    Blocked = 1 << 9,  // 512

    [Display(Name = "RoomStatus_Housekeeping", ResourceType = typeof(Resource))]
    Housekeeping = 1 << 10, // 1024

    [Display(Name = "RoomStatus_Exclusive", ResourceType = typeof(Resource))]
    Exclusive = 1 << 11, // 2048

    [Display(Name = "RoomStatus_OutOfOrder", ResourceType = typeof(Resource))]
    OutOfOrder = 1 << 12,  // 4096
}
