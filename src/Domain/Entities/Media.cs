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

    public int CalcWeight(string searchString)
    {
        int score = 0;

        // If referenced by groups
        if (Group.Name.Contains(searchString))
        {
            score += 8;
        };

        // Full match case
        if (Owner == searchString)
        {
            score += 10 * 10;
        }
        // Partial match case
        else if (Owner.Contains(searchString))
        {
            score += 10;
        }

        // Full match case
        if (Type.ToString() == searchString)
        {
            score += 3 * 10;
        }
        // Partial match case
        else if (Type.ToString().Contains(searchString))
        {
            score += 3;
        }

        // Full match case
        if (SerialNumber == searchString)
        {
            score += 8 * 10;
        }
        // Partial match case
        else if (SerialNumber.Contains(searchString))
        {
            score += 8;
        }

        // Full match case
        if (Description is not null && Description == searchString)
        {
            score += 6 * 10;
        }
        // Partial match case
        else if (Description is not null && Description.Contains(searchString))
        {
            score += 6;
        }

        return score;
    }
}
