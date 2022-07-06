namespace SvCodingCase.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

}
