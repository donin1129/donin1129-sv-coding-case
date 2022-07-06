using SvCodingCase.Domain.Common;

namespace SvCodingCase.Domain.Entities;
public class Building : BaseEntity
{
    public ICollection<Lock> Locks { get; set; }

    public string? ShortCut { get; set; }

    public string Name { get; set; }

    /*public Building(string name, string? shortCut, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        ShortCut = shortCut;
        Description = description;
    }*/
}
