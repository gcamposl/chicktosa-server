using System;
using System.Reflection.Metadata;
using AutoMapper;
using Ck.Server.Application.DTOs;
using Ck.Server.Application.DTOs.Validations;
using Ck.Server.Application.Services.Interfaces;
using Ck.Server.Domain.Entities;
using Ck.Server.Domain.Repositories;
using FluentValidation.Validators;

namespace Ck.Server.Application.Services
{
  public class PersonService : IPersonService
  {
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
      _personRepository = personRepository;
      _mapper = mapper;
    }
    public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
    {
      if (personDTO == null)
        return ResultService.Fail<PersonDTO>("Objeto deve ser informado!");

      var result = new PersonDTOValidator().Validate(personDTO);
      if (!result.IsValid)
        return ResultService.RequestError<PersonDTO>("Problema de validade!", result);

      var person = _mapper.Map<Person>(personDTO);
      var data = await _personRepository.CreateAsync(person);
      return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));
    }
  }
}