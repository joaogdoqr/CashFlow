using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestsUtilities;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange 
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Error_Title_Empty(string title)
    {
        // Arrange 
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();
        request.Title = title;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e=>e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }
    
    [Fact]
    public void Error_Date_Future()
    {
        // Arrange 
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(5);
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e=>e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE));
    }
    
    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        // Arrange 
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();
        request.PaymentType = (PaymentType)999;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e=>e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // Arrange 
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();
        request.Amount = amount;
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e=>e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}