using SvCodingCase.Domain.Common;
using SvCodingCase.Enums;

namespace SvCodingCase.Domain.Entities;
public class Media : BaseEntity
{
    public string SerialNumber { get; set; }  // TODO-ZD: I am not sure if there is a relationship between serial number and id. So I leave this as not related. 

    public Group Group { get; set; }

    public Guid GroupId { get; set; }

    public MediaType Type { get; set; }

    public string Owner { get; set; }

    /*public Media(string serialNumber, Group group, MediaType type, string owner)
    {
        Id = Guid.NewGuid();
        SerialNumber = serialNumber;
        Group = group;
        GroupId = group.Id;
        Type = type;
        Owner = owner;
    }*/
}
