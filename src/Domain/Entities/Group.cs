using SvCodingCase.Domain.Common;

namespace SvCodingCase.Domain.Entities;
public class Group : BaseEntity
{

    public ICollection<Media> Media { get; set; }

    public string Name { get; set; }

    public int CalcWeight(string searchString)
    {
        int score = 0;

        // Full match case
        if (Name == searchString)
        {
            score += 9 * 10;
        }
        // Partial match case
        else if (Name.Contains(searchString))
        {
            score += 9;
        }

        // Full match case
        if (Description is not null && Description == searchString)
        {
            score += 5 * 10;
        }
        // Partial match case
        else if (Description is not null && Description.Contains(searchString))
        {
            score += 5;
        }

        return score;
    }
}
