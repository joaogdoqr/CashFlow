﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper) : IGetByIdUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseExpense> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseExpense>(result);
        }
    }
}
