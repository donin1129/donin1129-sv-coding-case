using SvCodingCase.Enums;

namespace SvCodingCase.DTOs;

public class MediaDto
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string SerialNumber { get; set; } 

    public Guid GroupId { get; private set; }

    public MediaType Type { get; set; }

    public string Owner { get; set; }
}