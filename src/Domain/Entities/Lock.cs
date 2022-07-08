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

    public int CalcWeight(string searchString)
    {
        int score = 0;

        // If referenced by building
        if (Building.Name.Contains(searchString))
        {
            score += 8;
        }

        if (Building.ShortCut is not null && Building.ShortCut.Contains(searchString))
        {
            score += 5;
        }

        // Full match case
        if (Name == searchString)
        {
            score += 10 * 10;
        }
        // Partial match case
        else if (Name.Contains(searchString))
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

        // Full match case
        if (Floor is not null && Floor == searchString)
        {
            score += 6 * 10;
        }
        // Partial match case
        else if (Floor is not null && Floor.Contains(searchString))
        {
            score += 6;
        }

        // Full match case
        if (RoomNumber is not null && RoomNumber == searchString)
        {
            score += 6 * 10;
        }
        // Partial match case
        else if (RoomNumber is not null && RoomNumber.Contains(searchString))
        {
            score += 6;
        }

        return score;
    }
}
