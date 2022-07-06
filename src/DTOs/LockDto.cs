using SvCodingCase.Enums;

namespace SvCodingCase.DTOs;

public class LockDto
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string SerialNumber { get; set; }

    public Guid BuildingId { get; private set; }

    public LockType Type { get; set; }

    public string Name { get; set; }

    public string Floor { get; set; }

    public string RoomNumber { get; set; }
}