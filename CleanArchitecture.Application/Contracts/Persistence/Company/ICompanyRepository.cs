

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface ICompanyRepository
{

    Task<Company> AddCompanyAsync(Company company);

    Task<IReadOnlyList<Company>> GetCompanyList();
}