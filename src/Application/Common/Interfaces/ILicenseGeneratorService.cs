namespace SvCodingCase.Application.Common.Interfaces;

public interface ILicenseGeneratorService
{
    Task GenerateLicenseRequest(string username);

    Task<string> RetrieveDatabaseEntryIfCreated(string userid, TimeSpan timeSpan);

    Task OnLicenseGenerated(Tuple<string, string> idLicensePair);
}
