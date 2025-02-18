using FwksLabs.Orbita.Core.Entities;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Requests;

public sealed record CreateResumeRequest(
    string FirstName, string LastName, string JobTitle, string CompanyName, string Email, string PhoneNumber, string City, string State, string Country)
{
    public ResumeEntity? ToEntity() => ResumeEntity.FromRequest(FirstName, LastName, JobTitle, CompanyName, Email, PhoneNumber, City, State, Country);
};