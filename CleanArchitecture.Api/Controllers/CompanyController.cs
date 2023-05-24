using MediatR;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using CleanArchitecture.Application.Features.Companies;


[Route("api/v1/companies")]
[ApiController]
public class CompanyController  : ControllerBase {

        private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpGet]
    
    public async Task<ActionResult<List<Company>>> GetAllCompanies()
    {
        var dtos = await _mediator.Send(new GetCompanyListQuery());
        return Ok(dtos);
    }

     [HttpPost]    
    public async Task<ActionResult<CreateCompanyCommandResponse>> AddCompany([FromBody] CreateCompanyCommand createCompanyCommand)
    {
        var response = await _mediator.Send(createCompanyCommand);
        return Created("", response);
    }
}