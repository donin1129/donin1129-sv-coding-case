using SvCodingCase.Domain.Common;

namespace SvCodingCase.Domain.Entities;
public class Group : BaseEntity
{

    public ICollection<Media> Media { get; set; }

    public string Name { get; set; }

/*    public Group(string name, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }*/
}
