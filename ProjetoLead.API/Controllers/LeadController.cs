using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjetoLead.API.Dtos;
using ProjetoLead.API.Extensions;
using ProjetoLead.API.Infrastructure.Repositories;

namespace ProjetoLead.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadController : ControllerBase
{
    public readonly ILeadRepository _repository;
    private readonly IValidator<LeadDetailDto> _leadDetailValidator;

    public LeadController(ILeadRepository repository, IValidator<LeadDetailDto> leadDetailValidator)
    {
        _repository = repository;
        _leadDetailValidator = leadDetailValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var leads = await _repository.GetAllAsync();
        return Ok(leads);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var lead = await _repository.GetAsync(id);
        if (lead is null)
            return NotFound();
        return Ok(lead);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LeadDetailDto leadDetail)
    {
        var result = await _leadDetailValidator.ValidateAsync(leadDetail);

        if(!result.IsValid)
            return BadRequest(result.GetValidationError());

        var lead = await _repository.CreateAsync(leadDetail);

        if (lead is null)
            return UnprocessableEntity(new ErrorDetails(["Não foi possível cadastrar o lead"]));

        return Ok(lead);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] LeadDetailDto leadDetail)
    {
        var result = await _leadDetailValidator.ValidateAsync(leadDetail);

        if(!result.IsValid)
            return BadRequest(result.GetValidationError());

        var lead = await _repository.UpdateAsync(leadDetail);

        if (lead is null)
            return UnprocessableEntity(new ErrorDetails(["Não foi possível atualizar o lead"]));

        return Ok(lead);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var success = await _repository.DeleteAsync(id);
        if (success)
            return Ok();
        return NotFound();
    }

}