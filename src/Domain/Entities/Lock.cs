using SvCodingCase.Domain.Common;
using SvCodingCase.Enums;

namespace SvCodingCase.Domain.Entities;
public class Lock : BaseEntity
{
    public string SerialNumber { get; set; }  // TODO-ZD: I am not sure if there is a relationship between serial number and id. So I leave this as not related. 

    public Building Building { get; set; }

    public Guid BuildingId { get; set; }

    public LockType Type { get; set; }

    public string Name { get; set; }

    public string? Floor { get; set; }

    public string? RoomNumber { get; set; }

    /*public Lock(string serialNumber, Building building, LockType type, string name, string? floor, string? roomNumber, string? description)
    {
        Id = Guid.NewGuid();
        SerialNumber = serialNumber;
        Building = building;
        BuildingId = building.Id;
        Type = type;
        Name = name;
        Floor = floor;
        RoomNumber = roomNumber;
        Description = description;
    }*/
}
