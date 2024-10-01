using Bogus;
using CashFlow.Exception;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Expenses.Delete
{
    public class DeleteExpenseTest : CashFlowFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

        public DeleteExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.UserTeamMember.GetToken();
            _expenseId = webApplicationFactory.ExpenseTeamMember.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            result = await DoDelete($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var id = new Faker().Random.Int(min: 99999);
            var result = await DoDelete($"{METHOD}/{id}", _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorList = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

            errorList.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
        }
    }
}
