

using MediatR;

namespace CleanArchitecture.Application.Features.Companies;

public class GetCompanyListQuery : IRequest<List<Company>> { }