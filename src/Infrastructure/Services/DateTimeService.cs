using SvCodingCase.Application.Common.Interfaces;

namespace SvCodingCase.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
